using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Linq;
using Ruv.Business.Captura.Declaracion;
using Ruv.Data;
using Ruv.Data.Radicacion;
using Ruv.Data.Reconocimiento;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
//using ServiceStack.Text;

namespace Ruv.Business.Captura
{
    public class GuardarDatos
    {
        #region Parametros

        public long ObtenerRadicacion(clsRadicacion radicacionView, DbTransaction tra)
        {
            TBRADICACION myradicacion = new TBRADICACION();

            Radicacion.ParseViewToData_Declaracion(radicacionView, ref myradicacion);
            entRadicacion myEntRadicacion = new entRadicacion();
            myEntRadicacion.setRadicacion(myradicacion, tra);

            return myradicacion.ID;
        }

        public Int64 ObtenerRadicacion(clsRadicacion radicacionView)
        {
            DbConnection con = entidadRUV.GetInstance().CreateConnection();
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            long id = 0;
            try
            {
                id = ObtenerRadicacion(radicacionView, tran);
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return id;
        }

        public Int64 GuardarRadicacion(clsRadicacion radicacionView)
        {

            
            int? id_declarante = null;
            DbConnection con = entidadRUV.GetInstance().CreateConnection();
            con.Open();
            TBRADICACION myradicacion = new TBRADICACION();
            entRadicacion myEntRadicacion = new entRadicacion();

            string timestampError = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(radicacionView);
            File.WriteAllText(ConfigurationManager.AppSettings["PathArchivosLogIntegracion"] + $"/Log_{radicacionView.NRO_FORMULARIO}_{radicacionView.NumeroDocumento}_{timestampError}.txt", json);

            using (DbTransaction tran = con.BeginTransaction())
            {
                try
                {
                    Boolean resultado = false;

                    var rad = myEntRadicacion.getRadicacionByFUD(radicacionView.NRO_FORMULARIO);
                    if (rad.Count > 0)
                    {
                        int usuarioArcaDoc = Convert.ToInt32(ConfigurationManager.AppSettings["UsuarioArcaDoc"]);
                        if ((radicacionView.NRO_FORMULARIO.StartsWith("B") || radicacionView.NRO_FORMULARIO.StartsWith("R")) && radicacionView.ID_USUARIO_RADICA == usuarioArcaDoc)
                        {
                            var radicacion = rad.First();
                            entDeclaraciones myDeclaracion = new entDeclaraciones();
                            var declaracion = myDeclaracion.getDeclaraciones(radicacion.ID_DECLARACION.Value);

                            myradicacion.ID = radicacion.ID;
                            resultado = true;
                        }
                    }
                    else
                    {
                        Radicacion.ParseViewToData_Declaracion(radicacionView, ref myradicacion);
                        

                        eResultadoValidacionRadicacion ResultadoEstado = ValidarNumeroFormulario(myradicacion);

                        myradicacion.PARAM_RESULTADO_VALIDACION = (int)ResultadoEstado;

                        myEntRadicacion.setRadicacion(myradicacion, tran);
                        if (myradicacion.ID > 0)
                            resultado = true;




                        if (resultado)
                        {
                            #region GuardarImagen
                            if (radicacionView.DocumentoDigital != null)
                            {
                                string extension = string.Empty;
                                string fileName = string.Empty;
                                Procesos procesos = new Procesos();
                                if (!string.IsNullOrEmpty(radicacionView.RUTAIMAGEN))
                                {
                                    extension = Path.GetExtension(radicacionView.RUTAIMAGEN);
                                    fileName = string.Format("{0}{1}", myradicacion.ID.ToString(), extension);
                                }
                                else
                                {
                                    fileName = string.Format("{0}{1}", myradicacion.ID.ToString(), ".PDF");
                                }
                                var archivo = radicacionView.DocumentoDigital;
                                procesos.CargarPdf(archivo, fileName, false);
                                myradicacion.RUTAIMAGEN = fileName;
                            }
                            #endregion

                            #region Agregar Declaracion

                            GuardarDatos Guardar = new GuardarDatos();
                            Ruv.Data.TBDECLARACIONES declaracionData = new Ruv.Data.TBDECLARACIONES();
                            //1. Agregar Declaracion
                            //DeclaracionView esta vacia, da como resultado que se crea una declaracion vacia
                            clsDeclaracion declaracionView = new clsDeclaracion();

                            switch (ResultadoEstado)
                            {
                                case eResultadoValidacionRadicacion.validacionCorrecta:
                                    declaracionView.EstadoDeclaracion = Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion.RadicacionPendienteCritica5;
                                    break;
                                case eResultadoValidacionRadicacion.faltaNumeroFormulario:
                                    declaracionView.EstadoDeclaracion = Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion.RadicacionPendientePorVerificar;
                                    break;
                                case eResultadoValidacionRadicacion.NumeroFormularioInvalido:
                                    declaracionView.EstadoDeclaracion = Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion.RadicacionPendientePorVerificar;
                                    break;
                                case eResultadoValidacionRadicacion.ProcedenciaErronea:
                                    declaracionView.EstadoDeclaracion = Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion.RadicacionPendientePorVerificar;
                                    break;
                                case eResultadoValidacionRadicacion.NumeroFormularioRadicado:
                                    declaracionView.EstadoDeclaracion = Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion.RadicacionPendientePorVerificar;
                                    break;
                                case eResultadoValidacionRadicacion.NumeroFormularioNoAsignado:
                                    declaracionView.EstadoDeclaracion = Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion.RadicacionPendientePorVerificar;
                                    break;
                                case eResultadoValidacionRadicacion.NumeroFormularioInactivo:
                                    declaracionView.EstadoDeclaracion = Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion.RadicacionPendientePorVerificar;
                                    break;
                                default:
                                    break;
                            }

                            /* Dependiendo del estado de la declaracion (i.e. Resultado de la Validacion de la radicación) se debe actualizar el estado del formulario */
                            if (declaracionView.EstadoDeclaracion != eEstadoDeclaracion.RadicacionPendientePorVerificar)
                            {
                                string errorFormulario = string.Empty;
                                Ruv.Data.GestionFormulario.Administrador formularioManager = new Data.GestionFormulario.Administrador();
                                formularioManager.MarcarRadicado(radicacionView.NRO_FORMULARIO, tran, ref errorFormulario);
                                if (!string.IsNullOrEmpty(errorFormulario)) Errores.Add(errorFormulario);
                            }

                            declaracionView.UsuarioId = radicacionView.ID_USUARIO_RADICA;
                            declaracionView.UnidadTerritorialId = radicacionView.ID_UTERRITORIALRADICA;

                            if (declaracionView.TomaDeclaracion == null)
                                declaracionView.TomaDeclaracion = new clsTomaDeclaracion();

                            if (declaracionView.TomaDeclaracion.Encargado == null)
                                declaracionView.TomaDeclaracion.Encargado = new clsTomaDeclaracion_Encargado();

                            if (declaracionView.PersonasAfectadas == null)
                                declaracionView.PersonasAfectadas = new clsPersonasAfectadas();

                            //Se asocia la radicacion a la declaracion
                            declaracionView.RadicacionId = myradicacion.ID;
                            declaracionView.DeclaracionNumero = myradicacion.NRO_FORMULARIO;
                            declaracionView.TomaDeclaracion.LugarDeclaracionPais = radicacionView.ID_PAIS;
                            declaracionView.TomaDeclaracion.LugarDeclaracionDepartamento = radicacionView.ID_DEPARTAMENTO;
                            declaracionView.TomaDeclaracion.LugarDeclaracionMunicipio = radicacionView.ID_MUNICIPIO;
                            declaracionView.TomaDeclaracion.LugarDeclaracionEntidadMunicipio = radicacionView.ID_ENTIDADMUNICIPIO;
                            //Declaracion y Declarante - HOJA 1 y HOJA 4

                            if (!Guardar.InfoDeclaracion(ref declaracionView, tran))
                            {
                                this.Advertencias.AddRange(Guardar.Advertencias);
                                this.Errores.AddRange(Guardar.Errores);
                            }
                            else
                            {
                                //1. Agregar Persona como esDeclarante y RegistroPersona
                                clsPersonaAfectada personaView = new clsPersonaAfectada();
                                personaView.ID = int.MinValue;
                                personaView.PrimerNombre = radicacionView.PrimerNombre;
                                personaView.SegundoNombre = radicacionView.SegundoNombre;
                                personaView.PrimerApellido = radicacionView.PrimerApellido;
                                personaView.SegundoApellido = radicacionView.SegundoApellido;
                                personaView.TipoDocumento = radicacionView.TipoDocumento;
                                personaView.NumeroDocumento = radicacionView.NumeroDocumento;

                                declaracionView.TomaDeclaracion.DeclaranteId = int.MinValue;

                                declaracionView.PersonasAfectadas.ListaPersonas.Add(personaView);
                                //Caracterizacion - HOJA 2
                                if (!Guardar.Caracterizacion(declaracionView, tran, ref id_declarante))
                                {
                                    this.Advertencias.AddRange(Guardar.Advertencias);
                                    this.Errores.AddRange(Guardar.Errores);
                                }
                                else
                                    myEntRadicacion.updateRadicacion(myradicacion, tran);
                            }
                            #endregion

                        }

                    }

                    #region GuardarLogArcaDoc

                    if (!string.IsNullOrEmpty(radicacionView.COD_GESTOR_DOCUMENTAL) || !string.IsNullOrEmpty(radicacionView.NUM_EXPEDIENTE_SGD) || radicacionView.ID_EXPEDIENTE_SGD.HasValue)
                    {
                        
                        bool ActivoArcaDoc = false;
                        ActivoArcaDoc = Convert.ToBoolean(ConfigurationManager.AppSettings["ActivoArcaDoc"]);
                        if (ActivoArcaDoc)
                        {
                            //Invoca el metodo de guardar radicado en el Gestor Documental
                            string cError = string.Empty;
                            IntegradorGD.GuardarLogIntegracionSGD(radicacionView.NRO_FORMULARIO, radicacionView.COD_GESTOR_DOCUMENTAL, radicacionView.NUM_EXPEDIENTE_SGD, radicacionView.ID_EXPEDIENTE_SGD,
                                radicacionView.ID_USUARIO_RADICA.Value, tran, ref cError);
                            if (!string.IsNullOrEmpty(cError))
                            {
                                this.Errores.Add(cError);

                                resultado = false;
                                Exception ex = new Exception(cError);
                                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                                throw ex;
                            }
                        }
                    }

                    #endregion

                    if (resultado && this.Errores.Count == 0)
                        tran.Commit();
                    else
                    {
                        myradicacion.ID = -1;
                        Exception ex = new Exception("Error guardando radicando en RUV");
                        Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                        throw ex;
                    }


                }
                catch (Exception ex)
                {
                    Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                    tran.Rollback();
                    myradicacion.ID = -1;
                    throw ex;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        con.Close();
                }
            }
            
            return myradicacion.ID;
        }

        internal void Anexo13Siniestro(int idSiniestroAnexo13, int idSiniestroAnexo, DbTransaction tran)
        {
            entAnexo13 anexo13 = new entAnexo13();
            TBANEXO13_SINIESTRO a13siniestro = new TBANEXO13_SINIESTRO();
            a13siniestro.ID_SINIESTRO = idSiniestroAnexo;
            a13siniestro.ID_SINIESTRO_ANEXO13 = idSiniestroAnexo13;
            anexo13.sp_setAnexo13_siniestro(a13siniestro, tran);
        }

        public void ActualizarRadicacion(clsRadicacion radicacionView, DbTransaction tra)
        {
            TBRADICACION myradicacion = new TBRADICACION();
            Radicacion.ParseViewToData_DeclaracionUpdate(radicacionView, ref myradicacion);
            entRadicacion myEntRadicacion = new entRadicacion();

            myEntRadicacion.updateRadicacion(myradicacion, tra);
        }

        public Boolean ActualizarRadicacion(clsRadicacion radicacionView)
        {
            DbConnection con = entidadRUV.GetInstance().CreateConnection();
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            try
            {
                ActualizarRadicacion(radicacionView, tran);
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return true;
        }

        /// <summary>
        /// Valida la radicación en busca de inconsistencias por numero de formulario
        /// </summary>
        /// <param name="numeroFormulario">Numero de fromulario de la radicación</param>
        /// <returns>Enumeración con el resultado de la validación</returns>
        private eResultadoValidacionRadicacion ValidarNumeroFormulario(TBRADICACION radicacion)
        {
            //Verifica si el numero de formulario esta vacio
            if (String.IsNullOrEmpty(radicacion.NRO_FORMULARIO))
            {
                return eResultadoValidacionRadicacion.faltaNumeroFormulario;
            }
            else
            {
                string cError = string.Empty;
                Data.GestionFormulario.Contratos.IGetFormulario iFormulario = (Data.GestionFormulario.Contratos.IGetFormulario)new Data.GestionFormulario.Administrador();
                Ruv.Business.DTO.GestionFormulario.clsFormulario formulario = iFormulario.ObtenerFormulario(radicacion.NRO_FORMULARIO, ref cError);

                //Valida que el numero de formulario exista
                //if (!ExisteNumeroFormulario(numeroFormulario))
                if (formulario == null)
                {
                    return eResultadoValidacionRadicacion.NumeroFormularioInvalido;
                }
                else
                {
                    if (formulario.NIdEstado == (ushort)eEstadoFormulario.GENERADO || formulario.NIdEstado == (ushort)eEstadoFormulario.IMPRENTA)
                    {
                        return eResultadoValidacionRadicacion.NumeroFormularioNoAsignado;
                    }
                    else if (formulario.NIdEstado == (ushort)eEstadoFormulario.INACTIVO)
                    {
                        return eResultadoValidacionRadicacion.NumeroFormularioInactivo;
                    }
                    //Valida que la procedencia del formulario sea la correcta
                    else if (!ProcedenciaFormularioCoherente(radicacion, formulario))
                    {
                        return eResultadoValidacionRadicacion.ProcedenciaErronea;
                    }
                    //Valida que no se encuentre Radicado
                    if (formulario.NIdEstado == (ushort)(eEstadoFormulario.RADICADO))
                    {
                        return eResultadoValidacionRadicacion.NumeroFormularioRadicado;
                    }
                    if (formulario.NIdEstado == (ushort)(eEstadoFormulario.INACTIVO))
                    {
                        return eResultadoValidacionRadicacion.NumeroFormularioInvalido;
                    }
                }
            }

            //Si todas las validaciones estan correctas
            return eResultadoValidacionRadicacion.validacionCorrecta;
        }

        //private Boolean ExisteNumeroFormulario(String numeroFormulario)
        //{
        //    return true;
        //}

        private Boolean ProcedenciaFormularioCoherente(TBRADICACION radicacion, Ruv.Business.DTO.GestionFormulario.clsFormulario formulario)
        {
            if (radicacion.ID_ENTIDADMUNICIPIO.HasValue &&
                formulario.NIdEntidad.HasValue &&
                radicacion.ID_ENTIDADMUNICIPIO.Value == formulario.NIdEntidad.Value)
                return true;
            else
                return false;
        }

        public List<string> Errores;
        public List<string> Advertencias;

        #endregion

        public GuardarDatos()
        {
            Errores = new List<string>();
            Advertencias = new List<string>();
        }

        private void AddError(string msg)
        {
            this.Errores.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine + msg);
        }

        private void AddAdvertencia(string msg)
        {
            this.Advertencias.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine + msg);
        }

        public bool InfoDeclaracion(ref clsDeclaracion declaracionView, DbTransaction tran)
        {
            try
            {
                Ruv.Business.Captura.Declaracion.InfoDeclaracion.Guardar(ref declaracionView, tran);
            }
            catch (Exception ex)
            {
                string msgError = "- Error Guardando Declaración: " + Environment.NewLine + ex.Message;
                AddError(msgError);
                return false;
            }
            return true;
        }

        public bool Caracterizacion(clsDeclaracion declaracionView, DbTransaction tran, ref int? id_declarante)
        {
            try
            {
                int? id_jefeHogar = null;
                //int? id_declarante = null;
                const int FamiliaConsecutivo = 1;  //La familia registrada en la hoja 2 debe ser la Familia numero 1 en cualquier declaración
                //Realizar consulta y verificar si ya estan asociado las personas a la declaracion en la tabla registro_personas
                //var personaAsociadas = metodocargapersonasasociadas(declaracionView.ID)
                foreach (clsPersonaAfectada personaView in declaracionView.PersonasAfectadas.ListaPersonas)
                {
                    personaView.FamiliaConsecutivo = FamiliaConsecutivo;
                    Persona.Guardar(declaracionView, personaView, ref id_jefeHogar, ref id_declarante, tran);
                }

                #region Actualizar Jefe de Hogar
                if (id_jefeHogar != null)
                    JefeHogar.Actualizar((int)declaracionView.ID, id_jefeHogar ?? (int)Common.ThrowException("No hay ninguna persona registrada como jefe de hogar"), FamiliaConsecutivo, tran);
                //else
                //    this.this.Advertencias.Add("No hay ninguna persona registrada como jefe de hogar");
                #endregion
            }
            catch (Exception ex)
            {
                string msgError = "- Error Guardando Personas - Hoja de Caracterización: " + Environment.NewLine + ex.Message;
                AddError(msgError);
                return false;
            }
            return true;
        }

        #region ANEXOS:
        public void Anexos01(List<clsAnexo01> anexos, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo01 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo01.Guardar(anexo, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo01[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
                i++;
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "01", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos02(List<clsAnexo02> anexos, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo02 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo02.Guardar(anexo, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo02[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
                i++;
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "02", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos03(List<clsAnexo03> anexos, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo03 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo03.Guardar(anexo, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo03[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "03", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos04(List<clsAnexo04> anexos, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo04 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo04.Guardar(anexo, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo04[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "04", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos05(List<clsAnexo05> anexos, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo05 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo05.Guardar(anexo, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo05[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "05", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos06(List<clsAnexo06> anexos, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo06 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo06.Guardar(anexo, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo06[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "06", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos07(List<clsAnexo07> anexos, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo07 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo07.Guardar(anexo, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo07[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "07", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos08(List<clsAnexo08> anexos, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo08 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo08.Guardar(anexo, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo08[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "08", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos09(List<clsAnexo09> anexos, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo09 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo09.Guardar(anexo, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo09[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "09", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos10(List<clsAnexo10> anexos, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo10 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo10.Guardar(anexo, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo10[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "10", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos11(List<clsAnexo11> anexos, int Declarante, int idValoracion, DbTransaction tran)
        {
            int i = 1;
            foreach (clsAnexo11 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo11.Guardar(anexo, Declarante, idValoracion, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo11[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "11", Environment.NewLine, ex.Message));
            }
        }

        #region Anexo 13

        public void ActualizarAnexo13_IdRelacionado(clsDeclaracion declaracionView)
        {

            foreach (IAnexo anexo in declaracionView.TodosLosAnexos.Where(x => x.idAnexoRelacionado != null))
            {
                foreach (clsAnexo13 anexo13 in declaracionView.A13.Where(y => y.ID == anexo.idAnexoRelacionado))
                {
                    anexo13.idAnexoRelacionado = ((clsEntidadBase)anexo).ID;
                }
            }

        }

        public bool GuardarVictimasAnexo13(clsDeclaracion declaracionView, DbTransaction tran)
        {

            try
            {
                int ConsecutivoFamilia = 1;     //La familia numero 1 debe ser la registrada en la hoja 2, las familias del anexo 13 arrancan a partir del numero 2
                eEstadoRegistro EstadoRegistro;

                foreach (clsAnexo13 anexo in declaracionView.A13)
                {
                    int? id_jefeHogar = null;
                    int? id_declarante = null;
                    ConsecutivoFamilia++;

                    foreach (clsAnexo13_Victima victimaView in anexo.ListaPersonas)
                    {
                        //Almacenar el EstadoRegistro, solo aplica para el anexo 13
                        EstadoRegistro = victimaView.EstadoRegistro;

                        victimaView.FamiliaConsecutivo = ConsecutivoFamilia;
                        Persona.Guardar(declaracionView, victimaView, ref id_jefeHogar, ref id_declarante, tran);
                        victimaView.PersonaAfectadaId = victimaView.ID;
                        //Reiniciar EstadoRegistro
                        victimaView.EstadoRegistro = EstadoRegistro;
                    }

                    #region Actualizar Jefe de Hogar

                    anexo.JefeGrupoFamiliarId = id_jefeHogar;

                    if (id_jefeHogar != null)
                        JefeHogar.Actualizar((int)declaracionView.ID
                                                , id_jefeHogar ?? (int)Common.ThrowException("No hay ninguna persona registrada como jefe de hogar")
                                                , ConsecutivoFamilia, tran);
                    //else
                    //    this.this.Advertencias.Add("No hay ninguna persona registrada como jefe de hogar");
                    #endregion
                }
            }
            catch (Exception ex)
            {
                string msgError = "- Error Guardando Personas - Anexo 13: " + Environment.NewLine + ex.Message;
                AddError(msgError);
                return false;
            }
            return true;
        }

        public void Anexos13(clsDeclaracion declaracionView, DbTransaction tran)
        {
            List<clsAnexo13> anexos = declaracionView.A13;
            int i = 1;
            foreach (clsAnexo13 anexo in anexos)
            {
                try
                {
                    Ruv.Business.Captura.Anexos.Anexo13.Guardar(anexo, tran);

                    //Actualizar los datos de contacto del jefe del grupo familiar, del anexo13
                    DatosContactoAnexo13(declaracionView, anexo, tran);

                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Anexo13[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                var deletedAnnexIds = anexos.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
                if (deletedAnnexIds != null && deletedAnnexIds.Count > 0)
                {
                    for (int ax = 0; ax < anexos.Count; ax++)
                    {
                        if (deletedAnnexIds.Contains(anexos[ax].ID))
                            anexos.RemoveAt(ax);
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Anexos{0} con estado eliminado.{1}{2}", "13", Environment.NewLine, ex.Message));
            }
        }

        /// <summary>
        /// Actualizar los datos de contacto del jefe del grupo familiar, del anexo13
        /// </summary>
        /// <param name="declaracionView"></param>
        /// <param name="anexo"></param>
        private void DatosContactoAnexo13(clsDeclaracion declaracionView, clsAnexo13 anexo, DbTransaction tran)
        {
            clsAnexo13_Victima jefe = new clsAnexo13_Victima();
            jefe = anexo.ListaPersonas.FirstOrDefault(x => x.PersonaAfectadaId == anexo.JefeGrupoFamiliarId);

            int? id_jefeHogar = null;
            int? id_declarante = null;
            id_declarante = anexo.JefeGrupoFamiliarId;
            id_jefeHogar = anexo.JefeGrupoFamiliarId;

            clsTomaDeclaracion tomaDeclaracion = new clsTomaDeclaracion();
            tomaDeclaracion = declaracionView.TomaDeclaracion;

            //Datos de contacto
            declaracionView.TomaDeclaracion.DatoContactoDireccion = anexo.DatoContactoDireccion;
            declaracionView.TomaDeclaracion.DatoContactoPais = anexo.DatoContactoPais;
            declaracionView.TomaDeclaracion.DatoContactoDepartamento = anexo.DatoContactoDepartamento;
            declaracionView.TomaDeclaracion.DatoContactoMunicipio = anexo.DatoContactoMunicipio;
            declaracionView.TomaDeclaracion.DatoContactoTipoEntorno = anexo.DatoContactoTipoEntorno;
            declaracionView.TomaDeclaracion.DatoContactoLocalidadCorregimientoId = anexo.DatoContactoLocalidadCorregimientoId;
            declaracionView.TomaDeclaracion.DatoContactoBarrioVeredaId = anexo.DatoContactoBarrioVeredaId;
            declaracionView.TomaDeclaracion.DatoContactoTelefonoFijo = anexo.DatoContactoTelefonoFijo;
            declaracionView.TomaDeclaracion.DatoContactoTelefonoCelular = anexo.DatoContactoTelefonoCelular;
            declaracionView.TomaDeclaracion.DatoContactoCorreoElectronico = anexo.DatoContactoCorreoElectronico;

            //Datos de contacto Alterno
            declaracionView.TomaDeclaracion.DatoAlternoContactoDireccion = anexo.DatoAlternoContactoDireccion;
            declaracionView.TomaDeclaracion.DatoAlternoContactoPais = anexo.DatoAlternoContactoPais;
            declaracionView.TomaDeclaracion.DatoAlternoContactoDepartamento = anexo.DatoAlternoContactoDepartamento;
            declaracionView.TomaDeclaracion.DatoAlternoContactoMunicipio = anexo.DatoAlternoContactoMunicipio;
            declaracionView.TomaDeclaracion.DatoAlternoContactoTipoEntorno = anexo.DatoAlternoContactoTipoEntorno;
            declaracionView.TomaDeclaracion.DatoAlternoContactoLocalidadCorregimientoId = anexo.DatoAlternoContactoLocalidadCorregimientoId;
            declaracionView.TomaDeclaracion.DatoAlternoContactoBarrioVeredaId = anexo.DatoAlternoContactoBarrioVeredaId;
            declaracionView.TomaDeclaracion.DatoAlternoContactoTelefonoFijo = anexo.DatoAlternoContactoTelefonoFijo;
            declaracionView.TomaDeclaracion.DatoAlternoContactoTelefonoCelular = anexo.DatoAlternoContactoTelefonoCelular;
            declaracionView.TomaDeclaracion.DatoAlternoContactoCorreoElectronico = anexo.DatoAlternoContactoCorreoElectronico;

            //Actializar los datos en la tabla registro persona
            if (jefe != null)
            {
                jefe.ID = jefe.PersonaAfectadaId;
                jefe.EstadoRegistro = eEstadoRegistro.Modificado;
                RegistroPersona.Guardar(declaracionView, jefe, (int)jefe.PersonaAfectadaId, ref id_declarante, ref id_jefeHogar, tran);
                jefe.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                #region Actualizar Jefe de Hogar
                if (id_jefeHogar != null)
                    JefeHogar.Actualizar((int)declaracionView.ID, id_jefeHogar ?? (int)Common.ThrowException("No hay ninguna persona registrada como jefe de hogar"), jefe.FamiliaConsecutivo, tran);
                #endregion
            }
            //Regresar los datos originales.
            declaracionView.TomaDeclaracion = tomaDeclaracion;
        }


        public void NotificacionElectronica(clsDeclaracion declaracionView, DbTransaction tran)
        {
            Ruv.Business.Captura.Declaracion.NotificacionElectronica.Guardar(declaracionView, tran);
        }
        #endregion

        #endregion

        #region Lista Tareas
        public void RadicacionActualizarEstado(int idRadicacion, int param_estado, DbTransaction tran)
        {
            entListaTareas entidad = new entListaTareas();
            entidad.updTBESTADOPROCESOS(idRadicacion, param_estado, tran);
        }
        #endregion

        #region GLOSAS
        public void Glosas(ObservableCollection<clsGlosa> Glosas, int? IdProceso, DbTransaction tran)
        {
            int i = 1;
            foreach (clsGlosa Glosa in Glosas)
            {
                Glosa.ID_PROCESO = IdProceso;
                try
                {
                    // Convertir cls Glosas a TBGLOSAS
                    using (GestionGlosas GestGlosas = new GestionGlosas())
                        GestGlosas.InsertarGlosa(Glosa, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Glosas[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }
            try
            {
                //Eliminar las pendientes de eliminación
                var borradosGlosas = from a in Glosas
                                     where a.EstadoRegistro == eEstadoRegistro.Eliminado
                                     select a;
                foreach (clsGlosa Glosa in borradosGlosas)
                    using (GestionGlosas GestGlosas = new GestionGlosas())
                        GestGlosas.ActualizarGlosa(Glosa, tran);
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Glosas{0} con estado eliminado.{1}{2}", "11", Environment.NewLine, ex.Message));
            }
        }
        public void IntencionesGlosas(ObservableCollection<clsGlosaIntencion> IGlosas, int? IdProceso, DbTransaction tran)
        {
            int i = 1;
            foreach (clsGlosaIntencion Glosa in IGlosas)
            {
                Glosa.ID_PROCESO = IdProceso;
                try
                {
                    using (GestionGlosas GestGlosas = new GestionGlosas())
                        GestGlosas.InsertarIntencionGlosa(Glosa, tran);
                }
                catch (Exception ex)
                {
                    AddError(string.Format("- Error guardando Intenciones de Glosas[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                }
            }

            try
            {
                //Eliminar de la vista
                var borradosGlosas = from a in IGlosas
                                     where a.EstadoRegistro == eEstadoRegistro.Eliminado
                                     select a;
                foreach (clsGlosaIntencion Glosa in borradosGlosas)
                    using (GestionGlosas GestGlosas = new GestionGlosas())
                        GestGlosas.ActualizarIntencionGlosa(Glosa, tran);
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error quitando Intenciones de Glosas{0} con estado eliminado.{1}{2}", "11", Environment.NewLine, ex.Message));
            }
        }
        #endregion

        #region ActualizarEstado

        public static void ActualizarEstadoDeclaracion(clsDeclaracion declaracion, DbTransaction tran)
        {
            entRadicacion objRadicacion = new entRadicacion();
            objRadicacion.ActualizarEstadoDeclaracion(declaracion.ID.Value, declaracion.UsuarioId.Value, declaracion.EstadoDeclaracion.GetHashCode(), tran);
        }

        #endregion
    }
}
