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
    /// Clase que se encarga de realizar las operaciones CRUD, para la entidad Intenciones de GLOSA de la base de datos
    /// </summary>
    public class entIntecionesGlosas : entidadRUV
    {
        #region Guardar Datos
        /// <summary>
        /// Crear una Intención de Glosa
        /// </summary>
        /// <param name="objGlosas"></param>
        /// <returns></returns>
        public Int32 setIGlosas(TBGLOSAINTENCION objIGlosas, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_GLOSAS.SP_SETINGLOSAS", getParametros(objIGlosas));
            dbRUV.ExecuteNonQuery(cmd, tran);
            objIGlosas.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
            return Convert.ToInt32(objIGlosas.ID);
        }
        public void updIGlosas(TBGLOSAINTENCION objIGlosas, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_GLOSAS.SP_UPDINTENCIONGLOSAS", getParametros(objIGlosas));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBGLOSAINTENCION objData)
        {
            return new object[]{  
                                 objData.ID
                                ,objData.ID_PROCESO            
                                ,objData.PARAM_CATEGORIAINGLOSA
                                ,objData.DESCRIPCIONINGLOSA    
                                ,objData.FECHAINGLOSA          
                                ,objData.FECHAATENCION         
                                ,objData.FECHAESPERADAATEN     
                                ,objData.GLOSAATEND            
                                ,objData.GLOSANOATEND          
                                ,objData.MOTIVONOATEN          
                                ,objData.ID_USUARIOCREA        
                                ,objData.ID_USUARIOATIENDE     
                                ,objData.ID_USUARIOCOORDINA    
                                ,objData.ID_USUARIO            
                                ,objData.ID_UTERRITORIAL       
                                ,objData.PARAM_PROCESO
                                ,objData.PARAM_ESTADOGLOSA
                                ,null
            };
        }
        #endregion

        #region Obtener Datos
        public List<TBGLOSAINTENCION> getIGlosasXdeclaracion(int ID_PROCESO)
        {
            List<TBGLOSAINTENCION> registros = new List<TBGLOSAINTENCION>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_GLOSAS.SP_GETINTENCIONGLOSAS", new object[] { ID_PROCESO, null }))
            {
                while (dataReader.Read())
                {
                    int index = 0;
                    TBGLOSAINTENCION iglosa = EnterpriseLibraryContainer.Current.GetInstance<TBGLOSAINTENCION>();
                    iglosa.ID                      = (int)dbDefaults.getInt32(dataReader, index++);
                    iglosa.ID_PROCESO              = (int)dbDefaults.getInt32(dataReader, index++);
                    iglosa.PARAM_CATEGORIAINGLOSA  = (int)dbDefaults.getInt32(dataReader, index++);
                    iglosa.DESCRIPCIONINGLOSA      = dbDefaults.getString(dataReader, index++);
                    iglosa.FECHAINGLOSA            = dbDefaults.getDateTime(dataReader, index++);
                    iglosa.FECHAATENCION           = dbDefaults.getDateTime(dataReader, index++);
                    iglosa.FECHAESPERADAATEN       = dbDefaults.getDateTime(dataReader, index++);
                    iglosa.GLOSAATEND              = dbDefaults.getInt16(dataReader, index++);
                    iglosa.GLOSANOATEND            = dbDefaults.getInt16(dataReader, index++);
                    iglosa.MOTIVONOATEN            = dbDefaults.getString(dataReader, index++);
                    iglosa.ID_USUARIOCREA          = dbDefaults.getInt32(dataReader, index++);
                    iglosa.ID_USUARIOATIENDE       = dbDefaults.getInt32(dataReader, index++);
                    iglosa.ID_USUARIOCOORDINA      = dbDefaults.getInt32(dataReader, index++);
                    iglosa.ID_USUARIO              = dbDefaults.getInt32(dataReader, index++);
                    iglosa.ID_UTERRITORIAL         = dbDefaults.getInt16(dataReader, index++);
                    iglosa.PARAM_PROCESO           = (int)dbDefaults.getInt32(dataReader, index++);
                    iglosa.PARAM_ESTADOGLOSA       = dbDefaults.getInt32(dataReader, index++);

                    registros.Add(iglosa);

                }
            }
            return registros;
        }

        #endregion
    }
}
