using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data.Reconocimiento;
using Ruv.Data;

namespace Ruv.Business.Captura
{
    public class Radicacion
    {
        /// <summary>
        /// Realiza la conversión de la radicación de capa de presentación a la radicación que se mapeada a las base de datos
        /// </summary>
        /// <param name="radicacionView"></param>
        /// <param name="RadicacionData"></param>
        public static void ParseViewToData_Declaracion(clsRadicacion radicacionView, ref TBRADICACION RadicacionData)
        {
                RadicacionData.ID                           = -1; // es Autonumérico
                RadicacionData.ID_MUNICIPIO                 = radicacionView.ID_MUNICIPIO ;
                RadicacionData.FECHAREGISTRO                = radicacionView.FECHAREGISTRO ;          
                RadicacionData.ID_MUNICIPIO                 = radicacionView.ID_MUNICIPIO;            
                RadicacionData.ID_UTERRITORIALENVIA         = radicacionView.ID_UTERRITORIALENVIA;    
                RadicacionData.ID_UTERRITORIALRECIBE        = radicacionView.ID_UTERRITORIALRECIBE;   
                RadicacionData.PARAM_TIPOENTIDAD           = radicacionView. PARAM_TIPOENTIDAD;      
                RadicacionData.NOMBREENTIDAD                = radicacionView.NOMBREENTIDAD;           
                RadicacionData.FECHAENVIO                   = radicacionView.FECHAENVIO;              
                RadicacionData.FECHALLEGADA                 = radicacionView.FECHALLEGADA;            
                RadicacionData.CANTIDADDOCUMENTOS           = radicacionView.CANTIDADDOCUMENTOS;      
                RadicacionData.ID_UTERRITORIALRADICA        = radicacionView.ID_UTERRITORIALRADICA;   
                RadicacionData.ID_USUARIO_RADICA            = radicacionView.ID_USUARIO_RADICA;       
                RadicacionData.ID_RADICA_URGENCIA           = radicacionView.ID_RADICA_URGENCIA;      
                RadicacionData.PARAM_TIPOACCIONES           = radicacionView.PARAM_TIPOACCIONES;      
                RadicacionData.MODIFICACION                 = radicacionView.MODIFICACION;            
                RadicacionData.PARAM_ENTIDADENVIANOMBRE     = radicacionView.PARAM_ENTIDADENVIANOMBRE;
                RadicacionData.ID_TIPODOCUMENTAL            = radicacionView.ID_TIPODOCUMENTAL;       
                RadicacionData.NRO_FORMULARIO               = radicacionView.NRO_FORMULARIO;
                RadicacionData.CONSECUTIVO                  = radicacionView.CONSECUTIVO;
                RadicacionData.ID_TIPO_RADICACION           = radicacionView.ID_TIPORADICACION;
                RadicacionData.OBSERVACIONES                = radicacionView.OBSERVACIONES;
                RadicacionData.RUTAIMAGEN                   = radicacionView.RUTAIMAGEN;
                RadicacionData.ID_ENTIDADMUNICIPIO          = radicacionView.ID_ENTIDADMUNICIPIO;
                RadicacionData.ID_DECLARACION               = radicacionView.ID_DECLARACION;
                RadicacionData.PARAM_RESULTADO_VALIDACION   = radicacionView.PARAM_RESULTADO_VALIDACION;
        }

        public static void ParseViewToData_DeclaracionUpdate(clsRadicacion radicacionView, ref TBRADICACION RadicacionData)
        {
            RadicacionData.ID = (int)radicacionView.ID;
            RadicacionData.ID_MUNICIPIO = radicacionView.ID_MUNICIPIO;
            RadicacionData.FECHAREGISTRO = radicacionView.FECHAREGISTRO;
            RadicacionData.ID_MUNICIPIO = radicacionView.ID_MUNICIPIO;
            RadicacionData.ID_UTERRITORIALENVIA = radicacionView.ID_UTERRITORIALENVIA;
            RadicacionData.ID_UTERRITORIALRECIBE = radicacionView.ID_UTERRITORIALRECIBE;
            RadicacionData.PARAM_TIPOENTIDAD = radicacionView.PARAM_TIPOENTIDAD;
            RadicacionData.NOMBREENTIDAD = radicacionView.NOMBREENTIDAD;
            RadicacionData.FECHAENVIO = radicacionView.FECHAENVIO;
            RadicacionData.FECHALLEGADA = radicacionView.FECHALLEGADA;
            RadicacionData.CANTIDADDOCUMENTOS = radicacionView.CANTIDADDOCUMENTOS;
            RadicacionData.ID_UTERRITORIALRADICA = radicacionView.ID_UTERRITORIALRADICA;
            RadicacionData.ID_USUARIO_RADICA = radicacionView.ID_USUARIO_RADICA;
            RadicacionData.ID_RADICA_URGENCIA = radicacionView.ID_RADICA_URGENCIA;
            RadicacionData.PARAM_TIPOACCIONES = radicacionView.PARAM_TIPOACCIONES;
            RadicacionData.MODIFICACION = radicacionView.MODIFICACION;
            RadicacionData.PARAM_ENTIDADENVIANOMBRE = radicacionView.PARAM_ENTIDADENVIANOMBRE;
            RadicacionData.ID_TIPODOCUMENTAL = radicacionView.ID_TIPODOCUMENTAL;
            RadicacionData.NRO_FORMULARIO = radicacionView.NRO_FORMULARIO;
            RadicacionData.CONSECUTIVO = radicacionView.CONSECUTIVO;
            RadicacionData.ID_TIPO_RADICACION = radicacionView.ID_TIPORADICACION;
            RadicacionData.OBSERVACIONES = radicacionView.OBSERVACIONES;
            RadicacionData.RUTAIMAGEN = radicacionView.RUTAIMAGEN;
            RadicacionData.ID_ENTIDADMUNICIPIO = radicacionView.ID_ENTIDADMUNICIPIO;
            RadicacionData.PARAM_RESULTADO_VALIDACION = radicacionView.PARAM_RESULTADO_VALIDACION;
        }
    }
}