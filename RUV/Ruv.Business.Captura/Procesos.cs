using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.General;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using dglosa = Ruv.Data.GestionGlosa;
using Ruv.Data;
using Ruv.Data.Reconocimiento;
using System.Data.Common;
using Ruv.Infrastructure.Crosscutting.Utilities;
using System.Configuration;
using System.IO;

namespace Ruv.Business.Captura
{
    public class Procesos
    {
        #region Parametros
        public List<string> Errores { get; set; }

        public List<string> Advertencias { get; set; }
        #endregion

        public Procesos()
        {
            Errores = new List<string>();
            Advertencias = new List<string>();
        }

        #region GuardarDeclaracion

        /// <summary>
        /// Recibe una declaración y la graba en la base de datos.
        /// </summary>
        /// <param name="declaracion"></param>
        /// <returns></returns>
        public void DeclaracionAlmacenar(clsDeclaracion declaracion, string numeroDeclaracion, clsUsuario usuario)
        {
            DbConnection con = entidadRUV.GetInstance().CreateConnection();
            con.Open();
            using (DbTransaction tra = con.BeginTransaction())
            {
                try
                {

                    var securityHandler = new clsSeguridad();
                    string cErrorCredenciales = string.Empty;
                    if (!securityHandler.CredencialesValidas(numeroDeclaracion, ref cErrorCredenciales))
                        throw new Exception("No se pudo verificar las credenciales de usuario, " + cErrorCredenciales);

                    // En este punto, el usuario actual es: Seguridad.Usuario;
                    declaracion.UsuarioId = securityHandler.Usuario.Id;
                    declaracion.UnidadTerritorialId = securityHandler.Usuario.UnidadTerritorialId;

                    // Si la declaración proviene de DIGITACION
                    if (declaracion.EstadoDeclaracion == eEstadoDeclaracion.RadicadoPendienteCaptura)
                        declaracion.EstadoDeclaracion = eEstadoDeclaracion.ValoracionPendientePorAsignar;

                    // Si la declaración proviene de GLOSAS
                    if (declaracion.EstadoDeclaracion == eEstadoDeclaracion.CapturaPendientePorValidar)
                    {
                        if (usuario.Permisos.Contains(ePermisosUsuario.validar_Enmendar_corregir_declaración))
                            declaracion.EstadoDeclaracion = eEstadoDeclaracion.ValoracionPendientePorAsignar;
                    }

                    // Si la declaración proviene de digitación, pero se marca como incompleta (PendienteGlosas)
                    if (declaracion.PendienteGlosas)
                        declaracion.EstadoDeclaracion = eEstadoDeclaracion.CapturaPendientePorValidar;

                    bool success = GuardarDeclaracion(declaracion, declaracion.IdValoracion, tra);

                    if (success && declaracion.EstadoDeclaracion == eEstadoDeclaracion.CapturaPendientePorValidar)
                    {
                        string cError = string.Empty;
                        dglosa::Contratos.IGestionGlosa igestionglosa = (dglosa::Contratos.IGestionGlosa)Spring.GetService(resx::Dependencias.Objetos.GlosasData);
                        igestionglosa.AsignarGlosa(declaracion.ID, tra, ref cError);
                        if (cError != string.Empty)
                            Errores.Add(cError);
                    }

                    if (success && (Errores.Count == 0 && Advertencias.Count == 0))
                    {
                        // Auto RADICACION
                        if (declaracion.RadicacionId < 1)
                        {
                            clsRadicacion radicacion = RadicadoAutomatico(tra, ref declaracion);
                            declaracion.DocumentoDigitalNombre = declaracion.RadicacionId.ToString() + System.IO.Path.GetExtension(declaracion.DocumentoDigitalNombre);
                            radicacion.RUTAIMAGEN = declaracion.DocumentoDigitalNombre;
                            CargarPdf(declaracion.DocumentoDigital, declaracion.DocumentoDigitalNombre, declaracion.DocumentoAnexo);
                            new Ruv.Business.Captura.GuardarDatos().ActualizarRadicacion(radicacion, tra);

                            string cError = string.Empty;

                            bool ActivoArcaDoc = false;
                            ActivoArcaDoc = Convert.ToBoolean(ConfigurationManager.AppSettings["ActivoArcaDoc"]);
                            if (ActivoArcaDoc)
                            {
                                //Invoca el metodo de guardar radicado en el Gestor Documental
                                try
                                {
                                    IntegradorGD.GuardarRadicacion(declaracion, "Declaracion Individual", usuario.Id, tra, ref cError);
                                    if (!string.IsNullOrEmpty(cError))
                                    {
                                        Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception(cError)));
                                        Errores.Add(cError);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(e));
                                    Errores.Add(cError);
                                    tra.Rollback();
                                }
                                
                            }
                        }

                        // TODO: jairovg - Descomentariar cuando sea necesara la autoasignación del valorador. Falta realizar seguimiento.
                        //if (declaracion.EstadoDeclaracion == eEstadoDeclaracion.ValoracionPendientePorAsignar
                        //    || declaracion.EstadoDeclaracion == eEstadoDeclaracion.FinalizaCapturaSinRadicar)
                        //{
                        //    ValoracionService ObjValoracion = new ValoracionService();
                        //    var error = string.Empty;
                        //    bool AsignOk = ObjValoracion.AutoAsignaValorador(declaracion.ID.Value, ref error);
                        //    if (error != string.Empty) throw new Exception(error);
                        //}


                        declaracion.DocumentoDigital = null;

                        GrabarPrimeraVersion(declaracion, (int)declaracion.ID);

                        if (this.Advertencias.Count > 0 || this.Errores.Count > 0) tra.Rollback();
                        else tra.Commit();
                    }
                    else
                    {
                        tra.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    //clsLog.Registrar(ex);
                    Errores.Add(ex.Message);
                    tra.Rollback();
                }
            }
        }

        const string ClaveZip = "7Np#  *!!!array*9823!* Qnt  ";
        /// <summary>
        /// Grabar la declaración en el servidor
        /// </summary>
        void GrabarPrimeraVersion(clsDeclaracion Declaracion, int idDeclaracion)
        {
            clsUtil objUtil = new clsUtil();

            string path = System.Configuration.ConfigurationManager.AppSettings["PathArchivosDeclaracion"] + "PrimeraVersion\\Declaracion " + idDeclaracion.ToString() + "_" + Declaracion.UsuarioId + ".tmp";
            objUtil.GrabarArchivoSerializado<clsDeclaracion>(
                path,
                Declaracion,
                ClaveZip,
                true);
        }


        private bool GuardarDeclaracion(clsDeclaracion declaracionView, int idValoracion, DbTransaction tra)
        {
            int? id_declarante = null;
            GuardarDatos Guardar = new GuardarDatos();
            Ruv.Data.TBDECLARACIONES declaracionData = new Ruv.Data.TBDECLARACIONES();
            if (declaracionView.RadicacionId == null)
                declaracionView.RadicacionId = 0;

            //Declaracion y Declarante - HOJA 1 y HOJA 4
            if (!Guardar.InfoDeclaracion(ref declaracionView, tra))
            {
                this.Advertencias.AddRange(Guardar.Advertencias);
                this.Errores.AddRange(Guardar.Errores);
            }

            //Caracterizacion - HOJA 2
            if (!Guardar.Caracterizacion(declaracionView, tra, ref id_declarante))
            {
                this.Advertencias.AddRange(Guardar.Advertencias);
                this.Errores.AddRange(Guardar.Errores);
            }

            #region ANEXOS:
            Guardar.Anexos01(declaracionView.A01, idValoracion, tra);

            Guardar.Anexos02(declaracionView.A02, idValoracion, tra);

            Guardar.Anexos03(declaracionView.A03, idValoracion, tra);

            Guardar.Anexos04(declaracionView.A04, idValoracion, tra);

            Guardar.Anexos05(declaracionView.A05, idValoracion, tra);

            Guardar.Anexos06(declaracionView.A06, idValoracion, tra);

            Guardar.Anexos07(declaracionView.A07, idValoracion, tra);

            Guardar.Anexos08(declaracionView.A08, idValoracion, tra);

            Guardar.Anexos09(declaracionView.A09, idValoracion, tra);

            Guardar.Anexos10(declaracionView.A10, idValoracion, tra);

            Guardar.Anexos11(declaracionView.A11, id_declarante.Value, idValoracion, tra);

            #region Anexo 13
            //Actualizar los Id de los anexo relacionados.
            //Guardar.ActualizarAnexo13_IdRelacionado(declaracionView);
            Guardar.GuardarVictimasAnexo13(declaracionView, tra);
            Guardar.Anexos13(declaracionView, tra);

            bool activoAsociarA13 = false;
            activoAsociarA13 = Convert.ToBoolean(ConfigurationManager.AppSettings["AsociarAnexo13"]);
            if (activoAsociarA13)
            {
                foreach (var item in declaracionView.A13)
                {
                    var idSiniestroAnexo13 = item.ID;
                    foreach (var anexo in item.AnexosRelacionados)
                    {
                        int? idSiniestroAnexo = 0;
                        var a1 = declaracionView.A01.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a1 != null)
                            idSiniestroAnexo = a1.ID;
                        var a2 = declaracionView.A02.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a2 != null)
                            idSiniestroAnexo = a2.ID;
                        var a3 = declaracionView.A03.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a3 != null)
                            idSiniestroAnexo = a3.ID;
                        var a4 = declaracionView.A04.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a4 != null)
                            idSiniestroAnexo = a4.ID;
                        var a5 = declaracionView.A05.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a5 != null)
                            idSiniestroAnexo = a5.ID;
                        var a6 = declaracionView.A06.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a6 != null)
                            idSiniestroAnexo = a6.ID;
                        var a7 = declaracionView.A07.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a7 != null)
                            idSiniestroAnexo = a7.ID;
                        var a8 = declaracionView.A08.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a8 != null)
                            idSiniestroAnexo = a8.ID;
                        var a9 = declaracionView.A09.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a9 != null)
                            idSiniestroAnexo = a9.ID;
                        var a10 = declaracionView.A10.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a10 != null)
                            idSiniestroAnexo = a10.ID;
                        var a11 = declaracionView.A11.FirstOrDefault(x => x.ID_Interno == anexo);
                        if (a11 != null)
                            idSiniestroAnexo = a11.ID;

                        Guardar.Anexo13Siniestro(idSiniestroAnexo13.Value, idSiniestroAnexo.Value, tra);
                    }

                }
            }
            #endregion

            #endregion

            #region NOTIFICACION_ELECTRONICA

            Guardar.NotificacionElectronica(declaracionView, tra);

            #endregion

            #region GLOSAS
            /* Guardar.Glosas(declaracionView.Glosas, declaracionView.ID);
                    Guardar.IntencionesGlosas(declaracionView.IGlosas, declaracionView.ID);*/
            #endregion

            this.Advertencias.AddRange(Guardar.Advertencias);
            this.Errores.AddRange(Guardar.Errores);

            if (this.Advertencias.Count > 0 || this.Errores.Count > 0) return false;
            return true;
        }

        private clsRadicacion RadicadoAutomatico(DbTransaction tra, ref clsDeclaracion Declaracion)
        {
            //Datos para la Radicación Automatica.
            clsRadicacion radicacion = new clsRadicacion();

            radicacion.FECHALLEGADA = DateTime.Now;
            radicacion.FECHAREGISTRO = DateTime.Now;
            radicacion.NRO_FORMULARIO = Declaracion.DeclaracionNumero;
            radicacion.ID_DEPARTAMENTO = Declaracion.TomaDeclaracion.LugarDeclaracionDepartamento;
            radicacion.ID_ENTIDADMUNICIPIO = Declaracion.TomaDeclaracion.LugarDeclaracionEntidadMunicipio;
            radicacion.ID_MUNICIPIO = Declaracion.TomaDeclaracion.LugarDeclaracionMunicipio;
            radicacion.ID_USUARIO_RADICA = Declaracion.UsuarioId;
            radicacion.ID_UTERRITORIALRECIBE = Declaracion.UnidadTerritorialId;
            radicacion.ID_TIPORADICACION = (int)eTipoRadicacion.RadicacionDeclaracion;
            radicacion.ID_DECLARACION = Declaracion.ID;

            decimal varRadicacion = (decimal)new Ruv.Business.Captura.GuardarDatos().ObtenerRadicacion(radicacion, tra);

            if (varRadicacion > 0)
            {
                Declaracion.RadicacionId = (int)varRadicacion;
                radicacion.ID = Convert.ToInt32(varRadicacion);
            }
            return radicacion;
        }

        #endregion

        #region Cargar archivo

        public bool CargarPdf(byte[] fileData, string fileName, bool tomaEnLinea = true)
        {
            bool cargaimagen = false;
            string path = System.Configuration.ConfigurationManager.AppSettings["PathArchivosRadicacion"];
            string extencionActual = fileName.Split(new char[] { '.' })[1];
            if(tomaEnLinea)
                fileName = fileName.Replace(extencionActual, "zip");
            string file = fileName;
            string radid = fileName.Split(new char[] { '.' })[0];
            string[] archivos = Directory.GetFiles(path, radid + ".*", SearchOption.TopDirectoryOnly);

            if (archivos != null)
            {
                foreach (string i in archivos)
                {
                    File.Delete((i));
                }
            }

            string pathTmp = path;
            path = path + file;

            FileStream archivo_fisico = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(archivo_fisico);
            try
            {
                if (fileData != null) bw.Write(fileData);
                cargaimagen = true;
            }
            finally
            {
                archivo_fisico.Close();
                bw.Close();
            }
            return cargaimagen;
        }

        private void CargarPdf(byte[] fileData, string fileName, byte[] optionalFile)
        {
            if (fileData != null) CargarPdf(fileData, fileName);

            string path = ConfigurationManager.AppSettings["PathArchivosRadicacion"];
            string file = fileName;
            string radid = fileName.Split(new char[] { '.' })[0];

            if (optionalFile != null)
            {
                FileStream archivo_opt = null;
                BinaryWriter bwOpt = null;
                try
                {
                    archivo_opt = new FileStream(path + radid + "-XPS.zip", FileMode.CreateNew, FileAccess.Write);
                    bwOpt = new BinaryWriter(archivo_opt);
                    bwOpt.Write(optionalFile);
                }
                finally
                {
                    if (archivo_opt != null) archivo_opt.Close();
                }
            }
        }

        #endregion

        #region BuscarDeclaracion

        public clsDeclaracion ObtenerDeclaracion(int id_declaracion)
        {
            ObtenerDatos Obtener = new ObtenerDatos();
            clsDeclaracion declaracionView = new clsDeclaracion();
            //H1
            declaracionView.TomaDeclaracion = new clsTomaDeclaracion();
            declaracionView.ID = id_declaracion;

            #region  Declaracion y Declarante - HOJA 1
            TBDECLARACIONES declaracionData = new TBDECLARACIONES();
            try
            {
                Obtener.InfoDeclaracion(ref declaracionView);
            }
            catch (Exception ex)
            {
                Errores.Add(ex.Message);
                return declaracionView;
            }
            #endregion

            #region Caracterizacion - HOJA 2
            try
            {
                Obtener.Caracterizacion(declaracionView);
            }
            catch (Exception ex)
            {
                Errores.Add(ex.Message);
                return declaracionView;
            }
            #endregion

            #region ANEXOS:
            Obtener.Anexos01(declaracionView);
            Obtener.Anexos02(declaracionView);
            Obtener.Anexos03(declaracionView);
            Obtener.Anexos04(declaracionView);
            Obtener.Anexos05(declaracionView);
            Obtener.Anexos06(declaracionView);
            Obtener.Anexos07(declaracionView);
            Obtener.Anexos08(declaracionView);
            Obtener.Anexos09(declaracionView);
            Obtener.Anexos10(declaracionView);
            Obtener.Anexos11(declaracionView);
            Obtener.Anexos13(declaracionView);
            #endregion

            #region GLOSAS

            //using (GestionGlosas GestGlosas = new GestionGlosas())
            //    declaracionView.Glosas =GestGlosas.ObtenerGlosasxDec(declaracionView);

            //using (GestionGlosas GestGlosas = new GestionGlosas())
            //    declaracionView.IGlosas = GestGlosas.ObtenerInGlosasxDec(declaracionView);
            #endregion

            this.Advertencias.AddRange(Obtener.Advertencias);
            this.Errores.AddRange(Obtener.Errores);

            return declaracionView;
        }

        public List<clsBusquedaDeclaracion> BuscarDeclaracion(clsBusquedaDeclaracion parametros)
        {

            ObtenerDatos Obtener = new ObtenerDatos();
            return Obtener.BuscarDeclaracion(parametros);
        }

        #endregion

        #region ListaTareas

        public List<clsListaTareas> ObtenerListaTareas(int id_usuario, DateTime? FecharadicadoInicia, DateTime? FechaRadicadofinal, string NumeroFormulario, int? PageNumber, int? PageSize)
        {
            ObtenerDatos Obtener = new ObtenerDatos();
            List<clsListaTareas> ListaTareas = new List<clsListaTareas>();
            ListaTareas = Obtener.ObtenerListaTareas(id_usuario, FecharadicadoInicia, FechaRadicadofinal, NumeroFormulario, PageNumber, PageSize);
            return ListaTareas;
        }

        public List<clsListaTareas> ObtenerListaTareasPaginado(int id_usuario, int startRow, int pageSize, string sortColumns, string filterEx)
        {
            ObtenerDatos Obtener = new ObtenerDatos();
            List<clsListaTareas> ListaTareas = new List<clsListaTareas>();
            ListaTareas = Obtener.ObtenerListaTareasPaginado(id_usuario, startRow, pageSize, sortColumns, filterEx);
            return ListaTareas;
        }

        public int ObtenerListaTareasCantidad(int idUsuario)
        {
            ObtenerDatos Obtener = new ObtenerDatos();
            return Obtener.ObtenerListaTareasCantidad(idUsuario);
        }

        public int ObtenerListaTareasWPFCantidad(int idUsuario, DateTime? FecharadicadoInicia, DateTime? FechaRadicadofinal, string NumeroFormulario)
        {
            ObtenerDatos Obtener = new ObtenerDatos();
            return Obtener.ObtenerListaTareasWPFCantidad(idUsuario, FecharadicadoInicia, FechaRadicadofinal, NumeroFormulario);
        }

        public void RadicacionActualizarEstado(int idRadicacion, int param_estado)
        {
            DbConnection con = entidadRUV.GetInstance().CreateConnection();
            con.Open();
            using (DbTransaction tran = con.BeginTransaction())
            {
                try
                {
                    new GuardarDatos().RadicacionActualizarEstado(idRadicacion, param_estado, tran);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        con.Close();
                }
            }
        }


        public void ActualizarEstadoDeclaracion(clsDeclaracion declaracion)
        {
            DbConnection con = entidadRUV.GetInstance().CreateConnection();
            con.Open();
            using (DbTransaction tran = con.BeginTransaction())
            {
                try
                {
                    GuardarDatos.ActualizarEstadoDeclaracion(declaracion, tran);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                        con.Close();
                }
            }
        }
        #endregion
    }
}
