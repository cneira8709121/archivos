using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;
using System.Xml.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    /// <summary>
    /// Clase genérica con información sobre la afectación.
    /// </summary>
    [DataContract]
    public partial class clsAnexo_Afectacion : clsEntidadBase, IDataErrorInfo, IValidationEntity
    {
        public clsAnexo_Afectacion()
        {
            TiposDeAfectacion = new List<int>();
            _EstadoRegistro = eEstadoRegistro.Insertar;

            _VistaTiposDeAfectacion = CollectionViewSource.GetDefaultView(TiposDeAfectacion);
            _VistaTiposDeAfectacion.CollectionChanged += delegate
            {
                Console.WriteLine("=== Sentí el cambio === ");
            };
        }
        [XmlIgnore]
        private ICollectionView _VistaTiposDeAfectacion;
        [XmlIgnore]
        public ICollectionView VistaTiposDeAfectacion
        {
            get { return _VistaTiposDeAfectacion; }
            set { _VistaTiposDeAfectacion = value; }
        }


        private int? _Afectado;
        /// <summary>
        /// Si/No
        /// </summary>
        [DataMember]
        public int? Afectado
        {
            get { return _Afectado; }
            set
            {
                _Afectado = value;
                if (value != 1) TiposDeAfectacion = new List<int>();
                ReportarCambioPropiedad("Afectado");
                ReportarCambioPropiedad("TiposDeAfectacion");
            }
        }

        private int? afectadoId;
        /// <summary>
        /// Id De la persona afectada
        /// </summary>
        [DataMember]
        public int? AfectadoId
        {
            get { return afectadoId; }
            set { afectadoId = value; }
        }
        

        private List<int> _TiposDeAfectacion;
        [DataMember]
        public List<int> TiposDeAfectacion
        {
            get { return _TiposDeAfectacion; }
            set
            {
                _TiposDeAfectacion = value;
                ReportarCambioPropiedad("TiposDeAfectacion");
                ReportarCambioPropiedad("Afectado");
            }
        }

        private string _Otro;
        [DataMember]
        public string Otro
        {
            get { return _Otro; }
            set
            {
                _Otro = value;
                ReportarCambioPropiedad("Otro");
            }
        }
        public string Scope
        {
            get { return "Anexo Afectacion"; }
        }

    }
}
