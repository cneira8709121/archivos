using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ruv.WPF.Captura.Utils
{
    class CapturaGlosa : INotifyPropertyChanged, IDataErrorInfo
    {
        private int? _TipoDeRegistro;
        public int? TipoDeRegistro
        {
            get { return _TipoDeRegistro; }
            set
            {
                _TipoDeRegistro = value;
                ReportarCambio("TipoDeRegistro");
                ReportarCambio("Concepto");
            }
        }

        private int? _Categoria;
        public int? Categoria
        {
            get { return _Categoria; }
            set
            {
                _Categoria = value;
                ReportarCambio("Categoria");
            }
        }

        private int? _Concepto;
        public int? Concepto
        {
            get { return _Concepto; }
            set
            {
                _Concepto = value;
                ReportarCambio("Concepto");
            }
        }
        private DateTime _FechaAtencion;

        public DateTime FechaAtencion
        {
            get
            {
                if (_FechaAtencion == DateTime.MinValue)
                    return DateTime.Now;
                return _FechaAtencion;
                ;
            }
            set { _FechaAtencion = value; ReportarCambio("FechaAtencion"); }
        }


        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set
            {
                _Descripcion = value; ReportarCambio("Descripcion");
            }
        }
        Boolean formatoFechaValido(DateTime? propiedad)
        {
            DateTime fechaTent;
            if ((DateTime.TryParse(propiedad.ToString(), out fechaTent))
                && propiedad > new DateTime(1980, 1, 1) && propiedad < new DateTime(2020, 1, 1)
                )
                return true;
            else
                return false;
        }


        public bool HayParametrosMinimosParaRegistrar
        {
            get
            {
                var Valido = this["TipoDeRegistro"] == null
                    && this["Categoria"] == null
                    && this["Concepto"] == null
                    && this["Descripcion"] == null
                    && formatoFechaValido(FechaAtencion);
                return Valido;
            }
        }

        void ReportarCambio(string nombrePropiedad)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
                PropertyChanged(this, new PropertyChangedEventArgs("HayParametrosMinimosParaRegistrar"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {

                string Resultado = null;
                switch (columnName)
                {
                    case "TipoDeRegistro":
                        if (!TipoDeRegistro.HasValue)
                            Resultado = "El tipo de registro es obligatorio";
                        break;
                    case "Categoria":
                        if (!Categoria.HasValue)
                            Resultado = "La categoría es obligatoria";
                        break;
                    case "Concepto":
                        if (TipoDeRegistro == 1
                            && !Concepto.HasValue)
                            Resultado = "El concepto es obligatorio";
                        break;
                    case "Descripcion":
                        if (string.IsNullOrWhiteSpace(Descripcion))
                            Resultado = "La descripción es obligatoria";
                        break;
                }

                return Resultado;
            }
        }
    }
}
