using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.EntityClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Ruv.Infrastructure.Crosscutting.Resources;
using System.Data.OracleClient;
using Ruv.Infrastructure.Crosscutting.Resources.DB;
using Ruv.Business.DTO.Valoracion;
using Ruv.Business.DTO.Notificacion;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;


namespace Ruv.Data.Valoracion.Valoracion
{
    public class entValoracion : entidadRUV
    {


        #region Código viejo

        public TBVALORACION GetValoracionByIdOld(int valoracionId, DbTransaction transaction)
        {
            using (var d = new Dao())
            {
                d.AddInputParameter(new OracleParameter { ParameterName = "p_IdVal", OracleType = OracleType.Number, Value = valoracionId });
                d.AddOutputParameter(new OracleParameter() { ParameterName = "cu_Result", OracleType = OracleType.Cursor });
                using (IDataReader dr = d.ExecuteReader("PKG_VALORACION.sp_getValoracionPorID"))
                {
                    var valoracion = EnterpriseLibraryContainer.Current.GetInstance<TBVALORACION>();
                    if (dr.Read())
                    {
                        valoracion.ID = dr.GetInt("ID");
                        valoracion.ID_DECLARACION = dr.GetInt("ID_DECLARACION");

                        valoracion.TBDECLARACIONES = EnterpriseLibraryContainer.Current.GetInstance<TBDECLARACIONES>();
                        valoracion.TBDECLARACIONES.ID = dr.GetInt("ID_DECLARACION");

                        valoracion.ID_ESTADO_VAL = dr.GetInt("ID_ESTADO_VAL");
                        valoracion.FECHAASIGNACION = dr.GetDateTime("FECHAASIGNACION");
                        valoracion.ID_VALORADOR = dr.GetNullableInt("ID_VALORADOR");
                        valoracion.ID_ASIGNADOR = dr.GetNullableInt("ID_ASIGNADOR");
                        valoracion.FECHAVALORACION = dr.GetNullableDateTime("FECHAVALORACION");
                        valoracion.FECHAVALORACIONREAL = dr.GetNullableDateTime("FECHAVALORACIONREAL");
                        valoracion.ESDECLARACION = dr.GetNullableShort("ESDECLARACION");
                        valoracion.OBSERVACION = dr.GetString("OBSERVACION");

                        var informacionMotivacion = EnterpriseLibraryContainer.Current.GetInstance<TBVALORACION_MOTIVACION>();
                        informacionMotivacion.MOTIVACION_INCLUSION = dr.GetString("MOTIVACION_INCLUSION");
                        informacionMotivacion.MOTIVACION_NOINCLUSION = dr.GetString("MOTIVACION_NOINCLUSION");
                        informacionMotivacion.RESUELVE_ARTICULO1 = dr.GetString("RESUELVE_ARTICULO1");
                        informacionMotivacion.RESUELVE_ARTICULO2 = dr.GetString("RESUELVE_ARTICULO2");
                        informacionMotivacion.TIPOMOTIVACION = dr.GetString("TIPOMOTIVACION");
                        valoracion.TBVALORACION_MOTIVACION.Add(informacionMotivacion);
                    }
                    return valoracion;
                }
            }

        }

        #endregion

        public clsValoracion GetValoracionById(int valoracionId)
        {
            using (Dao d = new Dao())
            {
                d.RefreshParameters();

                d.AddInputParameter(new OracleParameter { ParameterName = "p_IdVal", OracleType = OracleType.Number, Value = valoracionId });
                d.AddOutputParameter(new OracleParameter() { ParameterName = "cu_Result", OracleType = OracleType.Cursor });

                IDataReader dr = d.ExecuteReader("PKG_VALORACION.sp_getValoracionPorID");
                List<clsValoracion> valoracion = ComplexDataAccessImplements.MapFromDataReaderI<clsValoracion>(dr, true);

                if (valoracion != null && valoracion.Count > 0)
                    return valoracion.FirstOrDefault();
                else
                    return null;
            }
        }

        public TBVALORACION GetValoracionByDeclaracionId(int DeclaracionId)
        {
            TBVALORACION _val = EnterpriseLibraryContainer.Current.GetInstance<TBVALORACION>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_getValoracionPorDeclaracion", new object[] { DeclaracionId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;
                    _val.TBDECLARACIONES = new TBDECLARACIONES();
                    TBVALORACION_MOTIVACION motivaciones = new TBVALORACION_MOTIVACION();
                    _val.ID = dbDefaults.getInt32(dr, index++).Value;
                    _val.ID_DECLARACION = dbDefaults.getInt32(dr, index++).Value;
                    _val.ID_ESTADO_VAL = dbDefaults.getInt32(dr, index++).Value; ;
                    _val.FECHAASIGNACION = dbDefaults.getDateTime(dr, index++).Value;
                    _val.ID_VALORADOR = dbDefaults.getInt32(dr, index++).Value;
                    _val.ID_ASIGNADOR = dbDefaults.getInt32(dr, index++).Value;
                    _val.FECHAVALORACION = dbDefaults.getDateTime(dr, index++);
                    _val.FECHAVALORACIONREAL = dbDefaults.getDateTime(dr, index++);
                    motivaciones.MOTIVACION_INCLUSION = dbDefaults.getString(dr, index++);
                    motivaciones.MOTIVACION_NOINCLUSION = dbDefaults.getString(dr, index++);
                    motivaciones.RESUELVE_ARTICULO1 = dbDefaults.getString(dr, index++);
                    motivaciones.RESUELVE_ARTICULO2 = dbDefaults.getString(dr, index++);
                    motivaciones.TIPOMOTIVACION = dbDefaults.getString(dr, index++);
                    _val.TBVALORACION_MOTIVACION.Add(motivaciones);
                    _val.ESDECLARACION = dbDefaults.getInt16(dr, index++);
                    _val.OBSERVACION = dbDefaults.getString(dr, index++);
                    _val.TBDECLARACIONES.PARAM_ESTADO = dbDefaults.getInt32(dr, index++);
                }
            }
            return _val;
        }

        public DataTable GetHechosPorValoracionId(int ValoracionId)
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_getHechosPorValoracion", new object[] { ValoracionId, null });
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataTable GetPersonasPorAnexoId(int anexoId)
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_GetPersonasPorAnexo", new object[] { anexoId, null });
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }


        //public List<TBESTADO_VAL> GetEstadosValoracion()
        //{
        //    Dao.SConection = General.CadenaConexionODAC;
        //    List<TBESTADO_VAL> estados = new List<TBESTADO_VAL>();
        //    string error = string.Empty;
        //    Dao d = new Dao();
        //    d.RefreshParameters();
        //    d.AddParameter(new OracleParameter() { ParameterName = "P_Result", OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

        //    IDataReader dr = null;
        //    try
        //    {

        //        dr = d.ExecuteReader("pkg_valoracion.sp_GetEstadosValPersona", ref error);
        //        if (!(error == null || error == string.Empty)) return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        RegistroTraza.I.Registrar(this.GetType().Name + ":::sp_GetEstadosValPersona", ex);
        //        error = ex.Message;
        //        return null;
        //    }

        //    return ComplexDataAccessImplements.MapFromDataReaderI<TBESTADO_VAL>(dr, true);


        //    //dr(List<TBESTADO_VAL>)

        //    return estados;

        //}

        public List<TBESTADO_VAL> GetEstadosValoracion()
        {

            List<TBESTADO_VAL> estados = new List<TBESTADO_VAL>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetEstadosValPersona", new object[] {null}))
            {
                while (dr.Read())
                {
                    int index = 0;

                    TBESTADO_VAL estado = EnterpriseLibraryContainer.Current.GetInstance<TBESTADO_VAL>();
                    estado.ID = dbDefaults.getInt32(dr, index++).Value;
                    estado.NOMBRE = dbDefaults.getString(dr, index++);
                    estado.TEXTO = dbDefaults.getString(dr, index++);
                    estados.Add(estado);
                }
            }
            return estados;
        }

        public List<TBOBSERVACION_VAL> GetObservacionesEstadoPorEstadoId(int estadoId)
        {

            List<TBOBSERVACION_VAL> observa = new List<TBOBSERVACION_VAL>();

            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetObservacionesPorEstadoId", new object[] { estadoId, null }))
            {
                while (dr.Read())
                {
                    int index = 0;

                    TBOBSERVACION_VAL obse = EnterpriseLibraryContainer.Current.GetInstance<TBOBSERVACION_VAL>();
                    obse.ID = dbDefaults.getInt32(dr, index++).Value;
                    obse.NOMBRE = dbDefaults.getString(dr, index++);
                    obse.TEXTO = dbDefaults.getString(dr, index++);
                    obse.ID_ESTADO_VAL = dbDefaults.getInt32(dr, index++).Value;
                    observa.Add(obse);
                }
            }
            return observa;
        }

        public List<TBOBSERVACION_VAL> GetObservacionesEstado()
        {
            List<TBOBSERVACION_VAL> observa = new List<TBOBSERVACION_VAL>();

            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetObservaciones", new object[] { null }))
            {
                while (dr.Read())
                {
                    int index = 0;

                    TBOBSERVACION_VAL obse = EnterpriseLibraryContainer.Current.GetInstance<TBOBSERVACION_VAL>();
                    obse.ID = dbDefaults.getInt32(dr, index++).Value;
                    obse.NOMBRE = dbDefaults.getString(dr, index++);
                    obse.TEXTO = dbDefaults.getString(dr, index++);
                    obse.ID_ESTADO_VAL = dbDefaults.getInt32(dr, index++).Value;
                    observa.Add(obse);
                }
            }
            return observa;
        }

        public List<TBPARAMETROS> GetHechosEnmarcado()
        {
            List<TBPARAMETROS> parametro = new List<TBPARAMETROS>();

            using (IDataReader dr = dbRUV.ExecuteReader("pkg_valoracion.sp_GetParamHechoEnMarcado", new object[] { null }))
            {
                while (dr.Read())
                {
                    int index = 0;

                    TBPARAMETROS param = EnterpriseLibraryContainer.Current.GetInstance<TBPARAMETROS>();
                    param.ID = dbDefaults.getInt32(dr, index++).Value;
                    param.NOMBRE = dbDefaults.getString(dr, index++);
                    parametro.Add(param);
                }
            }
            return parametro;
        }

        public List<TBPARAMETROS> GetDecretoLey(int idTipoParamatro)
        {
            List<TBPARAMETROS> parametro = new List<TBPARAMETROS>();
            using (IDataReader dr = dbRUV.ExecuteReader("pkg_common.sp_ObtenerParametros", new object[] { idTipoParamatro, null }))
            {
                while (dr.Read())
                {
                    int index = 0;

                    TBPARAMETROS param = EnterpriseLibraryContainer.Current.GetInstance<TBPARAMETROS>();
                    param.ID = dbDefaults.getInt32(dr, index++).Value;
                    param.NOMBRE = dbDefaults.getString(dr, index++);
                    parametro.Add(param);
                }
            }
            return parametro;
        }

        public TBVALORACION Actualizar(TBVALORACION valoracion, bool finalizar, DbTransaction transaction)
        {
            using (var d = new Dao())
            {
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_Id", OracleType = OracleType.Number, Value = valoracion.ID });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_EstadoId", OracleType = OracleType.Number, Value = valoracion.ID_ESTADO_VAL });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_FechaAsignacion", OracleType = OracleType.DateTime, Value = valoracion.FECHAASIGNACION });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_ValoradorId", OracleType = OracleType.Number, Value = valoracion.ID_VALORADOR });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_AsignadorId", OracleType = OracleType.Number, Value = valoracion.ID_ASIGNADOR });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_FechaValoracion", OracleType = OracleType.DateTime, Value = valoracion.FECHAVALORACION ?? DateTime.Now });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_FechaRealValoracion", OracleType = OracleType.DateTime, Value = valoracion.FECHAVALORACIONREAL ?? DateTime.Now });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_Motivacion_Inclusion", OracleType = OracleType.Clob, Value = !string.IsNullOrEmpty(valoracion.TBVALORACION_MOTIVACION.First().MOTIVACION_INCLUSION) ? valoracion.TBVALORACION_MOTIVACION.First().MOTIVACION_INCLUSION : " " });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_Motivacion_NoInclusion", OracleType = OracleType.Clob, Value = !string.IsNullOrEmpty(valoracion.TBVALORACION_MOTIVACION.First().MOTIVACION_NOINCLUSION) ? valoracion.TBVALORACION_MOTIVACION.First().MOTIVACION_NOINCLUSION : " " });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_ResuelveArticulo1", OracleType = OracleType.Clob, Value = !string.IsNullOrEmpty(valoracion.TBVALORACION_MOTIVACION.First().RESUELVE_ARTICULO1) ? valoracion.TBVALORACION_MOTIVACION.First().RESUELVE_ARTICULO1 : " " });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_ResuelveArticulo2", OracleType = OracleType.Clob, Value = !string.IsNullOrEmpty(valoracion.TBVALORACION_MOTIVACION.First().RESUELVE_ARTICULO2) ? valoracion.TBVALORACION_MOTIVACION.First().RESUELVE_ARTICULO2 : " " });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_EsDeclaracion", OracleType = OracleType.Number, Value = (valoracion.ESDECLARACION.HasValue) ? (short)(valoracion.ESDECLARACION.Value) : 0 });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_Observacion", OracleType = OracleType.Clob, Value = (!string.IsNullOrEmpty(valoracion.OBSERVACION)) ? valoracion.OBSERVACION : " " });
                d.AddInputParameter(new OracleParameter() { ParameterName = "p_Finalizar", OracleType = OracleType.Number, Value = finalizar ? 1 : 0 });
                d.AddOutputParameter(new OracleParameter() { ParameterName = "p_CantidadAfectadas", OracleType = OracleType.Number });
                d.ExecuteNonQuery("PKG_VALORACION.sp_ActualizarValoracion", transaction);
                var afectadas = Convert.ToInt32(d.GetOutputParameter("p_CantidadAfectadas"));
                if (afectadas > 0)
                {
                    var newValoracion = GetValoracionByIdOld(valoracion.ID, transaction);
                    return newValoracion;
                }
                return null;
            }
        }


        public int EliminarCausales(TBVALORACION valoracion)
        {

            DbCommand cmd = dbRUV.GetStoredProcCommand("pkg_valoracion.sp_EliminaTbCausalValoracion", new object[] { valoracion.ID, null });
            dbRUV.ExecuteNonQuery(cmd);
            int afectadas = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_AFECTADAS"));
            return afectadas;
        }


        public TBVALORACION GetPorId(int Id)
        {
            RuvEntities Context = new RuvEntities();

            TBVALORACION val = Context.TBVALORACION.First(x => x.ID == Id);
            return val;
        }

        public TBDECLARACIONES GetDeclaracionPorId(int Id)
        {
            using (RuvEntities Context = new RuvEntities())
            {
                return Context.TBDECLARACIONES.Where(x => x.ID == Id).First();
            }
        }

        private object[] ParametrosGuardar(TBVALORACION valoracion)
        {
            return new object[] {
                valoracion.ID,
                valoracion.ID_DECLARACION,
                valoracion.ID_ESTADO_VAL,
                valoracion.FECHAASIGNACION,
                valoracion.ID_VALORADOR,
                valoracion.ID_ASIGNADOR,
                (valoracion.FECHAVALORACION.HasValue) ? valoracion.FECHAVALORACION.Value : DateTime.Now,
                (valoracion.FECHAVALORACIONREAL.HasValue) ? valoracion.FECHAVALORACIONREAL.Value : DateTime.Now,
                !string.IsNullOrEmpty(valoracion.TBVALORACION_MOTIVACION.First().MOTIVACION_INCLUSION) ? valoracion.TBVALORACION_MOTIVACION.First().MOTIVACION_INCLUSION : " ",
                !string.IsNullOrEmpty(valoracion.TBVALORACION_MOTIVACION.First().MOTIVACION_NOINCLUSION) ? valoracion.TBVALORACION_MOTIVACION.First().MOTIVACION_NOINCLUSION : " ",
                !string.IsNullOrEmpty(valoracion.TBVALORACION_MOTIVACION.First().RESUELVE_ARTICULO1) ? valoracion.TBVALORACION_MOTIVACION.First().RESUELVE_ARTICULO1 : " ",
                !string.IsNullOrEmpty(valoracion.TBVALORACION_MOTIVACION.First().RESUELVE_ARTICULO2) ? valoracion.TBVALORACION_MOTIVACION.First().RESUELVE_ARTICULO2 : " ",
                (valoracion.ESDECLARACION.HasValue) ? (short)(valoracion.ESDECLARACION.Value) : 0,
                (!string.IsNullOrEmpty(valoracion.OBSERVACION)) ? valoracion.OBSERVACION : " ",
                null
            };
        }


        public DataSet GetInforme()
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_getInformacion", new object[] { null });
            if (ds.Tables.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        public DataSet GetResumenPorId(int valId)
        {
            DataSet ds = dbRUV.ExecuteDataSet("pkg_valoracion.sp_getResumenValoracion", new object[] { valId, null, null, null });
            if (ds.Tables.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        public List<Business.DTO.Valoracion.clsTareasValorador> getListaTareas(Infrastructure.Crosscutting.Common.Valoracion.clsConsultaValoracion eConsulta, ref string error)
        {
            Dao.SConection = General.CadenaConexionODAC;

            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.ValoradorId, OracleType = OracleType.Number, Value = eConsulta.ValoradorId, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.OrdenConsulta, OracleType = OracleType.VarChar, Value = eConsulta.OrdenarPor, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.FiltroConsulta, OracleType = OracleType.VarChar, Value = eConsulta.Filtro, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.RegInicialConsulta, OracleType = OracleType.Number, Value = eConsulta.Pagina, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.TamañoPaginaConsulta, OracleType = OracleType.Number, Value = eConsulta.Tamaño, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.ResultadoConsulta, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Procedimientos.ListaTareasValorador, ref error);
                if (!(error == null || error == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::getListaTareas", ex);
                error = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsTareasValorador>(dr, true);
        }

        public void getListaTareasCantidad(ref Infrastructure.Crosscutting.Common.Valoracion.clsConsultaValoracion eConsulta, ref string error)
        {
            Dao.SConection = General.CadenaConexionODAC;

            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.ValoradorId, OracleType = OracleType.Number, Value = eConsulta.ValoradorId, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.FiltroConsulta, OracleType = OracleType.VarChar, Value = eConsulta.Filtro, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.ResultadoConsulta, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

            try
            {
                d.ExecuteNonQuery(Procedimientos.ListaTareasValoradorCantidad, null, ref error);
                eConsulta.Total = int.Parse(d.GetOutputParameter(Parametros.ResultadoConsulta).ToString());
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::getListaTareasCantidad", ex);
                error = ex.Message;
            }
        }

        public bool GuarPrueba(string guarda, DbTransaction TRAN)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_COMMON.SP_TMP_TRANSACT_INSERT", guarda);
            dbRUV.ExecuteNonQuery(cmd, TRAN);
            return true;
        }

        public DataSet GetValoracionByIdFull(int IdValoracion)
        {
            DataSet ds = dbRUV.ExecuteDataSet("PKG_VALORACION.sp_GetValoracionFull", new object[] { IdValoracion, null, null, null, null, null });
            if (ds.Tables.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        public void GetValoracionByIdFull(int IdValoracion, clsValoracion valoracion)
        {
            using (Dao d = new Dao())
            {
                d.RefreshParameters();

                d.AddInputParameter(new OracleParameter { ParameterName = "pi_IdValoracion", OracleType = OracleType.Number, Value = IdValoracion });
                d.AddOutputParameter(new OracleParameter { ParameterName = "po_DetalleDeclaracion", OracleType = OracleType.Cursor });
                d.AddOutputParameter(new OracleParameter { ParameterName = "po_Principios", OracleType = OracleType.Cursor });
                d.AddOutputParameter(new OracleParameter { ParameterName = "po_RegistrosAnteriores", OracleType = OracleType.Cursor });
                d.AddOutputParameter(new OracleParameter { ParameterName = "po_Hechos", OracleType = OracleType.Cursor });
                d.AddOutputParameter(new OracleParameter { ParameterName = "po_Personas", OracleType = OracleType.Cursor });

                IDataReader dr = d.ExecuteReader("PKG_VALORACION.sp_GetValoracionFull");

                valoracion.PersonasDeclaracion = ComplexDataAccessImplements.MapFromDataReaderI<clsPersona>(dr, true);
                dr.NextResult();
                valoracion.CausalDevolucion = ComplexDataAccessImplements.MapFromDataReaderI<int>(dr, true);
                dr.NextResult();
                valoracion.RegistrosAnteriores = ComplexDataAccessImplements.MapFromDataReaderI<clsRegistrosValoracion>(dr, true);
                dr.NextResult();
                valoracion.Hechos = ComplexDataAccessImplements.MapFromDataReaderI<clsHechosValoracion>(dr, true);
            }
        }

        public void InsertaTipoMotivacion(int nidValoracion, string cTipoValoracion, DbTransaction transaction)
        {
            using (var d = new Dao())
            {
                d.AddParameter(new OracleParameter() { ParameterName = Parametros.IdValoracion, OracleType = OracleType.Number, Value = nidValoracion, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter() { ParameterName = Parametros.IdTipoMotivacion, OracleType = OracleType.VarChar, Value = cTipoValoracion, Direction = ParameterDirection.Input, IsNullable = true });
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.InsertaTipoMotivacion, transaction);
            }
        }

        public string ObtieneTipoMotivacion(int nidValoracion, DbTransaction transaction)
        {
            using (var d = new Dao())
            {
                d.AddParameter(new OracleParameter() { ParameterName = Parametros.IdValoracion, OracleType = OracleType.Number, Value = nidValoracion, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter() { ParameterName = Parametros.TipoMotivacionId, OracleType = System.Data.OracleClient.OracleType.VarChar, Size = 5, Direction = ParameterDirection.Output });
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerTipoMotivacion, transaction);
                return d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.TipoMotivacionId).ToString();
            }
        }

        public List<clsEntidadMunicipioNotificacion> ObtenerEntidadesMunicipio(ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Resultado, OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                var listaEntidadesMunicipio = ComplexDataAccessImplements.MapFromDataReaderI<clsEntidadMunicipioNotificacion>(d.ExecuteReader("PKG_NOTIFICACION.SP_GETPUNTOSNOTIFICACION", ref cError), true);
                return listaEntidadesMunicipio;
            }
        }

        public int AgregaPersonaValoracion(clsAgregarPersonaValoracion AgregaPersona, DbTransaction tra, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = Parametros.PrimerNombre, OracleType = OracleType.VarChar, Value = AgregaPersona.cPrimerNombre, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.SegundoNombre, OracleType = OracleType.VarChar, IsNullable = true, Value = AgregaPersona.cSegundoNombre, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.PrimerApellido, OracleType = OracleType.VarChar, Value = AgregaPersona.cPrimerApellido, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.SegundoApellido, OracleType = OracleType.VarChar, IsNullable = true, Value = AgregaPersona.cSegundoApellido, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.TipoDocumento, OracleType = OracleType.Number, IsNullable = true, Value = AgregaPersona.nTipoDocumento, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.NumeroDocumento, OracleType = OracleType.VarChar, IsNullable = true, Value = AgregaPersona.cNumeroDocumento, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.EstadoCivil, OracleType = OracleType.Number, IsNullable = true, Value = AgregaPersona.nEstadoCivil, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Genero, OracleType = OracleType.Number, IsNullable = true, Value = AgregaPersona.nGenero, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.MinoriaEtnica, OracleType = OracleType.Number, IsNullable = true, Value = AgregaPersona.nEtnia, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Gestante, OracleType = OracleType.Number, IsNullable = true, Value = AgregaPersona.nGestante, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.FechaNacimiento, OracleType = OracleType.DateTime, IsNullable = true, Value = AgregaPersona.cFechanacimiento, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.MujerCabezaHogar, OracleType = OracleType.Number, IsNullable = true, Value = AgregaPersona.nCabezaHogar, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Comunidad, OracleType = OracleType.VarChar, IsNullable = true, Value = AgregaPersona.cComunidad, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.nIdCreadoPersona, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

                try
                {
                    d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.AgregarPersonaValoracion, tra, ref cError);
                    return int.Parse(d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.PersonaAgregadaId).ToString());
                }
                catch (Exception ex)
                {
                    RegistroTraza.I.Registrar(this.GetType().Name + ":::AgregaPersonaValoracion", ex);
                    cError = ex.Message;
                    return 0;
                }
            }
        }

        public int AgregaRegPersonaValoracion(clsAgregarPersonaValoracion AgregaPersona, int nIdPersona, DbTransaction tra, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = Parametros.IdDeclaracion, OracleType = OracleType.Number, Value = AgregaPersona.nIdDeclaracion, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.IdPersona, OracleType = OracleType.Number, Value = nIdPersona, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Direccion, OracleType = OracleType.VarChar, Value = AgregaPersona.cDireccion, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Telefono, OracleType = OracleType.VarChar, Value = AgregaPersona.cTelefono, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.RelacionPersona, OracleType = OracleType.Number, Value = AgregaPersona.nRelacion, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Correoelectronico, OracleType = OracleType.VarChar, Value = AgregaPersona.cCorreoelectronico, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.MujerCabezaHogar, OracleType = OracleType.Number, Value = AgregaPersona.nCabezaHogar, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.RegimenEspecial, OracleType = OracleType.Number, Value = AgregaPersona.nRegimenEspecial, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Gestante, OracleType = OracleType.Number, Value = AgregaPersona.nGestante, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.Observacion, OracleType = OracleType.VarChar, Value = AgregaPersona.cComentarios, Direction = ParameterDirection.Input });
                d.AddParameter(new OracleParameter { ParameterName = Parametros.nIdCreadoPersona, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

                try
                {
                    d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.AgregaRegPersonaValoracion, tra, ref cError);
                    return int.Parse(d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.PersonaAgregadaId).ToString());
                }
                catch (Exception ex)
                {
                    RegistroTraza.I.Registrar(this.GetType().Name + ":::AgregaRegPersonaValoracion", ex);
                    cError = ex.Message;
                    return 0;
                }
            }
        }

        public bool AgregaDiscapacidadValoracion(clsAgregarPersonaValoracion AgregaPersona, int nIdRegPersona, DbTransaction tra, ref string cError)
        {
            foreach (int n in AgregaPersona.lnDiscapacidad)
            {
                using (Dao d = new Dao())
                {
                    d.AddParameter(new OracleParameter { ParameterName = Parametros.IdRegistroPersona, OracleType = OracleType.Number, Value = nIdRegPersona, Direction = ParameterDirection.Input });
                    d.AddParameter(new OracleParameter { ParameterName = Parametros.IdDiscapacidad, OracleType = OracleType.Number, Value = n, Direction = ParameterDirection.Input });
                    try
                    {
                        d.ExecuteNonQuery(Procedimientos.AgregaDiscapacidadValoracion, tra, ref cError);
                        if (!(cError == null || cError == string.Empty)) return false;
                    }
                    catch (Exception ex)
                    {
                        RegistroTraza.I.Registrar(this.GetType().Name + ":::AgregaDiscapacidadValoracion", ex);
                        cError = ex.Message;
                        return false;
                    }
                }
            }
            return true;
        }

        public List<clsCargaPersonasAsociadasDeclaracion> CargaPersonasAsociadasVal(int nIdDeclaracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter()
            {
                ParameterName = Parametros.IdDeclaracion,
                OracleType = OracleType.VarChar,
                IsNullable = true,
                Value = nIdDeclaracion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter()
            {
                ParameterName = Parametros.Resultado,
                OracleType = OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Procedimientos.CargaPersonasAsociadas, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::CargaPersonasAsociadasVal", ex);
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsCargaPersonasAsociadasDeclaracion>(dr, true);

        }

        public int CargaPersonasAsociadasCount(int nIdDeclaracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.NumeroFormulario, OracleType = OracleType.Number, Value = nIdDeclaracion, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.ResultadoConteo, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

            try
            {
                d.ExecuteNonQuery(Procedimientos.ContadorPersonasAsociadasDeclaracion, null, ref cError);
                if (!(cError == null || cError == string.Empty)) return 0;
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::CargaPersonasAsociadasCount", ex);
                cError = ex.Message;
                return 0;
            }

            return int.Parse(d.GetOutputParameter(Parametros.ResultadoConteo).ToString());
        }

        public int ObtenerIdValoracionporIdDeclaracion(int nIdDeclaracion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter()
            {
                ParameterName = Parametros.IdDeclaracion,
                OracleType = OracleType.Number,
                IsNullable = true,
                Value = nIdDeclaracion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter()
            {
                ParameterName = Parametros.IdValoracionOut,
                OracleType = OracleType.Number,
                Direction = ParameterDirection.Output
            });

            int idValoracion = 0;

            try
            {
                d.ExecuteNonQuery(Procedimientos.ObtieneIdValoracionporDeclaracion, null, ref cError);
                idValoracion = int.Parse(d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.IdValoracionOut).ToString());
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(this.GetType().Name + ":::ObtenerIdValoracionporIdDeclaracion", ex);
                cError = ex.Message;
                return 0;
            }

            if (!(cError == null || cError == string.Empty)) return 0;

            return idValoracion;

        }

    }

}
