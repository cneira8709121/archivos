using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using System.Runtime.Serialization;
using System.Reflection;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario
{
    [DataContract]
    public partial class clsControlFormulario : clsEntidadBase
    {

        public clsControlFormulario()
        {
            _EstadoRegistro = eEstadoRegistro.Insertar;
        }

        #region Propiedades

        private string cSerie;

        [DataMember]
        public string CSerie
        {
            get { return cSerie; }
            set
            {
                ReportarCambioPropiedad("CSerie");
                cSerie = value;
            }
        }


        private int nCantidad;
        [DataMember]
        public int NCantidad
        {
            get { return nCantidad; }
            set
            {
                nCantidad = value;
                ReportarCambioPropiedad("NCantidad");

            }
        }

        private string cSerieBuscar;

        public string CSerieBuscar
        {
            get { return cSerieBuscar; }
            set
            {
                cSerieBuscar = value;
                ReportarCambioPropiedad("CSerieBuscar");
            }
        }

        private int? nDesde;

        public int? NDesde
        {
            get { return nDesde; }
            set
            {
                nDesde = value;
                ReportarCambioPropiedad("NDesde");
                ReportarCambioPropiedad("NHasta");
            }
        }

        private int? nHasta;

        public int? NHasta
        {
            get { return nHasta; }
            set
            {
                nHasta = value;
                ReportarCambioPropiedad("NHasta");
                ReportarCambioPropiedad("NDesde");
            }
        }

        private DateTime? dGenerado;

        public DateTime? DGenerado
        {
            get { return dGenerado; }
            set
            {
                dGenerado = value;
                ReportarCambioPropiedad("DGenerado");
            }
        }

        private bool bMostrarBtnPDF;

        public bool BMostrarBtnPDF
        {
            get { return bMostrarBtnPDF; }
            set
            {
                bMostrarBtnPDF = value;
                ReportarCambioPropiedad("BMostrarBtnPDF");
            }
        }

        private bool bSePuedeBuscar = true;

        public bool BSePuedeBuscar
        {
            get { return bSePuedeBuscar; }
            set
            {
                bSePuedeBuscar = value;
                ReportarCambioPropiedad("BSePuedeBuscar");
            }
        }

        private bool bSePuedeDistribuir;

        public bool BSePuedeDistribuir
        {
            get { return bSePuedeDistribuir; }
            set
            {
                bSePuedeDistribuir = value;
                ReportarCambioPropiedad("BSePuedeDistribuir");
            }
        }

        private bool bSePuedeDistribuirFiltro;

        public bool BSePuedeDistribuirFiltro
        {
            get { return bSePuedeDistribuirFiltro; }
            set
            {
                bSePuedeDistribuirFiltro = value;
                ReportarCambioPropiedad("BSePuedeDistribuirFiltro");
            }
        }

        private bool bSePuedeSeparar;

        public bool BSePuedeSeparar
        {
            get { return bSePuedeSeparar; }
            set
            {
                bSePuedeSeparar = value;
                ReportarCambioPropiedad("BSePuedeSeparar");
            }
        }
        
        private List<clsFormulario> lstFormularios;

        [DataMember]
        public List<clsFormulario> LstFormularios
        {
            get { return lstFormularios; }
            set
            {
                lstFormularios = value;
                if (value == null) lstFormularios = new List<clsFormulario>();
                ReportarCambioPropiedad("LstFormularios");
                ReportarCambioPropiedad("Accion");
            }
        }
        #endregion

        #region Funcionalidad
        private eEstadoFormulario eFiltro = eEstadoFormulario.GENERADO;

        public eEstadoFormulario EFiltro
        {
            get { return eFiltro; }
            set
            {
                if (this.lstFormularios != null)
                    this.lstFormularios.ForEach(x => { x.BSelected = false; });

                eFiltro = value;

                BMostrarBtnPDF = value == eEstadoFormulario.ASIGNADO;
                if (value == eEstadoFormulario.IMPRENTA || value == eEstadoFormulario.GENERADO)
                {
                    this.bSoloLectura = false; this.bActivo = true;
                    BSeleccionMasiva = true;
                    this.BGeografiaSoloLectura = false;
                }
                else if (value == eEstadoFormulario.ASIGNADO)
                {
                    this.BSeleccionMasiva = true;
                    this.BSoloLectura = false;
                    this.bActivo = false;
                    this.BGeografiaSoloLectura = true;
                }
                else
                {
                    this.bSoloLectura = true; 
                    this.bActivo = false;
                    this.BSeleccionMasiva = false;
                    this.BGeografiaSoloLectura = false;
                }

                ReportarCambioPropiedad("BSoloLectura");
                ReportarCambioPropiedad("BActivo");
                ReportarCambioPropiedad("EFiltro");
                ReportarCambioPropiedad("LstFormularios");
                ReportarCambioPropiedad("BGeografiaSoloLectura");
            }
        }

        private bool? _bSeleccionMasiva = false;

        public bool? BSeleccionMasiva
        {
            get { return _bSeleccionMasiva; }
            set
            {
                _bSeleccionMasiva = value;

                ReportarCambioPropiedad("BSeleccionMasiva");
            }
        }

        private bool? visibilidadMasivos = false;

        public bool? VisibilidadMasivos
        {
            get { return visibilidadMasivos; }
            set
            {
                visibilidadMasivos = value;

                //if (value.HasValue && value.Value && lstFormularios != null)
                //{
                //    int count = lstFormularios
                //        .Where(x => x.BSelected)
                //        .Select(x => new { x.NIdPais, x.NIdDepartamento, x.NIdMunicipio, x.NIdEntidad })
                //        .Distinct()
                //        .Count();
                //    if (count > 0 && count == 1)
                //    {
                //        clsFormulario tmp = lstFormularios.First();
                //        NPaisId = tmp.NIdPais;
                //        NDepartamentoId = tmp.NIdDepartamento;
                //        NMunicipioId = tmp.NIdMunicipio;
                //        NEntidadMunicipioId = tmp.NIdEntidad;
                //    }
                //    else
                //    {
                //        NPaisId = null;
                //        NDepartamentoId = null;
                //        NMunicipioId = null;
                //        NEntidadMunicipioId = null;
                //    }
                //}

                ReportarCambioPropiedad("VisibilidadMasivos");
            }
        }

        private long? nPaisId;

        public long? NPaisId
        {
            get { return nPaisId; }
            set
            {
                nPaisId = value;
                if (this.lstFormularios != null) {
                    foreach (var item in this.lstFormularios.Where(x => x.BSelected && x.EfId != eEstadoFormulario.ASIGNADO))
                    {
                        item.NIdPais = value;
                    }
                }
                ReportarCambioPropiedad("NPaisId");
                ReportarCambioPropiedad("LstFormularios");
            }
        }

        private long? nDepartamentoId;

        public long? NDepartamentoId
        {
            get { return nDepartamentoId; }
            set
            {
                nDepartamentoId = value;
                if (this.lstFormularios != null) {
                    foreach (var item in this.lstFormularios.Where(x => x.BSelected && x.EfId != eEstadoFormulario.ASIGNADO))
                    {
                        item.NIdDepartamento = value;
                    }
                }
                ReportarCambioPropiedad("NDepartamentoId");
                ReportarCambioPropiedad("LstFormularios");
            }
        }

        private long? nMunicipioId;

        public long? NMunicipioId
        {
            get { return nMunicipioId; }
            set
            {
                nMunicipioId = value;
                if (this.lstFormularios != null) {
                    foreach (var item in this.lstFormularios.Where(x => x.BSelected && x.EfId != eEstadoFormulario.ASIGNADO))
                    {
                        item.NIdMunicipio = value;
                    }
                }
                ReportarCambioPropiedad("NMunicipioId");
                ReportarCambioPropiedad("LstFormularios");
            }
        }

        private short? nEntidadMunicipioId;

        public short? NEntidadMunicipioId
        {
            get { return nEntidadMunicipioId; }
            set
            {
                nEntidadMunicipioId = value;
                if (this.lstFormularios != null) {
                    foreach (var item in this.lstFormularios.Where(x => x.BSelected && x.EfId != eEstadoFormulario.ASIGNADO))
                    {
                        item.NIdEntidad = value;
                    }
                }
                ReportarCambioPropiedad("NEntidadMunicipioId");
                ReportarCambioPropiedad("LstFormularios");
            }
        }

        private long? nPaisIdFiltro;

        public long? NPaisIdFiltro
        {
            get { return nPaisIdFiltro; }
            set
            {
                nPaisIdFiltro = value;
                ReportarCambioPropiedad("NPaisIdFiltro");
            }
        }

        private long? nDepartamentoIdFiltro;

        public long? NDepartamentoIdFiltro
        {
            get { return nDepartamentoIdFiltro; }
            set
            {
                nDepartamentoIdFiltro = value;
                ReportarCambioPropiedad("NDepartamentoIdFiltro");
            }
        }

        private long? nMunicipioIdFiltro;

        public long? NMunicipioIdFiltro
        {
            get { return nMunicipioIdFiltro; }
            set
            {
                nMunicipioIdFiltro = value;
                ReportarCambioPropiedad("NMunicipioIdFiltro");
            }
        }

        private short? nEntidadMunicipioIdFiltro;

        public short? NEntidadMunicipioIdFiltro
        {
            get { return nEntidadMunicipioIdFiltro; }
            set
            {
                nEntidadMunicipioIdFiltro = value;
                ReportarCambioPropiedad("NEntidadMunicipioIdFiltro");
            }
        }

        private bool bSoloLectura;

        public bool BSoloLectura
        {
            get { return bSoloLectura; }
            set
            {
                bSoloLectura = value;
                ReportarCambioPropiedad("BSoloLectura");
            }
        }

        private bool bActivo;

        public bool BActivo
        {
            get { return bActivo; }
            set
            {
                bActivo = value;
                ReportarCambioPropiedad("BActivo");
            }
        }


        private string cObservacion;

        public string CObservacion
        {
            get { return cObservacion; }
            set
            {
                cObservacion = value;
                foreach (clsFormulario formularios in this.lstFormularios.Where(x => x.EfId != eEstadoFormulario.RADICADO))
                {
                    formularios.CObservacion = value;
                }
                ReportarCambioPropiedad("CObservacion");
            }
        }


        private eAccionEnFormulario _Accion = eAccionEnFormulario.Inactivar;

        public eAccionEnFormulario Accion
        {
            get { return _Accion; }
            set
            {
                this.lstFormularios.ForEach(x => { x.BSelected = false; });
                _Accion = value;
                ReportarCambioPropiedad("Accion");
                ReportarCambioPropiedad("lstFormularios");
            }
        }

        private bool bGeografiaSoloLectura;

        /// <summary>
        /// Diego Alvarez - 15/11/2013 - Booleano para deshabilitar columna geografía para estado ASIGNADO
        /// </summary>
        public bool BGeografiaSoloLectura
        {
            get { return this.bGeografiaSoloLectura; }
            set
            {
                this.bGeografiaSoloLectura = value;
                ReportarCambioPropiedad("BGeografiaSoloLectura");
            }
        }

        #endregion


    }
}
