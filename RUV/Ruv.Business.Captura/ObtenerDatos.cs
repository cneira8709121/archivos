using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.General;

using Ruv.Data;
using Ruv.Data.Reconocimiento;

using Ruv.Business.Captura.Declaracion;

namespace Ruv.Business.Captura
{
    public class ObtenerDatos
    {
        #region Parametros
        public Procesos Proceso { get; set; }

        public List<string> Errores;
        public List<string> Advertencias;
        #endregion

        public ObtenerDatos()
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

        #region Declaracion y Declarante
        public void InfoDeclaracion(ref clsDeclaracion declaracionView)
        {
            Ruv.Business.Captura.Declaracion.InfoDeclaracion.Obtener(ref declaracionView);

            #region Narracion - HOJA 3
            try
            {
                clsDescripcionHechos narracionView = declaracionView.DescripcionHechos;
                Narracion.Obtener((int)declaracionView.ID, ref narracionView);
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
            }
            #endregion
        }
        #endregion

        public void Caracterizacion(clsDeclaracion declaracionView)
        {
            //Obtener registros personas de la declaracion
            const int FamiliaConsecutivo = 1;   //Se obtienen los registros de la familia regiatrada en la hoja 2 de la declaración, la siempre queda con el ID = 1
            entRegistroPersona entRegPer = new entRegistroPersona();
            List<TBREGISTROS_PERSONAS> registrosPersonasData = RegistroPersona.ObtenerRegistrosPersona((int)declaracionView.ID, FamiliaConsecutivo);

            declaracionView.PersonasAfectadas = new clsPersonasAfectadas();
            //declaracionView.PersonasAfectadas.ListaPersonas = new System.Collections.ObjectModel.ObservableCollection<clsPersonaAfectada>();

            clsTomaDeclaracion tomaDeclaracion = declaracionView.TomaDeclaracion;

            foreach (TBREGISTROS_PERSONAS registroPersonaData in registrosPersonasData)
            {
                //Por cada registro persona, se agrega una persona afectas
                clsPersonaAfectada personaView = new clsPersonaAfectada();
                Persona.Obtener(registroPersonaData, (int)registroPersonaData.TBPERSONAS.ID, personaView);

                if (registroPersonaData.ESDECLARANTE == 1)
                {
                    declaracionView.PersonasAfectadas.Declaracion = declaracionView;
                    declaracionView.PersonasAfectadas.DeclaranteId = personaView.ID;
                    Persona.ParseDataToView_Declarante(registroPersonaData, ref tomaDeclaracion);
                }

                //Agregar personaAfectada a la declaración
                declaracionView.PersonasAfectadas.ListaPersonas.Add(personaView);
            }

            declaracionView.PersonasAfectadas.EstadoRegistro = eEstadoRegistro.SinModificaciones;
        }

        #region ANEXOS:
        public void Anexos01(clsDeclaracion declaracionView)
        {
            try
            {
                declaracionView.A01 = new List<clsAnexo01>();

                //Obtener los datos del encabezado del anexo
                int i = 0;
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo01.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo01 anexoView = Ruv.Business.Captura.Anexos.Anexo01.ObtenerAnexo(siniestroData);
                        declaracionView.A01.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo01[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }

            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos01.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos02(clsDeclaracion declaracionView)
        {
            try
            {
                declaracionView.A02 = new List<clsAnexo02>();

                //Obtener los datos del encabezado del anexo
                int i = 0;
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo02.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo02 anexoView = Ruv.Business.Captura.Anexos.Anexo02.ObtenerAnexo(siniestroData);
                        declaracionView.A02.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo02[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos02.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos03(clsDeclaracion declaracionView)
        {
            try
            {
                int i = 0;

                declaracionView.A03 = new List<clsAnexo03>();

                //Obtener los datos del encabezado del anexo
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo03.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo03 anexoView = Ruv.Business.Captura.Anexos.Anexo03.ObtenerAnexo(siniestroData);
                        declaracionView.A03.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo03[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos03.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos04(clsDeclaracion declaracionView)
        {
            try
            {
                int i = 0;
                declaracionView.A04 = new List<clsAnexo04>();

                //Obtener los datos del encabezado del anexo
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo04.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo04 anexoView = Ruv.Business.Captura.Anexos.Anexo04.ObtenerAnexo(siniestroData);
                        declaracionView.A04.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo04[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos04.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos05(clsDeclaracion declaracionView)
        {
            try
            {
                int i = 0;
                declaracionView.A05 = new List<clsAnexo05>();

                //Obtener los datos del encabezado del anexo
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo05.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo05 anexoView = Ruv.Business.Captura.Anexos.Anexo05.ObtenerAnexo(siniestroData);
                        declaracionView.A05.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo05[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos05.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos06(clsDeclaracion declaracionView)
        {
            try
            {
                int i = 0;
                declaracionView.A06 = new List<clsAnexo06>();

                //Obtener los datos del encabezado del anexo
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo06.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo06 anexoView = Ruv.Business.Captura.Anexos.Anexo06.ObtenerAnexo(siniestroData);
                        declaracionView.A06.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo06[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos06.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos07(clsDeclaracion declaracionView)
        {
            try
            {
                int i = 0;
                declaracionView.A07 = new List<clsAnexo07>();

                //Obtener los datos del encabezado del anexo
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo07.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo07 anexoView = Ruv.Business.Captura.Anexos.Anexo07.ObtenerAnexo(siniestroData);
                        declaracionView.A07.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo07[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos07.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos08(clsDeclaracion declaracionView)
        {
            try
            {
                int i = 0;
                declaracionView.A08 = new List<clsAnexo08>();

                //Obtener los datos del encabezado del anexo
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo08.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo08 anexoView = Ruv.Business.Captura.Anexos.Anexo08.ObtenerAnexo(siniestroData);
                        declaracionView.A08.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo08[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos08.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos09(clsDeclaracion declaracionView)
        {
            try
            {
                int i = 0;
                declaracionView.A09 = new List<clsAnexo09>();

                //Obtener los datos del encabezado del anexo
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo09.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo09 anexoView = Ruv.Business.Captura.Anexos.Anexo09.ObtenerAnexo(siniestroData);
                        declaracionView.A09.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo09[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos09.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos10(clsDeclaracion declaracionView)
        {
            try
            {
                int i = 0;
                declaracionView.A10 = new List<clsAnexo10>();

                //Obtener los datos del encabezado del anexo
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo10.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo10 anexoView = Ruv.Business.Captura.Anexos.Anexo10.ObtenerAnexo(siniestroData);
                        declaracionView.A10.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo10[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos10.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        public void Anexos11(clsDeclaracion declaracionView)
        {
            try
            {
                int i = 0;
                declaracionView.A11 = new List<clsAnexo11>();

                //Obtener los datos del encabezado del anexo
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo11.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo11 anexoView = Ruv.Business.Captura.Anexos.Anexo11.ObtenerAnexo(siniestroData);
                        declaracionView.A11.Add(anexoView);
                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo11[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos11.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        #region Anexo13
        public void Anexos13(clsDeclaracion declaracionView)
        {
            try
            {
                int i = 0;
                declaracionView.A13 = new List<clsAnexo13>();

                //Obtener los datos del encabezado del anexo
                List<TBSINIESTROS_PERSONA> siniestrosData = Ruv.Business.Captura.Anexos.Anexo13.ObtenerSiniestros((int)declaracionView.ID);
                foreach (TBSINIESTROS_PERSONA siniestroData in siniestrosData)
                {
                    try
                    {
                        clsAnexo13 anexoView = new clsAnexo13();
                        Ruv.Business.Captura.Anexos.Anexo13.ObtenerAnexo(siniestroData, (int)declaracionView.ID, anexoView);

                        //////Cargar los datos de contacto del jefe de hogar del anexo 13
                        ////DatosContactoAnexo13(declaracionView, anexoView);
                    
                        declaracionView.A13.Add(anexoView);

                    }
                    catch (Exception ex)
                    {
                        AddError(string.Format("- Error obteniendo Anexo13[{0}]:{1}{2}", i, Environment.NewLine, ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(string.Format("- Error obteniendo Anexos13.{0}{1}", Environment.NewLine, ex.Message));
            }
        }

        /// <summary>
        /// Actualizar los datos de contacto del jefe del grupo familiar, del anexo13
        /// </summary>
        /// <param name="declaracionView"></param>
        /// <param name="anexo"></param>
        private void DatosContactoAnexo13(clsDeclaracion declaracionView, clsAnexo13 anexo)
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
            anexo.DatoContactoDireccion = declaracionView.TomaDeclaracion.DatoContactoDireccion;
            anexo.DatoContactoPais = declaracionView.TomaDeclaracion.DatoContactoPais;
            anexo.DatoContactoDepartamento = declaracionView.TomaDeclaracion.DatoContactoDepartamento;
            anexo.DatoContactoMunicipio = declaracionView.TomaDeclaracion.DatoContactoMunicipio;
            anexo.DatoContactoTipoEntorno = declaracionView.TomaDeclaracion.DatoContactoTipoEntorno;
            anexo.DatoContactoLocalidadCorregimientoId = declaracionView.TomaDeclaracion.DatoContactoLocalidadCorregimientoId;
            anexo.DatoContactoBarrioVeredaId = declaracionView.TomaDeclaracion.DatoContactoBarrioVeredaId;
            anexo.DatoContactoTelefonoFijo = declaracionView.TomaDeclaracion.DatoContactoTelefonoFijo;
            anexo.DatoContactoTelefonoCelular = declaracionView.TomaDeclaracion.DatoContactoTelefonoCelular;
            anexo.DatoContactoCorreoElectronico = declaracionView.TomaDeclaracion.DatoContactoCorreoElectronico;
            
            //Datos de contacto Alterno
            anexo.DatoAlternoContactoDireccion = declaracionView.TomaDeclaracion.DatoAlternoContactoDireccion;
            anexo.DatoAlternoContactoPais = declaracionView.TomaDeclaracion.DatoAlternoContactoPais;
            anexo.DatoAlternoContactoDepartamento = declaracionView.TomaDeclaracion.DatoAlternoContactoDepartamento;
            anexo.DatoAlternoContactoMunicipio = declaracionView.TomaDeclaracion.DatoAlternoContactoMunicipio;
            anexo.DatoAlternoContactoTipoEntorno = declaracionView.TomaDeclaracion.DatoAlternoContactoTipoEntorno;
            anexo.DatoAlternoContactoLocalidadCorregimientoId = declaracionView.TomaDeclaracion.DatoAlternoContactoLocalidadCorregimientoId;
            anexo.DatoAlternoContactoBarrioVeredaId = declaracionView.TomaDeclaracion.DatoAlternoContactoBarrioVeredaId;
            anexo.DatoAlternoContactoTelefonoFijo = declaracionView.TomaDeclaracion.DatoAlternoContactoTelefonoFijo;
            anexo.DatoAlternoContactoTelefonoCelular = declaracionView.TomaDeclaracion.DatoAlternoContactoTelefonoCelular;
            anexo.DatoAlternoContactoCorreoElectronico = declaracionView.TomaDeclaracion.DatoAlternoContactoCorreoElectronico;                        
        }

        #endregion
        #endregion

        #region BuscarDeclaracion
        public List<clsBusquedaDeclaracion> BuscarDeclaracion(clsBusquedaDeclaracion parametros)
        {
            entDeclaraciones entListaDeclaracion = new entDeclaraciones();
            List<clsBusquedaDeclaracion> cListaDeclaraciones = new List<clsBusquedaDeclaracion>();

            System.Data.DataTable ListaDeclaraciones = new System.Data.DataTable();


            ListaDeclaraciones = entListaDeclaracion.BuscarDeclaracion(getParametrosBusquedaDecla(parametros));

            foreach (System.Data.DataRow DeclaracionData in ListaDeclaraciones.Rows)
            {
                clsBusquedaDeclaracion declaracionView = new clsBusquedaDeclaracion();

                declaracionView.Id = (int)DeclaracionData["Id"];
                declaracionView.FechaDeclaracion = (DeclaracionData["FechaDeclaracion"] == DBNull.Value) ? null : (DateTime?)DeclaracionData["FechaDeclaracion"];

                short? vDecl = Common.ParseIntToShortNullable((int)DeclaracionData["EstadoDeclaracion"]);
                string sDecl = (vDecl.HasValue) ? vDecl.ToString() : string.Empty;
                bool exists = Enum.IsDefined(typeof(Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion), sDecl);
                Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion eDecl = (exists) ? (Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion)vDecl : Ruv.Infrastructure.Crosscutting.Common.eEstadoDeclaracion.Ninguno;

                declaracionView.EstadoDeclaracion = eDecl;
                declaracionView.CodigoDeclaracion = (DeclaracionData["CodigoDeclaracion"] == DBNull.Value) ? null : (string)DeclaracionData["CodigoDeclaracion"];
                declaracionView.DeclaranteNumeroIdentificacion = (DeclaracionData["DeclaranteNumeroIdentificacion"] == DBNull.Value) ? null : (string)DeclaracionData["DeclaranteNumeroIdentificacion"];
                declaracionView.DeclarantePrimerNombre = (DeclaracionData["DeclarantePrimerNombre"] == DBNull.Value) ? null : (string)DeclaracionData["DeclarantePrimerNombre"];
                declaracionView.DeclaranteDemasNombres = (DeclaracionData["DeclaranteDemasNombres"] == DBNull.Value) ? null : (string)DeclaracionData["DeclaranteDemasNombres"];
                declaracionView.DeclarantePrimerApellido = (DeclaracionData["DeclarantePrimerApellido"] == DBNull.Value) ? null : (string)DeclaracionData["DeclarantePrimerApellido"];
                declaracionView.DeclaranteSegundoApellido = (DeclaracionData["DeclaranteSegundoApellido"] == DBNull.Value) ? null : (string)DeclaracionData["DeclaranteSegundoApellido"];

                cListaDeclaraciones.Add(declaracionView);
            }

            return cListaDeclaraciones;

        }

        private object[] getParametrosBusquedaDecla(clsBusquedaDeclaracion objParametros)
        {
            return new object[]{
                                 objParametros.CodigoDeclaracion
                                ,objParametros.DeclaranteNumeroIdentificacion
                                ,objParametros.DeclarantePrimerNombre
                                ,objParametros.DeclaranteDemasNombres
                                ,objParametros.DeclarantePrimerApellido
                                ,objParametros.DeclaranteSegundoApellido
                                , null
                            };
        }
        #endregion

        #region Lista Tareas:
        public List<clsListaTareas> ObtenerListaTareas(int id_usuario, DateTime? FecharadicadoInicia, DateTime? FechaRadicadofinal, string NumeroFormulario, int? PageNumber, int? PageSize)
        {
            //Obtener los datos del encabezado del anexo
            entListaTareas entListaTarea = new entListaTareas();
            List<clsListaTareas> cListaTareas = new List<clsListaTareas>();

            System.Data.DataTable ListaTareas = new System.Data.DataTable();

            ListaTareas = entListaTarea.getData(id_usuario, FecharadicadoInicia, FechaRadicadofinal, NumeroFormulario, PageNumber, PageSize);

            foreach (System.Data.DataRow tareaData in ListaTareas.Rows)
            {
                clsListaTareas tareaView = new clsListaTareas();

                tareaView.Declaracion = Common.ParseStringToIntNullable(tareaData["DECLARACION"].ToString());
                tareaView.Fecha = (DateTime)tareaData["FECHA"];
                tareaView.FechaLlegada = (DateTime)tareaData["FECHARADICACION"];
                tareaView.Accion = tareaData["ACCION"].ToString();
                tareaView.Formulario = tareaData["FORMULARIO"].ToString();                
                
                cListaTareas.Add(tareaView);
            }
           

            return cListaTareas;
        }

        public List<clsListaTareas> ObtenerListaTareasPaginado(int id_usuario, int startRow, int pageSize, string sortColumns, string filterEx)
        {
            //Obtener los datos del encabezado del anexo
            entListaTareas entListaTarea = new entListaTareas();
            List<clsListaTareas> cListaTareas = new List<clsListaTareas>();

            System.Data.DataTable ListaTareas = new System.Data.DataTable();

            ListaTareas = entListaTarea.getDataPaginado(id_usuario, startRow, pageSize, sortColumns, filterEx);

            foreach (System.Data.DataRow tareaData in ListaTareas.Rows)
            {
                clsListaTareas tareaView = new clsListaTareas();

                tareaView.Declaracion = Common.ParseStringToIntNullable(tareaData["DECLARACION"].ToString());
                tareaView.Fecha = (DateTime)tareaData["FECHA"];
                tareaView.FechaLlegada = (DateTime)tareaData["FECHALLEGADA"];
                tareaView.Accion = tareaData["ESTADO"].ToString();
                tareaView.IdAccion = int.Parse(tareaData["IDACCION"].ToString());
                tareaView.Formulario = tareaData["FORMULARIO"].ToString();
                tareaView.Regpersona = string.IsNullOrEmpty(tareaData["REGPERSONA"].ToString()) ? null : (int?)int.Parse(tareaData["REGPERSONA"].ToString());
                tareaView.Tipo = tareaData["TIPO"].ToString();
                tareaView.Correccion = string.IsNullOrEmpty(tareaData["CORRECCION"].ToString()) ? null : (int?)int.Parse(tareaData["CORRECCION"].ToString());

                cListaTareas.Add(tareaView);
            }


            return cListaTareas;
        }

        public int ObtenerListaTareasCantidad(int idUsuario)
        {
            entListaTareas entListaTarea = new entListaTareas();
            int cantidad = entListaTarea.getDataCount(idUsuario);
            return cantidad;
        }

        public int ObtenerListaTareasWPFCantidad(int idUsuario, DateTime? FecharadicadoInicia, DateTime? FechaRadicadofinal, string NumeroFormulario)
        {
            entListaTareas entListaTarea = new entListaTareas();
            int cantidad = entListaTarea.getDataCountWPF(idUsuario, FecharadicadoInicia, FechaRadicadofinal, NumeroFormulario);
            return cantidad;
        }

        #endregion
    }
}

