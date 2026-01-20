using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;

namespace Ruv.Infrastructure.Crosscutting.Common
{
    //DELEGADOS
    public delegate string ValidacionesAlternasDelegate(string columnName);

    public delegate void Error(object sender, ErrorEventArgs e);

    public delegate void OptionHandler(object sender, OptionEventArgs e);

    public delegate void CambiaValor(object sender, EventArgs e);

    public delegate void SelectIndexChanged(object sender, EventArgs e);

    public delegate void OnNotificacionClick(object sender, NotificacionEventArgs e);

    public delegate void OnBtnClick(object sender, EventArgs e);

    public delegate void OnBtnHerramienta(object sender, HerramientasEventArgs e);

    public delegate void OnGuardarOkPersona(object sender, PersonaAnexoEventArgs e);

    public class NotificacionEventArgs : EventArgs
    {
        public string CMensaje { get; set; }
    }

    public class PersonaAnexoEventArgs : EventArgs{
        clsPersonaAnexo _Persona;
        public clsPersonaAnexo Persona
        {
            get { return _Persona; }
            set { _Persona = value; }
        }
        public PersonaAnexoEventArgs(clsPersonaAnexo _persona)
            : base()
        {
            this._Persona = _persona;
        }
    }

    public class ErrorEventArgs : EventArgs
    {
        string errorMensaje;

        public string ErrorMensaje
        {
            get { return errorMensaje; }
            set { errorMensaje = value; }
        }
        public ErrorEventArgs(string _error)
            : base()
        {
            this.errorMensaje = _error;
        }
    }

    public class HerramientasEventArgs : EventArgs
    {
        private clsHerramientaAnexoPer herramienta;
        private int index;
        public clsHerramientaAnexoPer Herramienta { get { return herramienta; } }
        public int Index { get { return index; } }


        public HerramientasEventArgs(clsHerramientaAnexoPer _herramienta, int _index)
            : base()
        {
            this.herramienta = _herramienta;
            this.index = _index;
        }
    }

    public delegate void OnBtnNuevoHecho(object sender, HechoEventArgs e);
    public class HechoEventArgs : EventArgs
    {
        private clsHecho hecho;
        public clsHecho Hecho { get { return hecho; } }


        public HechoEventArgs(clsHecho _hecho)
            : base()
        {
            this.hecho = _hecho;
        }
    }



    public delegate void FiltroHandler(object sender, FiltroEventArgs e);
    public class FiltroEventArgs : EventArgs
    {
        private clsFiltro filtro;

        public clsFiltro Filtro
        {
            get { return filtro; }
            set { filtro = value; }
        }


        public FiltroEventArgs(clsFiltro _filtro)
            : base()
        {
            this.filtro = _filtro;
        }
    }
    public class OptionEventArgs : EventArgs
    {
        private string controlName;
        public string ControlName { get { return controlName; } }


        public OptionEventArgs(string _controlName)
            : base()
        {
            this.controlName = _controlName;
        }
    }

    public delegate void OnBtnGuardarPuntoClick(object sender, EventArgs e);
}
