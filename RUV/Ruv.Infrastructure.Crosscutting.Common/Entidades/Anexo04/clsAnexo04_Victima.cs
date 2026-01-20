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
    public partial class clsAnexo04_Victima : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
    {
        public clsAnexo04_Victima()
        {
            Afectacion = new clsAnexo_Afectacion() { Victima = this };
            DenunciaPrevia = new clsAnexo_DenunciaPrevia() { AnexoPadre = this.AnexoPadre };

            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        public string Scope { get { return "Anexo 04"; } }

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

        #region PREGUNTA 9 AL 12

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
                    VictimaDesaparecida = 0;
                    SePresentoEventoAnterior = null;
                    SePresentoEventoPosterior = null;
                    ActividadAlDesaparecer = null;
                    QuedoMenorDesprotegido = null;
                    HaRealizadoBusquedaDeVictima = null;
                }
                 * */
                ReportarCambioPropiedad("VictimaDeEsteHecho");
                ReportarCambioPropiedad("VictimaDesaparecida");
                ReportarCambioPropiedad("SePresentoEventoAnterior");
                ReportarCambioPropiedad("SePresentoEventoPosterior");
                ReportarCambioPropiedad("ActividadAlDesaparecer");
                ReportarCambioPropiedad("QuedoMenorDesprotegido");
                ReportarCambioPropiedad("HaRealizadoBusquedaDeVictima");
                
            }
        }

        private int? _VictimaDesaparecida;
        [DataMember]
        public int? VictimaDesaparecida
        {
            get { return _VictimaDesaparecida; }
            set
            {
                _VictimaDesaparecida = value;
                ReportarCambioPropiedad("VictimaDesaparecida");
                ReportarCambioPropiedad("HaRealizadoBusquedaDeVictima");
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
                ReportarCambioPropiedad("DenunciaPrevia");
            }
        }

        #endregion

        #region PREGUNTA 13

        private int? _SePresentoEventoAnterior;
        [DataMember]
        public int? SePresentoEventoAnterior
        {
            get { return _SePresentoEventoAnterior; }
            set
            {
                _SePresentoEventoAnterior = value;
                ReportarCambioPropiedad("SePresentoEventoAnterior");
            }
        }

        private int? _SePresentoEventoPosterior;
        [DataMember]
        public int? SePresentoEventoPosterior
        {
            get { return _SePresentoEventoPosterior; }
            set
            {
                _SePresentoEventoPosterior = value;
                ReportarCambioPropiedad("SePresentoEventoPosterior");
            }
        }

        private string _ActividadAlDesaparecer;
        [DataMember]
        public string ActividadAlDesaparecer
        {
            get { return _ActividadAlDesaparecer; }
            set
            {
                _ActividadAlDesaparecer = value;
                ReportarCambioPropiedad("ActividadAlDesaparecer");                
            }
        }

        private int? _QuedoMenorDesprotegido;
        [DataMember]
        public int? QuedoMenorDesprotegido
        {
            get { return _QuedoMenorDesprotegido; }
            set
            {
                _QuedoMenorDesprotegido = value;
                if (value != 1) MenorDesprotegidoId = null;
                ReportarCambioPropiedad("QuedoMenorDesprotegido");
                ReportarCambioPropiedad("MenorDesprotegidoId");
            }
        }

        private int? _MenorDesprotegidoId;
        [DataMember]
        public int? MenorDesprotegidoId
        {
            get { return _MenorDesprotegidoId; }
            set
            {
                _MenorDesprotegidoId = value;
                ReportarCambioPropiedad("MenorDesprotegidoId");
                ReportarCambioPropiedad("QuedoMenorDesprotegido");
            }
        }

        private int? _HaRealizadoBusquedaDeVictima;
        [DataMember]
        public int? HaRealizadoBusquedaDeVictima
        {
            get { return _HaRealizadoBusquedaDeVictima; }
            set
            {
                _HaRealizadoBusquedaDeVictima = value;
                if (value != 1) HarealizadoBusquedaAnteEntidad = null;
                ReportarCambioPropiedad("HaRealizadoBusquedaDeVictima");
                ReportarCambioPropiedad("HarealizadoBusquedaAnteEntidad");
                ReportarCambioPropiedad("VictimaDesaparecida");
            }
        }

        private string _HarealizadoBusquedaAnteEntidad;
        [DataMember]
        public string HarealizadoBusquedaAnteEntidad
        {
            get { return _HarealizadoBusquedaAnteEntidad; }
            set
            {
                _HarealizadoBusquedaAnteEntidad = value;
                ReportarCambioPropiedad("HarealizadoBusquedaAnteEntidad");
                ReportarCambioPropiedad("HaRealizadoBusquedaDeVictima");
            }
        }

        #endregion
    }
}
