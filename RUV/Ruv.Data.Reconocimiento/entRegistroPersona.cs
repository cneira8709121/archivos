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
    public class entRegistroPersona : entidadRUV
    {
        #region Guardar Datos
        public void setData(TBREGISTROS_PERSONAS objeRegPersona, DbTransaction tran)
        {
            object[] parametros = getParametros(objeRegPersona);
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setRegitroPersona", parametros);

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeRegPersona.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void insertVictimaFirma(int idRegPersona, byte[] firma, DbTransaction tran)
        {
            DbCommand command = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setRegPersonaFirma", new object[] { idRegPersona, firma });
            dbRUV.ExecuteNonQuery(command, tran);
        }

        public void updateData(TBREGISTROS_PERSONAS objeRegPersona, DbTransaction tran)
        {
            object[] parametros = getParametros(objeRegPersona);
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updRegitroPersona", parametros);

            dbRUV.ExecuteNonQuery(cmd, tran);
            objeRegPersona.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updateVictimaFirma(int idRegPersona, byte[] firma, DbTransaction tran)
        {
            DbCommand command = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updRegPersonaFirma", new object[] { idRegPersona, firma });
            dbRUV.ExecuteNonQuery(command, tran);
        }

        private object[] getParametros(TBREGISTROS_PERSONAS objeRegPersona)
        {
            return new object[]{
                                objeRegPersona.ID
                              , objeRegPersona.TBDECLARACIONES.ID
                              , objeRegPersona.TBPERSONAS.ID
                              , objeRegPersona.ESDECLARANTE
                              , objeRegPersona.CARACTERIZADO
                              , objeRegPersona.ACTIVO
                              , objeRegPersona.ID_MIJEFEHOGAR
                              , objeRegPersona.PARAM_ESTADOAYUDAS
                              , objeRegPersona.PUNTAJE_PERSONA
                              , objeRegPersona.ID_PROCESO
                              , objeRegPersona.PARAM_PROCESO
                              , objeRegPersona.ID_USUARIO
                              , objeRegPersona.ID_UTERRITORIAL
                              , objeRegPersona.DIRECCION
                              , objeRegPersona.TELEFONO
                              , objeRegPersona.MOVIL
                              , objeRegPersona.PARAM_RELACION
                              , objeRegPersona.SEDESPLAZO
                              , objeRegPersona.RESTRINGIDA
                              , objeRegPersona.NOVEDAD_INCLUSION
                              , objeRegPersona.OBS_RESTRINGIDA
                              , objeRegPersona.TIPO_RESTRICCION
                              , objeRegPersona.EMAIL
                              , objeRegPersona.DIRECCION_ALTERNA
                              , objeRegPersona.ID_ENTORNO_ALTERNO
                              , objeRegPersona.OTRO_ENTORNO_ALTERNO
                              , objeRegPersona.ID_DEPARTAMENTO_ALTERNO
                              , objeRegPersona.ID_MUNICIPIO_ALTERNO
                              , objeRegPersona.TELEFONO_ALTERNO
                              , objeRegPersona.MOVIL_ALTERNO
                              , objeRegPersona.EMAIL_ALTERNO
                              , objeRegPersona.CONSECUTIVO_PERSONA
                              , objeRegPersona.ESMUJERCABEZADEHOGAR
                              , objeRegPersona.PARAM_REGIMENESPECIAL
                              , objeRegPersona.GESTANTE_LACTANTE
                              , objeRegPersona.ID_DEPARTAMENTO
                              , objeRegPersona.ID_MUNICIPIO
                              , objeRegPersona.ID_ENTORNO
                              , objeRegPersona.OTRO_ENTORNO
                              , objeRegPersona.ID_TIPOPOBLACION
                              , objeRegPersona.ID_TIPOPOBLACION_ALTERNO
                              , objeRegPersona.PARAM_LOCALIDAD_CORREG
                              , objeRegPersona.PARAM_BARRIO_VEREDA
                              , objeRegPersona.OTRO_LOCALIDAD_CORREG
                              , objeRegPersona.OTRO_BARRIO_VEREDA
                              , objeRegPersona.PARAM_TIPO_ENTORNO
                              , objeRegPersona.PARAM_TIPO_ENTORNO_ALT
                              , objeRegPersona.PARAM_LOCALIDAD_CORREG_ALT
                              , objeRegPersona.PARAM_BARRIO_VEREDA_ALT
                              , objeRegPersona.OTRO_LOCALIDAD_CORREG_ALT
                              , objeRegPersona.OTRO_BARRIO_VEREDA_ALT
                              , objeRegPersona.ID_PAIS
                              , objeRegPersona.ID_PAIS_ALTERNO 
                              , objeRegPersona.CONSECUTIVO_FAMILIA  
                              , objeRegPersona.INDICATIVO_TELEFONO
                              , objeRegPersona.INDICATIVO_TELEFONO_ALTERNO
                              , objeRegPersona.ID_NACIONALIDAD
                              , objeRegPersona.ESHOMBRECABEZADEHOGAR
                              , objeRegPersona.CAMPESINADO
                              , objeRegPersona.PERSONA_BUSCADORA
                              , null
            };
        }

        #endregion

        #region Obtener Datos
        /// <summary>
        /// Obtener registros personas de una declaración.
        /// </summary>
        /// <param name="ID">ID de la declaración</param>
        /// <returns></returns>
        public List<TBREGISTROS_PERSONAS> getData(int ID, int FamiliaConsecutivo)
        {
            List<TBREGISTROS_PERSONAS> registros = new List<TBREGISTROS_PERSONAS>();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getRegistrosPersonas", new object[] { ID,FamiliaConsecutivo, null }))
            {
                while (dataReader.Read())
                {
                    TBREGISTROS_PERSONAS registro = EnterpriseLibraryContainer.Current.GetInstance<TBREGISTROS_PERSONAS>();
                    registro.TBDECLARACIONES = new TBDECLARACIONES();
                    registro.TBPERSONAS = new TBPERSONAS();

                    int index = 0;
                                                                
                    registro.ID                         = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.TBDECLARACIONES.ID         = (int)dbDefaults.getInt32(dataReader, index++);  
                    registro.TBPERSONAS.ID              = (int)dbDefaults.getInt32(dataReader, index++);  
                    registro.ESDECLARANTE               = dbDefaults.getInt16(dataReader, index++);  
                    registro.CARACTERIZADO              = dbDefaults.getInt16(dataReader, index++);  
                    registro.ACTIVO                     = dbDefaults.getInt16(dataReader, index++);  
                    registro.ID_MIJEFEHOGAR             = dbDefaults.getInt32(dataReader, index++);  
                    registro.PARAM_ESTADOAYUDAS         = dbDefaults.getInt32(dataReader, index++);  
                    registro.PUNTAJE_PERSONA            = dbDefaults.getInt32(dataReader, index++);  
                    registro.ID_PROCESO                 = dbDefaults.getInt32(dataReader, index++);  
                    registro.PARAM_PROCESO              = dbDefaults.getInt32(dataReader, index++);  
                    registro.ID_USUARIO                 = dbDefaults.getInt32(dataReader, index++);  
                    registro.ID_UTERRITORIAL            = dbDefaults.getInt16(dataReader, index++);  
                    registro.DIRECCION                  = dbDefaults.getString(dataReader, index++);  
                    registro.TELEFONO                   = dbDefaults.getString(dataReader, index++);  
                    registro.MOVIL                      = dbDefaults.getString(dataReader, index++);  
                    registro.PARAM_RELACION             = dbDefaults.getInt32(dataReader, index++);  
                    registro.SEDESPLAZO                 = dbDefaults.getInt16(dataReader, index++);  
                    registro.RESTRINGIDA                = dbDefaults.getInt16(dataReader, index++);  
                    registro.NOVEDAD_INCLUSION          = dbDefaults.getInt16(dataReader, index++);  
                    registro.OBS_RESTRINGIDA            = dbDefaults.getString(dataReader, index++);  
                    registro.TIPO_RESTRICCION           = dbDefaults.getInt32(dataReader, index++);  
                    registro.EMAIL                      = dbDefaults.getString(dataReader, index++);  
                    registro.DIRECCION_ALTERNA          = dbDefaults.getString(dataReader, index++);  
                    registro.ID_ENTORNO_ALTERNO         = dbDefaults.getInt32(dataReader, index++);  
                    registro.OTRO_ENTORNO_ALTERNO       = dbDefaults.getString(dataReader, index++);
                    registro.ID_PAIS_ALTERNO            = dbDefaults.getInt64(dataReader, index++);  
                    registro.ID_DEPARTAMENTO_ALTERNO    = dbDefaults.getInt64(dataReader, index++);  
                    registro.ID_MUNICIPIO_ALTERNO       = dbDefaults.getInt64(dataReader, index++);  
                    registro.TELEFONO_ALTERNO           = dbDefaults.getString(dataReader, index++);  
                    registro.MOVIL_ALTERNO              = dbDefaults.getString(dataReader, index++);  
                    registro.EMAIL_ALTERNO              = dbDefaults.getString(dataReader, index++);  
                    registro.CONSECUTIVO_PERSONA        = dbDefaults.getInt16(dataReader, index++);  
                    registro.ESMUJERCABEZADEHOGAR       = dbDefaults.getInt16(dataReader, index++);  
                    registro.PARAM_REGIMENESPECIAL      = dbDefaults.getInt32(dataReader, index++);  
                    registro.GESTANTE_LACTANTE          = dbDefaults.getInt16(dataReader, index++);
                    registro.ID_PAIS                    = dbDefaults.getInt64(dataReader, index++);
                    registro.ID_DEPARTAMENTO            = dbDefaults.getInt64(dataReader, index++);  
                    registro.ID_MUNICIPIO               = dbDefaults.getInt64(dataReader, index++);  
                    registro.ID_ENTORNO                 = dbDefaults.getInt32(dataReader, index++);  
                    registro.OTRO_ENTORNO               = dbDefaults.getString(dataReader, index++);  
                    registro.ID_TIPOPOBLACION           = dbDefaults.getInt32(dataReader, index++);  
                    registro.ID_TIPOPOBLACION_ALTERNO   = dbDefaults.getInt32(dataReader, index++);  
                    registro.PARAM_LOCALIDAD_CORREG     = dbDefaults.getInt32(dataReader, index++);    
                    registro.PARAM_BARRIO_VEREDA        = dbDefaults.getInt32(dataReader, index++);  
                    registro.OTRO_LOCALIDAD_CORREG      = dbDefaults.getString(dataReader, index++);  
                    registro.OTRO_BARRIO_VEREDA         = dbDefaults.getString(dataReader, index++);  
                    registro.PARAM_TIPO_ENTORNO         = dbDefaults.getInt32(dataReader, index++);  
                    registro.PARAM_TIPO_ENTORNO_ALT     = dbDefaults.getInt32(dataReader, index++);  
                    registro.PARAM_LOCALIDAD_CORREG_ALT = dbDefaults.getInt32(dataReader, index++);  
                    registro.PARAM_BARRIO_VEREDA_ALT    = dbDefaults.getInt32(dataReader, index++);  
                    registro.OTRO_LOCALIDAD_CORREG_ALT  = dbDefaults.getString(dataReader, index++);  
                    registro.OTRO_BARRIO_VEREDA_ALT     = dbDefaults.getString(dataReader, index++);
                    registro.CONSECUTIVO_FAMILIA        = dbDefaults.getInt16(dataReader, index++);
                    registro.INDICATIVO_TELEFONO        = dbDefaults.getInt16(dataReader, index++);
                    registro.INDICATIVO_TELEFONO_ALTERNO= dbDefaults.getInt16(dataReader, index++);
                    registro.ID_NACIONALIDAD            = dbDefaults.getInt32(dataReader, index++);
                    registro.ESHOMBRECABEZADEHOGAR      = dbDefaults.getInt16(dataReader, index++);
                    registro.CAMPESINADO                = dbDefaults.getInt16(dataReader, index++);
                    registro.PERSONA_BUSCADORA          = dbDefaults.getInt16(dataReader, index++);
                    registros.Add(registro);
                }
            }
            return registros;
        }
        #endregion
    }
}
