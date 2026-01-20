using System;
using System.ComponentModel;

namespace Ruv.WPF.Captura.Infrastructure
{
    public class clsElementoSeleccionable : INotifyPropertyChanged
    {

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Texto;
        public string Texto
        {
            get { return _Texto; }
            set { _Texto = value; }
        }

        private Boolean _Seleccionado;
        public Boolean Seleccionado
        {
            get { return _Seleccionado; }
            set { _Seleccionado = value; }
        }

        private bool _EsOtro;
        public bool EsOtro
        {
            get { return _EsOtro; }
            set { _EsOtro = value; }
        }

        private int _Numero;

        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }


        void ReportarCambio(string nombrePropiedad)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}