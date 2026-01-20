using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Data;
using Ruv.Data.Reconocimiento;
using System.Data.Objects.DataClasses;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace Ruv.Business.Captura
{
    public class GestionGlosas:IDisposable
    {
        entGlosas           _objDalGlosas;
        entIntecionesGlosas _objDalIntenGlosas;

        public GestionGlosas()
        {
            _objDalGlosas = new entGlosas();
            _objDalIntenGlosas = new entIntecionesGlosas();
        }
        public clsGlosa InsertarGlosa(clsGlosa myGlosa, DbTransaction tran)
        {
            TBGLOSAS nuevaGlosa = new TBGLOSAS();
            nuevaGlosa.ID                                =myGlosa.ID;
            nuevaGlosa.ID                                =myGlosa.ID;                         
            nuevaGlosa.PARAM_PROCESO                     =myGlosa.PARAM_PROCESO;           
            nuevaGlosa.ID_PROCESO                        =myGlosa.ID_PROCESO;              
            nuevaGlosa.PARAM_CATEGORIAGLOSA              =myGlosa.PARAM_CATEGORIAGLOSA;    
            nuevaGlosa.PARAM_CONCEPTOGLOSA               =myGlosa.PARAM_CONCEPTOGLOSA;     
            nuevaGlosa.DESCRIPCIONGLOSA                  =myGlosa.DESCRIPCIONGLOSA;        
            nuevaGlosa.FECHAGLOSA                        =myGlosa.FECHAGLOSA;              
            nuevaGlosa.FECHAATENCION                     =myGlosa.FECHAATENCION;           
            nuevaGlosa.FECHAESPERADAATEN                 =myGlosa.FECHAESPERADAATEN;       
            nuevaGlosa.GLOSAATEND                        =myGlosa.GLOSAATEND;              
            nuevaGlosa.GLOSANOATEND                      =myGlosa.GLOSANOATEND;            
            nuevaGlosa.MOTIVONOATEN                      =myGlosa.MOTIVONOATEN;            
            nuevaGlosa.ID_USUARIOCREA                    =myGlosa.ID_USUARIOCREA;          
            nuevaGlosa.ID_USUARIOATIENDE                 =myGlosa.ID_USUARIOATIENDE;       
            nuevaGlosa.ID_USUARIOCOORDINA                =myGlosa.ID_USUARIOCOORDINA;      
            nuevaGlosa.MOTIVOSIATEN                      =myGlosa.MOTIVOSIATEN;            
            nuevaGlosa.DEVOLUCION                        =myGlosa.DEVOLUCION;              
            nuevaGlosa.PARAM_CONCEPTODEVOLUCION          =myGlosa.PARAM_CONCEPTODEVOLUCION;
            nuevaGlosa.ID_USUARIO                        =myGlosa.ID_USUARIO;
            nuevaGlosa.ID_UTERRITORIAL                   =myGlosa.ID_UTERRITORIAL;
            nuevaGlosa.PARAM_ESTADOGLOSA                 = myGlosa.PARAM_ESTADOGLOSA;
            int Id = _objDalGlosas.setGlosas(nuevaGlosa, tran);
            myGlosa.ID = Id;
            return myGlosa;
        }
        public clsGlosaIntencion InsertarIntencionGlosa(clsGlosaIntencion myIGlosa, DbTransaction tran)
        {
            TBGLOSAINTENCION nuevaIGlosa = new TBGLOSAINTENCION();
            nuevaIGlosa.ID                          =myIGlosa.ID;                    
            nuevaIGlosa.ID_PROCESO                  =myIGlosa.ID_PROCESO;            
            nuevaIGlosa.PARAM_CATEGORIAINGLOSA      =myIGlosa.PARAM_CATEGORIAINGLOSA;
            nuevaIGlosa.DESCRIPCIONINGLOSA          =myIGlosa.DESCRIPCIONINGLOSA;    
            nuevaIGlosa.FECHAINGLOSA                =myIGlosa.FECHAINGLOSA;          
            nuevaIGlosa.FECHAATENCION               =myIGlosa.FECHAATENCION;         
            nuevaIGlosa.FECHAESPERADAATEN           =myIGlosa.FECHAESPERADAATEN;     
            nuevaIGlosa.GLOSAATEND                  =myIGlosa.GLOSAATEND;            
            nuevaIGlosa.GLOSANOATEND                =myIGlosa.GLOSANOATEND;          
            nuevaIGlosa.MOTIVONOATEN                =myIGlosa.MOTIVONOATEN;          
            nuevaIGlosa.ID_USUARIOCREA              =myIGlosa.ID_USUARIOCREA;        
            nuevaIGlosa.ID_USUARIOATIENDE           =myIGlosa.ID_USUARIOATIENDE;     
            nuevaIGlosa.ID_USUARIOCOORDINA          =myIGlosa.ID_USUARIOCOORDINA;    
            nuevaIGlosa.ID_USUARIO                  =myIGlosa.ID_USUARIO;            
            nuevaIGlosa.ID_UTERRITORIAL             =myIGlosa.ID_UTERRITORIAL;
            nuevaIGlosa.PARAM_PROCESO               = myIGlosa.PARAM_PROCESO;
            nuevaIGlosa.PARAM_ESTADOGLOSA           = myIGlosa.PARAM_ESTADOGLOSA;


            int Id = _objDalIntenGlosas.setIGlosas(nuevaIGlosa, tran);
            myIGlosa.ID = Id;
            return myIGlosa;
        }
        public void ActualizarGlosa(clsGlosa myGlosa, DbTransaction tran)
        {
            TBGLOSAS nuevaGlosa = new TBGLOSAS();
            nuevaGlosa.ID = myGlosa.ID;
            nuevaGlosa.ID = myGlosa.ID;
            nuevaGlosa.PARAM_PROCESO = myGlosa.PARAM_PROCESO;
            nuevaGlosa.ID_PROCESO = myGlosa.ID_PROCESO;
            nuevaGlosa.PARAM_CATEGORIAGLOSA = myGlosa.PARAM_CATEGORIAGLOSA;
            nuevaGlosa.PARAM_CONCEPTOGLOSA = myGlosa.PARAM_CONCEPTOGLOSA;
            nuevaGlosa.DESCRIPCIONGLOSA = myGlosa.DESCRIPCIONGLOSA;
            nuevaGlosa.FECHAGLOSA = myGlosa.FECHAGLOSA;
            nuevaGlosa.FECHAATENCION = myGlosa.FECHAATENCION;
            nuevaGlosa.FECHAESPERADAATEN = myGlosa.FECHAESPERADAATEN;
            nuevaGlosa.GLOSAATEND = myGlosa.GLOSAATEND;
            nuevaGlosa.GLOSANOATEND = myGlosa.GLOSANOATEND;
            nuevaGlosa.MOTIVONOATEN = myGlosa.MOTIVONOATEN;
            nuevaGlosa.ID_USUARIOCREA = myGlosa.ID_USUARIOCREA;
            nuevaGlosa.ID_USUARIOATIENDE = myGlosa.ID_USUARIOATIENDE;
            nuevaGlosa.ID_USUARIOCOORDINA = myGlosa.ID_USUARIOCOORDINA;
            nuevaGlosa.MOTIVOSIATEN = myGlosa.MOTIVOSIATEN;
            nuevaGlosa.DEVOLUCION = myGlosa.DEVOLUCION;
            nuevaGlosa.PARAM_CONCEPTODEVOLUCION = myGlosa.PARAM_CONCEPTODEVOLUCION;
            nuevaGlosa.ID_USUARIO = myGlosa.ID_USUARIO;
            nuevaGlosa.ID_UTERRITORIAL = myGlosa.ID_UTERRITORIAL;
            nuevaGlosa.PARAM_ESTADOGLOSA = myGlosa.PARAM_ESTADOGLOSA;
            _objDalGlosas.updGlosas(nuevaGlosa, tran);
        }
        public void ActualizarIntencionGlosa(clsGlosaIntencion myIGlosa, DbTransaction tran)
        {
            TBGLOSAINTENCION nuevaIGlosa = new TBGLOSAINTENCION();
            nuevaIGlosa.ID = myIGlosa.ID;
            nuevaIGlosa.ID_PROCESO = myIGlosa.ID_PROCESO;
            nuevaIGlosa.PARAM_CATEGORIAINGLOSA = myIGlosa.PARAM_CATEGORIAINGLOSA;
            nuevaIGlosa.DESCRIPCIONINGLOSA = myIGlosa.DESCRIPCIONINGLOSA;
            nuevaIGlosa.FECHAINGLOSA = myIGlosa.FECHAINGLOSA;
            nuevaIGlosa.FECHAATENCION = myIGlosa.FECHAATENCION;
            nuevaIGlosa.FECHAESPERADAATEN = myIGlosa.FECHAESPERADAATEN;
            nuevaIGlosa.GLOSAATEND = myIGlosa.GLOSAATEND;
            nuevaIGlosa.GLOSANOATEND = myIGlosa.GLOSANOATEND;
            nuevaIGlosa.MOTIVONOATEN = myIGlosa.MOTIVONOATEN;
            nuevaIGlosa.ID_USUARIOCREA = myIGlosa.ID_USUARIOCREA;
            nuevaIGlosa.ID_USUARIOATIENDE = myIGlosa.ID_USUARIOATIENDE;
            nuevaIGlosa.ID_USUARIOCOORDINA = myIGlosa.ID_USUARIOCOORDINA;
            nuevaIGlosa.ID_USUARIO = myIGlosa.ID_USUARIO;
            nuevaIGlosa.ID_UTERRITORIAL = myIGlosa.ID_UTERRITORIAL;
            nuevaIGlosa.PARAM_PROCESO = myIGlosa.PARAM_PROCESO;
            nuevaIGlosa.PARAM_ESTADOGLOSA = myIGlosa.PARAM_ESTADOGLOSA;
            _objDalIntenGlosas.updIGlosas(nuevaIGlosa, tran);
        }
        public ObservableCollection<clsGlosa> ObtenerGlosasxDec(clsDeclaracion myDeclaracion)
        {
            ObservableCollection<clsGlosa> resultadoGlosas = new ObservableCollection<clsGlosa>();
            if (myDeclaracion != null)
            {
                List<TBGLOSAS> Resultado = _objDalGlosas.getGlosasXdeclaracion((int)myDeclaracion.ID);
                foreach (TBGLOSAS myGlosa in Resultado)
                {
                    clsGlosa nuevaGlosa = new clsGlosa();
                    nuevaGlosa.ID = myGlosa.ID;
                    nuevaGlosa.ID = myGlosa.ID;
                    nuevaGlosa.PARAM_PROCESO = myGlosa.PARAM_PROCESO;
                    nuevaGlosa.ID_PROCESO = myGlosa.ID_PROCESO;
                    nuevaGlosa.PARAM_CATEGORIAGLOSA = myGlosa.PARAM_CATEGORIAGLOSA;
                    nuevaGlosa.PARAM_CONCEPTOGLOSA = myGlosa.PARAM_CONCEPTOGLOSA;
                    nuevaGlosa.DESCRIPCIONGLOSA = myGlosa.DESCRIPCIONGLOSA;
                    nuevaGlosa.FECHAGLOSA = myGlosa.FECHAGLOSA;
                    nuevaGlosa.FECHAATENCION = myGlosa.FECHAATENCION;
                    nuevaGlosa.FECHAESPERADAATEN = myGlosa.FECHAESPERADAATEN;
                    nuevaGlosa.GLOSAATEND = myGlosa.GLOSAATEND;
                    nuevaGlosa.GLOSANOATEND = myGlosa.GLOSANOATEND;
                    nuevaGlosa.MOTIVONOATEN = myGlosa.MOTIVONOATEN;
                    nuevaGlosa.ID_USUARIOCREA = myGlosa.ID_USUARIOCREA;
                    nuevaGlosa.ID_USUARIOATIENDE = myGlosa.ID_USUARIOATIENDE;
                    nuevaGlosa.ID_USUARIOCOORDINA = myGlosa.ID_USUARIOCOORDINA;
                    nuevaGlosa.MOTIVOSIATEN = myGlosa.MOTIVOSIATEN;
                    nuevaGlosa.DEVOLUCION = myGlosa.DEVOLUCION;
                    nuevaGlosa.PARAM_CONCEPTODEVOLUCION = myGlosa.PARAM_CONCEPTODEVOLUCION;
                    nuevaGlosa.ID_USUARIO = myGlosa.ID_USUARIO;
                    nuevaGlosa.ID_UTERRITORIAL = myGlosa.ID_UTERRITORIAL;
                    nuevaGlosa.PARAM_ESTADOGLOSA = myGlosa.PARAM_ESTADOGLOSA;

                    resultadoGlosas.Add(nuevaGlosa);
                }
            }
            return resultadoGlosas;
        }
        public ObservableCollection<clsGlosaIntencion> ObtenerInGlosasxDec(clsDeclaracion myDeclaracion)
        {
            ObservableCollection<clsGlosaIntencion> resultadoGlosas = new ObservableCollection<clsGlosaIntencion>();
            if (myDeclaracion != null)
            {
                List<TBGLOSAINTENCION> Resultado = _objDalIntenGlosas.getIGlosasXdeclaracion((int)myDeclaracion.ID);
                foreach (TBGLOSAINTENCION myIGlosa in Resultado)
                {
                    clsGlosaIntencion nuevaIGlosa = new clsGlosaIntencion();
                    nuevaIGlosa.ID = myIGlosa.ID;
                    nuevaIGlosa.ID_PROCESO = myIGlosa.ID_PROCESO;
                    nuevaIGlosa.PARAM_CATEGORIAINGLOSA = myIGlosa.PARAM_CATEGORIAINGLOSA;
                    nuevaIGlosa.DESCRIPCIONINGLOSA = myIGlosa.DESCRIPCIONINGLOSA;
                    nuevaIGlosa.FECHAINGLOSA = myIGlosa.FECHAINGLOSA;
                    nuevaIGlosa.FECHAATENCION = myIGlosa.FECHAATENCION;
                    nuevaIGlosa.FECHAESPERADAATEN = myIGlosa.FECHAESPERADAATEN;
                    nuevaIGlosa.GLOSAATEND = myIGlosa.GLOSAATEND;
                    nuevaIGlosa.GLOSANOATEND = myIGlosa.GLOSANOATEND;
                    nuevaIGlosa.MOTIVONOATEN = myIGlosa.MOTIVONOATEN;
                    nuevaIGlosa.ID_USUARIOCREA = myIGlosa.ID_USUARIOCREA;
                    nuevaIGlosa.ID_USUARIOATIENDE = myIGlosa.ID_USUARIOATIENDE;
                    nuevaIGlosa.ID_USUARIOCOORDINA = myIGlosa.ID_USUARIOCOORDINA;
                    nuevaIGlosa.ID_USUARIO = myIGlosa.ID_USUARIO;
                    nuevaIGlosa.ID_UTERRITORIAL = myIGlosa.ID_UTERRITORIAL;
                    nuevaIGlosa.PARAM_PROCESO = myIGlosa.PARAM_PROCESO;
                    nuevaIGlosa.PARAM_ESTADOGLOSA = myIGlosa.PARAM_ESTADOGLOSA;

                    resultadoGlosas.Add(nuevaIGlosa);
                }
            }
            return resultadoGlosas;
        }
        public void Dispose()
        {
            _objDalGlosas = null;
            _objDalIntenGlosas = null;
        }
    }
}
