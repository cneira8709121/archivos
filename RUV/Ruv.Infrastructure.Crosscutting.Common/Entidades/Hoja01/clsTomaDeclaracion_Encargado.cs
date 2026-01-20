using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;


namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public partial class clsTomaDeclaracion_Encargado : clsEntidadBase, IDataErrorInfo, IValidationEntity
    {

        /// <summary>
        /// Referencia a la declaración padre.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public clsTomaDeclaracion TomaDeclaracion { get; set; }

        public string Scope { get { return "HOJA 1"; } }

        #region CONSTRUCTOR
        public clsTomaDeclaracion_Encargado()
        {
            _EstadoRegistro = eEstadoRegistro.Insertar;
        }
        #endregion

        #region PREGUNTA 4

        private int? _RepresentanteTipo;
        [DataMember]
        public int? RepresentanteTipo
        {
            get { return _RepresentanteTipo; }
            set
            {
                _RepresentanteTipo = value;
                                
                if (!RepresentanteTipo.HasValue || RepresentanteTipo.Value == 0)
                {
                    //Limpiar datos del Representante
                    RepresentantePrimerNombre = null;
                    RepresentanteSegundoNombre = null;
                    RepresentantePrimerApellido = null;
                    RepresentanteSegundoApellido = null;
                    RepresentanteTipoDocumento = null;
                    RepresentanteNumeroDocumento = null;
                    RepresentanteDireccion = null;
                    RepresentanteTelefono = null;
                }
                ReportarCambioPropiedad("RepresentanteTipo");


                //Validar RepresentanteTipoAutoridadCompetente
                ReportarCambioPropiedad("RepresentanteTipoAutoridadCompetente");

                //Validar Todos los campos
                ReportarCambioPropiedad("RepresentantePrimerNombre");
                ReportarCambioPropiedad("RepresentanteSegundoNombre");
                ReportarCambioPropiedad("RepresentantePrimerApellido");
                ReportarCambioPropiedad("RepresentanteSegundoApellido");
                ReportarCambioPropiedad("RepresentanteTipoDocumento");
                ReportarCambioPropiedad("RepresentanteNumeroDocumento");
                ReportarCambioPropiedad("RepresentanteDireccion");
                ReportarCambioPropiedad("RepresentanteTelefono");

                //Validar tipo de Encargado
                if (TomaDeclaracion != null)
                    TomaDeclaracion.ReportarCambioPropiedad("DeclaranteFechaNacimiento");
            }
        }

        private string _RepresentanteTipoAutoridadCompetente;
        [DataMember]
        public string RepresentanteTipoAutoridadCompetente
        {
            get { return _RepresentanteTipoAutoridadCompetente; }
            set
            {
                _RepresentanteTipoAutoridadCompetente = value;
                ReportarCambioPropiedad("RepresentanteTipoAutoridadCompetente");

                //Validar RepresentanteTipo
                ReportarCambioPropiedad("RepresentanteTipo");
            }
        }

        private string _RepresentantePrimerNombre;
        [DataMember]
        public string RepresentantePrimerNombre
        {
            get { return _RepresentantePrimerNombre; }
            set
            {
                _RepresentantePrimerNombre = value;

                ReportarCambioPropiedad("RepresentantePrimerNombre");
                ReportarCambioPropiedad("RepresentanteNombreCompleto");

                //Validar RepresentanteTipo
                ReportarCambioPropiedad("RepresentanteTipo");
            }
        }

        private string _RepresentanteSegundoNombre;
        [DataMember]
        public string RepresentanteSegundoNombre
        {
            get { return _RepresentanteSegundoNombre; }
            set
            {
                _RepresentanteSegundoNombre = value;
                ReportarCambioPropiedad("RepresentanteSegundoNombre");
                ReportarCambioPropiedad("RepresentanteNombreCompleto");

                //Validar RepresentanteTipo
                ReportarCambioPropiedad("RepresentanteTipo");

            }
        }

        private string _RepresentantePrimerApellido;
        [DataMember]
        public string RepresentantePrimerApellido
        {
            get { return _RepresentantePrimerApellido; }
            set
            {
                _RepresentantePrimerApellido = value;
                ReportarCambioPropiedad("RepresentantePrimerApellido");
                ReportarCambioPropiedad("RepresentanteNombreCompleto");

                //Validar RepresentanteTipo
                ReportarCambioPropiedad("RepresentanteTipo");

            }
        }

        private string _RepresentanteSegundoApellido;
        [DataMember]
        public string RepresentanteSegundoApellido
        {
            get { return _RepresentanteSegundoApellido; }
            set
            {
                _RepresentanteSegundoApellido = value;
                ReportarCambioPropiedad("RepresentanteSegundoApellido");
                ReportarCambioPropiedad("RepresentanteNombreCompleto");

                //Validar RepresentanteTipo
                ReportarCambioPropiedad("RepresentanteTipo");

            }
        }

        /// <summary>
        /// Campo calculado, no requiere almacenamiento.
        /// </summary>
        public string RepresentanteNombreCompleto
        {
            get
            {
                string cadena = "";
                return cadena.UnirCadenas(
                  RepresentantePrimerNombre, RepresentanteSegundoNombre,
                  RepresentantePrimerApellido, RepresentanteSegundoApellido);
            }
        }


        private int? _RepresentanteTipoDocumento;
        [DataMember]
        public int? RepresentanteTipoDocumento
        {
            get { return _RepresentanteTipoDocumento; }
            set
            {
                _RepresentanteTipoDocumento = value;
                if (value == null)
                    RepresentanteNumeroDocumento = null;
                else
                if (Enum.GetValues(typeof(eTipoDocumentoSinNumero)).Cast<int>().Contains((int)value)) RepresentanteNumeroDocumento = null; 
                                        
                ReportarCambioPropiedad("RepresentanteTipoDocumento");
                ReportarCambioPropiedad("RepresentanteNumeroDocumento");

                //Validar RepresentanteTipo
                ReportarCambioPropiedad("RepresentanteTipo");

            }
        }

        private string _RepresentanteNumeroDocumento;
        [DataMember]
        public string RepresentanteNumeroDocumento
        {
            get { return _RepresentanteNumeroDocumento; }
            set
            {
                _RepresentanteNumeroDocumento = value;
                ReportarCambioPropiedad("RepresentanteNumeroDocumento");
                ReportarCambioPropiedad("RepresentanteTipoDocumento");

                //Validar RepresentanteTipo
                ReportarCambioPropiedad("RepresentanteTipo");

            }
        }

        private string _RepresentanteDireccion;
        [DataMember]
        public string RepresentanteDireccion
        {
            get { return _RepresentanteDireccion; }
            set
            {
                _RepresentanteDireccion = value;
                ReportarCambioPropiedad("RepresentanteDireccion");

                //Validar RepresentanteTipo
                ReportarCambioPropiedad("RepresentanteTipo");

            }
        }

        private string _RepresentanteTelefono;
        [DataMember]
        public string RepresentanteTelefono
        {
            get { return _RepresentanteTelefono; }
            set
            {
                _RepresentanteTelefono = value;
                ReportarCambioPropiedad("RepresentanteTelefono");

                //Validar RepresentanteTipo
                ReportarCambioPropiedad("RepresentanteTipo");

            }
        }

        #endregion

    }
}
