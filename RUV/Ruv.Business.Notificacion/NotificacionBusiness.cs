using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dto = Ruv.Business.DTO.Notificacion;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Notificacion;
using Ruv.Data.Notificacion.Contratos;
using u = Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using Ruv.Business.Notificacion.Contratos;
using Ruv.Data;
using System.Data.Common;
using Ruv.Infrastructure.Crosscutting.Common;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.Data.Feriados.Contratos;

namespace Ruv.Business.Notificacion
{
    public class NotificacionBusiness : INotificacionBusiness 
    {

        public IList<entidad::clsNotificacion> ObtenerNotificaciones(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas, string sortColumns, int startRow, int pageSize) {
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);

            IList<entidad::clsNotificacion> listNotificacion = new List<entidad::clsNotificacion>();
            var listDtoNotificacion = iNotificacionData.ObtenerNotificaciones(idUsuario, declaracion, tipoDocumento, documento, nombreDeclarante, paisNotificacion, departamentoNotificacion, municipioNotificacion, puntoNotificacion, direccionCitacion, soloAsignadas, sortColumns, startRow, pageSize);
            if (listDtoNotificacion != null) {
                foreach (dto::clsNotificacion dtoNotificacion in listDtoNotificacion) {
                    listNotificacion.Add(new entidad::clsNotificacion {
                        CUBICACIONNOTIFICACION = dtoNotificacion.UBICACIONNOTIFICACION,
                        ID_UBICACIONNOTIFICACION = dtoNotificacion.ID_UBICACIONNOTIFICACION,
                        CDIRECCIONNOTIFICACION = dtoNotificacion.DIRECCIONNOTIFICACION,
                        CESTADOPROCESO = dtoNotificacion.ESTADOPROCESO,
                        CESTADONOTIFICACION = dtoNotificacion.ESTADONOTIFICACION,
                        CID_DECLARACION = dtoNotificacion.ID_DECLARACION,
                        CNOMBRECOMPLETO = dtoNotificacion.NOMBRECOMPLETO,
                        CTIPODOCUMENTO = dtoNotificacion.TIPODOCUMENTO,
                        CNUMERODOCUMENTO = dtoNotificacion.NUMERODOCUMENTO,
                        CTELEFONONOTIFICACION = dtoNotificacion.TELEFONONOTIFICACION,
                        NID = dtoNotificacion.ID,
                        NID_ESTADONOTIFICACION = dtoNotificacion.ID_ESTADONOTIFICACION,
                        NID_PAQUETENOTIFICACION = dtoNotificacion.ID_PAQUETENOTIFICACION,
                        NID_USUARIO = dtoNotificacion.ID_USUARIO,
                        CNOMBREDEPARTAMENTO = dtoNotificacion.NOMBREDEPARTAMENTO,
                        CNOMBREMUNICIPIO = dtoNotificacion.NOMBREMUNICIPIO,
                        CNOMBREPAIS = dtoNotificacion.NOMBREPAIS,
                        CNumeroFormulario = dtoNotificacion.NumeroFormulario,
                        Aprobado = dtoNotificacion.Aprobado,
                        NID_PAIS = dtoNotificacion.ID_PAIS,
                        NID_DEPARTAMENTO = dtoNotificacion.ID_DEPARTAMENTO,
                        NID_MUNICIPIO = dtoNotificacion.ID_MUNICIPIO,
                        FechaFirma = dtoNotificacion.FECHAFIRMA,
                        IdPaisPuntoNotificacion = dtoNotificacion.ID_PAISPUNTO,
                        IdDepartamentoPuntoNotificacion = dtoNotificacion.ID_DEPARTAMENTOPUNTO,
                        IdMunicipioPuntoNotificacion = dtoNotificacion.ID_MUNICIPIOPUNTO,
                        IdPuntoAtencion = dtoNotificacion.ID_PUNTOATENCION,
                        IdDireccionTerritorial = dtoNotificacion.ID_DIRECCIONTERRITORIAL
                    });
                }
            }

            return listNotificacion;
        }

        public int ObtenerNotificacionesCantidad(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas) {
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            return iNotificacionData.ObtenerNotificacionesCantidad(idUsuario, declaracion, tipoDocumento, documento, nombreDeclarante, paisNotificacion, departamentoNotificacion, municipioNotificacion, puntoNotificacion, direccionCitacion, soloAsignadas);
        }

        public entidad::clsNotificacion ObtenerNotificacionPorId(int idNotificacion, ref string cError)
        {
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            var dtoNotificacion = iNotificacionData.ObtenerNotificacionPorId(idNotificacion, ref cError);
            if (dtoNotificacion != null)
            { 
                return new entidad::clsNotificacion()
                {
                    CUBICACIONNOTIFICACION = dtoNotificacion.UBICACIONNOTIFICACION,
                    CDIRECCIONNOTIFICACION = dtoNotificacion.DIRECCIONNOTIFICACION,
                    CESTADOPROCESO = dtoNotificacion.ESTADOPROCESO,
                    CESTADONOTIFICACION = dtoNotificacion.ESTADONOTIFICACION,
                    CID_DECLARACION = dtoNotificacion.ID_DECLARACION,
                    CNOMBRECOMPLETO = dtoNotificacion.NOMBRECOMPLETO,
                    CTIPODOCUMENTO = dtoNotificacion.TIPODOCUMENTO,
                    CNUMERODOCUMENTO = dtoNotificacion.NUMERODOCUMENTO,
                    CTELEFONONOTIFICACION = dtoNotificacion.TELEFONONOTIFICACION,
                    NID = dtoNotificacion.ID,
                    NID_ESTADONOTIFICACION = dtoNotificacion.ID_ESTADONOTIFICACION,
                    NID_PAQUETENOTIFICACION = dtoNotificacion.ID_PAQUETENOTIFICACION,
                    NID_USUARIO = dtoNotificacion.ID_USUARIO,
                    CNOMBREDEPARTAMENTO = dtoNotificacion.NOMBREDEPARTAMENTO,
                    CNOMBREMUNICIPIO = dtoNotificacion.NOMBREMUNICIPIO,
                    CNOMBREPAIS = dtoNotificacion.NOMBREPAIS,
                    CNumeroFormulario = dtoNotificacion.NumeroFormulario,
                    Aprobado = dtoNotificacion.Aprobado,
                    NID_PAIS = dtoNotificacion.ID_PAIS,
                    NID_DEPARTAMENTO = dtoNotificacion.ID_DEPARTAMENTO,
                    NID_MUNICIPIO = dtoNotificacion.ID_MUNICIPIO,
                    ID_UBICACIONNOTIFICACION = dtoNotificacion.ID_UBICACIONNOTIFICACION,
                    IdPaisPuntoNotificacion = dtoNotificacion.ID_PAISPUNTO,
                    IdDepartamentoPuntoNotificacion = dtoNotificacion.ID_DEPARTAMENTOPUNTO,
                    IdMunicipioPuntoNotificacion = dtoNotificacion.ID_MUNICIPIOPUNTO,
                    IdPuntoAtencion = dtoNotificacion.ID_PUNTOATENCION,
                    IdDireccionTerritorial = dtoNotificacion.ID_DIRECCIONTERRITORIAL
                };
            }
            return null;
        }

        public dto::clsPaqueteNotificacion CrearPaqueteNotificacionDesdeFiltro(int idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, string direccionCitacion, string ubicacionNotificacion, bool soloAsignadas, ref string cError)
        {
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            var notificacionId = iNotificacionData.CrearPaqueteNotificacionDesdeFiltro(idUsuario, declaracion, tipoDocumento, documento, nombreDeclarante, direccionCitacion, ubicacionNotificacion, soloAsignadas, ref cError);
            if (notificacionId.HasValue)
                return iNotificacionData.ObtenerPaquetePorId(notificacionId.Value, ref cError);

            return null;
        }

        public dto::clsNotificacionDetalle DetalleNotificacion(int nIdNotificacion)
        {
            Data.Notificacion.Contratos.INotificacionData iNotificacion = (Data.Notificacion.Contratos.INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            return iNotificacion.DetalleNotificaciones(nIdNotificacion);
        }

        public bool CreaPaqueteNotificacion(List<dto::clsNotificacion> lstnotificacion, int nIdUsuario, ref string cError)
        {
            Data.Notificacion.Contratos.INotificacionData iNotificacion = (Data.Notificacion.Contratos.INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            using (DbTransaction tra = Dao.InitTransaction())
            {
                int? nIdPaquete = iNotificacion.CreaPaqueteNotificacion(nIdUsuario, tra, ref cError);
                if (nIdPaquete.HasValue && string.IsNullOrEmpty(cError))
                {
                    if (iNotificacion.InsertaIdPaquete(lstnotificacion, nIdPaquete, tra, ref cError) && string.IsNullOrEmpty(cError))
                    {
                        tra.Commit();
                        return true;
                    }
                }
                tra.Rollback();
                return false;
            }
        }

        public int? CreaPaqueteNotificacion(List<int> lstnotificacion, int nIdUsuario, ref string cError)
        {
            Data.Notificacion.Contratos.INotificacionData iNotificacion = (Data.Notificacion.Contratos.INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            using (DbTransaction tra = Dao.InitTransaction())
            {
                int? nIdPaquete = iNotificacion.CreaPaqueteNotificacion(nIdUsuario, tra, ref cError);
                if (nIdPaquete.HasValue && string.IsNullOrEmpty(cError))
                {
                    if (iNotificacion.InsertaIdPaquete(lstnotificacion, nIdPaquete, tra, ref cError) && string.IsNullOrEmpty(cError))
                    {
                        tra.Commit();
                        return nIdPaquete;
                    }
                }
                tra.Rollback();
                return 0;
            }
        }

        public bool ActualizarNotificacion(int idNotificacion, string direccion, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
                if (iNotificacionData.ActualizarNotificacion(idNotificacion, direccion, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        public bool ActualizarPuntoNotificacion(entidad::clsNotificacion clsNotificacion, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
                if (iNotificacionData.ActualizarPuntoNotificacion(ClsNotificacionToDtoNotificacion(clsNotificacion), tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        private dto::clsNotificacion ClsNotificacionToDtoNotificacion(entidad::clsNotificacion clsNotificacion)
        {
            dto::clsNotificacion dtoNotificacion = new dto::clsNotificacion()
            {
                UBICACIONNOTIFICACION = clsNotificacion.CUBICACIONNOTIFICACION,
                DIRECCIONNOTIFICACION = clsNotificacion.CDIRECCIONNOTIFICACION,
                ESTADOPROCESO = clsNotificacion.CESTADOPROCESO,
                ESTADONOTIFICACION = clsNotificacion.CESTADONOTIFICACION,
                ID_DECLARACION = clsNotificacion.CID_DECLARACION,
                NOMBRECOMPLETO = clsNotificacion.CNOMBRECOMPLETO,
                TIPODOCUMENTO = clsNotificacion.CTIPODOCUMENTO,
                NUMERODOCUMENTO = clsNotificacion.CNUMERODOCUMENTO,
                TELEFONONOTIFICACION = clsNotificacion.CTELEFONONOTIFICACION,
                ID = clsNotificacion.NID,
                ID_ESTADONOTIFICACION = clsNotificacion.NID_ESTADONOTIFICACION,
                ID_PAQUETENOTIFICACION = clsNotificacion.NID_PAQUETENOTIFICACION,
                ID_USUARIO = clsNotificacion.NID_USUARIO,
                NOMBREDEPARTAMENTO = clsNotificacion.CNOMBREDEPARTAMENTO,
                NOMBREMUNICIPIO = clsNotificacion.CNOMBREMUNICIPIO,
                NOMBREPAIS = clsNotificacion.CNOMBREPAIS,
                NumeroFormulario = clsNotificacion.CNumeroFormulario,
                Aprobado = clsNotificacion.Aprobado,
                ID_PAIS = clsNotificacion.NID_PAIS,
                ID_DEPARTAMENTO = clsNotificacion.NID_DEPARTAMENTO,
                ID_MUNICIPIO = clsNotificacion.NID_MUNICIPIO,
                ID_UBICACIONNOTIFICACION = clsNotificacion.ID_UBICACIONNOTIFICACION,
                ID_PUNTOATENCION = clsNotificacion.IdPuntoAtencion,
                ID_DIRECCIONTERRITORIAL = clsNotificacion.IdDireccionTerritorial
            };
            return dtoNotificacion;
        }

        public bool SolicitaCorreccion(int nIdNotificacion, int nIdPuntoNotificacion, int nIdEstadoNotificacion, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
                if (iNotificacionData.SolicitaCorreccion(nIdNotificacion, nIdPuntoNotificacion, nIdEstadoNotificacion, tra, ref  cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;

            }
        }

        public bool CompararRegistrosCourier(int nIdPaqueteNotificacion,string cNombreArchivo, int nIdUsuario, ref string cError)
        {
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            
            List<dto::clsReporteCourier> lstReporte = iNotificacionData.CargarRegistrosCourier(cNombreArchivo, ref cError);
            if (lstReporte == null || !string.IsNullOrEmpty(cError)) return false;

            int cantidadNotificacionespaquete = iNotificacionData.ObtenerDetallePaqueteCount(nIdPaqueteNotificacion, ref cError);
            
            if (cantidadNotificacionespaquete == 0 || cantidadNotificacionespaquete <= 0 || !string.IsNullOrEmpty(cError)) return false;

            IList<dto::clsNotificacion> lstNotificacionActual = iNotificacionData.ObtenerDetallePaquete(nIdPaqueteNotificacion,1,cantidadNotificacionespaquete,ref cError);
            
            if (lstNotificacionActual == null || !string.IsNullOrEmpty(cError)) return false;

            try
            {
                //IEnumerable<dto::clsNotificacion> eNotificacion = lstNotificacionActual.Where(x => x.ID == int.Parse(lstReporte.Find(y => !string.IsNullOrEmpty(y.CAdicional1) && int.Parse(y.CAdicional1) == x.ID).CAdicional1));
                List<dto::clsNotificacion> eNotificacion = new List<dto::clsNotificacion>();
                foreach (dto::clsReporteCourier notReporte in lstReporte)
                {
                    if (!string.IsNullOrEmpty(notReporte.CContenido))
                    {
                        int nId = int.Parse(notReporte.CContenido);
                        foreach (dto::clsNotificacion not in lstNotificacionActual)
                        {
                            if (not.ID == nId) eNotificacion.Add(not);
                        }
                    }
                }

                if (eNotificacion == null || eNotificacion.ToList().Count == 0) cError = resx::Globalization.Errores.SinCoincidenciaNotificacionesCourier;
                else
                {
                    //TODO: jairovg - Hay que manejar la validación como warning
                    if (eNotificacion.ToList().Count < lstReporte.Count)
                    {
                        cError = resx::Globalization.Advertencia.SinCoincidenciaTotalNotificacionesCourier;
                        return false;
                    }
                    else
                    {
                        foreach (dto::clsNotificacion notificacion in eNotificacion)
                        {
                            dto::clsReporteCourier reporte = lstReporte.Find(y => !string.IsNullOrEmpty(y.CContenido) && int.Parse(y.CContenido) == notificacion.ID);
                            notificacion.ESTADOCOURIER = reporte.CEstado;
                            if (string.IsNullOrWhiteSpace(reporte.CEstado)) notificacion.ID_ESTADONOTIFICACION = (int)eEstadosNotificacion.NotificacionEstadoPorValidar;
                            else
                            {
                                if (notificacion.nEnvioResolucion == 1)
                                {
                                    switch (reporte.CEstado.ToUpper())
                                    {
                                        case "ENTREGADO":
                                            notificacion.ID_ESTADONOTIFICACION = (int)eEstadosNotificacion.NotificadoResolucion;
                                            break;
                                        case "DEVOLUCION":
                                            notificacion.ID_ESTADONOTIFICACION = (int)eEstadosNotificacion.NotificacionRechazada;
                                            break;
                                        case "EN PROCESO":
                                            notificacion.ID_ESTADONOTIFICACION = (int)eEstadosNotificacion.NotificacionEnProceso;
                                            break;
                                        default:
                                            notificacion.ID_ESTADONOTIFICACION = (int)eEstadosNotificacion.NotificacionEstadoPorValidar;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (reporte.CEstado.ToUpper())
                                    {
                                        case "ENTREGADO":
                                            notificacion.ID_ESTADONOTIFICACION = (int)eEstadosNotificacion.NotificacionEntregada;
                                            break;
                                        case "DEVOLUCION":
                                            notificacion.ID_ESTADONOTIFICACION = (int)eEstadosNotificacion.NotificacionRechazada;
                                            break;
                                        case "EN PROCESO":
                                            notificacion.ID_ESTADONOTIFICACION = (int)eEstadosNotificacion.NotificacionEnProceso;
                                            break;
                                        default:
                                            notificacion.ID_ESTADONOTIFICACION = (int)eEstadosNotificacion.NotificacionEstadoPorValidar;
                                            break;
                                    }
                                }
                            }
                            notificacion.DESTADOCOURIER = reporte.DEntrega;
                            notificacion.CausalDevolucion = reporte.CCausalDevolucion;
                            if (notificacion.ID_ESTADONOTIFICACION == (int)eEstadosNotificacion.NotificacionEntregada)
                            {
                                var feriados = new Ruv.Data.Feriados.GestionFeriados();
                                // TODO: jairovg - No se debe permitir calcular los días hábiles si la fecha DESTADOCOURIER es nula
                                var fechaVencimientoTerminos = feriados.CalcularDiasHabiles(notificacion.DESTADOCOURIER.Value, int.Parse(System.Configuration.ConfigurationManager.AppSettings["PlazoPlanA"].ToString()), false, ref cError);
                                notificacion.FECHAFINAL = fechaVencimientoTerminos;
                            }
                        }

                        using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
                        {
                            if (!iNotificacionData.ActualizarEstadoNotificacion(eNotificacion, tra, ref cError) || !string.IsNullOrEmpty(cError))
                            {
                                tra.Rollback();
                                return false;
                            }
                            tra.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }

            if (!string.IsNullOrEmpty(cError)) return false;
            return true;
        }

        public IList<entidad::clsNotificacion> ObtenerNotificacionesEntregadas(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, string sortColumns, int startRow, int pageSize, ref string cError)
        {
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            IList<entidad::clsNotificacion> listNotificacion = new List<entidad::clsNotificacion>();
            IList<dto::clsNotificacion> listDtoNotificacion = iNotificacionData.ObtenerNotificacionesEntregadas(idUsuario, busquedaGlobal, declaracion, tipoDocumento, documento, nombreDeclarante, estadoNotificacion, sortColumns, startRow, pageSize, ref cError);
            if (listDtoNotificacion != null)
            {
                foreach (dto::clsNotificacion dtoNotificacion in listDtoNotificacion) {
                    entidad::clsNotificacion notificacion = new entidad::clsNotificacion() {
                        CDIRECCIONNOTIFICACION = dtoNotificacion.DIRECCIONNOTIFICACION,
                        CESTADOPROCESO = dtoNotificacion.ESTADOPROCESO,
                        CESTADONOTIFICACION = dtoNotificacion.ESTADONOTIFICACION,
                        CID_DECLARACION = dtoNotificacion.ID_DECLARACION,
                        CNOMBRECOMPLETO = dtoNotificacion.NOMBRECOMPLETO,
                        CNUMERODOCUMENTO = dtoNotificacion.NUMERODOCUMENTO,
                        CTELEFONONOTIFICACION = dtoNotificacion.TELEFONONOTIFICACION,
                        NID = dtoNotificacion.ID,
                        NID_ESTADONOTIFICACION = dtoNotificacion.ID_ESTADONOTIFICACION,
                        NID_PAQUETENOTIFICACION = dtoNotificacion.ID_PAQUETENOTIFICACION,
                        NID_USUARIO = dtoNotificacion.ID_USUARIO,
                        CNOMBREDEPARTAMENTO = dtoNotificacion.NOMBREDEPARTAMENTO,
                        CNOMBREMUNICIPIO = dtoNotificacion.NOMBREMUNICIPIO,
                        CNOMBREPAIS = dtoNotificacion.NOMBREPAIS,
                        Aprobado = dtoNotificacion.Aprobado,
                        FechaFinal = dtoNotificacion.FECHAFINAL,
                        CTIPODOCUMENTO = dtoNotificacion.TIPODOCUMENTO,
                        CNumeroFormulario = dtoNotificacion.NumeroFormulario,
                        CUBICACIONNOTIFICACION = dtoNotificacion.UBICACIONNOTIFICACION
                    };
                    listNotificacion.Add(notificacion);
                }
            }

            return listNotificacion;
        }

        public int ObtenerNotificacionesEntregadasCantidad(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, ref string cError)
        {
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            return iNotificacionData.ObtenerNotificacionesEntregadasCantidad(idUsuario, busquedaGlobal, declaracion, tipoDocumento, documento, nombreDeclarante, estadoNotificacion, ref cError);
        }

        public bool CierraNotificacion(int nIdNotificacion, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
                if (iNotificacionData.CierraNotificacion(nIdNotificacion, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        public bool CambiarEstadoNotificacion(int nIdNotificacion, int idEstado,int diasHabiles, string cObservacion, ref string cError)
        {
            IGestionFeriados iFechaData = (IGestionFeriados)u::Spring.GetService(Objetos.FeriadosData);
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);

            //Los dias habiles seran 10 si se encuentra en el territorio colombiano, o 13 si se encuentra fuera de Colombia
            dto::clsNotificacion notificacion = iNotificacionData.ObtenerNotificacionPorId(nIdNotificacion, ref cError);
            // int diasHabiles = 10;
            if (notificacion.ID_PAIS != (int)ePaises.Colombia)
            {
                diasHabiles = diasHabiles + 3;
            }

            //Obtiene la fecha de finalizacion.
            DateTime? fechaFin = null;
            if (idEstado == (int)eEstadosNotificacion.EdictoPublicado)
            {
                fechaFin = iFechaData.CalcularDiasHabiles(DateTime.Now, diasHabiles, false, ref cError);
            }

            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                if (iNotificacionData.CambiarEstadoNotificacion(nIdNotificacion, idEstado, fechaFin, cObservacion, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        public int ObtenerPaquetesConteo(int idUsuario, string ordenServicio, DateTime? fechaInicio, DateTime? fechaFin, ref string cError) {
            INotificacionData iNotificacionData = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return iNotificacionData.ObtenerPaquetesConteo(idUsuario, ordenServicio, fechaInicio, fechaFin, ref cError);
        }

        public bool AgregaOrdenServicioBusiness(int nIdNotificacion,string OrdenServicio, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
                if (iNotificacionData.AgregaOrdenServicio(nIdNotificacion,OrdenServicio, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        public List<dto::clsPaqueteNotificacion> ObtenerPaquetes(int idUsuario, string ordenServicio, DateTime? fechaInicio, DateTime? fechaFin, int numeroPagina, int registrosPorPagina, ref string cError) {
            INotificacionData iNotificacionData = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return (iNotificacionData.ObtenerPaquetes(idUsuario, ordenServicio, fechaInicio, fechaFin, numeroPagina, registrosPorPagina, ref cError) ?? new List<dto::clsPaqueteNotificacion>()).ToList();
        }

        public dto::clsPaqueteNotificacion ObtenerPaquete(int id, ref string cError) {
            INotificacionData iNotificacionData = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return iNotificacionData.ObtenerPaquetePorId(id, ref cError);
        }

        public int ObtenerDetallePaqueteConteo(int idPaquete, ref string cError) {
            INotificacionData iNotificacionData = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return iNotificacionData.ObtenerDetallePaqueteCount(idPaquete, ref cError);
        }

        public List<dto::clsNotificacion> ObtenerDetallePaquete(int idPaquete, int numeroPagina, int registrosPorPagina, ref string cError) {
            INotificacionData iNotificacionData = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return (iNotificacionData.ObtenerDetallePaquete(idPaquete, numeroPagina, registrosPorPagina, ref cError) ?? new List<dto::clsNotificacion>()).ToList();
        }

        public bool ObservacionNotificacionBusiness(int nIdNotificacion, string ObservacionNotificacion, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
                if (iNotificacionData.ObservacionNotificacion(nIdNotificacion, ObservacionNotificacion, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        public bool AprobarNotificacion(int idNotificacion, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
                if (!iNotificacionData.AprobarNotificacion(idNotificacion, tra, ref cError) || !string.IsNullOrWhiteSpace(cError))
                {
                    tra.Rollback();
                    return false;
                }
                tra.Commit();
                return true;
            }
        }

        public bool AsociarCodigosGuiaNotificacion(int nIdPaqueteNotificacion, string cNombreArchivo, int nIdUsuario, ref string cError)
        {
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);

            List<dto::clsReporteCourier> lstReporte = iNotificacionData.CargarRegistrosCourier(cNombreArchivo, ref cError);
            if (lstReporte == null || !string.IsNullOrEmpty(cError)) return false;

            int cantidadNotificacionespaquete = iNotificacionData.ObtenerDetallePaqueteCount(nIdPaqueteNotificacion, ref cError);

            if (cantidadNotificacionespaquete == 0 || cantidadNotificacionespaquete <= 0 || !string.IsNullOrEmpty(cError)) return false;

            IList<dto::clsNotificacion> lstNotificacionActual = iNotificacionData.ObtenerDetallePaquete(nIdPaqueteNotificacion, 1, cantidadNotificacionespaquete, ref cError);

            if (lstNotificacionActual == null || !string.IsNullOrEmpty(cError)) return false;

            try
            {
                List<dto::clsNotificacion> eNotificacion = new List<dto::clsNotificacion>();
                foreach (dto::clsReporteCourier notReporte in lstReporte)
                {
                    if (!string.IsNullOrEmpty(notReporte.CContenido))
                    {
                        int nId = int.Parse(notReporte.CContenido);
                        foreach (dto::clsNotificacion not in lstNotificacionActual)
                        {
                            if (not.ID == nId) eNotificacion.Add(not);
                        }
                    }
                }

                if (eNotificacion == null || eNotificacion.ToList().Count == 0) cError = resx::Globalization.Errores.SinCoincidenciaNotificacionesCourier;
                else
                {                    
                    if (eNotificacion.ToList().Count < lstReporte.Count)
                    {
                        cError = resx::Globalization.Advertencia.SinCoincidenciaTotalNotificacionesCourier;
                        return false;
                    }
                    else
                    {
                        foreach (dto::clsNotificacion notificacion in eNotificacion)
                        {
                            dto::clsReporteCourier reporte = lstReporte.Find(y => !string.IsNullOrEmpty(y.CContenido) && int.Parse(y.CContenido) == notificacion.ID);
                            notificacion.cIdCodigoGuia = reporte.CEnvio;                           
                        }

                        using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
                        {
                            if (!iNotificacionData.AsociaCodigoGuiaNotificacion(eNotificacion, tra, ref cError) || !string.IsNullOrEmpty(cError))
                            {
                                tra.Rollback();
                                return false;
                            }
                            tra.Commit();
                        }
                    }
                }
            }
           catch (Exception ex)
            {
                cError = ex.Message;
            }

            if (!string.IsNullOrEmpty(cError)) return false;
            return true;
        }

        public bool ConfirmarEnvioNotificacion(int idPaqueteNotificacion, ref string cError) 
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
                if (!iNotificacionData.ConfirmarEnvioNotificacion(idPaqueteNotificacion, tra, ref cError) || !string.IsNullOrWhiteSpace(cError))
                {
                    tra.Rollback();
                    return false;
                }
                tra.Commit();
                return true;
            }
        }

        public List<dto::clsDatosCentroAtencion> ConsultaDatosCentroAtencion(int? idPais, int? idDepto, int? idMunicipio, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            return (iNotificacionData.ConsultaDatosCentroAtencion(idPais, idDepto, idMunicipio, numeroPagina, registrosPorPagina, ref cError) ?? new List<dto::clsDatosCentroAtencion>()).ToList();
        }

        public int ConsultaDatosCentroAtencionConteo(int? idPais, int? idDepto, int? idMunicipio, ref string cError)
        {
            INotificacionData iNotificacionData = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return iNotificacionData.ConsultaDatosCentroAtencionCount(idPais, idDepto, idMunicipio, ref cError);
        }

        public List<dto::clsDetalleDatosCentrosAtencion> DetalleDatosCentroAtencion(int nIdCentroAtencion, int nTipoCentroAtencion, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);
            return (iNotificacionData.ObtenerDetalleCentroAtencion(nIdCentroAtencion, nTipoCentroAtencion, numeroPagina, registrosPorPagina, ref cError) ?? new List<dto::clsDetalleDatosCentrosAtencion>()).ToList();
        }

        public int DetalleDatosCentroAtencionConteo(int nIdCentroAtencion, int nTipoCentroAtencion, ref string cError)
        {
            INotificacionData iNotificacionData = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return iNotificacionData.DetalleCentroAtencioncontador(nIdCentroAtencion,nTipoCentroAtencion,ref cError);
        }

        public IList<dto::clsHistoricoNotificacion> ObtenerHistorico(int idNotificacion) {
            var data = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return data.ObtenerHistorico(idNotificacion);
        }

        public IList<dto::clsHistoricoNotificacion> ObtenerHistoricoPaquete(int idPaqueteNotificacion) {
            var data = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return data.ObtenerHistoricoPaquete(idPaqueteNotificacion);
        }

        public bool ConfirmarPublicacionEdicto(int nIdNotificacion, int idEstado, int diasHabiles, string cObservacion, ref string cError)
        {
            IGestionFeriados iFechaData = (IGestionFeriados)u::Spring.GetService(Objetos.FeriadosData);
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);

            dto::clsNotificacion notificacion = iNotificacionData.ObtenerNotificacionPorId(nIdNotificacion, ref cError);
            if (notificacion.ID_PAIS != (int)ePaises.Colombia)
            {
                diasHabiles = diasHabiles + 3;
            }

            if (idEstado == (int)eEstadosNotificacion.EdictoPublicado)
            {
                //int TipoLey = iNotificacionData.ObtieneTipoLey(nIdNotificacion, ref cError);
                DateTime? fechaFin = null;
                if (string.IsNullOrEmpty(cError))// && TipoLey != null)
                {
                    //if (TipoLey == 1)
                    //{
                    //    fechaFin = iFechaData.CalcularDiasHabiles(DateTime.Now, diasHabiles, false, ref cError);
                    //}

                    //else if (TipoLey == 0)
                    //{
                        if (DateTime.Now.Hour < 10)
                        {
                            diasHabiles = diasHabiles - 1;
                            fechaFin = iFechaData.CalcularDiasHabiles(DateTime.Now, diasHabiles, false, ref cError);
                        }
                        else
                            fechaFin = iFechaData.CalcularDiasHabiles(DateTime.Now, diasHabiles, false, ref cError);
                    //}
                    using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
                    {
                        if (iNotificacionData.CambiarEstadoNotificacion(nIdNotificacion, idEstado, fechaFin, cObservacion, tra, ref cError) && string.IsNullOrEmpty(cError))
                        {
                            tra.Commit();
                            return true;
                        }
                        tra.Rollback();
                        return false;
                    }
                }
                else
                    return false;
            }
            else
            {
                cError = "El estado de la notificacion no es el apropiado para dar inicio a esta accion";
                return false;
            }
        
        }

        public bool ConfirmarDesfijarEdicto(int nIdNotificacion, int idEstado, string cObservacion, ref string cError)
        {
            IGestionFeriados iFechaData = (IGestionFeriados)u::Spring.GetService(Objetos.FeriadosData);
            INotificacionData iNotificacionData = (INotificacionData)u::Spring.GetService(Objetos.NotificacionData);

            dto::clsNotificacion notificacion = iNotificacionData.ObtenerNotificacionPorId(nIdNotificacion, ref cError);
            
            if (idEstado == (int)eEstadosNotificacion.NotificadoEdicto)
            {
                
                DateTime? fechaFin = null;
               
                fechaFin = iFechaData.CalcularDiasHabiles(DateTime.Now, 1, false, ref cError);
                   
                    using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
                    {
                        if (iNotificacionData.CambiarEstadoNotificacion(nIdNotificacion, idEstado, fechaFin, cObservacion, tra, ref cError) && string.IsNullOrEmpty(cError))
                        {
                            tra.Commit();
                            return true;
                        }
                        tra.Rollback();
                        return false;
                    }
                }
                else
                {
                    cError = "El estado de la notificacion no es el apropiado para dar inicio a esta accion";
                    return false;
                }
        }

        public IList<dto::clsEncargadoEntidad> ObtenerEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            var data = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return data.ObtenerEncargadosPorEntidad(nIdCentroAtencion, nTipoCentro, numeroPagina, registrosPorPagina, ref cError);
        }


        public int ContadorEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, ref string cError)
        {
            INotificacionData iNotificacionData = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return iNotificacionData.ContadorEncargadosPorEntidad(nIdCentroAtencion, nTipoCentro, ref cError);
        }


        public IList<dto.clsEstadosNotificacion> ObtenerEstadosDeNotificacion(ref string cError)
        {
            var data = u::Spring.GetService(Objetos.NotificacionData) as INotificacionData;
            return data.ObtenerEstadosDeNotificacion(ref cError);
        }
    }   
}