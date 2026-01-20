using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;
using Ruv.Business.DTO.Radicacion;
using System.Data.Common;

namespace Ruv.Data.Radicacion
{
    public class LiderRadicacion : Contratos.ILiderRadicacion
    {
        #region Public methods

        #region Services implementation

        public clsRadicacion GetRadicacion(long nIdDeclaracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdDeclaracion,
                OracleType = OracleType.Number,
                Value = nIdDeclaracion,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.OutRadicacion,
                OracleType = OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(resx::Procedimientos.ObtenerRadicacionIdDeclaracion, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsRadicacion> lstRadicacion = ComplexDataAccessImplements.MapFromDataReaderI<clsRadicacion>(dr, true);
            if (lstRadicacion != null && lstRadicacion.Count > 0) return lstRadicacion.FirstOrDefault();
            return null;
        }

        public clsRadicacion GetRadicacion(long nIdDeclaracion, string cNumeroFormulario, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.NumeroFormulario,
                OracleType = OracleType.VarChar,
                Value = cNumeroFormulario,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdDeclaracion,
                OracleType = OracleType.Number,
                Value = nIdDeclaracion,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = resx::Parametros.OutRadicacion,
                OracleType = OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(resx::Procedimientos.ObtenerRadicacionNumeroFormulario, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsRadicacion> lstRadicacion = ComplexDataAccessImplements.MapFromDataReaderI<clsRadicacion>(dr, true);
            if (lstRadicacion != null && lstRadicacion.Count > 0) return lstRadicacion.FirstOrDefault();
            return null;
        }

        public bool UpdateRadicacion(clsRadicacion rad, string cObservaciones, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.Id,
                OracleType = OracleType.Number,
                Value = rad.NId,
                Direction = ParameterDirection.InputOutput
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdMunicipioRad,
                OracleType = OracleType.Number,
                Value = rad.NIdMunicipio,
                Direction = ParameterDirection.Input
            });
            //TODO: jairovg - Validar qué pasa con este parámetro
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.ParamTipoEntidad,
                OracleType = OracleType.Number,
                Value = 0,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.NumeroFormularioRad,
                OracleType = OracleType.VarChar,
                Value = rad.CNumeroFormulario,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdTipoRadicacion,
                OracleType = OracleType.Number,
                Value = rad.NTipoRadicacion,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.Observaciones,
                OracleType = OracleType.VarChar,
                Value = cObservaciones,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.RutaImagenRad,
                OracleType = OracleType.VarChar,
                Value = rad.CRutaImagen,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdEntidadMunicipioRad,
                OracleType = OracleType.Number,
                Value = rad.NIdEntidad,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.ParamResultadoValidacion,
                OracleType = OracleType.Number,
                Value = rad.NTipoError,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.ActualizarRadicacion, tra, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return false;
            }

            if (!(cError == null || cError == string.Empty)) return false;
            return true;
        }

        #endregion

        #endregion
    }
}
