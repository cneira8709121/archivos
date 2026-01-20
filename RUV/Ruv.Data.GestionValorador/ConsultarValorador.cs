using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using Ruv.Business.DTO.GestionValorador;
using Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.Infrastructure.Crosscutting.Resources.DB;

namespace Ruv.Data.GestionValorador
{
    public class ConsultarValorador : Contratos.IConsultarValorador
    {

        public List<clsGestionValorador> ConsultaGestionVal(int PaginaNumber,int SizePagina, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.NumeroPagina,
                OracleType = OracleType.Number,
                Value = PaginaNumber,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.RegistrosPorPagina,
                OracleType = OracleType.Number,
                Value = SizePagina,
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
                dr = d.ExecuteReader(Procedimientos.ConsultaGestionValorador, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsGestionValorador>(dr, true);
        }

        public List<clsDetalleGestionVal> DetalleGestionValorador(int NIdValorador, DateTime FechaConsulta, int PaginaNumber, int SizePagina, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.ValoradorId,
                OracleType = OracleType.Number,
                Value = NIdValorador,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.FechaSolicitud,
                OracleType = OracleType.DateTime,
                Value = FechaConsulta,
                Direction = ParameterDirection.Input
            });


            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.NumeroPagina,
                OracleType = OracleType.Number,
                Value = PaginaNumber,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.RegistrosPorPagina,
                OracleType = OracleType.Number,
                Value = SizePagina,
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
                dr = d.ExecuteReader(Procedimientos.DetalleGestionValorador, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsDetalleGestionVal>(dr, true);
        }

        public int ConsultaValoradorCount(ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.ResultadoConteo,
                OracleType = OracleType.Number,
                Direction = ParameterDirection.Output
            });

            
            try
            {
                d.ExecuteNonQuery(Procedimientos.ContadorValoradores,null, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return 0;
            }
            //int retorno =dr[0];
            return int.Parse(d.GetOutputParameter(Parametros.ResultadoConteo).ToString());
        }

        public int DetalleValoradorCount(int NIdValorador, DateTime FechaConsulta, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.ValoradorId,
                OracleType = OracleType.Number,
                Value = NIdValorador,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.FechaSolicitud,
                OracleType = OracleType.DateTime,
                Value = FechaConsulta,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Parametros.ResultadoConteo,
                OracleType = OracleType.Number,
                Direction = ParameterDirection.Output
            });


            try
            {
                d.ExecuteNonQuery(Procedimientos.ContadorDetalleValora, null, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return 0;
            }
            //int retorno =dr[0];
            return int.Parse(d.GetOutputParameter(Parametros.ResultadoConteo).ToString());
        }
   }
    
}
