using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsAnexo13 : clsEntidadBase, IDataErrorInfo, IAnexo
    {
        #region VALIDACIONES

        [System.Xml.Serialization.XmlIgnore]
        public string this[string columnName]
        {
            get
            {
                if (!ValidationManager.ValidateProperty(clsDeclaracion.ConfiguracionValidaciones, Scope, columnName))
                    return null;
                string resultado = null;
                switch (columnName)
                {
                    case "ListaPersonas":
                    case "ListaPersonasOrdenada":
                      if (!ListaPersonas.Any())
                        resultado = "Debe relacionarse al menos una persona en la lista de afectados";
                      else
                      {
                        if (ListaPersonas.Count(x => x.Relacion.HasValue && x.Relacion.Value == (int)eRelacion.Jefe_de_hogar) == 0)
                          resultado = "Debe existir un Jefe(a) de hogar";
                      }
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
                        if (DatoContactoTelefonoFijo != null)
                        {
                          var validador = clsValidadorEntidades.ValidarNumeroTelefonico(DatoContactoTelefonoFijo, "telefono fijo", 7, 0, false);
                          if (!validador.Item1)
                            resultado = validador.Item2;
                        }
                        break;

                    case "DatoContactoTelefonoCelular":
                        if (!string.IsNullOrWhiteSpace(DatoContactoTelefonoCelular))
                        {
                          var validador = clsValidadorEntidades.ValidarNumeroTelefonico(DatoContactoTelefonoCelular, "telefono celular", 10, 10, false);
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
                          var validador = clsValidadorEntidades.ValidarNumeroTelefonico(DatoAlternoContactoTelefonoFijo, "telefono fijo alterno", 7, 0, false);
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
                          var validador = clsValidadorEntidades.ValidarNumeroTelefonico(DatoAlternoContactoTelefonoCelular, "telefono celular alterno", 10, 10, false);
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
                }

                return resultado;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public string Error
        {
            get { return null; }
        }

        #endregion
    }
}
