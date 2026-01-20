using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.Validacion
{
    [DataContract]
    public class clsPersonaIdentidad : INotifyPropertyChanged
    {

        public clsPersonaIdentidad() { }



        private int? tipoDeclaracion;
        [DataMember]
        public int? TipoDeclaracion
        {
            get
            {
                return tipoDeclaracion;
            }
            set
            {
                tipoDeclaracion = value;
                ReportarCambioPropiedad("TipoDeclaracion");
            }
        }

        private string numeroDocumento;

        [DataMember]
        public string NumeroDocumento
        {
            get { return numeroDocumento; }
            set { numeroDocumento = value;
                ReportarCambioPropiedad("NumeroDocumento");
                ReportarCambioPropiedad("Valido");
            }
        }

        private int? idTipoDocumento;

        [DataMember]
        public int? IdTipoDocumento
        {
            get { return idTipoDocumento; }
            set { idTipoDocumento = value;
                ReportarCambioPropiedad("IdTipoDocumento");
                ReportarCambioPropiedad("Valido");
            }
        }


        private string primerNombre;
        [DataMember]
        public string PrimerNombre
        {
            get { return primerNombre; }
            set { primerNombre = value;
                ReportarCambioPropiedad("PrimerNombre");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string segundoNombre;
        [DataMember]
        public string SegundoNombre
        {
            get { return segundoNombre; }
            set { segundoNombre = value;
                ReportarCambioPropiedad("SegundoNombre");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string primerApellido;
        [DataMember]
        public string PrimerApellido
        {
            get { return primerApellido; }
            set { primerApellido = value;
                ReportarCambioPropiedad("PrimerApellido");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string segundoApellido;
        [DataMember]
        public string SegundoApellido
        {
            get { return segundoApellido; }
            set { segundoApellido = value;
                ReportarCambioPropiedad("SegundoApellido");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string correo;
        [DataMember]
        public string Correo
        {
            get { return correo; }
            set { correo = value;
                ReportarCambioPropiedad("Correo");
                ReportarCambioPropiedad("Valido");
            }
        }

        private string celular;
        [DataMember]
        public string Celular
        {
            get { return celular; }
            set { celular = value;
                ReportarCambioPropiedad("Celular");
                ReportarCambioPropiedad("Valido");
            }
        }
        private string vigencia;
        [DataMember]
        public string Vigencia
        {
            get { return vigencia; }
            set { vigencia = value; }
        }
        private string resultado;

        

        [DataMember]
        public string Resultado
        {
            get { return resultado; }
            set { resultado = value; }
        }

        [DataMember]
        public List<clsPreguntasValidacion> PreguntasValidacion { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        void ReportarCambioPropiedad(string nombrePropiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));

            }
        }
    }
}
