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
    public partial class
        clsAnexo05 : clsEntidadBase, IDataErrorInfo, IAnexo, IValidationEntity
    {
        public clsAnexo05()
        {
            FechaYLugar = new clsAnexo_FechaYLugar();
            InformacionDeArribo = new clsAnexo_FechaYLugar();
            
            DeseaUbicarseEn = new clsAnexo_FechaYLugar { Titulo = "SoloLugar" };
            DeseaUbicarseEn.MetodoAlternoValidacion = ValidacionesDeseaUbicarseEn;

            CausaDesplazamiento = new List<int>();
            InformacionJefeGrupo = new clsAnexo_JefeDeGrupo();
            Victimas = new ObservableCollection<clsAnexo05_Victima>();
            Victimas.CollectionChanged += delegate
            {
                ReportarCambioPropiedad("Victimas");
            };
            DenunciaPrevia = new clsAnexo_DenunciaPrevia();
            DenunciaPrevia.AnexoPadre = this;

            //DeseaUbicarseEn.HechosPais = null;

            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        public string Scope { get { return "Anexo 05"; } }

        private int? _JefeGrupoFamiliarId;
        /// <summary>
        /// Código del jefe del grupo familiar.
        /// </summary>
        [DataMember]
        public int? JefeGrupoFamiliarId
        {
            get { return _JefeGrupoFamiliarId; }
            set
            {
                _JefeGrupoFamiliarId = value;
                ReportarCambioPropiedad("JefeGrupoFamiliarId");
            }
        }

        #region PREGUNTA 1

        private clsAnexo_FechaYLugar _FechaYLugar;
        [DataMember]
        public clsAnexo_FechaYLugar FechaYLugar
        {
            get { return _FechaYLugar; }
            set
            {
                _FechaYLugar = value;

                if (_FechaYLugar != null)
                {
                    _FechaYLugar.Titulo = "de desplazamiento";
                    _FechaYLugar.Contenedor = this;
                }
                ReportarCambioPropiedad("FechaYLugar");
            }
        }

        #endregion

        #region PREGUNTA 2

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

        #region PREGUNTAS 3 Y 4

        private int? _TipoDesplazamiento;
        [DataMember]
        public int? TipoDesplazamiento
        {
            get { return _TipoDesplazamiento; }
            set
            {
                _TipoDesplazamiento = value;
                ReportarCambioPropiedad("TipoDesplazamiento");
            }
        }

        private int? _TiempoResidenciaEnLugarExpulsorAños;
        [DataMember]
        public int? TiempoResidenciaEnLugarExpulsorAños
        {
            get { return _TiempoResidenciaEnLugarExpulsorAños; }
            set
            {
                _TiempoResidenciaEnLugarExpulsorAños = value;
                ReportarCambioPropiedad("TiempoResidenciaEnLugarExpulsorAños");
            }
        }

        private int? _TiempoResidenciaEnLugarExpulsorMeses;
        [DataMember]
        public int? TiempoResidenciaEnLugarExpulsorMeses
        {
            get { return _TiempoResidenciaEnLugarExpulsorMeses; }
            set
            {
                _TiempoResidenciaEnLugarExpulsorMeses = value;
                ReportarCambioPropiedad("TiempoResidenciaEnLugarExpulsorMeses");
            }
        }

        private int? _TiempoResidenciaEnLugarExpulsorDias;
        [DataMember]
        public int? TiempoResidenciaEnLugarExpulsorDias
        {
            get { return _TiempoResidenciaEnLugarExpulsorDias; }
            set
            {
                _TiempoResidenciaEnLugarExpulsorDias = value;
                ReportarCambioPropiedad("TiempoResidenciaEnLugarExpulsorDias");
            }
        }



        private int? _NuevoTipoDesplazamiento;
        [DataMember]
        public int? NuevoTipoDesplazamiento
        {
            get { return _NuevoTipoDesplazamiento; }
            set
            {
                _NuevoTipoDesplazamiento = value;
                ReportarCambioPropiedad("NuevoTipoDesplazamiento");
                ReportarCambioPropiedad("EsExilio");
            }
        }
        private int? _EsExilio;
        [DataMember]
        public int? EsExilio
        {
            get { return _EsExilio; }
            set
            {
                _EsExilio = value;
                ReportarCambioPropiedad("NuevoTipoDesplazamiento");
                ReportarCambioPropiedad("EsExilio");
            }
        }

        #endregion

        #region PREGUNTA 5

        private List<int> _CausaDesplazamiento;
        [DataMember]
        public List<int> CausaDesplazamiento
        {
            get { return _CausaDesplazamiento; }
            set
            {
                _CausaDesplazamiento = value;
                ReportarCambioPropiedad("CausaDesplazamiento");
            }
        }

        private string _CausaDesplazamientoOtro;
        [DataMember]
        public string CausaDesplazamientoOtro
        {
            get { return _CausaDesplazamientoOtro; }
            set
            {
                _CausaDesplazamientoOtro = value;
                ReportarCambioPropiedad("CausaDesplazamientoOtro");
            }
        }

        #endregion

        #region PREGUNTA 6

        private clsAnexo_FechaYLugar _InformacionDeArribo;
        [DataMember]
        public clsAnexo_FechaYLugar InformacionDeArribo
        {
            get  { return _InformacionDeArribo; }
            set
            {
                _InformacionDeArribo = value;
                if (_InformacionDeArribo != null)
                {
                    _InformacionDeArribo.Titulo = "de arribo";

                    _InformacionDeArribo.Contenedor = this;
                }
                ReportarCambioPropiedad("InformacionDeArribo");
            }
        }

        #endregion

        #region PREGUNTAS 7 Y 8

        private int? _DeseoDelHogar;
        [DataMember]
        public int? DeseoDelHogar
        {
            get { return _DeseoDelHogar; }
            set
            {
                _DeseoDelHogar = value;
                if (value != (int)eDeseoDelHogar.Reubicarse)
                {
                    // La fecha en 'DeseaUbicarseEn' es irrelevante, sin embargo
                    // se llena para que no genere mensajes de validación.
                    //DeseaUbicarseEn.HechosFecha = DateTime.Now;
                    DeseaUbicarseEn.HechosDepartamento = 0;
                    DeseaUbicarseEn.HechosMunicipio = 0;
                    DeseaUbicarseEn.TipoEntorno = null;
                    DeseaUbicarseEn.SkipValidation = true;
                }
                else {
                    DeseaUbicarseEn.SkipValidation = false;
                }

                ReportarCambioPropiedad("DeseoDelHogar");
                DeseaUbicarseEn.ReportarCambioPropiedad("HechosDepartamento");
                DeseaUbicarseEn.ReportarCambioPropiedad("HechosMunicipio");
                DeseaUbicarseEn.ReportarCambioPropiedad("TipoEntorno");
                
            }
        }

        private clsAnexo_FechaYLugar _DeseaUbicarseEn;
        [DataMember]
        public clsAnexo_FechaYLugar DeseaUbicarseEn
        {
            get { return _DeseaUbicarseEn; }
            set
            {
               // if (_DeseoDelHogar.Value == (int)eDeseoDelHogar.Reubicarse)
               // {
                    _DeseaUbicarseEn = value;
                    if (DeseoDelHogar.HasValue && DeseoDelHogar.Value == (int)eDeseoDelHogar.Reubicarse)
                    {
                        //Lugar de reubicacion del anexo 05 no tiene fecha
                        if (_DeseaUbicarseEn != null) _DeseaUbicarseEn.Titulo = "SoloLugar";
                    }
                //}
            }
        }

        #endregion

        #region PREGUNTA 9

        private ObservableCollection<clsAnexo05_Victima> _Victimas;
        [DataMember]
        public ObservableCollection<clsAnexo05_Victima> Victimas
        {
            get { return _Victimas; }
            set
            {
                _Victimas = value;
                ReportarCambioPropiedad("Victimas");
            }
        }

        #endregion

        #region PREGUNTAS 10 A 15

        private clsAnexo_JefeDeGrupo _InformacionJefeGrupo;
        [DataMember]
        public clsAnexo_JefeDeGrupo InformacionJefeGrupo
        {
            get { return _InformacionJefeGrupo; }
            set
            {
                _InformacionJefeGrupo = value;
                ReportarCambioPropiedad("InformacionJefeGrupo");
            }
        }

        #endregion

        #region IAnexo

        [System.Xml.Serialization.XmlIgnore]
        public string Nombre
        {
            get { return "5. Desplazamiento Forzado"; }
        }

        [System.Xml.Serialization.XmlIgnore]
        public int Numero
        {
            get { return 5; }
        }

        private int? _HechosFecha;
        [System.Xml.Serialization.XmlIgnore]
        public DateTime HechosFecha
        {
            get { return FechaYLugar.HechosFecha.Value; }
        }
        #endregion

        #region ID_Anexo5
        private int? _IdAnexo5;
        /// <summary>
        /// ID para enlazar la tabla tbsiniestros_persona con la tabla tbanexo5 
        /// </summary>
        [DataMember]
        public int? IdAnexo5
        {
            get { return _IdAnexo5; }
            set
            {
                _IdAnexo5 = value;
            }
        }

        //ID del anexo al cual pertenece el censo masivo (anexo13)
        private int? _idAnexoRelacionado;

        public int? idAnexoRelacionado
        {
            get { return _idAnexoRelacionado; }
            set { _idAnexoRelacionado = value; }
        }
        #endregion
    }
}
