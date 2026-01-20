using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OracleClient;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;
using System.Data.Common;
using Ruv.Business.DTO.Valoracion;

namespace Ruv.Data.Valoracion.Valoracion
{
    public class clsLiderValoracion : Contratos.ILiderValoracion
    {
        public bool AprobarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdUsuario,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = nIdUsuario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdDeclaracion,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = nIdDeclaracion,
                Direction = ParameterDirection.Input
            });        

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.Observacion,
                OracleType = OracleType.VarChar,
                IsNullable = true,
                Value = cObservacion,
                Direction = ParameterDirection.Input
            });

                d.ExecuteNonQuery(resx::Procedimientos.AprobarValoracion, tra, ref cError);
                if (string.IsNullOrEmpty(cError))
                {
                    return true;
                }

            return false;
        }

        public bool RechazarValoracion(int nIdUsuario, int nIdDeclaracion, string cObservacion, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdUsuario,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = nIdUsuario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdDeclaracion,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = nIdDeclaracion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.Observacion,
                OracleType = OracleType.VarChar,
                IsNullable = true,
                Value = cObservacion,
                Direction = ParameterDirection.Input
            });

            d.ExecuteNonQuery(resx::Procedimientos.RechazarValoracion, tra, ref cError);
            if (string.IsNullOrEmpty(cError))
            {
                return true;
            }           

            return false;
        }

        public List<clsValoracionHistorico> consultarValoracionHistorico(int nIdValoracion, ref string cError) 
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdValoracion,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = nIdValoracion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter 
            { 
                ParameterName = resx::Parametros.Resultado,
                OracleType = OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(resx::Procedimientos.ValoracionHistorico, ref cError);
                if (!string.IsNullOrEmpty(cError))
                    return null;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::consultarValoracionHistorico", ex);
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsValoracionHistorico>(dr, true);
        }

        public string consultarMotivacionValoracionHistorico(int nIdValoracion, ref string cError) {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdValoracion,
                OracleType = OracleType.Number,
                Value = nIdValoracion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter 
            { 
                ParameterName = resx::Parametros.Resultado,
                OracleType = OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(resx::Procedimientos.ValoracionMotivacion, ref cError);
                if (!string.IsNullOrEmpty(cError))
                    return null;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::consultarMotivacionValoracionHistorico", ex);
                cError = ex.Message;
                return null;
            }

            dr.Read();
            string motivacion = motivacion = dbDefaults.getString(dr, 0);

            return motivacion;
        }
    }
}
