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
    public partial class clsAnexo09_Victima : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
    {
        public clsAnexo09_Victima()
        {
            Afectacion = new clsAnexo_Afectacion() { Victima = this };
            DenunciaPrevia = new clsAnexo_DenunciaPrevia() { AnexoPadre = this.AnexoPadre };

            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        public string Scope { get { return "Anexo 09"; } }

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

        #region PREGUNTAS 8 AL 11

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
                    AtencionMedicaRecibioAtencionMedica = 0;
                    AtencionMedicaSolicitoAyuda = 0;
                    AtencionMedicaAyudaRecibida = null;
                    AtencionMedicaRecibioAyuda = 0;
                }
                 * */
                ReportarCambioPropiedad("VictimaDeEsteHecho");
                ReportarCambioPropiedad("AtencionMedicaRecibioAtencionMedica");
                ReportarCambioPropiedad("AtencionMedicaSolicitoAyuda");
                ReportarCambioPropiedad("AtencionMedicaRecibioAyuda");
            }
        }

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

        private clsAnexo_DenunciaPrevia _DenunciaPrevia;
        [DataMember]
        public clsAnexo_DenunciaPrevia DenunciaPrevia
        {
            get { return _DenunciaPrevia; }
            set
            {
                _DenunciaPrevia = value;
                ReportarCambioPropiedad("SePresento");
            }
        }

        #endregion

        #region PREGUNTA 12

        private int? _AtencionMedicaRecibioAtencionMedica;
        [DataMember]
        public int? AtencionMedicaRecibioAtencionMedica
        {
            get { return _AtencionMedicaRecibioAtencionMedica; }
            set
            {
                _AtencionMedicaRecibioAtencionMedica = value;
                if (value != 1)
                {
                    AtencionMedicaEntidad = null;
                    RecibioAtencionMedicaDepartamento = null;
                    RecibioAtencionMedicaMunicipio = null;
                }
                ReportarCambioPropiedad("AtencionMedicaRecibioAtencionMedica");
                ReportarCambioPropiedad("AtencionMedicaEntidad");
                ReportarCambioPropiedad("RecibioAtencionMedicaDepartamento");
                ReportarCambioPropiedad("RecibioAtencionMedicaMunicipio");
            }
        }

        private string _AtencionMedicaEntidad;
        [DataMember]
        public string AtencionMedicaEntidad
        {
            get { return _AtencionMedicaEntidad; }
            set
            {
                _AtencionMedicaEntidad = value;
                ReportarCambioPropiedad("AtencionMedicaEntidad");
            }
        }

        // Nuevo: 12-Ene-2012
        private Int64? _RecibioAtencionMedicaPais = 48L;
        [DataMember]
        public Int64? RecibioAtencionMedicaPais
        {
            get { return _RecibioAtencionMedicaPais; }
            set
            {
                _RecibioAtencionMedicaPais = value;
                ReportarCambioPropiedad("RecibioAtencionMedicaPais");
            }
        }

        // Nuevo: 12-Ene-2012
        private Int64? _RecibioAtencionMedicaDepartamento;
        [DataMember]
        public Int64? RecibioAtencionMedicaDepartamento
        {
            get { return _RecibioAtencionMedicaDepartamento; }
            set
            {
                _RecibioAtencionMedicaDepartamento = value;
                ReportarCambioPropiedad("RecibioAtencionMedicaDepartamento");
            }
        }

        // Nuevo: 12-Ene-2012
        private Int64? _RecibioAtencionMedicaMunicipio;
        [DataMember]
        public Int64? RecibioAtencionMedicaMunicipio
        {
            get { return _RecibioAtencionMedicaMunicipio; }
            set
            {
                _RecibioAtencionMedicaMunicipio = value;
                ReportarCambioPropiedad("RecibioAtencionMedicaMunicipio");
            }
        }

        private int? _AtencionMedicaSolicitoAyuda;
        [DataMember]
        public int? AtencionMedicaSolicitoAyuda
        {
            get { return _AtencionMedicaSolicitoAyuda; }
            set
            {
                _AtencionMedicaSolicitoAyuda = value;
                if (value != 1) AtencionMedicaSolicitoAyudaEntidad = null;
                ReportarCambioPropiedad("AtencionMedicaSolicitoAyuda");
                ReportarCambioPropiedad("AtencionMedicaSolicitoAyudaEntidad");
            }
        }

        private string _AtencionMedicaSolicitoAyudaEntidad;
        [DataMember]
        public string AtencionMedicaSolicitoAyudaEntidad
        {
            get { return _AtencionMedicaSolicitoAyudaEntidad; }
            set
            {
                _AtencionMedicaSolicitoAyudaEntidad = value;
                ReportarCambioPropiedad("AtencionMedicaSolicitoAyudaEntidad");
            }
        }

        private int? _AtencionMedicaRecibioAyuda;
        [DataMember]
        public int? AtencionMedicaRecibioAyuda
        {
            get { return _AtencionMedicaRecibioAyuda; }
            set
            {
                _AtencionMedicaRecibioAyuda = value;
                if (value != 1) AtencionMedicaAyudaRecibida = null;
                ReportarCambioPropiedad("AtencionMedicaRecibioAyuda");
                ReportarCambioPropiedad("AtencionMedicaAyudaRecibida");
            }
        }

        private string _AtencionMedicaAyudaRecibida;
        [DataMember]
        public string AtencionMedicaAyudaRecibida
        {
            get { return _AtencionMedicaAyudaRecibida; }
            set
            {
                _AtencionMedicaAyudaRecibida = value;
                ReportarCambioPropiedad("AtencionMedicaAyudaRecibida");
            }
        }

        #endregion
    }
}
