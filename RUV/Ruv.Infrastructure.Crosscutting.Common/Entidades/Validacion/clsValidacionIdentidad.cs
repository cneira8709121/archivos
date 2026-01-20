using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Validacion
{
    [DataContract]
    public class clsValidacionIdentidad : INotifyPropertyChanged
    {
        private int? tipoDeclaracion;
        [DataMember]
        [Required]
        public int? TipoDeclaracion
        {
            get { 
                return tipoDeclaracion; 
            }
            set { tipoDeclaracion = value;
                ReportarCambioPropiedad("TipoDeclaracion");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string numeroDocumento;

        [DataMember]
        public string NumeroDocumento
        {
            get { return numeroDocumento; }
            set
            {
                numeroDocumento = value;
                ReportarCambioPropiedad("NumeroDocumento");
                ReportarCambioPropiedad("Valido");
            }
        }

        private int? idTipoDocumento;

        [DataMember]
        public int? IdTipoDocumento
        {
            get { return idTipoDocumento; }
            set
            {
                idTipoDocumento = value;
                ReportarCambioPropiedad("IdTipoDocumento");
                ReportarCambioPropiedad("Valido");
            }
        }


        private string primerNombre;
        [DataMember]
        public string PrimerNombre
        {
            get { return primerNombre; }
            set
            {
                primerNombre = value;
                ReportarCambioPropiedad("PrimerNombre");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string segundoNombre;
        [DataMember]
        public string SegundoNombre
        {
            get { return segundoNombre; }
            set
            {
                segundoNombre = value;
                ReportarCambioPropiedad("SegundoNombre");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string primerApellido;
        [DataMember]
        public string PrimerApellido
        {
            get { return primerApellido; }
            set
            {
                primerApellido = value;
                ReportarCambioPropiedad("PrimerApellido");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string segundoApellido;
        [DataMember]
        public string SegundoApellido
        {
            get { return segundoApellido; }
            set
            {
                segundoApellido = value;
                ReportarCambioPropiedad("SegundoApellido");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string correo;
        [DataMember]
        public string Correo
        {
            get { return correo; }
            set
            {
                correo = value;
                ReportarCambioPropiedad("Correo");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string celular;
        [DataMember]
        public string Celular
        {
            get { return celular; }
            set
            {
                celular = value;
                ReportarCambioPropiedad("Celular");
                ReportarCambioPropiedad("Valido");
            }
        }

        private bool _valido;
        public bool Valido
        {
            get {
                _valido = Validar();
                return _valido;
            } set {
                _valido = value;
                ReportarCambioPropiedad("Valido");
            }
        }

        private bool Validar()
        {
            bool result = false;
            if(tipoDeclaracion == eTipoTomaDeclaracion.Virtual.GetHashCode())
            {
                result = tipoDeclaracion.HasValue && tipoDeclaracion.Value > 0 &&
                IdTipoDocumento > 0 &&
                !string.IsNullOrEmpty(NumeroDocumento) &&
                !string.IsNullOrEmpty(PrimerNombre) &&
                !string.IsNullOrEmpty(PrimerApellido) &&
                (!string.IsNullOrEmpty(Correo) ||
                !string.IsNullOrEmpty(Celular));
            }
            else
            {
                result = tipoDeclaracion.HasValue && tipoDeclaracion.Value > 0 &&
                IdTipoDocumento > 0 &&
                !string.IsNullOrEmpty(NumeroDocumento) &&
                !string.IsNullOrEmpty(PrimerNombre) &&
                !string.IsNullOrEmpty(PrimerApellido);
            }
            if(IdTipoDocumento.HasValue && IdTipoDocumento.Value != Common.eTipoDocumento.CedulaCiudadania.GetHashCode())
            {
                result = false;
            }
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ReportarCambioPropiedad(string nombrePropiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));

            }
        }

    }
}
