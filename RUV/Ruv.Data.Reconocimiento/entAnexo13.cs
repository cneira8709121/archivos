using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Ruv.Data.Reconocimiento
{
    public class entAnexo13 : entidadRUV
    {
        #region Guardar
        public void setAnexo13(TBANEXO13 objeAnexo13, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo13", getParametros(objeAnexo13));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo13.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }
        public void updAnexo13(TBANEXO13 objeAnexo13, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo13", getParametros(objeAnexo13));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }
        private object[] getParametros(TBANEXO13 objeAnexo)
        {
            return new object[]{
                                   objeAnexo.ID
                                  ,objeAnexo.TBSINIESTROS_PERSONA.ID
                                  ,objeAnexo.TBREGISTROS_PERSONAS.ID
                                  ,objeAnexo.ACTIVO
                                  ,null
            };
        }

        public void setAnexo13_mensajes(TBANEXO13_MENSAJE objeAnexo13, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo13_mensaje", getParametrosMensajes(objeAnexo13));

            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        public void updAnexo13_mensajes(TBANEXO13_MENSAJE objeAnexo13, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo13_mensaje", getParametrosMensajes(objeAnexo13));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametrosMensajes(TBANEXO13_MENSAJE objeAnexo)
        {
            return new object[]{
                                   objeAnexo.ID_SINIESTRO
                                  ,objeAnexo.MENSAJE_CELULAR
                                  ,objeAnexo.MENSAJE_CORREOE
                                  ,objeAnexo.MENSAJE_FIJO
                                  ,objeAnexo.MENSAJE_OTRO
                                  ,objeAnexo.ACTIVO
            };


        }

        public void sp_setAnexo13_siniestro(TBANEXO13_SINIESTRO objeAnexo13siniestro, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo13_siniestro", getParametrosAnexo13siniestro(objeAnexo13siniestro));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametrosAnexo13siniestro(TBANEXO13_SINIESTRO objeAnexo13siniestro)
        {
            return new object[]{
                                    objeAnexo13siniestro.ID_SINIESTRO_ANEXO13
                                   ,objeAnexo13siniestro.ID_SINIESTRO
                                    ,objeAnexo13siniestro.ID
                                  
            };


        }


        #endregion

        #region Obtener
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Siniestro.</param>
        /// <returns></returns>
        public List<TBANEXO13> getData(int ID)
        {
            List<TBANEXO13> registros = new List<TBANEXO13>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos13", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO13 registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO13>();
                    registro.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
                    registro.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

                    int index = 0;

                    #region Common Anexos
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBSINIESTROS_PERSONA.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.ID_SINIESTRO = registro.TBSINIESTROS_PERSONA.ID;
                    registro.ID_REGPERSONA = registro.TBREGISTROS_PERSONAS.ID;
                    #endregion

                    registro.ACTIVO = (short)dbDefaults.getInt16(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }

        public TBANEXO13_MENSAJE getDataMensaje(int ID)
        {
            TBANEXO13_MENSAJE registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO13_MENSAJE>();

            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos13_mensajes", new object[] { ID, null }))
            {
                if (dataReader.Read())
                {

                    int index = 0;

                    #region Common Anexos
                    registro.ID_SINIESTRO = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.MENSAJE_CELULAR = (short)dbDefaults.getInt32(dataReader, index++);
                    registro.MENSAJE_CORREOE = (short)dbDefaults.getInt32(dataReader, index++);
                    registro.MENSAJE_FIJO = (short)dbDefaults.getInt32(dataReader, index++);
                    registro.MENSAJE_OTRO = dbDefaults.getString(dataReader, index++);
                    #endregion

                    registro.ACTIVO = (short)dbDefaults.getInt16(dataReader, index++);

                }
            }
            return registro;
        }

        public List<TBANEXO13_SINIESTRO> getDataAnexo13Siniestro(int ID_SINIESTRO_ANEXO13)
        {
            List<TBANEXO13_SINIESTRO> siniestrosAnexo13 = new List<TBANEXO13_SINIESTRO>();

            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getAnexos13_siniestro", new object[] { ID_SINIESTRO_ANEXO13, null }))
            {
                if (dataReader.Read())
                {
                    TBANEXO13_SINIESTRO registro = new TBANEXO13_SINIESTRO();
                    int index = 0;

                    #region Common Anexos
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.ID_SINIESTRO = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.ID_SINIESTRO_ANEXO13 = (int)dbDefaults.getInt32(dataReader, index++);
                    #endregion
                    siniestrosAnexo13.Add(registro);
                }
            }
            return siniestrosAnexo13;
        }

        #endregion
    }
}
