using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using Ruv.Business.DTO.Valoracion;
using Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.Infrastructure.Crosscutting.Resources.DB;
using resx = Ruv.Infrastructure.Crosscutting.Resources.DB;

namespace Ruv.Data.Valoracion.Asignacion
{
    public class entAsignacion : entidadRUV
    {
        #region Obtener Datos
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public DataTable getDeclaracionesSinValorar()
        {
            List<TBDECLARACIONES> declaraciones = new List<TBDECLARACIONES>();
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_getdeclaracionessinvalorar", new object[] { null });
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public DataTable getDeclaracionesValorando()
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_getdeclaracionesvalorando", new object[] { null });
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Guardar Valoracion
        /// </summary>
        /// <param name="objData"></param>
        public void GuardarValoracion(TBVALORACION objData, int Valorador, DbTransaction tra)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_AsignarValoracion", ParametrosGuardar(objData, Valorador));

            dbRUV.ExecuteNonQuery(cmd);
            objData.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_ID_Valoracion"));
        }

        /// <summary>
        /// Parametros a enviar a procedimeinto
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        private object[] ParametrosGuardar(TBVALORACION objData, int Valorador)
        {
            return new object[]{     
                                  objData.ID
                                , objData.ID_DECLARACION
                                , objData.ID_ESTADO_VAL 
                                , objData.ID_VALORADOR
                                , objData.ID_ASIGNADOR
                                , Valorador
            };
        }

        /// <summary>
        /// Obtiene los Valoradores
        /// </summary>
        /// <returns></returns>
        public DataTable getValoradores()
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_getValoradores", new object[] { null });
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }


        public DataTable getDetalleDeclaracionPorId(int DeclaracionId)
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_getDetallesDeclaracion", new object[] { DeclaracionId, null });
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataSet getDeclaracionesSinValorarPaginado(int Inicio, int Fin, string sortColumns, string filtro, string Valor)
        {

            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_getDeclaSinValorarPaginada", new object[] { Inicio, Fin, sortColumns, filtro, Valor, null });
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        public int getDeclaracionesSinValorarCantidad(string filtro, string Valor)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_getDeclaSinValorarCantidad", new object[] { filtro, Valor, null });

            dbRUV.ExecuteNonQuery(cmd);
            return Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_Cantidad"));
        }

        #endregion

        public string AutoAsignarValoracion(int IdDeclaracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter
            {
                ParameterName = resx::Parametros.IdDeclaracion,
                OracleType = OracleType.Number,
                Value = IdDeclaracion,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(Procedimientos.AutoAsignaValoracion, null, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::AutoAsignarValoracion", ex);
                cError = ex.Message;
                return null;
            }

            return null;
        }


        public void GuardarValoracion(int usuarioId)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_AsignarValoracionAutomatico", new object[] { null, usuarioId });
            dbRUV.ExecuteNonQuery(cmd);
        }

        public List<Ruv.Business.DTO.Valoracion.clsDeclaracionesValoracion> getDeclaracionesValorandoPaginado(Ruv.Infrastructure.Crosscutting.Common.Valoracion.clsConsultaValoracion consulta, ref string error)
        {
            Dao.SConection = General.CadenaConexionODAC;

            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.OrdenConsulta, OracleType = OracleType.VarChar, Value = consulta.OrdenarPor, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.FiltroConsulta, OracleType = OracleType.VarChar, Value = consulta.Filtro, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.RegInicialConsulta, OracleType = OracleType.Number, Value = consulta.Pagina, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.TamañoPaginaConsulta, OracleType = OracleType.Number, Value = consulta.Tamaño, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.ResultadoConsulta, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Procedimientos.ValoracionesEnValoracion, ref error);
                if (!(error == null || error == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::getDeclaracionesValorandoPaginado", ex);
                error = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsDeclaracionesValoracion>(dr, true);
        }
        public void getDeclaracionesValorandoTotal(ref Ruv.Infrastructure.Crosscutting.Common.Valoracion.clsConsultaValoracion consulta, ref string error)
        {
            Dao.SConection = General.CadenaConexionODAC;

            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.FiltroConsulta, OracleType = OracleType.VarChar, Value = consulta.Filtro, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.ResultadoConsulta, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

            try
            {
                d.ExecuteNonQuery(Procedimientos.ValoracionesEnValoracionCantidad, null, ref error);
                consulta.Total = int.Parse(d.GetOutputParameter(Parametros.ResultadoConsulta).ToString());
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::getDeclaracionesValorandoTotal", ex);
                error = ex.Message;
            }
        }
    }
}
