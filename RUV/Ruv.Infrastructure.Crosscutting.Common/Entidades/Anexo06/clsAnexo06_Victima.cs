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
    public partial class clsAnexo06_Victima : clsEntidadBase, IDataErrorInfo, IVictima, IValidationEntity
    {
        public clsAnexo06_Victima()
        {
            Afectacion = new clsAnexo_Afectacion() { Victima = this };
            DenunciaPrevia = new clsAnexo_DenunciaPrevia() { AnexoPadre = this.AnexoPadre };

            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        public string Scope { get { return "Anexo 06"; } }

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

        #region PREGUNTA 9 AL 13

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
              // Reportar adicionalmente los otros Si/No.
              // Si cambia a NO, todos los siguientes cambian a no para evitar la validacion.
              if (value != 1)
              {
                VictimaFallecida = 0;
                //DenunciaPrevia.SePresento = 0;    Se solicitó que la denuncia previa no depende de si es victima del hecho
                //Afectacion.Afectado = 0;  Se solicitó que la afectacion no depende de si es victima del hecho
                AlgunMenorQuedoHuerfano = 0;
                RecuerdaNumeroPersonasMuertas = 0;
              }
                */
                ReportarCambioPropiedad("VictimaDeEsteHecho");
            }
        }

        private int? _VictimaFallecida;
        [DataMember]
        public int? VictimaFallecida
        {
            get { return _VictimaFallecida; }
            set
            {
                _VictimaFallecida = value;
                ReportarCambioPropiedad("VictimaFallecida");

                var DA = clsDeclaracion.DeclaracionActual;
                if (DA != null)
                {
                    var EsteAnexo06 = (from A in DA.A06.AsEnumerable()
                                       where A.Victimas.Any(x => x.PersonaAfectadaId == this.PersonaAfectadaId)
                                       select A).FirstOrDefault();
                    if (EsteAnexo06 != null)
                        EsteAnexo06.ReportarCambioPropiedad("Victimas");
                }
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

        private int? _AlgunMenorQuedoHuerfano;
        [DataMember]
        public int? AlgunMenorQuedoHuerfano
        {
            get { return _AlgunMenorQuedoHuerfano; }
            set
            {
                _AlgunMenorQuedoHuerfano = value;
                if (value != 1)
                {
                    MenorDesprotegidoId = null;
                    MenorQuedoHuerfanoDe = null;
                }
                ReportarCambioPropiedad("AlgunMenorQuedoHuerfano");
                ReportarCambioPropiedad("MenorDesprotegidoId");
                ReportarCambioPropiedad("MenorQuedoHuerfanoDe");
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
                ReportarCambioPropiedad("AlgunMenorQuedoHuerfano");
            }
        }

        private int? _MenorQuedoHuerfanoDe;
        /// <summary>
        /// Padre/Madre/Padre y madre
        /// </summary>
        [DataMember]
        public int? MenorQuedoHuerfanoDe
        {
            get { return _MenorQuedoHuerfanoDe; }
            set
            {
                _MenorQuedoHuerfanoDe = value;
                ReportarCambioPropiedad("MenorQuedoHuerfanoDe");
                ReportarCambioPropiedad("AlgunMenorQuedoHuerfano");
            }
        }

        private int? _RecuerdaNumeroPersonasMuertas;
        [DataMember]
        public int? RecuerdaNumeroPersonasMuertas
        {
            get { return _RecuerdaNumeroPersonasMuertas; }
            set
            {
                _RecuerdaNumeroPersonasMuertas = value;
                if (value != 1) NumeroPersonasMuertasEnEsteHecho = null;
                ReportarCambioPropiedad("RecuerdaNumeroPersonasMuertas");
                ReportarCambioPropiedad("NumeroPersonasMuertasEnEsteHecho");
            }
        }

        private int? _NumeroPersonasMuertasEnEsteHecho;
        [DataMember]
        public int? NumeroPersonasMuertasEnEsteHecho
        {
            get { return _NumeroPersonasMuertasEnEsteHecho; }
            set
            {
                _NumeroPersonasMuertasEnEsteHecho = value;
                ReportarCambioPropiedad("NumeroPersonasMuertasEnEsteHecho");
                ReportarCambioPropiedad("RecuerdaNumeroPersonasMuertas");
            }
        }



        #endregion


    }
}
