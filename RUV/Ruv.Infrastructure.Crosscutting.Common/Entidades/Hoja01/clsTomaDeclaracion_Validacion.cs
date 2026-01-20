using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsTomaDeclaracion : clsEntidadBase, IDataErrorInfo
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
                    case "LugarDeclaracionDepartamento":
                        if (!LugarDeclaracionDepartamento.HasValue)
                            resultado = "El departamento es obligatorio";
                        break;

                    case "LugarDeclaracionMunicipio":
                        if (!LugarDeclaracionMunicipio.HasValue)
                            resultado = "El municipio es obligatorio";
                        break;

                    case "LugarDeclaracionEntidadMunicipio":
                        if (!LugarDeclaracionEntidadMunicipio.HasValue)
                            resultado = "La entidad que atiende es obligatoria";
                        break;

                    case "FechaDeclaracion":
                        if (!FechaDeclaracion.HasValue)
                            resultado = "La fecha de la declaración es obligatoria";
                        if (FechaDeclaracion > DateTime.Today)
                            resultado = "La fecha de declaracion no puede ser mayor a la fecha actual";
                        if (FechaDeclaracion < Convert.ToDateTime("2012/01/02"))
                            resultado = string.Format("La fecha de declaracion '{0}' no puede ser menor al {1}", Convert.ToDateTime(FechaDeclaracion).ToString("dd MMM yyyy"), Convert.ToDateTime("2012/01/02").ToString("dd MMM yyyy"));
                        break;
                    //====================================================================

                    case "DeclarantePrimerNombre":
                        resultado = clsValidadorEntidades.ValidarN1(DeclarantePrimerNombre, "Primer Nombre del declarante", true);
                        break;

                    case "DeclaranteSegundoNombre":
                        resultado = clsValidadorEntidades.ValidarN2A1A2(DeclaranteSegundoNombre, "Segundo Nombre del declarante", false);
                        break;

                    case "DeclarantePrimerApellido":
                        resultado = clsValidadorEntidades.ValidarN2A1A2(DeclarantePrimerApellido, "Primer Apellido del declarante", true);
                        break;

                    case "DeclaranteSegundoApellido":
                        resultado = clsValidadorEntidades.ValidarN2A1A2(DeclaranteSegundoApellido, "Segundo Apellido del declarante", false);
                        break;

                    case "DeclaranteTipoDocumento":
                        if (!DeclaranteTipoDocumento.HasValue)
                            resultado = "El tipo de documento es obligatorio";
                        break;

                    case "DeclaranteNumeroDocumento":
                        // Para los tipos que no tienen numero de documento no se hace la validacion
                        if (DeclaranteTipoDocumento != (int)eTipoDocumento.NoInforma
                            && DeclaranteTipoDocumento != (int)eTipoDocumento.Indocumentado
                            && DeclaranteTipoDocumento != (int)eTipoDocumento.IndocumentadoExtranjero
                            && DeclaranteTipoDocumento != (int)eTipoDocumento.NoSabe
                            && DeclaranteTipoDocumento != (int)eTipoDocumento.NoResponde)
                        {
                            var ValidacionDocumentoDeclarante =
                              clsValidadorEntidades.ValidarDocumentoIdentificacion(true,
                              DeclaranteTipoDocumento, DeclaranteNumeroDocumento);
                            if (!ValidacionDocumentoDeclarante.Item1)
                                resultado = ValidacionDocumentoDeclarante.Item2;
                        }
                        break;

                    case "DeclaranteFechaNacimiento":
                        if (DeclaranteFechaNacimiento.HasValue)
                        {
                            resultado = clsValidadorEntidades.ValidarFechaNacimiento(true, DeclaranteFechaNacimiento, DeclaranteTipoDocumento);

                            if (resultado != null)
                                break;
                            //Validar si tiene tutor
                            int years = DateTime.Today.Subtract((DateTime)DeclaranteFechaNacimiento).Days / 365;

                            if (false && years > 100)
                                resultado = "El declarante debe tener una edad menor o igual a 100 años";
                            else if (DeclaranteFechaNacimiento >= DateTime.Today)
                                resultado = "La fecha de nacimiento del declarante debe ser menor a la fecha actual";
                            //Segun ficha de control de cambios un mayor de edad tambien puede tener tutor o funcionario.
                            //else if (Encargado.RepresentanteTipo == (int)eGaranteTipos.Tutor && years >= 18)
                            //    resultado = "El declarante debe ser menor de edad cuando tienen un tutor";
                            else if (Encargado.RepresentanteTipo != (int)eGaranteTipos.Tutor
                                        && Encargado.RepresentanteTipo != (int)eGaranteTipos.FuncionarioAutoridadCompetente
                                        && years < 18)
                                resultado = "El declarante debe tener un tutor cuando es menor de edad";
                        }
                        else
                            resultado = "La fecha de nacimiento del declarante debe tener formato valido y ser menor a la fecha actual";
                        break;

                    //====================================================================

                    case "DatoContactoDireccion":
                        if (string.IsNullOrWhiteSpace(DatoContactoDireccion))
                            resultado = "La dirección del declarante es obligatoria";
                        break;

                    case "DatoContactoDepartamento":
                        if (!DatoContactoDepartamento.HasValue)
                            resultado = "El departamento de contacto es obligatorio";
                        break;

                    case "DatoContactoMunicipio":
                        if (!DatoContactoMunicipio.HasValue)
                            resultado = "El municipio de contacto es obligatorio";
                        break;

                    case "DatoContactoTelefonoFijo":
                        if (!string.IsNullOrWhiteSpace(DatoContactoTelefonoFijo))
                        {
                            var validador = clsValidadorEntidades.ValidarNumeroTelefonico(DatoContactoTelefonoFijo, "telefono fijo", 10, 0, false);
                            if (!validador.Item1)
                                resultado = validador.Item2;
                        }
                        break;

                    case "DatoContactoTelefonoCelular":
                        if (!string.IsNullOrWhiteSpace(DatoContactoTelefonoCelular))
                        {
                            var validador = clsValidadorEntidades.ValidarNumeroTelefonico(DatoContactoTelefonoCelular, "telefono celular", 13, 0, false);
                            if (!validador.Item1)
                                resultado = validador.Item2;
                        }
                        break;

                    case "DatoContactoCorreoElectronico":
                        if (!string.IsNullOrEmpty(DatoContactoCorreoElectronico))
                        {
                            var validador = clsValidadorEntidades.ValidarCorreoElectronico(DatoContactoCorreoElectronico, "correo electronico", 7, false);
                            if (!validador.Item1)
                                resultado = validador.Item2;
                        }
                        else
                            resultado = "El correo electronico de contacto es obligatorio";
                        break;

                    //====================================================================

                    case "DatoAlternoContactoDireccion":
                        if (string.IsNullOrWhiteSpace(DatoAlternoContactoDireccion))
                            if (DatoAlternoContactoDepartamento.HasValue && DatoAlternoContactoMunicipio.HasValue
                                     && string.IsNullOrWhiteSpace(DatoAlternoContactoTelefonoFijo))
                                resultado = "Como definió departamento y municipio de contacto alterno, debe introduccir una dirección o un télefono alterno";
                        break;

                    case "DatoAlternoContactoDepartamento":
                        if (!DatoAlternoContactoDepartamento.HasValue)
                        {
                            if (!string.IsNullOrWhiteSpace(DatoAlternoContactoDireccion))
                                resultado = "Como definió una dirección de contacto alterno, debe definir el departamento y municipio de esta dirección";
                            else if (!string.IsNullOrWhiteSpace(DatoAlternoContactoTelefonoFijo))
                                resultado = "Como definió un télefono de contacto alterno, debe definir el departamento y municipio de este télefono";
                        }
                        break;

                    case "DatoAlternoContactoMunicipio":
                        if (DatoAlternoContactoDepartamento.HasValue && !DatoAlternoContactoMunicipio.HasValue)
                            resultado = "Debe especificar el municipio de contactoa alterno";
                        break;

                    case "DatoAlternoContactoTelefonoFijo":
                        if (!string.IsNullOrWhiteSpace(DatoAlternoContactoTelefonoFijo))
                        {
                            var validador = clsValidadorEntidades.ValidarNumeroTelefonico(DatoAlternoContactoTelefonoFijo, "telefono fijo alterno", 10, 0, false);
                            if (!validador.Item1)
                                resultado = validador.Item2;
                        }
                        else if (DatoAlternoContactoDepartamento.HasValue && DatoAlternoContactoMunicipio.HasValue
                                       && string.IsNullOrWhiteSpace(DatoAlternoContactoDireccion))
                            resultado = "Como definió departamento y municipio de contacto alterno, debe introduccir un télefono fijo o una dirección alterna";
                        break;

                    case "DatoAlternoContactoTelefonoCelular":
                        if (!string.IsNullOrWhiteSpace(DatoAlternoContactoTelefonoCelular))
                        {
                            var validador = clsValidadorEntidades.ValidarNumeroTelefonico(DatoAlternoContactoTelefonoCelular, "telefono celular alterno", 13, 0, false);
                            if (!validador.Item1)
                                resultado = validador.Item2;
                        }
                        break;

                    case "DatoAlternoContactoCorreoElectronico":
                        if (!string.IsNullOrEmpty(DatoAlternoContactoCorreoElectronico))
                        {
                            var validador = clsValidadorEntidades.ValidarCorreoElectronico(DatoAlternoContactoCorreoElectronico, "correo electronico alterno", 7, false);
                            if (!validador.Item1)
                                resultado = validador.Item2;
                        }
                        break;
                    //====================================================================
                    case "MedioDeContactoMensajeTexto":
                        if (!MedioDeContactoMensajeTexto.HasValue)
                            resultado = "Debe indicar se desea recibir mensajes de texto a través del celular";
                        break;
                    case "MedioDeContactoCorreoElectronico":
                        if (!MedioDeContactoCorreoElectronico.HasValue)
                            resultado = "Debe indicar se desea recibir mensajes a través de correo electrónico";
                        break;
                    case "MedioDeContactoMensajeVoz":
                        if (!MedioDeContactoMensajeVoz.HasValue)
                            resultado = "Debe indicar se desea recibir mensajes de voz a través del teléfono fijo";
                        break;
                    case "Hechos":
                        if (SeFinaliza)
                        {
                            if ((Hechos == null || !Hechos.Any(x => x > 0))
                              && string.IsNullOrWhiteSpace(HechosOtrosCual))
                                resultado = "Debe indicar al menos un hecho o escribirlo en \"Otro\"";
                        }
                        break;
                    case "HechosOtrosCual":
                        if (SeFinaliza)
                        {
                            if ((Hechos == null || !Hechos.Any(x => x > 0)) && string.IsNullOrWhiteSpace(HechosOtrosCual))
                                resultado = "Debe indicar al menos un hecho o escribirlo en \"Otro\"";
                        }
                        break;
                    case "DeclaranteNacionalidad":
                        if (!DeclaranteNacionalidad.HasValue || DeclaranteNacionalidad.Value == 0)
                            resultado = "La Nacionalidad del Declarante es obligatoria";
                        break;
                }

                return resultado;
            }
        }
        public string Error
        {
            get { return null; }
        }

        #endregion
    }
}
