using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OracleClient;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;
using Ruv.Business.DTO.CriticaN;
using System.Data.Common;

namespace Ruv.Data.CriticaN
{
    public class Gestion : Contratos.ICriticaN
    {
        #region Public methods

        #region Services implementation

        //public bool GuardarValidacion(List<clsRespuestaCritica> lstRespuesta, DbTransaction tra, ref string cError)
        //{
        //    foreach (clsRespuestaCritica rc in lstRespuesta)
        //    {
        //        Dao.RefreshParameters();
        //        Dao.AddParameter(new OracleParameter
        //        {
        //            ParameterName = resx::Parametros.IdCriticaN,
        //            OracleType = OracleType.Number,
        //            IsNullable = true,
        //            Value = rc.NIdCriticaN,
        //            Direction = ParameterDirection.Input
        //        });

        //        Dao.AddParameter(new OracleParameter
        //        {
        //            ParameterName = resx::Parametros.Respuesta,
        //            OracleType = OracleType.Number,
        //            IsNullable = true,
        //            Value = rc.NRespuesta,
        //            Direction = ParameterDirection.Input
        //        });

        //        Dao.AddParameter(new OracleParameter
        //        {
        //            ParameterName = resx::Parametros.IdUsuario,
        //            OracleType = OracleType.Number,
        //            Value = rc.NIdUsuario,
        //            Direction = ParameterDirection.Input
        //        });

        //        Dao.AddParameter(new OracleParameter
        //        {
        //            ParameterName = resx::Parametros.IdRadicacion,
        //            OracleType = OracleType.Number,
        //            Value = rc.NIdRadicacion,
        //            Direction = ParameterDirection.Input
        //        });

        //        Dao.AddParameter(new OracleParameter
        //        {
        //            ParameterName = resx::Parametros.ObservacionCriticaN,
        //            OracleType = OracleType.VarChar,
        //            IsNullable = true,
        //            Value = rc.CObservacion,
        //            Direction = ParameterDirection.Input
        //        });

        //        try
        //        {
        //            Dao.ExecuteNonQuery(resx::Procedimientos.InsertaCriticaN, tra, ref cError);
        //        }
        //        catch (Exception ex)
        //        {
        //            cError = ex.Message;
        //            return false;
        //        }
        //    }

        //    if (!(cError == null || cError == string.Empty)) return false;
        //    return true;
        //}

        public bool GuardarValidacion(List<clsRespuestaCritica> lstRespuesta, DbTransaction tra, ref string cError)
        {
            string idsCriticaN = string.Empty;
            string respuestas = string.Empty;
            long idUsuario = 0;
            long idRadicacion = 0;
            string observacion = string.Empty;

            if (lstRespuesta != null && lstRespuesta.Count > 0)
            {
                idsCriticaN = String.Join(",", lstRespuesta.Select(x => x.NIdCriticaN).ToArray());
                respuestas = String.Join(",", lstRespuesta.Select(x => x.NRespuesta).ToArray());
                idUsuario = lstRespuesta.FirstOrDefault().NIdUsuario;
                idRadicacion = lstRespuesta.FirstOrDefault().NIdRadicacion;
                observacion = lstRespuesta.FirstOrDefault().CObservacion;
            }

            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdCriticaN,
                OracleType = OracleType.VarChar,
                IsNullable = true,
                Value = idsCriticaN,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.Respuesta,
                OracleType = OracleType.VarChar,
                IsNullable = true,
                Value = respuestas,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdUsuario,
                OracleType = OracleType.Number,
                Value = idUsuario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdRadicacion,
                OracleType = OracleType.Number,
                Value = idRadicacion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.ObservacionCriticaN,
                OracleType = OracleType.VarChar,
                IsNullable = true,
                Value = observacion,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.InsertaCriticaN, tra, ref cError);
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
