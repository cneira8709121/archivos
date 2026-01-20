using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsPersonaAfectada : clsEntidadBase, IDataErrorInfo
    {
        #region VALIDACIONES

        public string this[string columnName]
        {
            get
            {
                if (!ValidationManager.ValidateProperty(clsDeclaracion.ConfiguracionValidaciones, Scope, columnName))
                    return null;

                string resultado = null;
                switch (columnName)
                {
                    case "NumeroConsecutivo":
                        if (string.IsNullOrWhiteSpace(PrimerNombre))
                            resultado = "El número de consecutivo es obligatorio";
                        break;

                    case "PrimerNombre":
                        resultado = clsValidadorEntidades.ValidarN1(PrimerNombre, "Primer Nombre", true);
                        break;
                    case "SegundoNombre":
                        resultado = clsValidadorEntidades.ValidarN2A1A2(SegundoNombre, "Segundo Nombre", false);
                        break;
                    case "PrimerApellido":
                        resultado = clsValidadorEntidades.ValidarN2A1A2(PrimerApellido, "Primer Apellido", true);
                        break;
                    case "SegundoApellido":
                        resultado = clsValidadorEntidades.ValidarN2A1A2(SegundoApellido, "Segundo Apellido", false);
                        break;

                    case "TipoDocumento":
                        if (!TipoDocumento.HasValue)
                            resultado = "El tipo de documento es obligatorio";
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(NumeroDocumento))
                            {
                                resultado = ValidarPersonasRepetidas(TipoDocumento, NumeroDocumento);
                            }

                            if (TipoDocumento.Value == (int)eTipoDocumento.LibretaMilitar && Genero.HasValue && Genero.Value == (int)eGenero.Mujer)
                                resultado = "La libreta militar como tipo de documento no puede ser para una mujer";
                        }
                        break;
                    case "NumeroDocumento":
                        // Para los tipos que no tienen numero de documento no se hace la validacion
                        if (TipoDocumento != (int)eTipoDocumento.NoInforma
                            && TipoDocumento != (int)eTipoDocumento.Indocumentado
                            && TipoDocumento != (int)eTipoDocumento.IndocumentadoExtranjero
                            && TipoDocumento != (int)eTipoDocumento.NoSabe
                            && TipoDocumento != (int)eTipoDocumento.NoResponde)
                        {
                            var ValidacionNumeroDocumento =
                            clsValidadorEntidades.ValidarDocumentoIdentificacion(true,
                            TipoDocumento, NumeroDocumento);
                            if (!ValidacionNumeroDocumento.Item1)
                                resultado = ValidacionNumeroDocumento.Item2;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(NumeroDocumento))
                                resultado = "El tipo de documento seleccionado no permite digitar numero de documento";
                        }
                        break;
                    case "FechaNacimiento":
                        if (!FechaNacimiento.HasValue)
                        {
                            // La fecha de nacimiento de las personas que no son declarantes, no es obligatorio. Se valida en la hoja 1 para el declarante.
                            //resultado = "La fecha de nacimiento es obligatoria ó la digitada no es valida";
                        }
                        else
                        {
                            if (FechaNacimiento.Value.CompareTo(DateTime.Today) > 0)
                                resultado = "La fecha de Nacimiento, no puede ser mayor a la fecha actual";

                            if (TipoDocumento == (int)eTipoDocumento.CedulaCiudadania)
                                if (FechaNacimiento.Value.CompareTo(DateTime.Today.AddYears(-18)) > 0)
                                    resultado = "La fecha de nacimiento no concuerda con el tipo de documento seleccionado (Cedula de ciudadanía)";
                        }
                        break;
                    case "HechosVictimizantes":
                        if (!HechosVictimizantes.Any())
                            resultado = "Debe seleccionar al menos un hecho victimizante";
                        break;
                    case "Relacion":
                        if (!Relacion.HasValue)
                            resultado = "La relacion es obligatoria";
                        else
                        {
                            var DA = clsDeclaracion.DeclaracionActual;
                            var PA = DA.PersonasAfectadas.ListaPersonas;

                            if (Relacion == (int)eRelacion.Esposo_Compañero)
                            {
                                if (PA.Any(x => x.Relacion.HasValue && x.Relacion.Value == (int)eRelacion.Esposo_Compañero && x.ID != ID))
                                    resultado = "Ya existe un Esposo(a)/Compañero(a)";
                            }

                            if (Relacion == (int)eRelacion.Jefe_de_hogar)
                            {
                                if (PA.Any(x => x.Relacion.HasValue && x.Relacion.Value == (int)eRelacion.Jefe_de_hogar && x.ID != ID))
                                    resultado = "Ya existe un Jefe(a) de hogar";
                            }
                        }
                        break;
                    case "EstadoCivil":
                        if (!Relacion.HasValue)
                            resultado = "El estado civil es obligatorio";
                        break;
                    case "Genero":
                        if (!Genero.HasValue)
                            resultado = "El género es obligatorio";
                        else if (Genero.Value == (int)eGenero.Mujer && TipoDocumento.HasValue && TipoDocumento.Value == (int)eTipoDocumento.LibretaMilitar)
                            resultado = "Una mujer no puede tener libreta militar como tipo de documento";
                        break;
                    case "OrientacionSexual":
                        if (clsDeclaracion.DeclaracionActual.VersionFUD == 2) { 
                            if (!OrientacionSexual.HasValue)
                                resultado = "La orientación sexual es obligatoria";
                        }
                        break;
                    case "IdentidadGenero":
                        if (clsDeclaracion.DeclaracionActual.VersionFUD == 2)
                        {
                            if (!IdentidadGenero.HasValue)
                                resultado = "La identidad de género es obligatoria";
                        }
                        break;
                    case "Discapacidades":
                        if (!Discapacidades.Any())
                            resultado = "Debe seleccionar al menos una discapacidad o marcar la opción \"Ninguna\"";
                        break;
                    //case "OtraDiscapacidad":
                    //    if (Discapacidades.Contains((int)eDiscapacidades.Otra) && string.IsNullOrWhiteSpace(OtraDiscapacidad))
                    //        resultado = "Debe indicar en el campo 'Cual' la discapacidad";
                    //    else if (Discapacidades.Contains((int)eDiscapacidades.Otra) && OtraDiscapacidad.Length > 500)
                    //        resultado = "La descripcion de la discapacidad no puede ser mayor a 500 caracteres";
                    //    break;
                    case "PertenenciaEtnica":
                        if (!PertenenciaEtnica.HasValue)
                            resultado = "Debe indicar la pertenecia étnica, o marcar la opción 'Ninguna'";
                        break;
                    case "MujerCabezaDeHogar":
                        if (Genero.HasValue && !MujerCabezaDeHogar.HasValue)
                            resultado = "Debe indicar si es hombre o mujer cabeza de hogar";
                        break;
                    case "GestanteLactante":
                        if (!GestanteLactante.HasValue)
                            resultado = "Debe indicar si es o no gestante o lactante";
                        break;
                    case "OtraComunidadEtnica":
                        if (!string.IsNullOrWhiteSpace(OtraComunidadEtnica))
                            resultado = clsValidadorEntidades.ValidarAlfanumerico(OtraComunidadEtnica, "Otra Comunidad Étnica");
                        break;
                    case "Nacionalidad":
                        if (!Nacionalidad.HasValue || Nacionalidad.Value == 0)
                            resultado = "La Nacionalidad es obligatoria";
                        break;
                    case "Campesinado":
                        if (clsDeclaracion.DeclaracionActual.VersionFUD == 2) { 
                            if (!Campesinado.HasValue)
                                resultado = "Debe indicar si es o no campesinado";
                        }
                        break;
                    case "PersonaBuscadora":
                        if (clsDeclaracion.DeclaracionActual.VersionFUD == 2)
                        {
                            if (!PersonaBuscadora.HasValue)
                                resultado = "Debe indicar si es o no es persona buscadora";
                        }
                        break;
                }

                return resultado;
            }
        }

        /// <summary>
        /// Luis.Esteban 19Jun2012
        /// Validar que no existan personas repetidas.
        /// </summary>
        /// <returns></returns>
        string ValidarPersonasRepetidas(int? TipoDocumento, string NumeroDocumento)
        {
            clsDeclaracion DA = clsDeclaracion.DeclaracionActual;
            string resultado = null;
            List<clsPersonaAfectada> personaRepetida = new List<clsPersonaAfectada>();

            if (DA.PersonasAfectadas.ListaPersonas.Any(x => x.NumeroDocumento == NumeroDocumento && x.TipoDocumento == TipoDocumento && x.ID != ID))
                return string.Format(
                            "El documento '{0}' con el numero '{1}' ya existe en esta declaración lo cual no esta permitido.",
                            Enum.GetName(typeof(eTipoDocumento), TipoDocumento), NumeroDocumento);


            foreach (clsAnexo13 A13 in DA.A13)
            {
                //Se busca en los anexos 13
                if (A13.ListaPersonas.Any(x => x.NumeroDocumento == NumeroDocumento && x.TipoDocumento == TipoDocumento))
                    return string.Format(
                                "El documento '{0}' con el numero '{1}' ya existe en un anexo 13 de esta declaración lo cual no esta permitido.",
                                Enum.GetName(typeof(eTipoDocumento), TipoDocumento), NumeroDocumento);
            }

            return resultado;
        }
        public string Error
        {
            get { return null; }
        }

        #endregion

    }
}
