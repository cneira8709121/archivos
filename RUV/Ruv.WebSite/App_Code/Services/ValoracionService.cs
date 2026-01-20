using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using Ionic.Zip;
using Microsoft.Reporting.WebForms;
using Ruv.Business.DTO.ActosAdministrativos;
using Ruv.Business.Valoracion.Asignacion;
using Ruv.Business.Valoracion.Valoracion;
using Ruv.Data;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Devolucion;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using val = Ruv.Business.DTO.Valoracion;
// NOTA: puede usar el comando "Cambiar nombre" del menú "Refactorizar" para cambiar el nombre de clase "ValoracionService" en el código, en svc y en el archivo de configuración a la vez.

    [AspNetCompatibilityRequirements(RequirementsMode
    = AspNetCompatibilityRequirementsMode.Required)]
    public class ValoracionService : IValoracionService
    {
        public List<clsDeclaracionValoraracion> ListarDeclaracionesSinValorar()
        {
            AsignarValoradorBusiness objAsignarValBusiness = new AsignarValoradorBusiness();
            return objAsignarValBusiness.ListarDeclaracionesSinValorar();
        }

        public void ListaDeclaracionesEnValPaginada(ref clsConsultaValoracion consulta, ref string error)
        {
            AsignarValoradorBusiness objAsignarValBusiness = new AsignarValoradorBusiness();
            objAsignarValBusiness.DeclaracionesEnValoracion(ref consulta, ref error);
        }

        public void ListaDeclaracionesEnValTotal(ref clsConsultaValoracion consulta, ref string error)
        {
            AsignarValoradorBusiness objAsignarValBusiness = new AsignarValoradorBusiness();
            objAsignarValBusiness.DeclaracionesEnValoracion(ref consulta, ref error);
        }

        public List<clsDeclaracionValoraracion> ListarDeclaracionesSinValorarPaginada(int Inicio, int Fin, string sortColumns, string filtro, string Valor)
        {
            AsignarValoradorBusiness objAsignarValBusiness = new AsignarValoradorBusiness();
            return objAsignarValBusiness.ListarDeclaracionesSinValorarPaginado(Inicio, Fin, sortColumns, filtro, Valor);
        }

        public List<clsDeclaracionValoraracion> ListarDeclaracionesAsignadas()
        {
            AsignarValoradorBusiness objAsignarValBusiness = new AsignarValoradorBusiness();
            return objAsignarValBusiness.DeclaracionesEnValoracion();
        }

        public List<clsValorador> ListarValoradoresDisponibles()
        {
            AsignarValoradorBusiness objAsignarValBusiness = new AsignarValoradorBusiness();
            return objAsignarValBusiness.ListarValoradores();
        }

        public bool Asignar(List<clsValoracion> asignaciones)
        {
            AsignarValoradorBusiness objAsignarValBusiness = new AsignarValoradorBusiness();
            bool Result = objAsignarValBusiness.Guardar(asignaciones);
            if (!string.IsNullOrEmpty(objAsignarValBusiness.ErrorMessage))
            {
                Exception ex = new Exception(objAsignarValBusiness.StackTrace);
                RegistroTraza.I.Registrar(ex);
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return Result;
        }

        public bool Reasignar(List<clsValoracion> asignaciones)
        {
            AsignarValoradorBusiness objAsignarValBusiness = new AsignarValoradorBusiness();
            bool Result = objAsignarValBusiness.Guardar(asignaciones);
            if (!string.IsNullOrEmpty(objAsignarValBusiness.ErrorMessage))
            {
                Exception ex = new Exception(objAsignarValBusiness.StackTrace);
                RegistroTraza.I.Registrar(ex);
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return Result;
        }

        public List<clsValoradorTareas> ListarValoracionesPorValoradorId(int valoradorId)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.getListaTareas(valoradorId);
        }

        public string GuardarValoracion(clsValoracion valoracion, bool finalizar) {
            var valoracionBusiness = new ValoracionesBusiness();

            var errorMessage = string.Empty;
            using (DbTransaction transaction = Dao.InitTransaction()) {
                try {
                    valoracionBusiness.GuardarValoracion(valoracion, finalizar, transaction);
                    valoracionBusiness.InsertaTipoMotivacion(valoracion.Id, valoracion.cIdTipoMotivo, transaction);

                    if (valoracion.EstadoId == (int)eEstadosValoracion.NoValoradoDevuelto) {
                        var devolucionService = new DevolucionService();
                        var devolucion = new clsDevolucion { NIdDeclaracion = valoracion.DeclaracionId
                                                           , NIdUsuario = RUV.Current.Usuario.Id
                                                           , CObservaciones = valoracion.Observacion
                                                           , LstCausalesDevolucion = valoracion.CausalDevolucion};

                        devolucionService.SolicitarDevolucion(devolucion, ref errorMessage);
                    }

                    if (!string.IsNullOrEmpty(errorMessage)) {
                        RegistroTraza.I.Registrar(string.Format("No se pudo finalizar la valoración: {0}", errorMessage));
                        throw new ApplicationException(string.Format("No se pudo finalizar la valoración: {0}", errorMessage));
                    }

                    transaction.Commit();

                    //if (finalizar) { 
                    //    var actosAdminService = new ActosAdminService();
                    //    actosAdminService.GenerarDocumentoValoracion(valoracion.Id, false, ref errorMessage);
                    //}

                    return errorMessage;
                }
                catch (Exception ex) {
                    RegistroTraza.I.Registrar(ex);
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    transaction.Rollback();
                    return string.Format("{0} chr(13) {1}", ex.Message, ex.StackTrace);
                }
            }
        }

        public clsValoracion ValoracionPorId(int ValoracionId, bool Completa)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.getValoracion(ValoracionId, Completa);
        }

        public clsValoracion ValoracionPorDeclaracionId(int NIdDeclaracion)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.getValoracionByDeclaracionID(NIdDeclaracion);
        }

        public List<clsDeclaracionInfoValoracion> InformacionDeclaracionPorId(int ValoracionId)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetInforDeclaracionVal(ValoracionId);
        }

        public List<clsHechosValoracion> HechosPorDeclaracionId(int declaracionId)
        {
            throw new NotImplementedException();
        }

        public List<clsPersonaAnexo> ListarPersonasPorHecho(int hechoId, int TipohechoId)
        {
            throw new NotImplementedException();
        }

        public List<clsEstadosValoracion> ListarEstados()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetEstadosValoracion();
        }

        public List<clsObservacionEstado> ListarObservacionEstadoPorEstadoId(int estadoId)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetObservacionesEstadoPorEstado(estadoId);
        }

        public List<clsPrincipioEstado> ListarPrincipioEstadoPorEstadoId(int estadoId)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetPrincipiosPorEstado(estadoId);
        }

        public List<clsAutores> ListarAutores()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetAutores();
        }

        public List<clsInfracciones> ListarInfracciones()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetInfracciones();
        }

        public List<clsHerramientas> ListarHerramientasPorTipo(int tipo)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetHerramientasPorTipo(tipo);
        }

        public clsHerramientas HerramientaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<clsPersona> ListarPersonasPorDeclaracion(int declaracionId)
        {
            AsignarValoradorBusiness objAsignar = new AsignarValoradorBusiness();
            return objAsignar.ListarDetalleDeclaracionPorId(declaracionId);
        }

        public List<clsTipoHerramienta> ListarTiposDeHerramienta()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetTiposHerramienta();
        }

        public clsTipoHerramienta TipoHerramientaPorId(int Id)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetTipoHerramientaPorId(Id);
        }

        public List<clsAutores> ListarAutoresPorAnexo(int ValAnexoPer)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetAutores(ValAnexoPer);
        }

        public List<clsInfracciones> ListarInfraccionesPorValPerId(int valAnexoPerId)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetInfracciones(valAnexoPerId);
        }

        public List<clsRegistrosAnteriores> ListarRegistrosAnteriores()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetRegistrosAnteriores();
        }

        public List<clsPreguntasRegAnt> ListarPreguntasRegAnt()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetPreguntasRegAnt();
        }

        public bool DeshacerAsignacion(clsValoracion valoracion)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.DeshacerAsignacion(valoracion);
        }

        public DataSet getInforme()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetInformeValoracion();
        }

        public DataSet getResumenPorId(int valId)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.getReportePorId(valId);
        }

        public List<clsParametroGeneral> ListarParametros()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetParametros();
        }

        public List<clsSubEtnias> ListarSubEtnias(int etniaId)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetSubEtnias(etniaId);
        }

        public List<Ruv.Infrastructure.Crosscutting.Common.Valoracion.clsGeografia> ListarGeografia(int? nivel, int? tipo, int? padre)
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetGeografia(nivel,tipo, padre);
        }

        public string NuevoHecho(clsHecho HechoVictimizante)
        {
            try
            {
                ValoracionesBusiness obj = new ValoracionesBusiness();
                obj.NuevoAnexo(HechoVictimizante);
                return string.Empty;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(ex);
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return string.Format("{0} chr(13) {1}", ex.Message, ex.StackTrace);
            }
        }

        public int CantidadDeclaracionesSinValorar(string filtro, string Valor)
        {
            AsignarValoradorBusiness objAsignarValBusiness = new AsignarValoradorBusiness();
            return objAsignarValBusiness.CantidadDeclaracionesSinValorar(filtro, Valor);
        }

        public bool AsignarTodos(int usuarioId)
        {
            AsignarValoradorBusiness objAsignar = new AsignarValoradorBusiness();
            return objAsignar.Guardar(usuarioId);
        }

        public bool AutoAsignaValorador(int IdDeclaracion, ref string cError)
        {
            AsignarValoradorBusiness objAsignar = new AsignarValoradorBusiness();
            return objAsignar.AutoAsignaValorador(IdDeclaracion, ref cError);
        }

        public void ListaTareasValorador(ref clsConsultaValoracion eConsulta, ref string error)
        {
            ValoracionesBusiness objValBusiness = new ValoracionesBusiness();
            objValBusiness.ListaTareasValorador(ref eConsulta, ref error);
        }

        public void ListaTareasValoradorCantidad(ref clsConsultaValoracion eConsulta, ref string error)
        {
            ValoracionesBusiness objValBusiness = new ValoracionesBusiness();
            objValBusiness.ListaTareasValoradorCantidad(ref eConsulta, ref error);
        }

        public List<clsPrincipioEstado> ListarPrincipios()
        {
            ValoracionesBusiness objValBusiness = new ValoracionesBusiness();
            return objValBusiness.GetPrincipios();
        }

        public List<clsObservacionEstado> ListarObservacion()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetObservacionesEstado();
        }

        public List<clsHerramientas> ListarHerramientas()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetHerramientas();
        }

        internal List<Ruv.Infrastructure.Crosscutting.Common.Valoracion.clsGeografia> ListarGeografia()
        {
            ValoracionesBusiness objValoracion = new ValoracionesBusiness();
            return objValoracion.GetGeografia();
        }

        #region [Metodos Internos]

        /// <summary>
        /// Genera el documento de la valoración (Acto administrativo)
        /// </summary>
        /// <param name="idValoracion">id de la valoración</param>
        [Obsolete]
        private void GenerarDocumentoValoracion(int idValoracion)
        {
            //List<byte[]> filesByte = new List<byte[]>();
            IDictionary<string, byte[]> dicfilesByte = new Dictionary<string, byte[]>();

            CargaDatosValoracionService service = new CargaDatosValoracionService();
            string cError = string.Empty;
            IList<clsNotificacionVal> listclsNotificacionVal = service.CargaDatosValoracionNoti(idValoracion, ref cError);

            string tipo = string.Empty;
            //Pregunta por el resultado de la valoracion
            if (listclsNotificacionVal != null)
            {
                if (listclsNotificacionVal.FirstOrDefault().nTipoDocumentoVal == (int)eTipoDocumentoValoracion.Incluido)
                {
                    tipo = "Incluido";
                }
                if (listclsNotificacionVal.FirstOrDefault().nTipoDocumentoVal == (int)eTipoDocumentoValoracion.Excluido)
                {
                    tipo = "NoIncluido";
                }
                if (listclsNotificacionVal.FirstOrDefault().nTipoDocumentoVal == (int)eTipoDocumentoValoracion.Mixto)
                {
                    tipo = "Mixto";
                }
            }
            else
            {
                listclsNotificacionVal = new List<clsNotificacionVal>();
            }
            //TODO: obtener a partir del cargo del usuario, el valor para mostrar o no la firma
            bool esJefeRegistro = false;
            if (RUV.Current.Usuario.Cargo == Cargos.JefeRegistro)
                esJefeRegistro = true;

            //Resolucion
            ReportViewer viewerResolucion = new ReportViewer();
            viewerResolucion.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("/Reportes/Valoracion/" + tipo + "/ReporteValoracionResolucion.rdlc");
            viewerResolucion.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
            viewerResolucion.LocalReport.SetParameters(new ReportParameter("EsJefeRegistro", esJefeRegistro.ToString(), true));
            viewerResolucion.LocalReport.Refresh();
            byte[] bytesResolucion = viewerResolucion.LocalReport.Render("PDF");

            //Aviso
            ReportViewer viewerAviso = new ReportViewer();
            viewerAviso.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("/Reportes/Valoracion/" + tipo + "/ReporteValoracionAviso.rdlc");
            viewerAviso.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
            viewerAviso.LocalReport.SetParameters(new ReportParameter("EsJefeRegistro", esJefeRegistro.ToString(), true));
            viewerAviso.LocalReport.Refresh();
            byte[] bytesAviso = viewerAviso.LocalReport.Render("PDF");

            //NotificacionPersonal
            ReportViewer viewerNotificacionPersonal = new ReportViewer();
            viewerNotificacionPersonal.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("/Reportes/Valoracion/" + tipo + "/ReporteValoracionNotificacionPersonal.rdlc");
            viewerNotificacionPersonal.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
            viewerNotificacionPersonal.LocalReport.Refresh();
            byte[] bytesNotificacionPersonal = viewerNotificacionPersonal.LocalReport.Render("PDF");

            //Citacion
            ReportViewer viewerCitacion = new ReportViewer();
            viewerCitacion.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("/Reportes/Valoracion/" + tipo + "/ReporteValoracionCitacion.rdlc");
            viewerCitacion.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", listclsNotificacionVal));
            viewerCitacion.LocalReport.SetParameters(new ReportParameter("EsJefeRegistro", esJefeRegistro.ToString(), true));
            viewerCitacion.LocalReport.Refresh();
            byte[] bytesCitacion = viewerCitacion.LocalReport.Render("PDF");

            dicfilesByte.Add("Resolucion", bytesResolucion);
            dicfilesByte.Add("Aviso", bytesAviso);
            dicfilesByte.Add("Notificacion", bytesNotificacionPersonal);
            dicfilesByte.Add("Citacion", bytesCitacion);

            string path = System.Configuration.ConfigurationManager.AppSettings["PathArchivosActosAdmin"];

            string nombreArchivo = idValoracion.ToString();

            string folderName = path + nombreArchivo;
            System.IO.Directory.CreateDirectory(folderName);
            using (ZipFile zip = new ZipFile())
            {                
                foreach (KeyValuePair<string, byte[]> keyValuePair in dicfilesByte)
                {
                    System.IO.File.WriteAllBytes(folderName + "/" + keyValuePair.Key + ".pdf", keyValuePair.Value);
                    zip.AddEntry(keyValuePair.Key + ".pdf", keyValuePair.Value);
                }
                zip.Save(folderName + "/" + nombreArchivo + ".zip");
            }
        }

        public List<clsEntidadMunicipio> ObtenerEntidadesMunicipio(ref string cError)
        {
            var valBusiness = new ValoracionesBusiness();
            return valBusiness.ObtenerEntidadesMunicipio(ref cError);
        }

        public bool AgregarPersonaService(clsAgregarPersonaValoracion AgregaPerso, ref string cError)
        {
            var valBusiness = new ValoracionesBusiness();
            return valBusiness.AgregaPersonaValoracion(AgregaPerso, ref cError);
        }

        public List<val::clsCargaPersonasAsociadasDeclaracion> CargaDatosPersonasAsociadas(int nIddeclaracion, ref string cError)
        {
            var ValBusiness = new ValoracionesBusiness();
            return ValBusiness.CargaDatosPersonasAsociadas(nIddeclaracion, ref cError);
        }

        public int CargaDatosPersonasAsociadasCount(int nIdDelcaracion, ref string cError)
        {
            var ValBusiness = new ValoracionesBusiness();
            return ValBusiness.CargaPersonasAsociadasCount(nIdDelcaracion, ref cError);
        }

        public int ObtenerIdValoracionporIdDeclaracionServ(int nIdDeclaracion, ref string cError)
        {
            var ValBusiness = new ValoracionesBusiness();
            int nIdValoracion = ValBusiness.ObtenerIdValoracionporIdDeclaracionB(nIdDeclaracion, ref cError);
            return nIdValoracion;
        }

        #endregion
    }
