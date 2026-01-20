using System;
using System.ComponentModel;
using System.Windows;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Infrastructure
{
    public class clsBase : DependencyObject, INotifyPropertyChanged
    {
        internal void CambioEnPropiedad(string nombrePropiedad)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
        }

        private string _MensajeEstado;
        /// <summary>
        /// El último mensaje de estado.
        /// </summary>
        public string MensajeEstado
        {
            get { return _MensajeEstado; }
            set
            {
                _MensajeEstado = value;
                CambioEnPropiedad("MensajeEstado");
            }
        }

        private eErrores _TipoDeError;
        /// <summary>
        /// El tipo de error presentado.
        /// </summary>
        public eErrores TipoDeError
        {
            get { return _TipoDeError; }
            set
            {
                _TipoDeError = value;
                if (value == eErrores.Ninguno)
                    MensajeEstado = "";
                CambioEnPropiedad("TipoDeError");
            }
        }

        /// <summary>
        /// Verdadero: La última operacion en línea fué exitosa.
        /// </summary>
        private Boolean _OnlineOperation = true;
        public Boolean OnlineOperation
        {
            get { return _OnlineOperation; }
            set
            {
                if (_OnlineOperation && !value)
                {
                    // Si la operación pasó de correcta a incorrecta, pasar al modo offline.
                    RUV.I.Red.EstadoRed = eEstadoRed.NoDisponible;
                }
                _OnlineOperation = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Verdadero: Esta clase está realizando alguna operación y se encuentra 
        /// ocupada, por lo tanto debería esperarse a que termine antes de cerrarla.
        /// </summary>
        public Boolean EstaOcupado
        {
            get { return (Boolean)GetValue(EstaOcupadoProperty); }
            set
            {
                Dispatcher.BeginInvoke(
                   System.Windows.Threading.DispatcherPriority.Normal,
                   new Action(() =>
                   {
                       SetValue(EstaOcupadoProperty, value);
                   }));
            }
        }

        public static readonly DependencyProperty EstaOcupadoProperty =
            DependencyProperty.Register("EstaOcupado", typeof(Boolean),
            typeof(clsBase), new UIPropertyMetadata(false));

    }
}
