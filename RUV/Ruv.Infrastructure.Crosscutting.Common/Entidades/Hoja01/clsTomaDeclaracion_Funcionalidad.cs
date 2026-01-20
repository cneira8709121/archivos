using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Linq;




namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    public partial class clsTomaDeclaracion : clsEntidadBase, IDataErrorInfo
    {

        #region ESTA PROPIEDADES NO SE ALMACENAN, EXISTEN SOLO PARA REFERENCIA HACIA EL DECLARANTE.

        /// <summary>
        /// Objeto referencia del declarante.
        /// </summary>
        clsPersonaAfectada PADeclarante = null;

        [System.Xml.Serialization.XmlIgnore()]
        public string DeclarantePrimerNombre
        {
            get
            {
                if (!_DeclaranteId.HasValue || PADeclarante == null) return null;
                return PADeclarante.PrimerNombre;
            }
            set
            {
                if (PADeclarante == null) return;
                PADeclarante.PrimerNombre = value;
                ReportarCambioPropiedad("DeclarantePrimerNombre");
                ReportarCambioPropiedad("DeclaranteNombreCompleto");
            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public string DeclaranteSegundoNombre
        {
            get
            {
                if (!_DeclaranteId.HasValue || PADeclarante == null) return null;
                return PADeclarante.SegundoNombre;
            }
            set
            {
                if (PADeclarante == null) return;
                PADeclarante.SegundoNombre = value;
                ReportarCambioPropiedad("DeclaranteSegundoNombre");
                ReportarCambioPropiedad("DeclaranteNombreCompleto");
            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public string DeclarantePrimerApellido
        {
            get
            {
                if (!_DeclaranteId.HasValue || PADeclarante == null) return null;
                return PADeclarante.PrimerApellido;
            }
            set
            {
                if (PADeclarante == null) return;
                PADeclarante.PrimerApellido = value;
                ReportarCambioPropiedad("DeclarantePrimerApellido");
                ReportarCambioPropiedad("DeclaranteNombreCompleto");
            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public string DeclaranteSegundoApellido
        {
            get
            {
                if (!_DeclaranteId.HasValue || PADeclarante == null) return null;
                return PADeclarante.SegundoApellido;
            }
            set
            {
                if (PADeclarante == null) return;
                PADeclarante.SegundoApellido = value;
                ReportarCambioPropiedad("DeclaranteSegundoApellido");
                ReportarCambioPropiedad("DeclaranteNombreCompleto");
            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public string DeclaranteNombreCompleto
        {
            get
            {
                if (!_DeclaranteId.HasValue || PADeclarante == null) return null;

                string Cadena = "";
                return Cadena.UnirCadenas(
                  PADeclarante.PrimerNombre, PADeclarante.SegundoNombre
                , PADeclarante.PrimerApellido, PADeclarante.SegundoApellido);
            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public int? DeclaranteTipoDocumento
        {
            get
            {
                if (!_DeclaranteId.HasValue || PADeclarante == null) return null;
                return PADeclarante.TipoDocumento;
            }
            set
            {
                if (PADeclarante == null) return;
                PADeclarante.TipoDocumento = value;
                if (value == null)
                    DeclaranteNumeroDocumento = null;
                else
                {
                    if (Enum.GetValues(typeof(eTipoDocumentoSinNumero)).Cast<int>().Contains((int)value)) DeclaranteNumeroDocumento = null;
                    if((int)value == (int)eTipoDocumento.CedulaCiudadania ||
                        (int)value == (int)eTipoDocumento.LibretaMilitar ||
                        (int)value == (int)eTipoDocumento.TarjetaIdentidad ||
                        (int)value == (int)eTipoDocumento.RegistroCivil ||
                        (int)value == (int)eTipoDocumento.NUIP ||
                        (int)value == (int)eTipoDocumento.NIP ||
                        (int)value == (int)eTipoDocumento.Indocumentado)
                    {
                        DeclaranteNacionalidad = 48;
                    }
                    else
                    {
                        DeclaranteNacionalidad = 0;
                    }
                }
                  

                ReportarCambioPropiedad("DeclaranteTipoDocumento");
                ReportarCambioPropiedad("DeclaranteNumeroDocumento");
                ReportarCambioPropiedad("DeclaranteFechaNacimiento");
                ReportarCambioPropiedad("DeclaranteNacionalidad");
            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public string DeclaranteNumeroDocumento
        {
            get
            {
                if (!_DeclaranteId.HasValue || PADeclarante == null) return null;
                return PADeclarante.NumeroDocumento;
            }
            set
            {
                if (PADeclarante == null) return;
                PADeclarante.NumeroDocumento = value;
                ReportarCambioPropiedad("DeclaranteNumeroDocumento");
            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public DateTime? DeclaranteFechaNacimiento
        {
            get
            {
                if (!_DeclaranteId.HasValue || PADeclarante == null) return null;
                return PADeclarante.FechaNacimiento;
            }
            set
            {
                if (PADeclarante == null) return;
                PADeclarante.FechaNacimiento = value;
                ReportarCambioPropiedad("DeclaranteFechaNacimiento");

                //Validar tipo de Encargado
                if (Encargado != null)
                    Encargado.ReportarCambioPropiedad("RepresentanteTipo");
            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public int? DeclaranteNacionalidad
        {
            get
            {
                if (!_DeclaranteId.HasValue || PADeclarante == null) return null;
                return PADeclarante.Nacionalidad;
            }
            set
            {
                if (PADeclarante == null) return;
                PADeclarante.Nacionalidad = value;

                ReportarCambioPropiedad("DeclaranteNacionalidad");
                ReportarCambioPropiedad("DeclaranteTipoDocumento");
            }
        }

        #endregion

    }
}
