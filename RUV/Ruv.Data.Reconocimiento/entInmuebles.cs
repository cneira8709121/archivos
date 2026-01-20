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
    public class entInmuebles : entidadRUV
    {
        public void setAnexo11_Inmuebles(TBANEXO11_INMUEBLES objeAnexo11_Inmuebles, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setAnexo11_Inmuebles", getParametros(objeAnexo11_Inmuebles));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeAnexo11_Inmuebles.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updAnexo11_Inmuebles(TBANEXO11_INMUEBLES objeAnexo11_Inmuebles, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updAnexo11_Inmuebles", getParametros(objeAnexo11_Inmuebles));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBANEXO11_INMUEBLES objeAnexo11_Inmuebles)
        {
            return new object[]{  
                                    objeAnexo11_Inmuebles.ID
                                  , objeAnexo11_Inmuebles.TBANEXO11.ID
                                  , objeAnexo11_Inmuebles.TBREGISTROS_PERSONAS.ID
                                  , objeAnexo11_Inmuebles.PARAM_TIPO_INMUBLE
                                  , objeAnexo11_Inmuebles.ID_DEPARTAMENTO
                                  , objeAnexo11_Inmuebles.ID_MUNICIPIO
                                  , objeAnexo11_Inmuebles.ID_ENTRONO
                                  , objeAnexo11_Inmuebles.ID_TIPOPOBLADO
                                  , objeAnexo11_Inmuebles.OTRO_ENTORNO
                                  , objeAnexo11_Inmuebles.PARAM_TIPO_TENENCIA
                                  , objeAnexo11_Inmuebles.NOMBRE_DIRECCION
                                  , objeAnexo11_Inmuebles.AREA
                                  , objeAnexo11_Inmuebles.PARAM_UNIDAD_AREA
                                  , objeAnexo11_Inmuebles.ACTIVO
                                  , objeAnexo11_Inmuebles.PARAM_LOCALIDAD_CORREG
                                  , objeAnexo11_Inmuebles.PARAM_BARRIO_VEREDA
                                  , objeAnexo11_Inmuebles.OTRO_LOCALIDAD_CORREG
                                  , objeAnexo11_Inmuebles.OTRO_BARRIO_VEREDA
                                  , objeAnexo11_Inmuebles.PARAM_TIPO_ENTORNO
                                  , null
            };
        }

        #region Obtener
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID Anexo11.</param>
        /// <returns></returns>
        public List<TBANEXO11_INMUEBLES> getData(int ID)
        {
            List<TBANEXO11_INMUEBLES> registros = new List<TBANEXO11_INMUEBLES>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_geInmuebleA11", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBANEXO11_INMUEBLES registro = EnterpriseLibraryContainer.Current.GetInstance<TBANEXO11_INMUEBLES>();
                    registro.TBANEXO11 = new TBANEXO11();
                    registro.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

                    int index = 0;
                                        
                    registro.ID                         = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBANEXO11.ID               = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBREGISTROS_PERSONAS.ID    = (int)dbDefaults.getInt32(dataReader, index++);                                  
                    registro.PARAM_TIPO_INMUBLE         = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_DEPARTAMENTO            = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_MUNICIPIO               = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_ENTRONO                 = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_TIPOPOBLADO             = dbDefaults.getInt32(dataReader, index++);
                    registro.OTRO_ENTORNO               = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_TIPO_TENENCIA        = dbDefaults.getInt32(dataReader, index++);
                    registro.NOMBRE_DIRECCION           = dbDefaults.getString(dataReader, index++);
                    registro.AREA                       = dbDefaults.getDecimal(dataReader, index++);
                    registro.PARAM_UNIDAD_AREA          = dbDefaults.getInt32(dataReader, index++);
                    registro.ACTIVO                     = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_LOCALIDAD_CORREG     = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_BARRIO_VEREDA        = dbDefaults.getInt32(dataReader, index++);
                    registro.OTRO_LOCALIDAD_CORREG      = dbDefaults.getString(dataReader, index++);
                    registro.OTRO_BARRIO_VEREDA         = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_TIPO_ENTORNO         = dbDefaults.getInt32(dataReader, index++);

                    registros.Add(registro);
                }
            }
            return registros;
        }

        #endregion
    }
}
