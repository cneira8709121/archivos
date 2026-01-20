using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Objects;

namespace Ruv.Data.Reconocimiento
{
    /// <summary>
    /// Clase que se encarga de realizar las operaciones CRUD, para la entidad GLOSAS de la base de datos
    /// </summary>
    public class entGlosas : entidadRUV
    {
        #region Guardar Datos
        /// <summary>
        /// Crear una Glosa
        /// </summary>
        /// <param name="objGlosas"></param>
        /// <returns></returns>
        public Int32 setGlosas(TBGLOSAS objGlosas, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_GLOSAS.SP_SETGLOSAS", getParametros(objGlosas));
            dbRUV.ExecuteNonQuery(cmd, tran);
            objGlosas.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
            return Convert.ToInt32(objGlosas.ID);
        }
        public void updGlosas(TBGLOSAS objGlosas, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_GLOSAS.SP_UPDGLOSAS", getParametros(objGlosas));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }
        private object[] getParametros(TBGLOSAS objData)
        {
            return new object[]{  
                                 objData.ID
                                ,objData.PARAM_PROCESO
                                ,objData.ID_PROCESO
                                ,objData.PARAM_CATEGORIAGLOSA
                                ,objData.PARAM_CONCEPTOGLOSA
                                ,objData.DESCRIPCIONGLOSA
                                ,objData.FECHAGLOSA
                                ,objData.FECHAATENCION
                                ,objData.FECHAESPERADAATEN
                                ,objData.GLOSAATEND
                                ,objData.GLOSANOATEND
                                ,objData.MOTIVONOATEN
                                ,objData.ID_USUARIOCREA
                                ,objData.ID_USUARIOATIENDE
                                ,objData.ID_USUARIOCOORDINA
                                ,objData.MOTIVOSIATEN
                                ,objData.DEVOLUCION
                                ,objData.PARAM_CONCEPTODEVOLUCION
                                ,objData.ID_USUARIO         
                                ,objData.ID_UTERRITORIAL
                                ,objData.PARAM_ESTADOGLOSA
                                ,null
            };
        }

        #endregion

        #region Obtener Datos
        public List<TBGLOSAS> getGlosasXdeclaracion(int ID_PROCESO)
        {
            List<TBGLOSAS> registros = new List<TBGLOSAS>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_GLOSAS.SP_GETGLOSAS", new object[] { ID_PROCESO, null }))
            {
                while (dataReader.Read())
                {
                    int index = 0;
                    TBGLOSAS glosa = EnterpriseLibraryContainer.Current.GetInstance<TBGLOSAS>();
                    glosa.ID                        = (int)dbDefaults.getInt32(dataReader, index++);
                    glosa.PARAM_PROCESO = (int)dbDefaults.getInt32(dataReader, index++);
                    glosa.ID_PROCESO = (int)dbDefaults.getInt32(dataReader, index++);
                    glosa.PARAM_CATEGORIAGLOSA      = (int)dbDefaults.getInt32(dataReader, index++);
                    glosa.PARAM_CONCEPTOGLOSA       = (int)dbDefaults.getInt32(dataReader, index++);
                    glosa.DESCRIPCIONGLOSA          = dbDefaults.getString(dataReader, index++);
                    glosa.FECHAGLOSA                = dbDefaults.getDateTime(dataReader, index++);
                    glosa.FECHAATENCION             = dbDefaults.getDateTime(dataReader, index++);
                    glosa.FECHAESPERADAATEN         = dbDefaults.getDateTime(dataReader, index++);
                    glosa.GLOSAATEND                = dbDefaults.getInt16(dataReader, index++);
                    glosa.GLOSANOATEND              = dbDefaults.getInt16(dataReader, index++);
                    glosa.MOTIVONOATEN              = dbDefaults.getString(dataReader, index++);
                    glosa.ID_USUARIOCREA            = dbDefaults.getInt32(dataReader, index++);
                    glosa.ID_USUARIOATIENDE         = dbDefaults.getInt32(dataReader, index++);
                    glosa.ID_USUARIOCOORDINA        = dbDefaults.getInt32(dataReader, index++);
                    glosa.MOTIVOSIATEN              = dbDefaults.getString(dataReader, index++);
                    glosa.DEVOLUCION                = dbDefaults.getInt16(dataReader, index++);
                    glosa.PARAM_CONCEPTODEVOLUCION  = dbDefaults.getInt32(dataReader, index++);
                    glosa.ID_USUARIO                = dbDefaults.getInt32(dataReader, index++);
                    glosa.ID_UTERRITORIAL           = dbDefaults.getInt16(dataReader, index++);
                    glosa.PARAM_ESTADOGLOSA         = dbDefaults.getInt32(dataReader, index++);
                    registros.Add(glosa);
                }
            }
            return registros;
        }

        #endregion
    }
}
