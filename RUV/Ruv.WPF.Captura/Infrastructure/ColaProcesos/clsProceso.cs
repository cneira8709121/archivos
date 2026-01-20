using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Ruv.WPF.Captura.Infrastructure.ColaProcesos
{
    public class clsProceso : INotifyPropertyChanged
    {
        private string _Id;
        public string Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                ReportarCambioPropiedad("Id");
            }
        }

        private string _ArchivoDeclaracion;
        public string ArchivoDeclaracion
        {
            get { return _ArchivoDeclaracion; }
            set
            {
                _ArchivoDeclaracion = value;
                ReportarCambioPropiedad("ArchivoDeclaracion");
            }
        }

        private string _NombreDeclarante;
        public string NombreDeclarante
        {
            get { return _NombreDeclarante; }
            set
            {
                _NombreDeclarante = value;
                ReportarCambioPropiedad("NombreDeclarante");
            }
        }


        private string _ArchivoDocumentoEscaneado;
        public string ArchivoDocumentoEscaneado
        {
            get { return _ArchivoDocumentoEscaneado; }
            set
            {
                _ArchivoDocumentoEscaneado = value;
                ReportarCambioPropiedad("ArchivoDocumentoEscaneado");
            }
        }

        private DateTime _FechaEnCola;
        /// <summary>
        /// Fecha en que el item entró a la cola por primera vez,
        /// </summary>
        public DateTime FechaEnCola
        {
            get { return _FechaEnCola; }
            set
            {
                _FechaEnCola = value;
                ReportarCambioPropiedad("FechaEnCola");
            }
        }

        private DateTime? _FechaUltimaTransmision;
        /// <summary>
        /// Fecha de la última transmisión o intento de transmisión.
        /// </summary>
        public DateTime? FechaUltimaTransmision
        {
            get { return _FechaUltimaTransmision; }
            set
            {
                _FechaUltimaTransmision = value;
                ReportarCambioPropiedad("FechaUltimaTransmision");
            }
        }

        private int _Estado;
        public int Estado
        {
            get { return _Estado; }
            set
            {
                _Estado = value;
                ReportarCambioPropiedad("Estado");
            }
        }

        private StringCollection _ErroresDB;
        public StringCollection ErroresDB
        {
            get { return _ErroresDB; }
            set
            {
                _ErroresDB = value;

                ReportarCambioPropiedad("ErroresDB");
            }
        }

        private StringCollection _AdvertenciasDB;
        public StringCollection AdvertenciasDB
        {
            get { return _AdvertenciasDB; }
            set
            {
                _AdvertenciasDB = value;
                ReportarCambioPropiedad("AdvertenciasDB");
            }
        }


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void ReportarCambioPropiedad(string nombrePropiedad)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));


        }

        #endregion
    }
}