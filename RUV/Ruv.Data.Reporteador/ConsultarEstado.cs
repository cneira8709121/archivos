using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using Ruv.Business.DTO.Reporteador;
using Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.Infrastructure.Crosscutting.Resources.DB;

namespace Ruv.Data.Reporteador
{
    public class ConsultarEstado : Contratos.IConsultarEstado
    {
        #region Métodos públicos

        #region Implementación de interfaces

        public int ConsultarEstadoDeclaracionConteo(clsDeclarante declarante, ref string cError)
        {
            Dao.SConection = General.CadenaConexionODAC;

            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.NumeroFormulario, OracleType = OracleType.VarChar, Value = declarante.CNumeroFormulario, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.PrimerNombre, OracleType = OracleType.VarChar, Value = declarante.CPrimerNombre, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.PrimerApellido, OracleType = OracleType.VarChar, Value = declarante.CPrimerApellido, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.NumeroDocumento, OracleType = OracleType.VarChar, Value = declarante.CNumeroDocumento, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.ResultadoConteo, OracleType = OracleType.Number, Direction = ParameterDirection.Output });
            
            try
            {
                d.ExecuteNonQuery(Procedimientos.ReporteDeclaracionCount, null, ref cError);
                if (!(cError == null || cError == string.Empty)) return 0;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return 0;
            }

            return int.Parse(d.GetOutputParameter(Parametros.ResultadoConteo).ToString());
        }

        public List<clsDeclarante> ConsultarEstadoDeclaracion(clsDeclarante declarante, int numeroPagina, int registrosPorPagina, ref string cError)
        {
            Dao.SConection = General.CadenaConexionODAC;

            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.NumeroFormulario, OracleType = OracleType.VarChar, Value = declarante.CNumeroFormulario, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.PrimerNombre, OracleType = OracleType.VarChar, Value = declarante.CPrimerNombre, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.PrimerApellido, OracleType = OracleType.VarChar, Value = declarante.CPrimerApellido, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.NumeroDocumento, OracleType = OracleType.VarChar, Value = declarante.CNumeroDocumento, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.NumeroPagina, OracleType = OracleType.Number, Value = numeroPagina, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.RegistrosPorPagina, OracleType = OracleType.Number, Value = registrosPorPagina, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Procedimientos.ReporteDeclaracion, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsDeclarante>(dr, true);
        }

        public List<clsDetalleDeclaracion> ConsultarDetalleDeclaracion(int nIdDeclaracion, ref string cError)
        {
            Dao.SConection = General.CadenaConexionODAC;

            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.IdDeclaracion,
                OracleType = OracleType.Double,
                Value = nIdDeclaracion,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.Resultado,
                OracleType = OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Procedimientos.ReporteDetalleDeclaracion, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsDetalleDeclaracion>(dr, true);
        }

        #endregion

        #endregion
    }
}
