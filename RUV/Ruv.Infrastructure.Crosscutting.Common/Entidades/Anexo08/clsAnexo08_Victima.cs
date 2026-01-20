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
    public partial class clsAnexo08_Victima : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
    {
        public clsAnexo08_Victima()
        {
            Afectacion = new clsAnexo_Afectacion() { Victima = this };
            DenunciaPrevia = new clsAnexo_DenunciaPrevia() { AnexoPadre = this.AnexoPadre };

            _EstadoRegistro = eEstadoRegistro.Insertar;

            //Afectacion.TiposDeAfectacion
        }

        public string Scope { get { return "Anexo 08"; } }

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

        #region PREGUNTAS 8 A 9

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
                    PersonaEstaSecuestrada = 0;
                    TipoDeSecuestro = null;
                    FinalidadSecuestroExtorsivo = null;
                    OtraFinalidadSecuestroOtro = null;
                    HanPedidoContraprestacionPorLibertad = null;
                    ContraprestacionPedida = null;
                    SituacionActualVictima = null;
                    ComoSeProdujoLiberacion = null;
                    FechaLiberacion = null;
                }
                 * */
                ReportarCambioPropiedad("VictimaDeEsteHecho");
                ReportarCambioPropiedad("SituacionActualVictima");

            }
        }

        private int? _PersonaEstaSecuestrada;
        [DataMember]
        public int? PersonaEstaSecuestrada
        {
            get { return _PersonaEstaSecuestrada; }
            set
            {
                _PersonaEstaSecuestrada = value;
                //if (value != 1)
                //{
                //    TipoDeSecuestro = null;
                //    FinalidadSecuestroExtorsivo = null;
                //    OtraFinalidadSecuestroOtro = null;
                //    HanPedidoContraprestacionPorLibertad = null;
                //    ContraprestacionPedida = null;
                //    SituacionActualVictima = null;
                //    ComoSeProdujoLiberacion = null;
                //    FechaLiberacion = null;
                //}                
                ReportarCambioPropiedad("PersonaEstaSecuestrada");
                ReportarCambioPropiedad("TipoDeSecuestro");
                ReportarCambioPropiedad("SituacionActualVictima");
            }
        }

        #endregion

        #region PREGUNTAS 11 Y 12

        private clsAnexo_Afectacion _Afectacion;
        [DataMember]
        public clsAnexo_Afectacion Afectacion
        {
            get { return _Afectacion; }
            set
            {
                _Afectacion = value;
                ReportarCambioPropiedad("Afectacion");
                ReportarCambioPropiedad("SituacionActualVictima");
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

        #region PREGUNTA 13

        private int? _TipoDeSecuestro;
        [DataMember]
        public int? TipoDeSecuestro
        {
            get { return _TipoDeSecuestro; }
            set
            {
                _TipoDeSecuestro = value;
                if (value != (int)eTipoSecuestro.EXTORSIVO) FinalidadSecuestroExtorsivo = null;
                ReportarCambioPropiedad("TipoDeSecuestro");
                ReportarCambioPropiedad("PersonaEstaSecuestrada");
                ReportarCambioPropiedad("FinalidadSecuestroExtorsivo");
                ReportarCambioPropiedad("OtraFinalidadSecuestroOtro");
                ReportarCambioPropiedad("HanPedidoContraprestacionPorLibertad");
            }
        }

        private int? _FinalidadSecuestroExtorsivo;
        [DataMember]
        public int? FinalidadSecuestroExtorsivo
        {
            get { return _FinalidadSecuestroExtorsivo; }
            set
            {
                _FinalidadSecuestroExtorsivo = value;
                ReportarCambioPropiedad("FinalidadSecuestroExtorsivo");
                ReportarCambioPropiedad("TipoDeSecuestro");
                ReportarCambioPropiedad("OtraFinalidadSecuestroOtro");
            }
        }

        private string _OtraFinalidadSecuestroOtro;
        [DataMember]
        public string OtraFinalidadSecuestroOtro
        {
            get { return _OtraFinalidadSecuestroOtro; }
            set
            {
                _OtraFinalidadSecuestroOtro = value;
                ReportarCambioPropiedad("OtraFinalidadSecuestroOtro");
                ReportarCambioPropiedad("FinalidadSecuestroExtorsivo");
            }
        }

        private int? _HanPedidoContraprestacionPorLibertad;
        [DataMember]
        public int? HanPedidoContraprestacionPorLibertad
        {
            get { return _HanPedidoContraprestacionPorLibertad; }
            set
            {
                _HanPedidoContraprestacionPorLibertad = value;
                if (value == 0) ContraprestacionPedida = null; 
                ReportarCambioPropiedad("HanPedidoContraprestacionPorLibertad");
                ReportarCambioPropiedad("ContraprestacionPedida");
            }
        }

        private string _ContraprestacionPedida;
        [DataMember]
        public string ContraprestacionPedida
        {
            get { return _ContraprestacionPedida; }
            set
            {
                _ContraprestacionPedida = value;
                ReportarCambioPropiedad("ContraprestacionPedida");
                ReportarCambioPropiedad("HanPedidoContraprestacionPorLibertad");
            }
        }

        private int? _SituacionActualVictima;
        [DataMember]
        public int? SituacionActualVictima
        {
            get { return _SituacionActualVictima; }
            set
            {
                _SituacionActualVictima = value;
                if (value != (int)eSituacionVictimaSecuestro.LIBRE)
                {
                    ComoSeProdujoLiberacion = null;
                    FechaLiberacion = null;
                }
                ReportarCambioPropiedad("Afectacion");
                ReportarCambioPropiedad("SituacionActualVictima");
                ReportarCambioPropiedad("ComoSeProdujoLiberacion");
                ReportarCambioPropiedad("FechaLiberacion");
                
            }
        }

        #endregion

        #region PREGUNTA 14

        private int? _ComoSeProdujoLiberacion;
        [DataMember]
        public int? ComoSeProdujoLiberacion
        {
            get { return _ComoSeProdujoLiberacion; }
            set
            {
                _ComoSeProdujoLiberacion = value;
                ReportarCambioPropiedad("ComoSeProdujoLiberacion");
            }
        }

        private DateTime? _FechaLiberacion;
        [DataMember]
        public DateTime? FechaLiberacion
        {
            get { return _FechaLiberacion; }
            set
            {
                _FechaLiberacion = value;
                ReportarCambioPropiedad("FechaLiberacion");
            }
        }

        #endregion

    }
}
