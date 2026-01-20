using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using Ruv.Business.DTO.Feriado;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;

namespace Ruv.Data.Feriados
{
    public class GestionFeriados : Contratos.IGestionFeriados
    {
        public int? CreacionFestivo(DateTime fecha, string nombre, string descripcion, bool recurrente, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Fecha, OracleType = OracleType.DateTime, Value = fecha, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Nombre, OracleType = OracleType.NVarChar, Value = nombre, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Descripcion, OracleType = OracleType.NVarChar, Value = descripcion, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Recurrente, OracleType = OracleType.Number, Value = recurrente ? 1 : 0, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConsulta, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

                try {
                    d.ExecuteNonQuery(resx::Procedimientos.InsertarFestivo, null, ref cError);
                    return int.Parse(d.GetOutputParameter(resx::Parametros.ResultadoConsulta).ToString());
                }
                catch (Exception ex) {
                    cError = ex.Message;
                    return null;
                }
            }
        }

        public void BorrarFestivo(int idFestivo, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Id, OracleType = OracleType.Number, Value = idFestivo, Direction = ParameterDirection.Input });

                try {
                    d.ExecuteNonQuery(resx::Procedimientos.BorrarFestivo, null, ref cError);
                }
                catch (Exception ex) {
                    cError = ex.Message;
                }
            }
        }

        public DateTime? CalcularDiasHabiles(DateTime fecha, int numeroDias, bool contarCero, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.FechaInicio, OracleType = OracleType.DateTime, Value = fecha, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.NumeroDias, OracleType = OracleType.Number, Value = numeroDias, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ContarCero, OracleType = OracleType.Number, Value = contarCero ? 1 : 0, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.ResultadoConsulta, OracleType = OracleType.DateTime, Direction = ParameterDirection.Output });

                try {
                    d.ExecuteNonQuery(resx::Procedimientos.FechaDiasHabiles, null, ref cError);
                    return Convert.ToDateTime(d.GetOutputParameter(resx::Parametros.ResultadoConsulta));
                }
                catch (Exception ex) {
                    cError = ex.Message;
                    return null;
                }
            }
        }

        public List<Feriado> ConsultarFestivos(int ano, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Ano, OracleType = OracleType.Number, Value = ano, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = resx::Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

                try {
                    IDataReader dr = d.ExecuteReader(resx::Procedimientos.ConsultarFeriadosPorAnio, ref cError);
                    if (!(cError == null || cError == string.Empty)) return null;
                    return ComplexDataAccessImplements.MapFromDataReaderI<Feriado>(dr, true);
                }
                catch (Exception ex) {
                    cError = ex.Message;
                    return null;
                }
            }
        }
    }
}
