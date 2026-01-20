using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using Ruv.Business.DTO.General;
using Ruv.Business.DTO.Notificacion;
using Ruv.Infrastructure.Crosscutting.Resources.DB;

namespace Ruv.Data.General
{
    public class entGeografia
    {
        // sp_ObtenerGeografiaCompleta
        // en PKG_COMMON
        public List<clsGeografia> ObtenerGeografiaCompleta(ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                return ComplexDataAccessImplements.MapFromDataReaderI<clsGeografia>(d.ExecuteReader("PKG_COMMON.SP_OBTENERGEOGRAFIACOMPLETA", ref cError), true);
            }
        }

        public List<clsGeografia> ObtenerPaises(ref string cError) {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                return ComplexDataAccessImplements.MapFromDataReaderI<clsGeografia>(d.ExecuteReader("PKG_COMMON.SP_OBTENERPAISES", ref cError), true);
            }
        }

        public List<clsGeografia> ObtenerDepartamentosPorPais(int idPais, ref string cError) {
            using (Dao d = new Dao()) {
                d.AddParameter(new OracleParameter { ParameterName = Parametros.IdPais, OracleType = OracleType.Number, Value = idPais, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                return ComplexDataAccessImplements.MapFromDataReaderI<clsGeografia>(d.ExecuteReader("PKG_COMMON.SP_OBTENERDEPARTAMENTOSPORPAIS", ref cError), true);
            }
        }

        public List<clsGeografia> ObtenerMunicipiosPorDepartamento(int idDepartamento, ref string cError) {
            using (Dao d = new Dao()) {
                d.AddParameter(new OracleParameter { ParameterName = Parametros.IdDepartamento, OracleType = OracleType.Number, Value = idDepartamento, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                return ComplexDataAccessImplements.MapFromDataReaderI<clsGeografia>(d.ExecuteReader("PKG_COMMON.SP_OBTENERMUNIPORDEPARTAMENTO", ref cError), true);
            }
        }

        public List<clsEntidadMunicipioNotificacion> ObtenerEntidadesPorMunicipio(int idMunicipio, ref string cError) { 
            using (Dao d = new Dao()) {
                d.AddParameter(new OracleParameter { ParameterName = Parametros.IdMunicipio, OracleType = OracleType.Number, Value = idMunicipio, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                return ComplexDataAccessImplements.MapFromDataReaderI<clsEntidadMunicipioNotificacion>(d.ExecuteReader("PKG_COMMON.sp_ObtenerEntidadesPorMunicip", ref cError), true);
            }
        }

        public List<clsPuntoAtencionDireccionTerritorial> ObtenerPuntosAtencionyDTPorMunicipio(int idMunicipio) { 
            using (Dao d = new Dao()) {
                d.AddParameter(new OracleParameter { ParameterName = Parametros.IdMunicipio, OracleType = OracleType.Number, Value = idMunicipio, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                return ComplexDataAccessImplements.MapFromDataReaderI<clsPuntoAtencionDireccionTerritorial>(d.ExecuteReader("PKG_COMMON.sp_ObtenerPAyDTPorMunicipio"), true);
            }
        }

        /// <summary>
        /// Consulta que retorna la dirección del punto de notificación
        /// </summary>
        /// <param name="idPuntoNotificacion">Id del punto de notificación</param>
        /// <param name="tipoPunto">PuntoAtencion = 0, DireccionTerritorial = 1, Personeria = 2</param>
        /// <returns>Dirección del punto de notificación</returns>
        /// <remarks>ivan.suarez@globant.com 12/09/2013</remarks>
        public string ObtenerDireccionPuntoNotificacion(int idPuntoNotificacion, int tipoPunto) 
        {
            using (Dao d = new Dao())
            {
                d.AddInputParameter(new OracleParameter { ParameterName = Parametros.IdPuntoNotificacion, OracleType = OracleType.Number, Value = idPuntoNotificacion });
                d.AddInputParameter(new OracleParameter { ParameterName = Parametros.TipoPuntoNotificacion, OracleType = OracleType.Number, Value = tipoPunto });
                d.AddOutputParameter(new OracleParameter { ParameterName = Parametros.Direccion, OracleType = OracleType.VarChar, Size = 100 });
                d.ExecuteNonQuery(Procedimientos.ObtenerDireccionPuntoNotificacion, null);
                string direccion = d.GetOutputParameter(Parametros.Direccion).ToString();
                return direccion;
            }
        }

        /// <summary>
        /// Procedimiento que actualiza la dirección del punto de notificación
        /// </summary>
        /// <param name="idPuntoNotificacion">Id del punto de notificación</param>
        /// <param name="tipoPunto">PuntoAtencion = 0, DireccionTerritorial = 1, Personeria = 2</param>
        /// <param name="direccion">Nueva dirección del punto de notificación</param>
        /// <remarks>ivan.suarez@globant.com 12/09/2013</remarks>
        public bool ActualizarDireccionPuntoNotificacion(int idPuntoNotificacion, int tipoPunto, string direccion, DbTransaction transaction, ref string cError)
        {
            bool respuesta = false;
            using (Dao d = new Dao())
            {
                d.AddInputParameter(new OracleParameter { ParameterName = Parametros.IdPuntoNotificacion, OracleType = OracleType.Number, Value = idPuntoNotificacion });
                d.AddInputParameter(new OracleParameter { ParameterName = Parametros.TipoPuntoNotificacion, OracleType = OracleType.Number, Value = tipoPunto });
                d.AddInputParameter(new OracleParameter { ParameterName = Parametros.Direccion, OracleType = OracleType.VarChar, Size = 100, Value = direccion });

                respuesta = d.ExecuteNonQuery(Procedimientos.ActualizarDireccionPuntoNotificacion, transaction, ref cError);
                if (!(cError == null || cError == string.Empty))
                    respuesta = false;

                return respuesta;
            }
        }

    }
}
