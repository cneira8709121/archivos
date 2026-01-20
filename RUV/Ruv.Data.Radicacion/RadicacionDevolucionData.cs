using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.Radicacion;
using System.Data.Common;
using System.Data.OracleClient;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;
using System.Data;

namespace Ruv.Data.Radicacion
{
    public class RadicacionDevolucionData : Contratos.IRadicacionDevolucionData
    {
        public Int32 RadicarDevolucion(clsRadicacion rad, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.FechaLlegada,
                OracleType = OracleType.DateTime,
                Value = rad.DLlegada,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdUsuario,
                OracleType = OracleType.Number,
                Value = rad.NIdUsuarioRadica,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.NumeroFormulario,
                OracleType = OracleType.VarChar,
                Value = rad.CNumeroFormulario,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.Observaciones,
                OracleType = OracleType.VarChar,
                Value = rad.CObservaciones,
                IsNullable = true,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.OutIdRadicacion,
                OracleType = OracleType.Number,
                Direction = ParameterDirection.InputOutput
            });

            Decimal idGenerado = 0;
            try
            {
                d.ExecuteNonQuery(resx::Procedimientos.InsertaRadicacionDevolucion, tra, ref cError);

                if (!string.IsNullOrEmpty(cError)) return 0;
                
                DbParameter dbParameter = d.LstParameter.FirstOrDefault(x => x.ParameterName == resx::Parametros.OutIdRadicacion);
                idGenerado = dbParameter == null ? 0 : (Decimal)dbParameter.Value;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return 0;
            }

            return (Int32)idGenerado;
        }
    }
}
