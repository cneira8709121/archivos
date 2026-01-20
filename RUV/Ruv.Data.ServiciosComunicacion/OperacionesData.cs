using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.ServiciosComunicacion;
using System.Data;
using Ruv.Business.DTO.ServiciosComunicacion;
using Ruv.Data.ServiciosComunicacion.Contratos;

namespace Ruv.Data.ServiciosComunicacion
{
    public class OperacionesData : IOperacionesData
    {
        public List<clsPersona> ObtenerPersonas(int pagina, int tamano)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroPagina,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = pagina,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.RegistrosPorPagina,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = tamano,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConsulta,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            string cError = string.Empty;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerPersonas, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsPersona>(dr, true);
        }

        public clsPersona ObtenerPersonaPorId(int ID)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Id,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = ID,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConsulta,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            string cError = string.Empty;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerPersonaPorId, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsPersona> listClsPersona = ComplexDataAccessImplements.MapFromDataReaderI<clsPersona>(dr, true);

            if (listClsPersona != null && listClsPersona.Count > 0)
                return listClsPersona.FirstOrDefault();
            else
                return null;
        }

        public clsPersona ObtenerPersonaPorDocumento(string documento)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroDocumento,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = documento,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConsulta,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            string cError = string.Empty;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerPersonaPorDocumento, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsPersona> listClsPersona = ComplexDataAccessImplements.MapFromDataReaderI<clsPersona>(dr, true);

            if (listClsPersona != null && listClsPersona.Count > 0)
                return listClsPersona.FirstOrDefault();
            else
                return null;
        }

        public List<clsSiniestro> ObtenerSiniestrosPorIdPersona(int ID)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Id,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = ID,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConsulta,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            string cError = string.Empty;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerHechosPorIdPersona, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsSiniestro>(dr, true);
        }

        public List<clsGrupoFamiliar> ObtenerGrupoFamiliar(int ID)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Id,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = ID,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConsulta,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            string cError = string.Empty;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerGrupoFamiliar, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsGrupoFamiliar>(dr, true);
        }
    }
}
