using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Ruv.Business.DTO.Notificacion;
using Ruv.Data.Notificacion.Contratos;
using Ruv.Infrastructure.Crosscutting.Utilities;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;
using System.Data.OracleClient;
using System.IO;

namespace Ruv.Data.Notificacion
{
    public class NotificacionData : INotificacionData
    {
        #region Private methods

        /// <summary>
        /// Actualiza el estado de una notificación
        /// </summary>
        /// <param name="notificacion">La notificación a actualizar</param>
        /// <param name="tra">Transacción</param>
        /// <param name="cError"></param>
        /// <returns></returns>
        private bool ActualizarEstadoNotificacion(clsNotificacion notificacion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdNotificacion,
                OracleType = OracleType.Number,
                Value = notificacion.ID,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.nIdEstadoNotificacion,
                OracleType = OracleType.Number,
                Value = notificacion.ID_ESTADONOTIFICACION,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.EstadoCourier,
                OracleType = OracleType.VarChar,
                Value = notificacion.ESTADOCOURIER,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.Fecha,
                OracleType = OracleType.DateTime,
                Value = notificacion.DESTADOCOURIER,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.FechaFinal, OracleType = OracleType.DateTime, Value = notificacion.FECHAFINAL, IsNullable = true, Direction = ParameterDirection.Input });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.ActualizaEstadoCourier, tra, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }

            if (!string.IsNullOrEmpty(cError)) return false;
            return true;
        }

        private bool AsociaCodigoGuiaNotificacion(clsNotificacion notificacion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.PINId,
                OracleType = OracleType.Number,
                Value = notificacion.ID,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.codigoguia,
                OracleType = OracleType.VarChar,
                Value = notificacion.cIdCodigoGuia,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.AsociarCodigosGuia, tra, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }

            if (!string.IsNullOrEmpty(cError)) return false;
            return true;
        }

        #endregion

        public IList<clsNotificacion> ObtenerNotificaciones(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas, string sortColumns, int startRow, int pageSize) {
            using (var d = new Dao()) {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdUsuario, OracleType = OracleType.Number, Value = idUsuario, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Declaracion, OracleType = OracleType.VarChar, Value = declaracion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.TipoDocumento, OracleType = OracleType.Number, Value = tipoDocumento, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Documento, OracleType = OracleType.VarChar, Value = documento, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.NombreDeclarante, OracleType = OracleType.VarChar, Value = nombreDeclarante, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.PaisNotificacion, OracleType = OracleType.Number, Value = paisNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.DepartamentoNotificacion, OracleType = OracleType.Number, Value = departamentoNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.MunicipioNotificacion, OracleType = OracleType.Number, Value = municipioNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.EntidadNotificacion, OracleType = OracleType.VarChar, Value = puntoNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.DireccionCitacion, OracleType = OracleType.VarChar, Value = direccionCitacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.SoloAsignaciones, OracleType = OracleType.Number, Value = soloAsignadas ? 1 : 0, Direction = ParameterDirection.Input });

                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.OrdenConsulta, OracleType = OracleType.VarChar, Value = sortColumns ?? string.Empty, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.NumeroPagina, OracleType = OracleType.Number, Value = startRow, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.RegistrosPorPagina, OracleType = OracleType.Number, Value = pageSize, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConsulta, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

                IDataReader dr = d.ExecuteReader(resx::Procedimientos.ConsultaNotificacionesPaginado);
                return ComplexDataAccessImplements.MapFromDataReaderI<clsNotificacion>(dr, true);
            }
        }

        public IList<clsNotificacion> ObtenerNotificaciones(ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

                IDataReader dr = null;
                try
                {
                    dr = d.ExecuteReader(resx::Procedimientos.ObtenerTodasNotificaciones, ref cError);
                    if (!(cError == null || cError == string.Empty)) return null;
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                    return null;
                }

                return ComplexDataAccessImplements.MapFromDataReaderI<clsNotificacion>(dr, true);
            }
        }

        public int ObtenerNotificacionesCantidad(int? idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? paisNotificacion, int? departamentoNotificacion, int? municipioNotificacion, string puntoNotificacion, string direccionCitacion, bool soloAsignadas) {
            using (var d = new Dao()) {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdUsuario, OracleType = OracleType.Number, Value = idUsuario, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Declaracion, OracleType = OracleType.VarChar, Value = declaracion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.TipoDocumento, OracleType = OracleType.Number, Value = tipoDocumento, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Documento, OracleType = OracleType.VarChar, Value = documento, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.NombreDeclarante, OracleType = OracleType.VarChar, Value = nombreDeclarante, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.PaisNotificacion, OracleType = OracleType.Number, Value = paisNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.DepartamentoNotificacion, OracleType = OracleType.Number, Value = departamentoNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.MunicipioNotificacion, OracleType = OracleType.Number, Value = municipioNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.EntidadNotificacion, OracleType = OracleType.VarChar, Value = puntoNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.DireccionCitacion, OracleType = OracleType.VarChar, Value = direccionCitacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.SoloAsignaciones, OracleType = OracleType.Number, Value = soloAsignadas ? 1 : 0, Direction = ParameterDirection.Input });

                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConteo, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

                d.ExecuteNonQuery(resx::Procedimientos.ConsultaCantidadNotificaciones, null);
                return Convert.ToInt32(d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConteo));
            }
        }

        public clsNotificacion ObtenerNotificacionPorId(int idNotificacion, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdNotificacion, OracleType = OracleType.Number, Value = idNotificacion, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConsulta, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

                IDataReader dr = null;
                try
                {
                    dr = d.ExecuteReader(resx::Procedimientos.ConsultaNotificacionPorId, ref cError);
                    if (!(cError == null || cError == string.Empty)) return null;
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                    return null;
                }

                return ComplexDataAccessImplements.MapFromDataReaderI<clsNotificacion>(dr, true).FirstOrDefault();
            }
        }

        public int? CrearPaqueteNotificacionDesdeFiltro(int idUsuario, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, string direccionCitacion, string ubicacionNotificacion, bool soloAsignadas, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdUsuario, OracleType = OracleType.Number, Value = idUsuario, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Declaracion, OracleType = OracleType.VarChar, Value = declaracion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.TipoDocumento, OracleType = OracleType.Number, Value = tipoDocumento, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Documento, OracleType = OracleType.VarChar, Value = documento, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.NombreDeclarante, OracleType = OracleType.VarChar, Value = nombreDeclarante, IsNullable = true, Direction = ParameterDirection.Input });
                //d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.PaisNotificacion, OracleType = OracleType.Number, Value = paisNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                //d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.DepartamentoNotificacion, OracleType = OracleType.Number, Value = departamentoNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                //d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.MunicipioNotificacion, OracleType = OracleType.Number, Value = municipioNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                //d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.EntidadNotificacion, OracleType = OracleType.Number, Value = entidadMunicipioNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.DireccionCitacion, OracleType = OracleType.VarChar, Value = direccionCitacion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.SoloAsignaciones, OracleType = OracleType.Number, Value = soloAsignadas ? 1 : 0, Direction = ParameterDirection.Input });

                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdPaqueteNotificacion, OracleType = OracleType.Number, Direction = ParameterDirection.Output });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConteo, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

                try
                {
                    d.ExecuteNonQuery(resx::Procedimientos.CrearPaqueteNotificacionDesdeFiltro, null, ref cError);
                    var registrosIncluidos = Convert.ToInt32(d.GetOutputParameter(resx::Parametros.ResultadoConteo));
                    if (registrosIncluidos <= 0) {
                        cError = "No se pudo generar el paquete, la consulta no retorna notificaciones válidas";
                        return null; 
                    }
                    return Convert.ToInt32(d.GetOutputParameter(resx::Parametros.IdPaqueteNotificacion));
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                }

                return null;

            }
        }

        public bool InsertaNotificacion(int nIdDeclaracion,DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDeclaracion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdDeclaracion,
                Direction = ParameterDirection.Input
            });           

            try {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.InsertaNotificacion, tra, ref cError);
            }
            catch (Exception ex) {
                cError = ex.Message;
                return false;
            }

            if (!(cError == null || cError == string.Empty)) return false;
            return true;
        }

        public bool ActualizarNotificacion(int idNotificacion, string direccion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdNotificacion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = idNotificacion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.DireccionEnvio,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = direccion,
                Direction = ParameterDirection.Input
            });
           

            bool respuesta = false;
            try
            {
                respuesta = d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ActualizarNotificacion, tra, ref cError);
                if (!(cError == null || cError == string.Empty))
                    respuesta = false;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }
            return respuesta;
        }

        public bool ActualizarPuntoNotificacion(clsNotificacion notificacion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdNotificacion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = notificacion.ID,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPais,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = notificacion.ID_PAIS,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDepartamento,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = notificacion.ID_DEPARTAMENTO,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdMunicipio,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = notificacion.ID_MUNICIPIO,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.DireccionEnvio,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = notificacion.DIRECCIONNOTIFICACION,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = "pi_IdPuntoAtencion",
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = notificacion.ID_PUNTOATENCION,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = "pi_IdDireccionTerritorial",
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = notificacion.ID_DIRECCIONTERRITORIAL,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            bool respuesta = false;
            try
            {
                respuesta = d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ActualizarPuntoNotificacion, tra, ref cError);
                if (!(cError == null || cError == string.Empty))
                    respuesta = false;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }
            return respuesta;
        }

        public bool ActualizarEstadoNotificacion(IEnumerable<clsNotificacion> eNotificacion, DbTransaction tra, ref string cError)
        {
            foreach (clsNotificacion notificacion in eNotificacion)
            {
                if (!ActualizarEstadoNotificacion(notificacion, tra, ref cError) || !string.IsNullOrEmpty(cError)) return false;
            }
            return true;
        }

        public clsNotificacionDetalle DetalleNotificaciones(int idNotificacion) {
            using (var d = new Dao()) {
                d.AddInputParameter(new OracleParameter { ParameterName = resx::Parametros.IdNotificacion, OracleType = OracleType.Number, Value = idNotificacion });
                d.AddOutputParameter(new OracleParameter { ParameterName = resx::Parametros.Resultado, OracleType = OracleType.Cursor });

                return ComplexDataAccessImplements.MapFromDataReaderI<clsNotificacionDetalle>(d.ExecuteReader(resx::Procedimientos.DetalleNotificacion), true).FirstOrDefault();
            }     
        }

        public int? CreaPaqueteNotificacion(int nIdUsuario,DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdUsuario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPaqueteNotificacion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Direction = ParameterDirection.Output
            });

            int? nIdPaqueteNotificacion = null;

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.CreaPaqueteNotificacion, tra, ref cError);

                if (!string.IsNullOrEmpty(cError)) return null;

                DbParameter dbParameter = d.LstParameter.FirstOrDefault(x => x.ParameterName == Infrastructure.Crosscutting.Resources.DB.Parametros.IdPaqueteNotificacion);
                nIdPaqueteNotificacion = dbParameter == null ? null : (int?)(decimal)dbParameter.Value;
            }

            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return nIdPaqueteNotificacion;
        }

        public bool InsertaIdPaquete(List<clsNotificacion> lstNotificacion, int? nIdNotificacion, DbTransaction tra, ref string cError)
        {
            foreach (clsNotificacion Nt in lstNotificacion)
            {
                Dao d = new Dao();
                d.RefreshParameters();

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                    {
                        ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdNotificacion,
                        OracleType = System.Data.OracleClient.OracleType.Number,
                        Value = Nt.ID,
                        Direction = ParameterDirection.Input
                    });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPaqueteNotificacion,
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = nIdNotificacion,
                    Direction = ParameterDirection.Input
                });

                try
                {
                    d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ActualizaEstadoEnviadoNotificacion, tra, ref cError);
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                    return false;
                }
                                
            }
            if (!(cError == null || cError == string.Empty)) return false;
            return true;
        }

        public bool InsertaIdPaquete(List<int> lstNotificacion, int? nIdPaqueteNotificacion, DbTransaction tra, ref string cError)
        {
            foreach (int Nid in lstNotificacion)
            {
                Dao d = new Dao();
                d.RefreshParameters();

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdNotificacion,
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = Nid,
                    Direction = ParameterDirection.Input
                });

                d.AddParameter(new System.Data.OracleClient.OracleParameter
                {
                    ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPaqueteNotificacionIn,
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = nIdPaqueteNotificacion,
                    Direction = ParameterDirection.Input
                });

                try
                {
                    d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ActualizaEstadoEnviadoNotificacion, tra, ref cError);
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                    return false;
                }

            }
            if (!(cError == null || cError == string.Empty)) return false;
            return true;
        }

        public bool SolicitaCorreccion(int nIdNotificacion, int nIdPuntoNotificacion,int nIdEstadoNotificacion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdNotificacion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdNotificacion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPuntoNotificacion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdPuntoNotificacion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.nIdEstadoNotificacion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdEstadoNotificacion,
                Direction = ParameterDirection.Input
            });


            bool respuesta = false;
            try
            {
                respuesta = d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.SolicitaCorrecionDireccion, tra, ref cError);
                if (!(cError == null || cError == string.Empty))
                    respuesta = false;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }
            return respuesta;
        }

        public bool SolicitarCorreccion(IEnumerable<clsNotificacion> eNotificacion, DbTransaction tra, ref string cError)
        {
            foreach (clsNotificacion notificacion in eNotificacion)
            {
                // TODO: jairovg - Quitar comentario o cambiar cuando esté la nueva columna del punto de notificación y la solicitud de la corrección.
                //if (!SolicitarCorreccion(notificacion.ID, notificacion.ID_PUNTONOTIFICACION, notificacion.ID_ESTADONOTIFICACION, tra, ref cError) || !string.IsNullOrEmpty(cError)) return false;
            }
            return true;
        }

        public List<clsReporteCourier> CargarRegistrosCourier(string cNombreArchivo, ref string cError)
        {
            try
            {
                byte[] byteArray = File.ReadAllBytes(cNombreArchivo);
                ExcelHelper er = new ExcelHelper();
                List<clsReporteCourier> lstCourier = er.ImportFromExcel<clsReporteCourier>(byteArray);
                return lstCourier;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }   
            //using (ExcelHelper er = new ExcelHelper())
            //{
            //    er.FilePath = cNombreArchivo;
            //    IDataReader dr = null;
            //    try
            //    {
            //        dr = er.DataReader(er.LstTables[0].ToString(), ref cError);
            //    }
            //    catch (Exception e)
            //    {
            //        cError = e.Message;
            //    }

            //    if (dr == null || !string.IsNullOrEmpty(cError)) return null;

            //    return ComplexDataAccessImplements.MapFromDataReaderI<clsReporteCourier>(dr, true);
            
        }

        public IList<clsNotificacion> ObtenerNotificacionesEntregadas(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, string sortColumns, int startRow, int pageSize, ref string cError)
        {
            using (Dao d = new Dao()) {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdUsuario, OracleType = OracleType.Number, Value = idUsuario, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.BusquedaGlobal, OracleType = OracleType.Number, Value = busquedaGlobal ? 1 : 0, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Declaracion, OracleType = OracleType.VarChar, Value = declaracion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.TipoDocumento, OracleType = OracleType.Number, Value = tipoDocumento, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Documento, OracleType = OracleType.VarChar, Value = documento, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.NombreDeclarante, OracleType = OracleType.VarChar, Value = nombreDeclarante, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.nIdEstadoNotificacion, OracleType = OracleType.VarChar, Value = estadoNotificacion, IsNullable = true, Direction = ParameterDirection.Input });
               
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.OrdenConsulta, OracleType = OracleType.VarChar, Value = sortColumns ?? string.Empty, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.NumeroPagina, OracleType = OracleType.Number, Value = startRow, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.RegistrosPorPagina, OracleType = OracleType.Number, Value = pageSize, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConsulta, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

                IDataReader dr = null;
                try
                {
                    dr = d.ExecuteReader(resx::Procedimientos.ConsultaNotificacionesEntregadas, ref cError);
                    if (!(cError == null || cError == string.Empty)) return null;
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                    return null;
                }

                return ComplexDataAccessImplements.MapFromDataReaderI<clsNotificacion>(dr, true);
            }
        }

        public int ObtenerNotificacionesEntregadasCantidad(int idUsuario, bool busquedaGlobal, string declaracion, int? tipoDocumento, string documento, string nombreDeclarante, int? estadoNotificacion, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdUsuario, OracleType = OracleType.Number, Value = idUsuario, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.BusquedaGlobal, OracleType = OracleType.Number, Value = busquedaGlobal ? 1 : 0, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Declaracion, OracleType = OracleType.VarChar, Value = declaracion, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.TipoDocumento, OracleType = OracleType.Number, Value = tipoDocumento, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Documento, OracleType = OracleType.VarChar, Value = documento, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.NombreDeclarante, OracleType = OracleType.VarChar, Value = nombreDeclarante, IsNullable = true, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.nIdEstadoNotificacion, OracleType = OracleType.VarChar, Value = estadoNotificacion, IsNullable = true, Direction = ParameterDirection.Input });

                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConteo, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

                int cantidad = 0;
                try
                {
                    d.ExecuteNonQuery(resx::Procedimientos.ConsultaNotificacionesEntregadasConteo, null, ref cError);
                    cantidad = Convert.ToInt32(d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConteo));
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                }

                return cantidad;
            }
        }

        public bool CierraNotificacion(int nIdNotificacion,DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdNotificacion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdNotificacion,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.CierraNotificacion, tra, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }

            if (!string.IsNullOrEmpty(cError)) return false;
            return true;
        }

        public bool CambiarEstadoNotificacion(int nIdNotificacion, int idEstado, DateTime? fechaFinal, string cObservacion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdNotificacion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdNotificacion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEstado,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = idEstado,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.FechaFin,
                OracleType = System.Data.OracleClient.OracleType.DateTime,
                Value = fechaFinal,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Observacion,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = cObservacion,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.CambiarEstadoNotificacion, tra, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }

            if (!string.IsNullOrEmpty(cError)) return false;
            return true;
        }

        /// <summary>
        /// Obtiene el total de paquetes de notificacionObtenerPaquetePorId generados, filtrados por usuario actor, orden de servicio y fecha de generación
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario actor</param>
        /// <param name="ordenServicio">Filtro de orden de servicio</param>
        /// <param name="fechaInicio">Filtro de fecha generacion</param>
        /// <param name="fechaFin">Filtro de fecha generacion</param>
        /// <returns>Total de registros de paquete de notificación</returns>
        public int ObtenerPaquetesConteo(int idUsuario, string ordenServicio, DateTime? fechaInicio, DateTime? fechaFin, ref string cError)
        {
            using (Dao d = new Dao()) { 
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdUsuario, OracleType = System.Data.OracleClient.OracleType.Number, Value = idUsuario, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.OrdenServicio, OracleType = System.Data.OracleClient.OracleType.VarChar, Value = ordenServicio, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.FechaInicio, OracleType = System.Data.OracleClient.OracleType.DateTime, Value = fechaInicio, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.FechaFin, OracleType = System.Data.OracleClient.OracleType.DateTime, Value = fechaFin, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConteo, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

                try {
                    d.ExecuteNonQuery(resx::Procedimientos.ObtenerPaquetesNotificacionConteo, null, ref cError);
                    return int.Parse(d.GetOutputParameter(resx::Parametros.ResultadoConteo).ToString());
                }
                catch (OracleException ex) {
                    cError = ex.Message;
                    return 0;
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de paquetes de notificacion generados, filtrados por usuario actor, orden de servicio y fecha de generación
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario actor</param>
        /// <param name="ordenServicio">Filtro de orden de servicio</param>
        /// <param name="fechaInicio">Filtro de fecha generacion</param>
        /// <param name="fechaFin">Filtro de fecha generacion</param>
        /// <returns>Colección de <see cref="clsPaqueteNotificacion"/></returns>
        public IList<clsPaqueteNotificacion> ObtenerPaquetes(int idUsuario, string ordenServicio, DateTime? fechaInicio, DateTime? fechaFin, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            using (Dao d = new Dao()) {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdUsuario, OracleType = System.Data.OracleClient.OracleType.Number, Value = idUsuario, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.OrdenServicio, OracleType = System.Data.OracleClient.OracleType.VarChar, Value = ordenServicio, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.FechaInicio, OracleType = System.Data.OracleClient.OracleType.DateTime, Value = fechaInicio, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.FechaFin, OracleType = System.Data.OracleClient.OracleType.DateTime, Value = fechaFin, Direction = ParameterDirection.Input, IsNullable = true });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.NumeroPagina, OracleType = System.Data.OracleClient.OracleType.Number, Value = numeroPagina, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.RegistrosPorPagina, OracleType = System.Data.OracleClient.OracleType.Number, Value = registrosPorPagina, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

                try {
                    return ComplexDataAccessImplements.MapFromDataReaderI<clsPaqueteNotificacion>(d.ExecuteReader(resx::Procedimientos.ObtenerPaquetesNotificacion, ref cError), true);
                }
                catch (OracleException ex) {
                    cError = ex.Message;
                    return null;
                }
            }
        }

        public bool AgregaOrdenServicio(int nIdNotificacion,string OrdenServicio, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPaqueteNotificacionIn,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdNotificacion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.OrdenServicio,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = OrdenServicio,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.AgregarOrdenServicio, tra, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }

            if (!string.IsNullOrEmpty(cError)) return false;
            return true;
        }

        /// <summary>
        /// Obtiene el paquete de notificacion correspondiente a un id
        /// </summary>
        /// <param name="id">Identificador del paquete</param>
        /// <returns><see cref="clsPaqueteNotificacion"/> correspondiente al identificador</returns>
        public clsPaqueteNotificacion ObtenerPaquetePorId(int id, ref string cError) {
            using (Dao d = new Dao()) {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Id, OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = id });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

                try {
                    return ComplexDataAccessImplements.MapFromDataReaderI<clsPaqueteNotificacion>(d.ExecuteReader(resx::Procedimientos.ObtenerPaqueteNotificacion, ref cError), true).FirstOrDefault();
                }
                catch (OracleException ex) {
                    cError = ex.Message;
                    return null;
                }
            }
        }

        /// <summary>
        /// Obtiene el total de notificaciones de un paquete
        /// </summary>
        /// <param name="idPaqueteNotificacion">Identificador del paquete</param>
        /// <returns>Total de registros de notificacion del paquete</returns>
        public int ObtenerDetallePaqueteCount(int idPaqueteNotificacion, ref string cError) {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdPaqueteNotificacionIn, OracleType = System.Data.OracleClient.OracleType.Number, Value = idPaqueteNotificacion, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConteo, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

                try
                {
                    d.ExecuteNonQuery(resx::Procedimientos.ObtenerDetallePaqueteNotificacionConteo, null, ref cError);
                    return int.Parse(d.GetOutputParameter(resx::Parametros.ResultadoConteo).ToString());
                }
                catch (OracleException ex)
                {
                    cError = ex.Message;
                    return 0;
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de notificaciones de un paquete
        /// </summary>
        /// <param name="idPaqueteNotificacion">Identificador del paquete</param>
        /// <returns>Coleccion de <see cref="clsNotificacion"/></returns>
        public IList<clsNotificacion> ObtenerDetallePaquete(int idPaqueteNotificacion, int numeroPagina, int registrosPorPagina, ref string cError) {
            using (Dao d = new Dao()) {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdPaqueteNotificacionIn, OracleType = System.Data.OracleClient.OracleType.Number, Value = idPaqueteNotificacion, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.NumeroPagina, OracleType = System.Data.OracleClient.OracleType.Number, Value = numeroPagina, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.RegistrosPorPagina, OracleType = System.Data.OracleClient.OracleType.Number, Value = registrosPorPagina, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

                try
                {
                    return ComplexDataAccessImplements.MapFromDataReaderI<clsNotificacion>(d.ExecuteReader(resx::Procedimientos.ObtenerDetallePaqueteNotificacion, ref cError), true);
                }
                catch (OracleException ex)
                {
                    cError = ex.Message;
                    return null;
                }
            }
        }

        public bool ObservacionNotificacion(int nIdNotificacion, string ObservacionNotificacion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdNotificacion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdNotificacion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Observacion,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = ObservacionNotificacion,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObservacionNotificacion, tra, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }

            if (!string.IsNullOrEmpty(cError)) return false;
            return true;
        }

        public bool AprobarNotificacion(int idNotificacion, DbTransaction tra, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdNotificacion, OracleType = OracleType.Number, Value = idNotificacion, Direction = ParameterDirection.Input });

                bool fueAprobado = false;
                try
                {
                    fueAprobado = d.ExecuteNonQuery(resx::Procedimientos.AprobarNotificacion, tra, ref cError);
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                    return false;
                }

                if (!fueAprobado || !string.IsNullOrEmpty(cError)) return false;
                return true;
            }
        }        

        public bool AsociaCodigoGuiaNotificacion(IEnumerable<clsNotificacion> eNotificacion, DbTransaction tra, ref string cError)
        {
            foreach (clsNotificacion notificacion in eNotificacion)
            {
                if (!AsociaCodigoGuiaNotificacion(notificacion, tra, ref cError) || !string.IsNullOrEmpty(cError)) return false;
            }
            return true;
        }


        public bool ConfirmarEnvioNotificacion(int idPaqueteNotificacion, DbTransaction tra, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.IdPaqueteNotificacion, OracleType = OracleType.Number, Value = idPaqueteNotificacion, Direction = ParameterDirection.Input });

                bool fueAprobado = false;
                try
                {
                    fueAprobado = d.ExecuteNonQuery(resx::Procedimientos.ConfirmarEnvioNotificacion, tra, ref cError);
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                    return false;
                }

                if (!fueAprobado || !string.IsNullOrEmpty(cError)) return false;
                return true;
            }
        }

        public IList<clsDatosCentroAtencion> ConsultaDatosCentroAtencion(int? idPais, int? idDepto, int? idMunicipio, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdPais,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = idPais,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdDepartamento,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = idDepto,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdMunicipio,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = idMunicipio,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter 
            { 
                ParameterName = resx::Parametros.NumeroPagina, 
                OracleType = System.Data.OracleClient.OracleType.Number, 
                Value = numeroPagina, 
                Direction = ParameterDirection.Input 
            });

            d.AddParameter(new OracleParameter 
            { 
                ParameterName = resx::Parametros.RegistrosPorPagina, 
                OracleType = System.Data.OracleClient.OracleType.Number, 
                Value = registrosPorPagina, 
                Direction = ParameterDirection.Input 
            });

            d.AddParameter(new OracleParameter 
            { 
                ParameterName = resx::Parametros.Resultado, 
                OracleType = OracleType.Cursor, 
                Direction = ParameterDirection.Output 
            });

            try
            {
                return ComplexDataAccessImplements.MapFromDataReaderI<clsDatosCentroAtencion>(d.ExecuteReader(resx::Procedimientos.ConsultaNotificacionesCentroAtencion, ref cError), true);
            }
            catch (OracleException ex)
            {
                cError = ex.Message;
                return null;
            }
        }

        public int ConsultaDatosCentroAtencionCount(int? idPais, int? idDepto, int? idMunicipio, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdPais,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = idPais,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdDepartamento,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = idDepto,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdMunicipio,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = idMunicipio,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter 
            { 
                ParameterName = resx::Parametros.ResultadoConteo, 
                OracleType = OracleType.Number, 
                Direction = ParameterDirection.Output 
            });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.ConsultaNotificacionesCentroAtencionCount, null, ref cError);
                return int.Parse(d.GetOutputParameter(resx::Parametros.ResultadoConteo).ToString());
            }
            catch (OracleException ex)
            {
                cError = ex.Message;
                return 0;
            }
        }

        public IList<clsDetalleDatosCentrosAtencion> ObtenerDetalleCentroAtencion(int nIdCentroAtencion,int nTipoCentro, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

                d.AddParameter(new OracleParameter { 
                    ParameterName = resx::Parametros.IdCentroAtencion, 
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = nIdCentroAtencion, 
                    Direction = ParameterDirection.Input 
                });
                d.AddParameter(new OracleParameter
                {
                    ParameterName = resx::Parametros.TipoCentroAtencion,
                    OracleType = System.Data.OracleClient.OracleType.Number,
                    Value = nTipoCentro,
                    Direction = ParameterDirection.Input
                });
                d.AddParameter(new OracleParameter { 
                    ParameterName = resx::Parametros.NumeroPagina, 
                    OracleType = System.Data.OracleClient.OracleType.Number, 
                    Value = numeroPagina, Direction = ParameterDirection.Input 
                });
                d.AddParameter(new OracleParameter { 
                    ParameterName = resx::Parametros.RegistrosPorPagina, 
                    OracleType = System.Data.OracleClient.OracleType.Number, 
                    Value = registrosPorPagina, 
                    Direction = ParameterDirection.Input 
                });
                d.AddParameter(new OracleParameter { 
                    ParameterName = resx::Parametros.Resultado, 
                    OracleType = OracleType.Cursor, 
                    Direction = ParameterDirection.Output 
                });

                try
                {
                    return ComplexDataAccessImplements.MapFromDataReaderI<clsDetalleDatosCentrosAtencion>(d.ExecuteReader(resx::Procedimientos.DetalleCentroAtencion, ref cError), true);
                }
                catch (OracleException ex)
                {
                    cError = ex.Message;
                    return null;
                }            
        }

        public int DetalleCentroAtencioncontador(int nIdCentroAtencion, int nTipoCentro,ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdCentroAtencion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdCentroAtencion,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.TipoCentroAtencion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nTipoCentro,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.ResultadoConteo,
                OracleType = OracleType.Number,
                Direction = ParameterDirection.Output
            });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.DetalleCentroAtencioncontador, null, ref cError);
                return int.Parse(d.GetOutputParameter(resx::Parametros.ResultadoConteo).ToString());
            }
            catch (OracleException ex)
            {
                cError = ex.Message;
                return 0;
            }
        }

        public IList<clsHistoricoNotificacion> ObtenerHistorico(int idNotificacion) {
            using (var d = new Dao()) {
                d.AddInputParameter(new OracleParameter { ParameterName = resx::Parametros.IdNotificacion, OracleType = OracleType.Number, Value = idNotificacion });
                d.AddOutputParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConsulta, OracleType = OracleType.Cursor });

                return ComplexDataAccessImplements.MapFromDataReaderI<clsHistoricoNotificacion>(d.ExecuteReader(resx::Procedimientos.ObtenerHistoricoNotificacion), true);
            }
        }

        public IList<clsHistoricoNotificacion> ObtenerHistoricoPaquete(int idPaqueteNotificacion) {
            using (var d = new Dao()) {
                d.AddInputParameter(new OracleParameter { ParameterName = resx::Parametros.IdPaqueteNotificacionIn, OracleType = OracleType.Number, Value = idPaqueteNotificacion });
                d.AddOutputParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConsulta, OracleType = OracleType.Cursor });

                return ComplexDataAccessImplements.MapFromDataReaderI<clsHistoricoNotificacion>(d.ExecuteReader(resx::Procedimientos.ObtenerHistoricoPaqueteNotificacion), true);
            }
        }

        public int ObtieneTipoLey(int nIdNotficacion, ref string cError)
        {
            Dao d = new Dao();

            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdNotificacion,
                OracleType = OracleType.Number,
                Value = nIdNotficacion,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.ResultadoConteo,
                OracleType = OracleType.Number,
                Direction = ParameterDirection.Output
            });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.EvaluaLeyViejaoNueva, null, ref cError);
                return int.Parse(d.GetOutputParameter(resx::Parametros.ResultadoConteo).ToString());
            }
            catch (OracleException ex)
            {
                cError = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Consulta una lista de encargados pertenecientes a una entidad
        /// </summary>
        /// <param name="nIdEntidad">Id de la Entidad que se va a consultar</param>
        /// <param name="cError">Mensaje de error, inconvenientes en el SP</param>
        /// <returns>Lista de encargados</returns>
        /// <remarks>ivan.suarez@globant.com, 03/Julio/2013 </remarks>
        public IList<clsEncargadoEntidad> ObtenerEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdCentroAtencion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdCentroAtencion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.TipoCentroAtencion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nTipoCentro,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter { 
                    ParameterName = resx::Parametros.NumeroPagina, 
                    OracleType = System.Data.OracleClient.OracleType.Number, 
                    Value = numeroPagina, Direction = ParameterDirection.Input 
            });
                
            d.AddParameter(new OracleParameter { 
                ParameterName = resx::Parametros.RegistrosPorPagina, 
                OracleType = System.Data.OracleClient.OracleType.Number, 
                Value = registrosPorPagina, 
                Direction = ParameterDirection.Input 
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.Resultado,
                OracleType = OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            try
            {
                return ComplexDataAccessImplements.MapFromDataReaderI<clsEncargadoEntidad>(d.ExecuteReader(resx::Procedimientos.getEncargadosPorEntidad, ref cError), true);
            }
            catch (OracleException ex)
            {
                cError = ex.Message;
                return null;
            }            
        }

        public int ContadorEncargadosPorEntidad(int nIdCentroAtencion, int nTipoCentro, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdCentroAtencion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdCentroAtencion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.TipoCentroAtencion,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nTipoCentro,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.ResultadoConteo,
                OracleType = OracleType.Number,
                Direction = ParameterDirection.Output
            });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.CountEncargadosPorEntidad, null, ref cError);
                return int.Parse(d.GetOutputParameter(resx::Parametros.ResultadoConteo).ToString());
            }
            catch (OracleException ex)
            {
                cError = ex.Message;
                return 0;
            }
        }

        public IList<clsEstadosNotificacion> ObtenerEstadosDeNotificacion(ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.Resultado,
                OracleType = OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            try
            {
                return ComplexDataAccessImplements.MapFromDataReaderI<clsEstadosNotificacion>(d.ExecuteReader(resx::Procedimientos.ObtenerEstadosDeNotificacion, ref cError), true);
            }
            catch (OracleException ex)
            {
                cError = ex.Message;
                return null;
            }
        }
    }
}
