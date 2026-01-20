using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public class clsEntidadBase : INotifyPropertyChanged
    {

        private int? _ID;
        /// <summary>
        /// El código de este registro.
        /// </summary>
        [DataMember]
        public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private Guid _ID_Interno;
        [DataMember]
        public Guid ID_Interno
        {
            get { return _ID_Interno; }
            set { _ID_Interno = value; }
        }

        


        /// <summary>
        /// Esta variable se usa para que al momento de cargar la declaracion desde el disco duro
        /// Respete los valores de la propiedad "EstadoRegistro" y no aplique la regla que esta en el set de dicha propiedad
        /// </summary>
        [XmlIgnore]
        private static Boolean _DesSerializando = false;

        [XmlIgnore]
        public static Boolean DesSerializando
        {
            get { return _DesSerializando; }
            set { _DesSerializando = value; }
        }

        protected eEstadoRegistro _EstadoRegistro = eEstadoRegistro.Insertar;
        /// <summary>
        /// El estado de este registro.
        /// </summary>
        [DataMember]
        public eEstadoRegistro EstadoRegistro
        {
            get { return _EstadoRegistro; }
            set
            {
                if (DesSerializando)
                {
                    _EstadoRegistro = value;
                    return;
                }

                if (
                  (_EstadoRegistro == eEstadoRegistro.Insertar
                  || _EstadoRegistro == eEstadoRegistro.Eliminado)
                  && value == eEstadoRegistro.Modificado)
                {
                    // Los insertados no se pueden pasar a Modificados.
                }
                else
                    _EstadoRegistro = value;
            }
        }

        private bool seFinaliza;

        public bool SeFinaliza
        {
            get { return seFinaliza; }
            set { seFinaliza = value; }
        }


        public void ReportarCambioPropiedad(string nombrePropiedad)
        {
            if (!clsDeclaracion.DesSerializando)
            {
                if (nombrePropiedad == "EstadoRegistro") return;
                EstadoRegistro = eEstadoRegistro.Modificado;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
                }
            }
        }

        public bool MostroAdvertenciaVictima1 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        #region FILTRO PARA COLECCCIONES

        /// <summary>
        /// Filtro para los ICollectionView que filtra los registros eliminados.
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        protected bool FiltroOmitirEliminados(object entidad)
        {
            return (entidad as clsEntidadBase).EstadoRegistro != eEstadoRegistro.Eliminado;
        }

        #endregion
    }
}
