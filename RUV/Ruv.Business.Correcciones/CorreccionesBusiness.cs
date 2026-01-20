using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.Correcciones.Contratos;
using dto = Ruv.Business.DTO.Correcciones;
using System.Data.Common;
using Ruv.Data.Correcciones.Contratos;
using entidad = Ruv.Infrastructure.Crosscutting.Common.Entidades.Correcciones;
using u = Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using Ruv.Business.DTO.Correcciones;
using not = Ruv.Data.Notificacion.Contratos;
using Ruv.Infrastructure.Crosscutting.Utilities;

namespace Ruv.Business.Correcciones
{
    public class CorreccionesBusiness : ICorreccionesBusiness
    {
        #region Public methods

        #region Services implementation

        public bool SolicitarCorreccion(int IdRegPersona, int idUsuarioSolicita, IList<dto::clsCorreccion> correcciones, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                Data.Correcciones.Contratos.ICorreccionesData iCorreccionesData = (Data.Correcciones.Contratos.ICorreccionesData)new Data.Correcciones.CorreccionesData();
                if (iCorreccionesData.SolicitarCorreccion(IdRegPersona, idUsuarioSolicita, correcciones, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                   
                        tra.Commit();
                        return true;                  
                }
                tra.Rollback();
                return false;
            }
        }

        public int SolicitarCorreccionOut(int IdRegPersona, int idUsuarioSolicita, IList<dto::clsCorreccion> correcciones, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                Data.Correcciones.Contratos.ICorreccionesData iCorreccionesData = (Data.Correcciones.Contratos.ICorreccionesData)new Data.Correcciones.CorreccionesData();
                int idCorreccion = iCorreccionesData.SolicitarCorreccionOut(IdRegPersona, idUsuarioSolicita, correcciones, tra, ref cError);
                if (idCorreccion != 0 && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return idCorreccion;
                }
                tra.Rollback();
                return 0;
            }
        }

        public List<entidad::clsCargaDatosCorreccion> CargaDatosCorreccion(int IdRegPersona, ref string cError)
        {
            ICorreccionesData iCorreccionesData = (ICorreccionesData)new Data.Correcciones.CorreccionesData();
            List<dto::clsCargaDatosCorreccion> lstCargaDatosCorreccion = iCorreccionesData.CargaDatosCorreccion(IdRegPersona, ref cError);
            List<entidad::clsCargaDatosCorreccion> lstDatosCorreccion = new List<entidad::clsCargaDatosCorreccion>();
            if (lstCargaDatosCorreccion != null)
            {
                lstDatosCorreccion = new List<entidad::clsCargaDatosCorreccion>();
                foreach (var x in lstCargaDatosCorreccion)
                {
                    entidad.clsCargaDatosCorreccion datosCorr = new entidad.clsCargaDatosCorreccion();
                    datosCorr.CPrimerNombre = x.CPrimerNombre;
                    datosCorr.CSegundoNombre = x.CSegundoNombre;
                    datosCorr.CPrimerApellido = x.CPrimerApellido;
                    datosCorr.CSegundoApellido = x.CSegundoApellido;
                    datosCorr.NTipoDocumento = x.NTipoDocumento;
                    datosCorr.CNumeroDocumento = x.CNumeroDocumento;
                    datosCorr.DNacimiento = x.DNacimiento;
                    datosCorr.NGenero = x.NGenero;
                    datosCorr.NEtnia = x.NEtnia;
                    datosCorr.CDireccion = x.CDireccion;
                    datosCorr.CTelefono = x.CTelefono;
                    datosCorr.CCorreo = x.CCorreo;
                    if (!string.IsNullOrEmpty(x.CDiscapacidades))
                    {
                        datosCorr.LstDiscapacidad = new List<int>();
                        x.CDiscapacidades.Split('|').ToList().ForEach(z =>
                        {
                            if (z != null)
                                datosCorr.LstDiscapacidad.Add(Convert.ToInt32(z));
                        });
                    }
                    lstDatosCorreccion.Add(datosCorr);
                }

            }

            return lstDatosCorreccion;
        }

        public entidad::clsCargaDatosCorreccion ConsultarCorreccion(int idCorreccion, ref string cError)
        {
            Data.Correcciones.Contratos.ICorreccionesData iCorreccionesData = (Data.Correcciones.Contratos.ICorreccionesData)new Data.Correcciones.CorreccionesData();
            dto::clsCargaDatosCorreccion dtoDatosCorreccion = iCorreccionesData.ConsultarCorreccion(idCorreccion, ref cError);
            entidad::clsCargaDatosCorreccion entidadDatosCorreccion = null;
            entidadDatosCorreccion = new entidad.clsCargaDatosCorreccion()
            {
                CCorreo = dtoDatosCorreccion.CCorreo,
                CDireccion = dtoDatosCorreccion.CDireccion,
                CPrimerApellido = dtoDatosCorreccion.CPrimerApellido,
                CPrimerNombre = dtoDatosCorreccion.CPrimerNombre,
                CSegundoApellido = dtoDatosCorreccion.CSegundoApellido,
                CSegundoNombre = dtoDatosCorreccion.CSegundoNombre,
                CTelefono = dtoDatosCorreccion.CTelefono,
                DNacimiento = dtoDatosCorreccion.DNacimiento,
                LstDiscapacidad = dtoDatosCorreccion.CDiscapacidades == null ? null : dtoDatosCorreccion.CDiscapacidades.Split('|').ToList().Select(x => Int32.Parse(x)).ToList(),
                NEtnia = dtoDatosCorreccion.NEtnia,
                NSubetnia = dtoDatosCorreccion.NSubEtnia,
                NGenero = dtoDatosCorreccion.NGenero,
                CNumeroDocumento = dtoDatosCorreccion.CNumeroDocumento,
                NTipoDocumento = dtoDatosCorreccion.NTipoDocumento

            };

            return entidadDatosCorreccion;
        }

        public bool RechazarCorreccion(int idCorreccion, int idUsuarioRechaza, string observaciones, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                Data.Correcciones.Contratos.ICorreccionesData iCorreccionesData = (Data.Correcciones.Contratos.ICorreccionesData)new Data.Correcciones.CorreccionesData();
                if (iCorreccionesData.RechazarCorreccion(idCorreccion, idUsuarioRechaza, observaciones, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    var InfoCorreccion = iCorreccionesData.CargaInformacionCorreccion(idCorreccion, ref cError);
                    var datosCorreccion = iCorreccionesData.CargaDatosCorreccion(InfoCorreccion.nIdRegPersona, ref cError).FirstOrDefault();

                    if (string.IsNullOrEmpty(cError) && datosCorreccion != null) {
                        var descripcionRechazo = string.Empty;
                        var declarante = datosCorreccion.CPrimerNombre
                            + (!string.IsNullOrEmpty(datosCorreccion.CSegundoNombre) ? " " + datosCorreccion.CSegundoNombre : string.Empty)
                            + (!string.IsNullOrEmpty(datosCorreccion.CPrimerApellido) ? " " + datosCorreccion.CPrimerApellido : string.Empty)
                            + (!string.IsNullOrEmpty(datosCorreccion.CSegundoApellido) ? " " + datosCorreccion.CSegundoApellido : string.Empty);
                        descripcionRechazo += string.Format("La solicitud de corrección para <b>{0}</b> (con documento <b>{1}</b>), creada el {2}, fue rechazada. <br /><br />", declarante, datosCorreccion.CNumeroDocumento, InfoCorreccion.dFechaSolicitud.ToString("dd/MM/yyyy"));
                        descripcionRechazo += string.Format("Las observaciones del líder de correcciones son: <br /> <i>{0}</i>", observaciones);

                        not::INotificacionInternaData iInsertaNotificacionInterna = (not::INotificacionInternaData)Spring.GetService(Objetos.NotificacionInternaData);
                        if (iInsertaNotificacionInterna.GenerarNotificacionInterna(idCorreccion, idUsuarioRechaza, 0, InfoCorreccion.nIdUsuarioSolicitante, string.Format("La solicitud de correccion {0} fue rechazada", idCorreccion), descripcionRechazo, tra, ref cError))
                        {
                            tra.Commit();
                            return true;
                        }      
                    }
                }
                tra.Rollback();
                return false;
            }
        }

        public int ConsultarEstadoDeclaracionConteo(DTO.Reporteador.clsDeclarante declarante, ref string cError)
        {
            ICorreccionesData iCorreccionesData = (ICorreccionesData)u::Spring.GetService(Objetos.CorreccionesData);
            return iCorreccionesData.ConsultarEstadoDeclaracionConteo(declarante, ref cError);
        }

        public List<DTO.Reporteador.clsDeclarante> ConsultarEstadoDeclaracion(DTO.Reporteador.clsDeclarante declarante, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            ICorreccionesData iCorreccionesData = (ICorreccionesData)u::Spring.GetService(Objetos.CorreccionesData);
            return iCorreccionesData.ConsultarEstadoDeclaracion(declarante, numeroPagina, registrosPorPagina, ref cError);
        }

        public bool AprobarCorreccion(int idCorreccion, int idUsuarioAprueba, string observaciones, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                Data.Correcciones.Contratos.ICorreccionesData iCorreccionesData = (Data.Correcciones.Contratos.ICorreccionesData)new Data.Correcciones.CorreccionesData();
                if (iCorreccionesData.AprobarCorreccion(idCorreccion, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    var InfoCorreccion = iCorreccionesData.CargaInformacionCorreccion(idCorreccion, ref cError);
                    var datosCorreccion = iCorreccionesData.CargaDatosCorreccion(InfoCorreccion.nIdRegPersona, ref cError).FirstOrDefault();

                    if (string.IsNullOrEmpty(cError) && datosCorreccion != null)
                    {
                        var descripcionRechazo = string.Empty;
                        var declarante = datosCorreccion.CPrimerNombre
                            + (!string.IsNullOrEmpty(datosCorreccion.CSegundoNombre) ? " " + datosCorreccion.CSegundoNombre : string.Empty)
                            + (!string.IsNullOrEmpty(datosCorreccion.CPrimerApellido) ? " " + datosCorreccion.CPrimerApellido : string.Empty)
                            + (!string.IsNullOrEmpty(datosCorreccion.CSegundoApellido) ? " " + datosCorreccion.CSegundoApellido : string.Empty);
                        descripcionRechazo += string.Format("La solicitud de corrección para <b>{0}</b> (con documento <b>{1}</b>), creada el {2}, fue aprobada. <br /><br />", declarante, datosCorreccion.CNumeroDocumento, InfoCorreccion.dFechaSolicitud.ToString("dd/MM/yyyy"));
                        descripcionRechazo += string.Format("Las observaciones del líder de correcciones son: <br /> <i>{0}</i>", observaciones);

                        not::INotificacionInternaData iInsertaNotificacionInterna = (not::INotificacionInternaData)Spring.GetService(Objetos.NotificacionInternaData);
                        if (iInsertaNotificacionInterna.GenerarNotificacionInterna(idCorreccion, idUsuarioAprueba, 0, InfoCorreccion.nIdUsuarioSolicitante, string.Format("La solicitud de correccion {0} fue aprobada", idCorreccion), descripcionRechazo, tra, ref cError))
                        {
                            tra.Commit();
                            return true;
                        }
                    }
                }
                tra.Rollback();
                return false;
            }
        
        }

        public IList<entidad::clsCorreccion> ConsultarCamposCorreccion(int idCorreccion, ref string cError)
        {
            Data.Correcciones.Contratos.ICorreccionesData iCorreccionesData = (Data.Correcciones.Contratos.ICorreccionesData)new Data.Correcciones.CorreccionesData();
            IList<dto::clsCorreccion> listDtoCamposCorreccion = iCorreccionesData.ConsultarCamposCorreccion(idCorreccion, ref cError);
            List<entidad::clsCorreccion> lstClsCamposCorreccion = new List<entidad::clsCorreccion>();
            if (listDtoCamposCorreccion != null)
            {
                foreach (var x in listDtoCamposCorreccion)
                {
                    entidad.clsCorreccion campoCorreccion = new entidad.clsCorreccion()
                    {
                        Campo = x.Campo,
                        Valor = x.Valor
                    };

                    lstClsCamposCorreccion.Add(campoCorreccion);
                }

            }

            return lstClsCamposCorreccion;
        }

        public string ObtienenombreSubEtnia(int nIdSubetnia, ref string cError)
        {
            ICorreccionesData iCorreccionesData = (ICorreccionesData)u::Spring.GetService(Objetos.CorreccionesData);
            return iCorreccionesData.ObtieneNombreSubEtnia(nIdSubetnia, ref cError);
        }

        #endregion

        #endregion
    }
}
