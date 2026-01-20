using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Clase genérica con información sobre una denuncia previa.
    /// </summary>
    [DataContract]
    public partial class clsAnexo_DenunciaPrevia : clsEntidadBase, IDataErrorInfo, IValidationEntity
    {
        public clsAnexo_DenunciaPrevia()
        {
            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        private int? _SePresento;
        /// <summary>
        /// Si/No
        /// </summary>
        [DataMember]
        public int? SePresento
        {
            get { return _SePresento; }
            set
            {
                _SePresento = value;
                if (value != 1)
                {
                    Entidad = null;
                    Fecha = null;
                    OtraEntidad = null;
                    Pais = null;
                    Departamento = null;
                    Municipio = null;
                    Codigo = null;
                }
                ReportarCambioPropiedad("SePresento");
                ReportarCambioPropiedad("Entidad");
                ReportarCambioPropiedad("OtraEntidad");
                ReportarCambioPropiedad("Fecha");
                ReportarCambioPropiedad("Departamento");
                ReportarCambioPropiedad("Municipio");
            }
        }

        private int? _Entidad;
        [DataMember]
        public int? Entidad
        {
            get { return _Entidad; }
            set
            {
                _Entidad = value;
                ReportarCambioPropiedad("Entidad");
                ReportarCambioPropiedad("SePresento");
            }
        }

        private string _OtraEntidad;
        [DataMember]
        public string OtraEntidad
        {
            get { return _OtraEntidad; }
            set
            {
                _OtraEntidad = value;
                ReportarCambioPropiedad("OtraEntidad");
                //ReportarCambioPropiedad("SePresento");
            }
        }

        private DateTime? _Fecha;
        [DataMember]
        public DateTime? Fecha
        {
            get { return _Fecha; }
            set
            {
                _Fecha = value;
                ReportarCambioPropiedad("Fecha");
                ReportarCambioPropiedad("SePresento");
            }
        }

        private Int64? _Pais;// = 48L;
        [DataMember]
        public Int64? Pais
        {
            get { return _Pais; }
            set
            {
                _Pais = value;
                ReportarCambioPropiedad("Pais");
                ReportarCambioPropiedad("SePresento");
            }
        }

        private Int64? _Departamento;
        [DataMember]
        public Int64? Departamento
        {
            get { return _Departamento; }
            set
            {
                _Departamento = value;
                ReportarCambioPropiedad("Departamento");
                ReportarCambioPropiedad("SePresento");
            }
        }

        private Int64? _Municipio;
        [DataMember]
        public Int64? Municipio
        {
            get { return _Municipio; }
            set
            {
                _Municipio = value;
                ReportarCambioPropiedad("Municipio");
                ReportarCambioPropiedad("SePresento");
            }
        }

        private string _Codigo;
        [DataMember]
        public string Codigo
        {
            get { return _Codigo; }
            set
            {
                _Codigo = value;
                ReportarCambioPropiedad("Codigo");
                ReportarCambioPropiedad("SePresento");
            }
        }

        public string Scope
        {
            get { return "Denuncia Previa"; }
        }
    }
}
