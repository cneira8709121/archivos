using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsTomaDeclaracion_Encargado : clsEntidadBase, IDataErrorInfo
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
                    case "RepresentanteTipo":
                    case "RepresentanteTipoAutoridadCompetente":
                        if (RepresentanteTipo.HasValue
                            && RepresentanteTipo.Value == (int)eOpcionesOtros.Representante
                            && string.IsNullOrWhiteSpace(RepresentanteTipoAutoridadCompetente))
                        {
                            resultado = "Al seleccionar 'Funcionario o autoridad competente' debe digitar el nombre de la misma";
                        }

                        if (resultado == null && columnName == "RepresentanteTipo" && !RepresentanteTipo.HasValue)
                        {
                            string existeAlgunDato = ExisteAlgunDato();
                            if (existeAlgunDato != null)
                            {
                                resultado = string.Format("Hay información en '{0}' del garante, debe seleccionar el tipo de garante", existeAlgunDato);
                            }
                        }
                        break;
                    case "RepresentanteTipoDocumento":
                        if (RepresentanteTipo.HasValue)
                            resultado = ValidarDatosObligatorios(RepresentanteTipoDocumento, "Tipo de Documento");
                        if (resultado == null)
                        {
                            var ValidacionRepresentanteNumeroDocumento =
                                  clsValidadorEntidades.ValidarDocumentoIdentificacion(false,
                                  RepresentanteTipoDocumento, RepresentanteNumeroDocumento);
                            if (!ValidacionRepresentanteNumeroDocumento.Item1)
                                resultado = ValidacionRepresentanteNumeroDocumento.Item2;
                        }
                        break;
                    case "RepresentanteNumeroDocumento":
                         // Para los tipos que no tienen numero de documento no se hace la validacion
                        if (RepresentanteTipoDocumento != (int)eTipoDocumento.NoInforma
                            && RepresentanteTipoDocumento != (int)eTipoDocumento.Indocumentado
                            && RepresentanteTipoDocumento != (int)eTipoDocumento.NoSabe
                            && RepresentanteTipoDocumento != (int)eTipoDocumento.NoResponde)
                        {
                            if (RepresentanteTipo.HasValue)
                                resultado = ValidarDatosObligatorios(RepresentanteNumeroDocumento, "Numero de Documento");
                            if (resultado == null)
                            {
                                var ValidacionRepresentanteNumeroDocumento =
                                      clsValidadorEntidades.ValidarDocumentoIdentificacion(false,
                                      RepresentanteTipoDocumento, RepresentanteNumeroDocumento);
                                if (!ValidacionRepresentanteNumeroDocumento.Item1)
                                    resultado = ValidacionRepresentanteNumeroDocumento.Item2;
                            }
                        }
                        break;
                    case "RepresentantePrimerNombre":
                        if (RepresentanteTipo.HasValue)
                        {
                            resultado = clsValidadorEntidades.ValidarN1(RepresentantePrimerNombre, "Primer Nombre del garante", true);
                        }
                        break;
                    case "RepresentanteSegundoNombre":
                        if (RepresentanteTipo.HasValue)
                        {
                            resultado = clsValidadorEntidades.ValidarN2A1A2(RepresentanteSegundoNombre, "Segundo Nombre del garante", false);
                        }
                        break;
                    case "RepresentantePrimerApellido":
                        if (RepresentanteTipo.HasValue)
                        {
                            resultado = clsValidadorEntidades.ValidarN2A1A2(RepresentantePrimerApellido, "Primer Apellido del garante", true);
                        }
                        break;
                    case "RepresentanteSegundoApellido":
                        if (RepresentanteTipo.HasValue)
                        {
                            resultado = clsValidadorEntidades.ValidarN2A1A2(RepresentanteSegundoApellido, "Segundo Apellido del garante", false);
                        }
                        break;
                    //====================================================================
                }

                return resultado;
            }
        }

        #region Datos Obligatorios
        private string ExisteAlgunDato()
        {
            if (!string.IsNullOrWhiteSpace(RepresentantePrimerNombre))
                return "Primer Nombre";
            if (!string.IsNullOrWhiteSpace(RepresentanteSegundoNombre))
                return "Segundo Nombre";
            if (!string.IsNullOrWhiteSpace(RepresentantePrimerApellido))
                return "Primer Apellido";
            if (!string.IsNullOrWhiteSpace(RepresentanteSegundoApellido))
                return "Segundo Apellido";
            if (!string.IsNullOrWhiteSpace(RepresentanteNumeroDocumento))
                return "Numero de Documento";
            if (!string.IsNullOrWhiteSpace(RepresentanteDireccion))
                return "Direccion";
            if (!string.IsNullOrWhiteSpace(RepresentanteTelefono))
                return "Telefono";
            return null;
        }

        private string ValidarDatosObligatorios(string valor, string dato)
        {
            if (RepresentanteTipo.HasValue && string.IsNullOrWhiteSpace(valor))
            {
                return string.Format("Al seleccionar ayuda de un garante su '{0}' es obligatorio", dato);
            }
            return null;
        }

        private string ValidarDatosObligatorios(int? valor, string dato)
        {
            if (RepresentanteTipo.HasValue && valor == null)
                return string.Format("Al seleccionar ayuda de un garante su '{0}' es obligatorio", dato);
            else
                return null;
        }
        #endregion

        public string Error
        {
            get { return null; }
        }

        #endregion
    }
}
