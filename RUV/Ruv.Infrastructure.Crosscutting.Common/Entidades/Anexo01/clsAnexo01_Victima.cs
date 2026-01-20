using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
    [DataContract]
    public partial class clsAnexo01_Victima : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
    {
        public clsAnexo01_Victima()
        {
            Bienes = new ObservableCollection<clsAnexo01_Victima_Bien>();
            Afectacion = new clsAnexo_Afectacion() { Victima = this };
            DenunciaPrevia = new clsAnexo_DenunciaPrevia() { AnexoPadre = this.AnexoPadre };

            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        public string Scope { get { return "Anexo 01"; } }

        private int? _PersonaAfectadaId;
        [DataMember]
        public int? PersonaAfectadaId
        {
            get { return _PersonaAfectadaId; }
            set
            {
                _PersonaAfectadaId = value;
                ReportarCambioPropiedad("PersonaAfectadaId");
            }
        }

        #region PREGUNTA 9

        private int? _VictimaDeEsteHecho;
        [DataMember]
        public int? VictimaDeEsteHecho
        {
            get { return _VictimaDeEsteHecho; }
            set
            {
                _VictimaDeEsteHecho = value;
                /*FICHA CONTROL DE CAMBIOS RUV 27-03-12
                    No se debe bloquear ningun campo cuando se marque la persona como "no víctima" 
              if (value != 1)
              {
                  //Afectacion.Afectado = 0;  Se solicitó que la afectacion no depende de si es victima del hecho
                  //DenunciaPrevia.SePresento = 0;    Se solicitó que la denuncia previa no depende de si es victima del hecho
            
                  AtencionMedicaRecibio = 0;            
                  Bienes.Clear();
            
              }
                   * */
                ReportarCambioPropiedad("VictimaDeEsteHecho");
            }
        }

        #endregion

        #region PREGUNTA 10

        private clsAnexo_Afectacion _Afectacion;
        [DataMember]
        public clsAnexo_Afectacion Afectacion
        {
            get { return _Afectacion; }
            set
            {
                _Afectacion = value;
                ReportarCambioPropiedad("Afectacion");
            }
        }

        #endregion

        #region PREGUNTA 11

        private clsAnexo_DenunciaPrevia _DenunciaPrevia;
        [DataMember]
        public clsAnexo_DenunciaPrevia DenunciaPrevia
        {
            get { return _DenunciaPrevia; }
            set
            {
                _DenunciaPrevia = value;
                ReportarCambioPropiedad("DenunciaPrevia");
            }
        }

        #endregion

        #region PREGUNTA 12

        private ObservableCollection<clsAnexo01_Victima_Bien> _Bienes;
        /// <summary>
        /// Daño en bienes muebles o inmuebeles.
        /// </summary>
        [DataMember]
        public ObservableCollection<clsAnexo01_Victima_Bien> Bienes
        {
            get { return _Bienes; }
            set
            {
                _Bienes = value;
                ReportarCambioPropiedad("Bienes");
            }
        }

        #endregion

        #region PREGUNTA 13

        private int? _AtencionMedicaRecibio;
        /// <summary>
        /// Si/No
        /// </summary>
        [DataMember]
        public int? AtencionMedicaRecibio
        {
            get { return _AtencionMedicaRecibio; }
            set
            {
                _AtencionMedicaRecibio = value;
                if (value != 1)
                {
                    AtencionEntidadMedica = null;
                    AtencionMedicaDepartamento = null;
                    AtencionMedicaMunicipio = null;
                }
                ReportarCambioPropiedad("AtencionMedicaRecibio");
                ReportarCambioPropiedad("AtencionEntidadMedica");
                ReportarCambioPropiedad("AtencionMedicaDepartamento");
                ReportarCambioPropiedad("AtencionMedicaMunicipio");
            }
        }

        private string _AtencionEntidadMedica;
        [DataMember]
        public string AtencionEntidadMedica
        {
            get { return _AtencionEntidadMedica; }
            set
            {
                _AtencionEntidadMedica = value;
                ReportarCambioPropiedad("AtencionEntidadMedica");
            }
        }

        private Int64? _AtencionMedicaPais = 48L;
        [DataMember]
        public Int64? AtencionMedicaPais
        {
            get { return _AtencionMedicaPais; }
            set
            {
                _AtencionMedicaPais = value;
                ReportarCambioPropiedad("AtencionMedicaPais");
            }
        }

        private Int64? _AtencionMedicaDepartamento;
        [DataMember]
        public Int64? AtencionMedicaDepartamento
        {
            get { return _AtencionMedicaDepartamento; }
            set
            {
                _AtencionMedicaDepartamento = value;
                ReportarCambioPropiedad("AtencionMedicaDepartamento");
            }
        }

        private Int64? _AtencionMedicaMunicipio;
        [DataMember]
        public Int64? AtencionMedicaMunicipio
        {
            get { return _AtencionMedicaMunicipio; }
            set
            {
                _AtencionMedicaMunicipio = value;
                ReportarCambioPropiedad("AtencionMedicaMunicipio");
            }
        }

        #endregion
    }
}
