using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;
using System.Data.Objects.DataClasses;
using Ruv.Data.Radicacion;
using Ruv.Business.Captura.Declaracion;
using System.Data.Common;

namespace Ruv.Business.Captura.Anexos
{
    public class Anexo13
    {
        public const int TipoAnexo = (int)Common.eTiposAnexo.Anexo13;

        #region Guardar
        public static void Guardar(clsAnexo13 anexoSiniestro, DbTransaction tran)
        {
            //Guardar Siniestro
            Anexo13.GuardarSiniestro(anexoSiniestro, tran);

            //Guardar los datos de los mensajes del anexo 13
            Anexo13.GuardarMensajes(anexoSiniestro, tran);

            //Reiniciar EstadoRegistro
            if (anexoSiniestro.EstadoRegistro != eEstadoRegistro.Eliminado)
                anexoSiniestro.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            #region Guardar Anexos
            int id_siniestro = (int)anexoSiniestro.ID;
            
            foreach (clsAnexo13_Victima anexoView in anexoSiniestro.ListaPersonas)
            {
                //Guardar Anexo
                Anexo13.GuardarAnexo(anexoView, id_siniestro, tran);

                //Si esta eliminado continua con el siguiente
                if (anexoView.EstadoRegistro == eEstadoRegistro.Eliminado)
                    continue;

                //Actualizar Afectaciones del Anexo
                Afectaciones.Guardar((int)anexoView.ID, TipoAnexo, anexoView.TiposDeAfectacion, tran);

                //Reiniciar EstadoRegistro
                if (anexoView.EstadoRegistro != eEstadoRegistro.Eliminado)
                    anexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;                 
            }
            #endregion                        

            //Eliminar de la vista
            var deletedVictimsIds = anexoSiniestro.ListaPersonas.Where(x => x.EstadoRegistro == eEstadoRegistro.Eliminado).Select(x => x.ID).ToList();
            if (deletedVictimsIds != null && deletedVictimsIds.Count > 0)
            {
                for (int i = 0; i < anexoSiniestro.ListaPersonas.Count; i++)
                {
                    if (deletedVictimsIds.Contains(anexoSiniestro.ListaPersonas[i].ID))
                        anexoSiniestro.ListaPersonas.RemoveAt(i);
                }
            }            
            //var borrados = from a in anexoSiniestro.ListaPersonas 
            //               where a.EstadoRegistro == eEstadoRegistro.Eliminado
            //               select a;
            //foreach (clsAnexo13_Victima anexoView in borrados)
            //    anexoSiniestro.ListaPersonas.Remove(anexoView);               
        }

        #region Siniestro
        private static void GuardarSiniestro(clsAnexo13 anexoSiniestro, DbTransaction tran)
        {
            entSiniestroPersona entBdSiniestro = new entSiniestroPersona();

            TBSINIESTROS_PERSONA siniestroData = new TBSINIESTROS_PERSONA();
            Anexo13.ParseViewToData_Siniestro(anexoSiniestro, ref siniestroData);

            //Asignar tipo hecho
            siniestroData.PARAM_TIPOHECHO = TipoAnexo;

            switch (anexoSiniestro.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    siniestroData.ACTIVO = 1;
                    entBdSiniestro.setData(siniestroData, tran);
                    anexoSiniestro.ID = siniestroData.ID;
                    break;
                case eEstadoRegistro.Modificado:
                    entBdSiniestro.updateData(siniestroData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    siniestroData.ACTIVO = 0;
                    entBdSiniestro.updateData(siniestroData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }

        public static void ParseViewToData_Siniestro(clsAnexo13 anexo, ref Ruv.Data.TBSINIESTROS_PERSONA siniestroData)
        {
            siniestroData.ID = anexo.ID ?? -1;

            if (siniestroData.TBREGISTROS_PERSONAS == null)
                siniestroData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

            if (!anexo.JefeGrupoFamiliarId.HasValue)
                throw new InvalidOperationException("No se pudo determinar el jefe del grupo familiar para el anexo");

            siniestroData.TBREGISTROS_PERSONAS.ID = anexo.JefeGrupoFamiliarId.Value;
            
            //TODO: Anexo 13 Tomar los datos del anexo5
            //A-1
            /*
            siniestroData.FECHASINIESTRO = anexo.FechaYLugar.HechosFecha;
            siniestroData.ID_DEPARTAMENTO = anexo.FechaYLugar.HechosDepartamento;
            siniestroData.ID_MUNICIPIO = anexo.FechaYLugar.HechosMunicipio;
            //ENTORNO
            siniestroData.PARAM_TIPO_ENTORNO = (int?)anexo.FechaYLugar.TipoEntorno;
            siniestroData.PARAM_LOCALIDAD_CORREG = anexo.FechaYLugar.LocalidadCorregimientoId;
            siniestroData.OTRO_LOCALIDAD_CORREG = anexo.FechaYLugar.LocalidadCorregimientoNombre;
            siniestroData.PARAM_BARRIO_VEREDA = anexo.FechaYLugar.BarrioVeredaId;
            siniestroData.OTRO_BARRIO_VEREDA = anexo.FechaYLugar.BarrioVeredaNombre;
              */          
            siniestroData.ACTIVO = (short)((anexo.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
                      
        }

        #endregion

        #region Mensajes
        private static void GuardarMensajes(clsAnexo13 anexoSiniestro, DbTransaction tran)
        {
            entAnexo13 entBdMensaje = new entAnexo13();

            //TBSINIESTROS_PERSONA siniestroData = new TBSINIESTROS_PERSONA();
            TBANEXO13_MENSAJE mensajesData = new TBANEXO13_MENSAJE();
            Anexo13.ParseViewToData_mensajes(anexoSiniestro, ref mensajesData);

            switch (anexoSiniestro.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    mensajesData.ACTIVO = 1;
                    entBdMensaje.setAnexo13_mensajes(mensajesData, tran);
                    break;
                case eEstadoRegistro.Modificado:
                    entBdMensaje.updAnexo13_mensajes(mensajesData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    mensajesData.ACTIVO = 0;
                    entBdMensaje.updAnexo13_mensajes(mensajesData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }

        public static void ParseViewToData_mensajes(clsAnexo13 anexo, ref Ruv.Data.TBANEXO13_MENSAJE mensajes)
        {
            mensajes.ID_SINIESTRO = anexo.ID ?? -1;
            mensajes.MENSAJE_CELULAR = anexo.MedioDeContactoMensajeTexto != null ? (short)anexo.MedioDeContactoMensajeTexto : (short)0;
            mensajes.MENSAJE_CORREOE = anexo.MedioDeContactoCorreoElectronico != null ? (short)anexo.MedioDeContactoCorreoElectronico : (short)0;
            mensajes.MENSAJE_FIJO = anexo.MedioDeContactoMensajeVoz != null ? (short)anexo.MedioDeContactoMensajeVoz : (short)0;
            mensajes.MENSAJE_OTRO = anexo.MedioDeContactoOtro;            
            mensajes.ACTIVO = (short)((anexo.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
        }

        #endregion

        #region Anexo
        private static void GuardarAnexo(clsAnexo13_Victima anexoView, int id_siniestro, DbTransaction tran)
        {
            TBANEXO13 anexoData = new TBANEXO13();
            Anexo13.ParseViewToData_Anexo(anexoView, id_siniestro, ref anexoData);

            entAnexo13 entBd = new entAnexo13();
            switch (anexoView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Activar
                    anexoData.ACTIVO = 1;
                    entBd.setAnexo13(anexoData, tran);
                    anexoView.ID = anexoData.ID;
                    break;
                case eEstadoRegistro.Modificado:
                    entBd.updAnexo13(anexoData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    //Desactivar
                    anexoData.ACTIVO = 0;
                    entBd.updAnexo13(anexoData, tran);
                    break;
                case eEstadoRegistro.SinModificaciones:
                    //Nothing
                    break;
            }
        }

        public static void ParseViewToData_Anexo(clsAnexo13_Victima anexoView, int id_siniestro, ref TBANEXO13 anexoData)
        {
            #region Common Anexos
            anexoData.ID = anexoView.ID ?? -1;

            if (anexoData.TBSINIESTROS_PERSONA == null)
                anexoData.TBSINIESTROS_PERSONA = new TBSINIESTROS_PERSONA();
            anexoData.TBSINIESTROS_PERSONA.ID = id_siniestro;

            if (anexoData.TBREGISTROS_PERSONAS == null)
                anexoData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();
                      
            anexoData.TBREGISTROS_PERSONAS.ID = (int)anexoView.PersonaAfectadaId;                               
          
            #endregion
            
            anexoData.ACTIVO = (short)((anexoView.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
        }

        public static void GuardarAnexo13Siniestro(clsAnexo13 anexo)
        {
            
        }

        #endregion
        #endregion

        #region Obtener
        public static List<TBSINIESTROS_PERSONA> ObtenerSiniestros(int id_declaracion)
        {
            //Obtener los datos del encabezado del anexo
            entSiniestroPersona entSinPer = new entSiniestroPersona();
            List<TBSINIESTROS_PERSONA> siniestrosData = new List<TBSINIESTROS_PERSONA>();

            siniestrosData = entSinPer.getData(TipoAnexo, id_declaracion);
            return siniestrosData;
        }

        public static void ObtenerAnexo(TBSINIESTROS_PERSONA siniestroData, int idDeclaracion, clsAnexo13 anexoView)
        {
            entAnexo13 entAnexo = new entAnexo13();
            //clsAnexo13 anexoView = new clsAnexo13();

            //Obtener los datos de mensajes de contacto
            TBANEXO13_MENSAJE mensajesData = entAnexo.getDataMensaje(siniestroData.ID);

            //Pasar datos a la vista
            Anexo13.ParseDataToView_Siniestro(siniestroData,mensajesData, ref anexoView);

            //Reiniciar Estado
            anexoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
            
            //Obtener las personas del anexo
            List<TBANEXO13> anexosData = entAnexo.getData(siniestroData.ID);
            

            foreach (TBANEXO13 anexoData in anexosData)
            {  
                                

                int FamiliaConsecutivo = (int)siniestroData.TBREGISTROS_PERSONAS.CONSECUTIVO_FAMILIA;     //TODO: Anexo 13 traer de la consulta de anexo13
                entRegistroPersona entRegPer = new entRegistroPersona();
                List<TBREGISTROS_PERSONAS> registrosPersonasData = RegistroPersona.ObtenerRegistrosPersona(idDeclaracion, FamiliaConsecutivo);
                                

                TBREGISTROS_PERSONAS registroPersonaData = registrosPersonasData.FirstOrDefault(x => x.ID == anexoData.ID_REGPERSONA);   

                                
                //Por cada registro persona, se agrega una persona afectas
                clsAnexo13_Victima personaView = new clsAnexo13_Victima();

                anexoData.TBREGISTROS_PERSONAS = registroPersonaData;

                Anexo13.ParseDataToView_Anexo(anexoData,ref personaView);

                Persona.Obtener(registroPersonaData, (int)registroPersonaData.TBPERSONAS.ID, personaView);

                //if (registroPersonaData.ESDECLARANTE == 1)
                //{
                //    declaracionView.PersonasAfectadas.Declaracion = declaracionView;
                //    declaracionView.PersonasAfectadas.DeclaranteId = personaView.ID;
                //    Persona.ParseDataToView_Declarante(registroPersonaData, ref tomaDeclaracion);
                //}

                if (registroPersonaData.PARAM_RELACION == (int)eRelacion.Jefe_de_hogar)
                {
                    ParseDataToView_DatosContacto(anexoView, registroPersonaData);
                }

                //Agregar personaAfectada a al anexo 13

                personaView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

                //Obtener Afectaciones
                personaView.TiposDeAfectacion = Afectaciones.Obtener(anexoData.ID, TipoAnexo);

                //Reiniciar Estado
                personaView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
                anexoView.ListaPersonas.Add(personaView);
                
                
            }
            //return anexoView;
        }

        public static void ParseDataToView_Siniestro(Ruv.Data.TBSINIESTROS_PERSONA siniestroData,TBANEXO13_MENSAJE mensajesData, ref clsAnexo13 anexoView)
        {
            //TBDECLARACIONES declaracionData, ref clsDeclaracion declaracionView
            anexoView.ID = siniestroData.ID;

            if (siniestroData.TBREGISTROS_PERSONAS == null)
                siniestroData.TBREGISTROS_PERSONAS = new TBREGISTROS_PERSONAS();

            anexoView.JefeGrupoFamiliarId = siniestroData.TBREGISTROS_PERSONAS.ID;

            //TODO: Anexo 13 Tomar los datos del anexo 5, o grabarlos del anexo 5
            /*
            //A-1
            anexoView.FechaYLugar.HechosFecha = siniestroData.FECHASINIESTRO;
            anexoView.FechaYLugar.HechosDepartamento = siniestroData.ID_DEPARTAMENTO;
            anexoView.FechaYLugar.HechosMunicipio = siniestroData.ID_MUNICIPIO;
            //ENTORNO
            anexoView.FechaYLugar.TipoEntorno = (eTipoEntorno?)siniestroData.PARAM_TIPO_ENTORNO;
            anexoView.FechaYLugar.LocalidadCorregimientoId = siniestroData.PARAM_LOCALIDAD_CORREG;
            anexoView.FechaYLugar.LocalidadCorregimientoNombre = siniestroData.OTRO_LOCALIDAD_CORREG;
            anexoView.FechaYLugar.BarrioVeredaId = siniestroData.PARAM_BARRIO_VEREDA;
            anexoView.FechaYLugar.BarrioVeredaNombre = siniestroData.OTRO_BARRIO_VEREDA;
            */
            
            anexoView.MedioDeContactoMensajeTexto = mensajesData.MENSAJE_CELULAR;
            anexoView.MedioDeContactoMensajeVoz = mensajesData.MENSAJE_FIJO;
            anexoView.MedioDeContactoCorreoElectronico = mensajesData.MENSAJE_CORREOE;
            anexoView.MedioDeContactoOtro = mensajesData.MENSAJE_OTRO;


            //anexoView.EstadoRegistro = (Int16)siniestroData.ACTIVO;
        }

        public static void ParseDataToView_Anexo(TBANEXO13 anexoData, ref clsAnexo13_Victima anexoView)
        {

            #region Common Anexos
            anexoView.ID = anexoData.ID;

            if (anexoData.TBREGISTROS_PERSONAS != null)
                anexoView.PersonaAfectadaId = anexoData.TBREGISTROS_PERSONAS.ID;


            anexoView.FamiliaConsecutivo = (short)anexoData.TBREGISTROS_PERSONAS.CONSECUTIVO_FAMILIA;  

            //TODO: Anexo 13 Revisar afectación
            /*
            anexoView.Afectacion.Afectado = anexoData.AFECTADO;
            anexoView.Afectacion.Otro = anexoData.OTRA_AFECTACION;
            */            
            #endregion

            //TODO: Anexo 13 Revisar afectación
            /*
            if (anexoView.Afectacion != null)
                anexoData.OTRA_AFECTACION = anexoView.Afectacion.Otro;
            */
            anexoData.ACTIVO = (short)((anexoView.EstadoRegistro == eEstadoRegistro.Eliminado) ? 0 : 1);
        }

        public static void ParseDataToView_DatosContacto(clsAnexo13 anexoView, TBREGISTROS_PERSONAS registroPersonaData)
        {
            //Datos de contacto
            anexoView.DatoContactoDireccion = registroPersonaData.DIRECCION;
            anexoView.DatoContactoPais = registroPersonaData.ID_PAIS;
            anexoView.DatoContactoDepartamento = registroPersonaData.ID_DEPARTAMENTO;
            anexoView.DatoContactoMunicipio = registroPersonaData.ID_MUNICIPIO;
            anexoView.DatoContactoTipoEntorno = (eTipoEntorno?)registroPersonaData.ID_ENTORNO;
            anexoView.DatoContactoLocalidadCorregimientoId = registroPersonaData.PARAM_LOCALIDAD_CORREG;
            anexoView.DatoContactoBarrioVeredaId = registroPersonaData.PARAM_BARRIO_VEREDA;
            anexoView.DatoContactoTelefonoFijo = registroPersonaData.TELEFONO;
            anexoView.DatoContactoTelefonoCelular = registroPersonaData.MOVIL;
            anexoView.DatoContactoCorreoElectronico = registroPersonaData.EMAIL;

            //Datos de contacto Alterno
            anexoView.DatoAlternoContactoDireccion = registroPersonaData.DIRECCION_ALTERNA;
            anexoView.DatoAlternoContactoPais = registroPersonaData.ID_PAIS_ALTERNO;
            anexoView.DatoAlternoContactoDepartamento = registroPersonaData.ID_DEPARTAMENTO_ALTERNO;
            anexoView.DatoAlternoContactoMunicipio = registroPersonaData.ID_MUNICIPIO_ALTERNO;
            anexoView.DatoAlternoContactoTipoEntorno = (eTipoEntorno?)registroPersonaData.ID_ENTORNO_ALTERNO;
            anexoView.DatoAlternoContactoLocalidadCorregimientoId = registroPersonaData.PARAM_LOCALIDAD_CORREG_ALT;
            anexoView.DatoAlternoContactoBarrioVeredaId = registroPersonaData.PARAM_BARRIO_VEREDA_ALT;
            anexoView.DatoAlternoContactoTelefonoFijo = registroPersonaData.TELEFONO_ALTERNO;
            anexoView.DatoAlternoContactoTelefonoCelular = registroPersonaData.MOVIL_ALTERNO;
            anexoView.DatoAlternoContactoCorreoElectronico = registroPersonaData.EMAIL_ALTERNO;

        }
        #endregion       
    }
}
