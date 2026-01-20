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
    public class entPersona : entidadRUV
    {
        #region Guardar Datos
        public void setData(TBPERSONAS objePersona, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setPersona", getParametros(objePersona));

            dbRUV.ExecuteNonQuery(cmd, tran);
            objePersona.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updateData(TBPERSONAS objePersona, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updPersona", getParametros(objePersona));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }
         
        private object[] getParametros(TBPERSONAS objePersona)
        {
            return new object[]{
                                  objePersona.ID                   
                                , objePersona.PRIMERNOMBRE                 
                                , objePersona.SEGUNDONOMBRE                
                                , objePersona.PRIMERAPELLIDO               
                                , objePersona.SEGUNDOAPELLIDO              
                                , objePersona.PARAM_TIPODOCUMENTO          
                                , objePersona.NUMERODOCUMENTO              
                                , objePersona.ID_DEPARTAMENTOEXPEDICION    
                                , objePersona.ID_MUNICIPIOEXPEDICION       
                                , objePersona.PARAM_ESTADOCIVIL            
                                , objePersona.PARAM_GENERO                 
                                , objePersona.PARAM_PROFESION              
                                , objePersona.CUALPROFESION                
                                , objePersona.ID_DEPARTAMENTO              
                                , objePersona.ID_MUNICIPIO                 
                                , objePersona.PARAM_MINORIAETNICA          
                                , objePersona.GESTANTE                     
                                , objePersona.PARAM_REGIMENSALUD           
                                , objePersona.LEEYESCRIBE                  
                                , objePersona.ASISTIAALAESCUELA            
                                , objePersona.ASISTEAESCUELA               
                                , objePersona.ULTIMOGRADO                  
                                , objePersona.PARAM_NIVELESCOLAR           
                                , objePersona.PARAM_OCUPACIONANTERIOR      
                                , objePersona.PARAM_ACTVIDADANTERIOR       
                                , objePersona.PARAM_OCUPACIONACTUAL        
                                , objePersona.PARAM_ACTVIDADACTUAL         
                                , objePersona.FECHANACIMIENTO              
                                , objePersona.ESTAFALLECIDO                
                                , objePersona.CUALREGIMENSALUD             
                                , objePersona.CUALACTIVIDADANTERIOR        
                                , objePersona.CUALACTIVIDADACTUAL          
                                , objePersona.PARAM_BIENESABANDONADOS      
                                , objePersona.PARAM_CREDITOSVIGENTES       
                                , objePersona.ID_PROCESO                   
                                , objePersona.PARAM_PROCESO                
                                , objePersona.ID_USUARIO                   
                                , objePersona.ID_UTERRITORIAL              
                                , objePersona.ID_ORIGENFUENTE              
                                , objePersona.OBSERVACIONES                
                                , objePersona.FECHAEXPEDICIONDOC           
                                , objePersona.REGISTRADURIA_1              
                                , objePersona.REGISTRADURIA_2              
                                , objePersona.ID_PERSONASIFA               
                                , objePersona.ID_PERSONAJUNTOS             
                                , objePersona.ESMUJERCABEZADEHOGAR         
                                , objePersona.ESMENORSINACUDIENTE          
                                , objePersona.PARAM_ETNIAPERTENECE         
                                , objePersona.PARAM_ROLCOMPLEMETARIO       
                                , objePersona.PARAM_RESGUARDO              
                                , objePersona.CUALETNIAOPUEBLO             
                                , objePersona.CUALROLCOMPLEMENTARIO        
                                , objePersona.CUALORGANIZACIONSOCIAL       
                                , objePersona.PARAM_PROTECCIONBIENES       
                                , objePersona.PORQUEPROTECCIONBIENES       
                                , objePersona.PARAM_PREDIOFUEDESPOJADO     
                                , objePersona.PARAM_FORMADESPOJO           
                                , objePersona.PARAM_AUTORDESPOJO           
                                , objePersona.PARAM_LEGISLAZIONTERRITORIO  
                                , objePersona.PARAM_AFECTACION             
                                , objePersona.COMUNAS_AFECTADAS            
                                , objePersona.PARAM_OTROSBIENESABANDONADOS 
                                , objePersona.VIGENCIA_REGISTRADURIA       
                                , objePersona.REGISTRADURIA                
                                , objePersona.COMUNIDAD
                                , objePersona.PARAM_ORIENTACIONSEXUAL
                                , objePersona.PARAM_IDENTIDADGENERO
                                , null
            };
        }
        #endregion


        #region Obtener Datos

        public TBPERSONAS getData(int ID)
        {
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getPersona", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBPERSONAS registro = EnterpriseLibraryContainer.Current.GetInstance<TBPERSONAS>();
                    int index = 0;
                    registro.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    registro.PRIMERNOMBRE = dbDefaults.getString(dataReader, index++);
                    registro.SEGUNDONOMBRE = dbDefaults.getString(dataReader, index++);
                    registro.PRIMERAPELLIDO = dbDefaults.getString(dataReader, index++);
                    registro.SEGUNDOAPELLIDO = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_TIPODOCUMENTO = dbDefaults.getInt32(dataReader, index++);
                    registro.NUMERODOCUMENTO = dbDefaults.getString(dataReader, index++);
                    registro.ID_DEPARTAMENTOEXPEDICION = dbDefaults.getInt16(dataReader, index++);
                    registro.ID_MUNICIPIOEXPEDICION = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_ESTADOCIVIL = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_GENERO = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_PROFESION = dbDefaults.getInt32(dataReader, index++);
                    registro.CUALPROFESION = dbDefaults.getString(dataReader, index++);
                    registro.ID_DEPARTAMENTO = dbDefaults.getInt16(dataReader, index++);
                    registro.ID_MUNICIPIO = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_MINORIAETNICA = dbDefaults.getInt32(dataReader, index++);
                    registro.GESTANTE = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_REGIMENSALUD = dbDefaults.getInt32(dataReader, index++);
                    registro.LEEYESCRIBE = dbDefaults.getInt16(dataReader, index++);
                    registro.ASISTIAALAESCUELA = dbDefaults.getInt16(dataReader, index++);
                    registro.ASISTEAESCUELA = dbDefaults.getInt16(dataReader, index++);
                    registro.ULTIMOGRADO = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_NIVELESCOLAR = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_OCUPACIONANTERIOR = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_ACTVIDADANTERIOR = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_OCUPACIONACTUAL = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_ACTVIDADACTUAL = dbDefaults.getInt32(dataReader, index++);
                    registro.FECHANACIMIENTO = dbDefaults.getDateTime(dataReader, index++);
                    registro.ESTAFALLECIDO = dbDefaults.getInt16(dataReader, index++);
                    registro.CUALREGIMENSALUD = dbDefaults.getString(dataReader, index++);
                    registro.CUALACTIVIDADANTERIOR = dbDefaults.getString(dataReader, index++);
                    registro.CUALACTIVIDADACTUAL = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_BIENESABANDONADOS = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_CREDITOSVIGENTES = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_PROCESO = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_PROCESO = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_USUARIO = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_UTERRITORIAL = dbDefaults.getInt16(dataReader, index++);
                    registro.ID_ORIGENFUENTE = dbDefaults.getInt16(dataReader, index++);
                    registro.OBSERVACIONES = dbDefaults.getString(dataReader, index++);
                    registro.FECHAEXPEDICIONDOC = dbDefaults.getDateTime(dataReader, index++);
                    registro.REGISTRADURIA_1 = dbDefaults.getInt16(dataReader, index++);
                    registro.REGISTRADURIA_2 = dbDefaults.getInt16(dataReader, index++);
                    registro.ID_PERSONASIFA = dbDefaults.getInt32(dataReader, index++);
                    registro.ID_PERSONAJUNTOS = dbDefaults.getInt32(dataReader, index++);
                    registro.ESMUJERCABEZADEHOGAR = dbDefaults.getInt16(dataReader, index++);
                    registro.ESMENORSINACUDIENTE = dbDefaults.getInt16(dataReader, index++);
                    registro.PARAM_ETNIAPERTENECE = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_ROLCOMPLEMETARIO = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_RESGUARDO = dbDefaults.getInt32(dataReader, index++);
                    registro.CUALETNIAOPUEBLO = dbDefaults.getString(dataReader, index++);
                    registro.CUALROLCOMPLEMENTARIO = dbDefaults.getString(dataReader, index++);
                    registro.CUALORGANIZACIONSOCIAL = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_PROTECCIONBIENES = dbDefaults.getInt32(dataReader, index++);
                    registro.PORQUEPROTECCIONBIENES = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_PREDIOFUEDESPOJADO = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_FORMADESPOJO = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_AUTORDESPOJO = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_LEGISLAZIONTERRITORIO = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_AFECTACION = dbDefaults.getInt32(dataReader, index++);
                    registro.COMUNAS_AFECTADAS = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_OTROSBIENESABANDONADOS = dbDefaults.getInt32(dataReader, index++);
                    registro.VIGENCIA_REGISTRADURIA = dbDefaults.getString(dataReader, index++);
                    registro.REGISTRADURIA = dbDefaults.getInt16(dataReader, index++);
                    registro.COMUNIDAD = dbDefaults.getString(dataReader, index++);
                    registro.PARAM_ORIENTACIONSEXUAL = dbDefaults.getInt32(dataReader, index++);
                    registro.PARAM_IDENTIDADGENERO = dbDefaults.getInt32(dataReader, index++);

                    return registro;
                }
            }
            return null;
        }        
        #endregion
    }
}
