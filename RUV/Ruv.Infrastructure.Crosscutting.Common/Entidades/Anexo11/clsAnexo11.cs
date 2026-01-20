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
  public partial class clsAnexo11 : clsEntidadBase, IDataErrorInfo, IAnexo, IValidationEntity
  {
    public clsAnexo11()
    {
      FechaYLugar = new clsAnexo_FechaYLugar();
      DenunciaPrevia = new clsAnexo_DenunciaPrevia();
      DenunciaPrevia.AnexoPadre = this;
      BienesInmuebles = new ObservableCollection<clsAnexo11_BienInmueble>();
      BienesInmuebles.CollectionChanged += delegate { ReportarCambioPropiedad("BienesInmuebles"); ReportarCambioPropiedad("BienesMuebles"); }; 
      BienesMuebles = new ObservableCollection<clsAnexo11_BienMueble>();
      BienesMuebles.CollectionChanged += delegate { ReportarCambioPropiedad("BienesMuebles"); ReportarCambioPropiedad("BienesInmuebles"); };
      CreditosPasivos = new ObservableCollection<clsAnexo11_CreditoPasivo>();
      CreditosPasivos.CollectionChanged += delegate { ReportarCambioPropiedad("CreditosPasivos"); };

      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

    public string Scope { get { return "Anexo 11"; } }

    private int? _JefeGrupoFamiliarId;
    /// <summary>
    /// Código del jefe del grupo familiar.
    /// PARA ESTE ANEXO ESTA PROPIEDAD NO TIENE USO.
    /// SE AGREGA PARA CUMPLIR CON LA INTERFÁS IANEXO.
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

    #region PREGUNTAS 3 A 8

    private ObservableCollection<clsAnexo11_BienInmueble> _BienesInmuebles;
    [DataMember]
    public ObservableCollection<clsAnexo11_BienInmueble> BienesInmuebles
    {
      get { return _BienesInmuebles; }
      set
      {
        _BienesInmuebles = value;
        ReportarCambioPropiedad("BienesInmuebles");
      }
    }

    #endregion

    #region PREGUNTA 9 A 11

    private int? _LoteFueDespojado;
    [DataMember]
    public int? LoteFueDespojado
    {
      get { return _LoteFueDespojado; }
      set
      {
        _LoteFueDespojado = value;
        if (value != (int)eSiNoNsNr.Si)
        {
            DespojoTipo = null;
            DespojoQuien = null;
            EstadoActualLote = null;
        }
        ReportarCambioPropiedad("LoteFueDespojado");
        ReportarCambioPropiedad("DespojoTipo");
        ReportarCambioPropiedad("DespojoQuien");
        ReportarCambioPropiedad("EstadoActualLote");
      }
    }

    private int? _DespojoTipo;
    [DataMember]
    public int? DespojoTipo
    {
      get { return _DespojoTipo; }
      set
      {
        _DespojoTipo = value;
        ReportarCambioPropiedad("DespojoTipo");
      }
    }

    private string _DespojoQuien;
    [DataMember]
    public string DespojoQuien
    {
      get { return _DespojoQuien; }
      set
      {
        _DespojoQuien = value;
        ReportarCambioPropiedad("DespojoQuien");
      }
    }

    private int? _EstadoActualLote;
    [DataMember]
    public int? EstadoActualLote
    {
      get { return _EstadoActualLote; }
      set
      {
        _EstadoActualLote = value;
        ReportarCambioPropiedad("EstadoActualLote");
      }
    }

    #endregion

    #region PREGUNTA 12

    private int? _SolicitaProteccionMuebles;
    [DataMember]
    public int? SolicitaProteccionMuebles
    {
      get { return _SolicitaProteccionMuebles; }
      set
      {
        _SolicitaProteccionMuebles = value;
        ReportarCambioPropiedad("SolicitaProteccionMuebles");
      }
    }

    private string _SolicitaProteccionMueblesPorque;
    [DataMember]
    public string SolicitaProteccionMueblesPorque
    {
      get { return _SolicitaProteccionMueblesPorque; }
      set
      {
        _SolicitaProteccionMueblesPorque = value;
        ReportarCambioPropiedad("SolicitaProteccionMueblesPorque");
      }
    }

    #endregion

    #region PREGUNTAS 13 A 17

    private ObservableCollection<clsAnexo11_BienMueble> _BienesMuebles;
    [DataMember]
    public ObservableCollection<clsAnexo11_BienMueble> BienesMuebles
    {
      get { return _BienesMuebles; }
      set
      {
        _BienesMuebles = value;
        ReportarCambioPropiedad("BienesMuebles");
      }
    }

    #endregion

    #region PREGUNTA 18

    private ObservableCollection<clsAnexo11_CreditoPasivo> _CreditosPasivos;
    [DataMember]
    public ObservableCollection<clsAnexo11_CreditoPasivo> CreditosPasivos
    {
      get { return _CreditosPasivos; }
      set
      {
        _CreditosPasivos = value;
        ReportarCambioPropiedad("CreditosPasivos");
      }
    }

    #endregion

    #region IAnexo

    [System.Xml.Serialization.XmlIgnore]
    public string Nombre
    {
      get { return "11. Despojo y/o abandono forzado de bienes muebles e inmuebles"; }
    }

    [System.Xml.Serialization.XmlIgnore]
    public int Numero
    {
      get { return 11; }
    }

    private int? _HechosFecha;
    [System.Xml.Serialization.XmlIgnore]
    public DateTime HechosFecha
    {
        get { return FechaYLugar.HechosFecha.Value; }
    }

    //ID del anexo al cual pertenece el censo masivo (anexo13)
    private int? _idAnexoRelacionado;

    public int? idAnexoRelacionado
    {
      get { return _idAnexoRelacionado; }
      set { _idAnexoRelacionado = value; }
    }
    #endregion


      #region ID_Anexo11
    private int? _IdAnexo11;
    /// <summary>
    /// ID para enlazar la tabla tbsiniestros_persona con la tabla tbanexo11 
    /// Ya que el anexo 11 no tienen victimas
    /// </summary>
    [DataMember]
    public int? IdAnexo11
    {
        get { return _IdAnexo11; }
        set
        {
            _IdAnexo11 = value;
        }
    }
      #endregion
  }
}
