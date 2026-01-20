using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Ruv.Infrastructure.Crosscutting.Resources;
using Ruv.Infrastructure.Crosscutting.Resources.DB;

namespace Ruv.Data.Radicacion
{
    public class entRadicacion : entidadRUV
    {
        public void setRadicacion(TBRADICACION objRadicacion, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RADICACION.SP_SETRADICACION", getParametros(objRadicacion));
            dbRUV.ExecuteNonQuery(cmd, tran);
            objRadicacion.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_ID"));
        }

        public List<TBRADICACION> getRadicacionByFUD(string FUD)
        {
            List<TBRADICACION> Resultado = new List<TBRADICACION>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RADICACION.sp_getRadicacionPorFUD", new object[] { FUD, null }))
            {
                while (dataReader.Read())
                {
                    try
                    {
                        TBRADICACION RegistroRad = EnterpriseLibraryContainer.Current.GetInstance<TBRADICACION>();
                        RegistroRad.ID = (Int32)dbDefaults.getInt32(dataReader, 0);
                        RegistroRad.FECHAREGISTRO = (DateTime)dbDefaults.getDateTime(dataReader, 1);
                        RegistroRad.ID_MUNICIPIO = dbDefaults.getInt32(dataReader, 3);
                        RegistroRad.FECHALLEGADA = dbDefaults.getDateTime(dataReader, 9);
                        RegistroRad.ID_USUARIO_RADICA = dbDefaults.getInt32(dataReader, 12);
                        RegistroRad.ID_DECLARACION = dbDefaults.getInt32(dataReader, 18);
                        Resultado.Add(RegistroRad);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    
                }
            }
            return Resultado;
        }

        public List<TBRADICACION> getRadicacion(int ID)
        {
            List<TBRADICACION> Resultado = new List<TBRADICACION>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getDeclaracion", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBRADICACION RegistroRad = EnterpriseLibraryContainer.Current.GetInstance<TBRADICACION>();
                    RegistroRad.ID = (Int32)dbDefaults.getInt32(dataReader, 0);
                    RegistroRad.FECHAREGISTRO = (DateTime)dbDefaults.getDateTime(dataReader, 1);
                    RegistroRad.CONSECUTIVO = (Int64)dbDefaults.getInt64(dataReader, 2);
                    RegistroRad.ID_MUNICIPIO = dbDefaults.getInt32(dataReader, 3);
                    RegistroRad.ID_UTERRITORIALENVIA = dbDefaults.getInt32(dataReader, 4);
                    RegistroRad.ID_UTERRITORIALRECIBE = dbDefaults.getInt32(dataReader, 5);
                    RegistroRad.PARAM_TIPOENTIDAD = dbDefaults.getInt32(dataReader, 6);
                    RegistroRad.NOMBREENTIDAD = dbDefaults.getString(dataReader, 7);
                    RegistroRad.FECHAENVIO = dbDefaults.getDateTime(dataReader, 8);
                    RegistroRad.FECHALLEGADA = dbDefaults.getDateTime(dataReader, 9);
                    RegistroRad.CANTIDADDOCUMENTOS = dbDefaults.getInt32(dataReader, 10);
                    RegistroRad.ID_UTERRITORIALRADICA = dbDefaults.getInt16(dataReader, 11);
                    RegistroRad.ID_USUARIO_RADICA = dbDefaults.getInt32(dataReader, 12);
                    RegistroRad.ID_RADICA_URGENCIA = dbDefaults.getInt32(dataReader, 13);
                    RegistroRad.PARAM_TIPOACCIONES = dbDefaults.getInt32(dataReader, 14);
                    RegistroRad.MODIFICACION = dbDefaults.getInt16(dataReader, 14);
                    RegistroRad.PARAM_ENTIDADENVIANOMBRE = dbDefaults.getInt32(dataReader, 16);
                    RegistroRad.ID_TIPODOCUMENTAL = dbDefaults.getInt32(dataReader, 17);
                    //RegistroRad.ID_TIPO_RADICACION = dbDefaults.getInt32(dataReader, 18);
                    //RegistroRad.OBSERVACIONES = dbDefaults.getString(dataReader, 19);

                    Resultado.Add(RegistroRad);
                }
            }
            return Resultado;
        }

        public void updateRadicacion(TBRADICACION objRadicacion, DbTransaction tra)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RADICACION.SP_UPDRADICACION", getUpdParametros(objRadicacion));
            dbRUV.ExecuteNonQuery(cmd, tra);
        }

        private object[] getParametros(TBRADICACION objRad)
        {
            return new object[]{
                                     null
                                    ,objRad.ID_MUNICIPIO
                                    ,objRad.PARAM_TIPOENTIDAD
                                    ,objRad.FECHALLEGADA
                                    ,objRad.ID_USUARIO_RADICA
                                    ,objRad.NRO_FORMULARIO
                                    ,objRad.ID_TIPO_RADICACION
                                    ,objRad.OBSERVACIONES
                                    ,objRad.ID_ENTIDADMUNICIPIO
                                    ,(objRad.ID_DECLARACION != 0) ? objRad.ID_DECLARACION : null
                                    ,objRad.PARAM_RESULTADO_VALIDACION
            };
        }

        private object[] getUpdParametros(TBRADICACION objRad)
        {
            return new object[]{   
                                     objRad.ID
                                    ,objRad.ID_MUNICIPIO
                                    ,objRad.PARAM_TIPOENTIDAD
                                    ,objRad.NRO_FORMULARIO
                                    ,objRad.ID_TIPO_RADICACION
                                    ,objRad.OBSERVACIONES
                                    ,objRad.RUTAIMAGEN
                                    ,objRad.ID_ENTIDADMUNICIPIO
                                    ,null
            };
            //No enviar objRad.PARAM_RESULTADO_VALIDACION, ya que sobreescribe el que se pone en "SP_SETRADICACION"
        }

        public void ActualizarEstadoDeclaracion(int DeclaracionId, int UsuarioId, int Estado, DbTransaction tran)
        {
            Dao.SConection = General.CadenaConexionODAC;

            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.IdDeclaracion, OracleType = OracleType.Number, Value = DeclaracionId, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.IdUsuario, OracleType = OracleType.Number, Value = UsuarioId, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter() { ParameterName = Parametros.ParametroEstado, OracleType = OracleType.Number, Value = Estado, Direction = ParameterDirection.Input });

            string error = string.Empty;
            d.ExecuteNonQuery(Procedimientos.ActualizarEstadoDeclaracion, tran, ref error);

            if (!string.IsNullOrEmpty(error))
            {
                throw new ArgumentException("Error");
            }
        }

        public Boolean ExisteNumeroFormulario()
        {
            return true;
        }
    }
}
