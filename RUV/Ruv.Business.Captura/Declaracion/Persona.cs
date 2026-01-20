using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;

using System.Data.Objects.DataClasses;
using System.Data.Common;

namespace Ruv.Business.Captura.Declaracion
{
    public class Persona
    {
        #region Guardar Datos
        public static void Guardar(clsDeclaracion declaracionView, IPersonaAfectada personaView, ref int? id_jefeHogar, ref int? id_declarante, DbTransaction tran)
        {
            Ruv.Data.Reconocimiento.entPersona entPers = new entPersona();
            Ruv.Data.TBPERSONAS personaData = new TBPERSONAS();
            Persona.ParseViewToData(personaView, personaData);

            //Insertar/Actualizar base de datos
            switch (personaView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    entPers.setData(personaData, tran);
                    break;
                case eEstadoRegistro.Modificado:
                    entPers.updateData(personaData, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    break;
                case eEstadoRegistro.SinModificaciones:
                    break;
            }

            #region RegistroPersona
            int id_persona = personaData.ID;
            RegistroPersona.Guardar(declaracionView, personaView, id_persona, ref id_declarante, ref id_jefeHogar, tran);
            //NOTE: personaView.ID == TBREGISTROS_PERSONAS.ID
            #endregion

            //Reiniciar EstadoRegistro
            if (personaView.EstadoRegistro != eEstadoRegistro.Eliminado)
                personaView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            #region Hechos

            HechosVictimizantes.Guardar(declaracionView, personaView, id_declarante, tran);
            #endregion

            #region Discapacidades
            Discapacidades.Guardar(personaView, tran);
            #endregion
        }

        public static void ParseViewToData(IPersonaAfectada personaView, Ruv.Data.TBPERSONAS personaData)
        {
            personaData.ID = (int)personaView.ID;   
            personaData.PRIMERNOMBRE = personaView.PrimerNombre;
            personaData.SEGUNDONOMBRE = personaView.SegundoNombre;
            personaData.PRIMERAPELLIDO = personaView.PrimerApellido;
            personaData.SEGUNDOAPELLIDO = personaView.SegundoApellido;
            personaData.PARAM_TIPODOCUMENTO = personaView.TipoDocumento;
            personaData.NUMERODOCUMENTO = personaView.NumeroDocumento;

            //EN declaracion.TomaDeclaracion, solo para el declarante
            //personaData.ID_DEPARTAMENTOEXPEDICION = personaView.
            //personaData.ID_MUNICIPIOEXPEDICION = personaView

            personaData.PARAM_ESTADOCIVIL = personaView.EstadoCivil;
            //personaData.PARAM_GENERO = personaView.Genero ?? (int)Common.ThrowException("Debe declarar genero de la persona con identificación " + personaView.NumeroDocumento);
            personaData.PARAM_GENERO = personaView.Genero;
            personaData.PARAM_IDENTIDADGENERO = personaView.IdentidadGenero;
            personaData.PARAM_ORIENTACIONSEXUAL = personaView.OrientacionSexual;

            //EN declaracion.TomaDeclaracion, solo para el declarante
            //personaData.ID_DEPARTAMENTO = personaView
            //personaData.ID_MUNICIPIO = personaView

            personaData.GESTANTE = Common.ParseIntToShortNullable(personaView.GestanteLactante);
            personaData.PARAM_REGIMENSALUD = personaView.RegimenEspecial;

            personaData.FECHANACIMIENTO = personaView.FechaNacimiento;
            //EN declaracion.TomaDeclaracion, solo para el declarante
            //personaData.LEEYESCRIBE = personaView

            /*
            personaData.ID_PROCESO = personaView
            personaData.PARAM_PROCESO = personaView
            personaData.ID_USUARIO = personaView
            personaData.ID_UTERRITORIAL = personaView
             */
            
            personaData.PARAM_ETNIAPERTENECE = personaView.PertenenciaEtnica;
            personaData.PARAM_MINORIAETNICA = personaView.ComunidadEtnica1;
            personaData.PARAM_RESGUARDO = personaView.ComunidadEtnica2;
            personaData.CUALETNIAOPUEBLO = personaView.OtraComunidadEtnica;           

        }

        public static void ParseViewToData_Declarante(clsTomaDeclaracion tomaDeclaracion, Ruv.Data.TBREGISTROS_PERSONAS registroPersonaData)
        {
            //DATOS CONTACTO
            registroPersonaData.ID_PAIS = Common.ParseIntToLongNullable(tomaDeclaracion.DatoContactoPais);
            registroPersonaData.ID_DEPARTAMENTO = Common.ParseIntToLongNullable(tomaDeclaracion.DatoContactoDepartamento);
            registroPersonaData.ID_MUNICIPIO = Common.ParseIntToLongNullable(tomaDeclaracion.DatoContactoMunicipio);
            registroPersonaData.INDICATIVO_TELEFONO = Common.ParseStringToShortNullable(tomaDeclaracion.DatoContactoIndicativo);
            registroPersonaData.DIRECCION = tomaDeclaracion.DatoContactoDireccion;
            registroPersonaData.TELEFONO = tomaDeclaracion.DatoContactoTelefonoFijo;
            registroPersonaData.MOVIL = tomaDeclaracion.DatoContactoTelefonoCelular;
            registroPersonaData.EMAIL = tomaDeclaracion.DatoContactoCorreoElectronico;

            //ENTORNO
            registroPersonaData.PARAM_TIPO_ENTORNO = (int?)tomaDeclaracion.DatoContactoTipoEntorno;
            registroPersonaData.PARAM_LOCALIDAD_CORREG = tomaDeclaracion.DatoContactoLocalidadCorregimientoId;
            registroPersonaData.OTRO_LOCALIDAD_CORREG = tomaDeclaracion.DatoContactoLocalidadCorregimientoNombre;
            registroPersonaData.PARAM_BARRIO_VEREDA = tomaDeclaracion.DatoContactoBarrioVeredaId;
            registroPersonaData.OTRO_BARRIO_VEREDA = tomaDeclaracion.DatoContactoBarrioVeredaNombre;
            
            //DATOS CONTACTO ALTERNO
            registroPersonaData.ID_PAIS_ALTERNO = Common.ParseIntToLongNullable(tomaDeclaracion.DatoAlternoContactoPais);
            registroPersonaData.ID_DEPARTAMENTO_ALTERNO = Common.ParseIntToLongNullable(tomaDeclaracion.DatoAlternoContactoDepartamento);
            registroPersonaData.ID_MUNICIPIO_ALTERNO = Common.ParseIntToLongNullable(tomaDeclaracion.DatoAlternoContactoMunicipio);
            registroPersonaData.DIRECCION_ALTERNA = tomaDeclaracion.DatoAlternoContactoDireccion;
            registroPersonaData.INDICATIVO_TELEFONO_ALTERNO = Common.ParseStringToShortNullable(tomaDeclaracion.DatoContactoAlternoIndicativo);
            registroPersonaData.TELEFONO_ALTERNO = tomaDeclaracion.DatoAlternoContactoTelefonoFijo;
            registroPersonaData.MOVIL_ALTERNO = tomaDeclaracion.DatoAlternoContactoTelefonoCelular;
            registroPersonaData.EMAIL_ALTERNO = tomaDeclaracion.DatoAlternoContactoCorreoElectronico;
            //ENTORNO
            registroPersonaData.PARAM_TIPO_ENTORNO_ALT = (int?)tomaDeclaracion.DatoAlternoContactoTipoEntorno;
            registroPersonaData.PARAM_LOCALIDAD_CORREG_ALT = tomaDeclaracion.DatoAlternoContactoLocalidadCorregimientoId;
            registroPersonaData.OTRO_LOCALIDAD_CORREG_ALT = tomaDeclaracion.DatoAlternoContactoLocalidadCorregimientoNombre;
            registroPersonaData.PARAM_BARRIO_VEREDA_ALT = tomaDeclaracion.DatoAlternoContactoBarrioVeredaId;
            registroPersonaData.OTRO_BARRIO_VEREDA_ALT = tomaDeclaracion.DatoAlternoContactoBarrioVeredaNombre;

        }
        #endregion

        #region Obtener Datos
        public static IPersonaAfectada Obtener(TBREGISTROS_PERSONAS registroPersonaData, int id_registroPersona, IPersonaAfectada personaView)
        {
            entPersona entPer = new entPersona();

            //Por cada registro persona, se agrega una persona afectas
            //clsPersonaAfectada personaView = new clsPersonaAfectada();

            //Obtener datos de la persona del registro persona
            TBPERSONAS personaData = entPer.getData(id_registroPersona);

            //Pasar datos a la vista
            Persona.ParseDataToView(registroPersonaData, personaData, ref personaView);
            
            //Obtener hechos victimizantes de la persona
            personaView.HechosVictimizantes = HechosVictimizantes.Obtener((int)personaView.ID);

            //Obtener discapacidades de la persona
            personaView.Discapacidades = Discapacidades.Obtener((int)personaView.ID);
            personaView.OtraDiscapacidad = Discapacidades.ObtenerOtro((int)personaView.ID);

            personaView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            return personaView;
        }

        public static void ParseDataToView(TBREGISTROS_PERSONAS registroPersonaData, TBPERSONAS personaData, ref IPersonaAfectada personaView)
        {

            personaView.ID = registroPersonaData.ID;
            personaView.NumeroConsecutivo = registroPersonaData.CONSECUTIVO_PERSONA ?? -1;
            personaView.TipoDocumento = personaData.PARAM_TIPODOCUMENTO;
            personaView.NumeroDocumento = personaData.NUMERODOCUMENTO;
            personaView.Genero = personaData.PARAM_GENERO;
            personaView.IdentidadGenero = personaData.PARAM_IDENTIDADGENERO;
            personaView.OrientacionSexual = personaData.PARAM_ORIENTACIONSEXUAL;

            personaView.PrimerNombre = personaData.PRIMERNOMBRE;
            personaView.SegundoNombre = personaData.SEGUNDONOMBRE;
            personaView.PrimerApellido = personaData.PRIMERAPELLIDO;
            personaView.SegundoApellido = personaData.SEGUNDOAPELLIDO;

            personaView.Relacion = registroPersonaData.PARAM_RELACION;
            personaView.FechaNacimiento = personaData.FECHANACIMIENTO;
            personaView.EstadoCivil = personaData.PARAM_ESTADOCIVIL;
            personaView.RegimenEspecial = registroPersonaData.PARAM_REGIMENESPECIAL;
            
            personaView.PertenenciaEtnica = personaData.PARAM_ETNIAPERTENECE;
            personaView.ComunidadEtnica1 = personaData.PARAM_MINORIAETNICA;
            personaView.ComunidadEtnica2 = personaData.PARAM_RESGUARDO;
            personaView.OtraComunidadEtnica = personaData.CUALETNIAOPUEBLO;

            personaView.GestanteLactante = registroPersonaData.GESTANTE_LACTANTE;
            personaView.MujerCabezaDeHogar = registroPersonaData.ESMUJERCABEZADEHOGAR;

            personaView.Nacionalidad = registroPersonaData.ID_NACIONALIDAD;
            personaView.HombreCabezaDeHogar = registroPersonaData.ESHOMBRECABEZADEHOGAR;

            personaView.Campesinado = registroPersonaData.CAMPESINADO;
            personaView.PersonaBuscadora = registroPersonaData.PERSONA_BUSCADORA;
        }

        public static void ParseDataToView_Declarante(TBREGISTROS_PERSONAS registroPersonaData, ref clsTomaDeclaracion tomaDeclaracion)
        {
            //DATOS CONTACTO
            tomaDeclaracion.DatoContactoPais = registroPersonaData.ID_PAIS;
            tomaDeclaracion.DatoContactoDepartamento = registroPersonaData.ID_DEPARTAMENTO;
            tomaDeclaracion.DatoContactoMunicipio = (Int32?)registroPersonaData.ID_MUNICIPIO;
            tomaDeclaracion.DatoContactoDireccion = registroPersonaData.DIRECCION;
            tomaDeclaracion.DatoContactoIndicativo = registroPersonaData.INDICATIVO_TELEFONO.ToString();
            tomaDeclaracion.DatoContactoTelefonoFijo = registroPersonaData.TELEFONO;
            tomaDeclaracion.DatoContactoTelefonoCelular = registroPersonaData.MOVIL;
            tomaDeclaracion.DatoContactoCorreoElectronico = registroPersonaData.EMAIL;

            //ENTORNO
            tomaDeclaracion.DatoContactoTipoEntorno = (eTipoEntorno?)registroPersonaData.PARAM_TIPO_ENTORNO;
            tomaDeclaracion.DatoContactoLocalidadCorregimientoId = registroPersonaData.PARAM_LOCALIDAD_CORREG;
            tomaDeclaracion.DatoContactoLocalidadCorregimientoNombre = registroPersonaData.OTRO_LOCALIDAD_CORREG;
            tomaDeclaracion.DatoContactoBarrioVeredaId = registroPersonaData.PARAM_BARRIO_VEREDA;
            tomaDeclaracion.DatoContactoBarrioVeredaNombre = registroPersonaData.OTRO_BARRIO_VEREDA;


            //DATOS CONTACTO ALTERNO
            tomaDeclaracion.DatoAlternoContactoPais = registroPersonaData.ID_PAIS_ALTERNO;
            tomaDeclaracion.DatoAlternoContactoDepartamento = registroPersonaData.ID_DEPARTAMENTO_ALTERNO;
            tomaDeclaracion.DatoAlternoContactoMunicipio = registroPersonaData.ID_MUNICIPIO_ALTERNO;
            tomaDeclaracion.DatoAlternoContactoDireccion = registroPersonaData.DIRECCION_ALTERNA;
            tomaDeclaracion.DatoContactoAlternoIndicativo = registroPersonaData.INDICATIVO_TELEFONO_ALTERNO.ToString();
            tomaDeclaracion.DatoAlternoContactoTelefonoFijo = registroPersonaData.TELEFONO_ALTERNO;
            tomaDeclaracion.DatoAlternoContactoTelefonoCelular = registroPersonaData.MOVIL_ALTERNO;
            tomaDeclaracion.DatoAlternoContactoCorreoElectronico = registroPersonaData.EMAIL_ALTERNO;
            
            //ENTORNO
            tomaDeclaracion.DatoAlternoContactoTipoEntorno = (eTipoEntorno?)registroPersonaData.PARAM_TIPO_ENTORNO_ALT;
            tomaDeclaracion.DatoAlternoContactoLocalidadCorregimientoId = registroPersonaData.PARAM_LOCALIDAD_CORREG_ALT;
            tomaDeclaracion.DatoAlternoContactoLocalidadCorregimientoNombre = registroPersonaData.OTRO_LOCALIDAD_CORREG_ALT;
            tomaDeclaracion.DatoAlternoContactoBarrioVeredaId = registroPersonaData.PARAM_BARRIO_VEREDA_ALT;
            tomaDeclaracion.DatoAlternoContactoBarrioVeredaNombre = registroPersonaData.OTRO_BARRIO_VEREDA_ALT;
        }
        #endregion
    }
}
