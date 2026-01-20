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
  public partial class clsAnexo03 : clsEntidadBase, IDataErrorInfo, IAnexo, IValidationEntity
  {
    public clsAnexo03()
    {
      NiñosNacidosPorAbusoSexual = new ObservableCollection<int>();
      NiñosNacidosPorAbusoSexual.CollectionChanged += delegate
      {
        ReportarCambioPropiedad("NiñosNacidosPorAbusoSexual");
      };

      Victimas = new ObservableCollection<clsAnexo03_Victima>();
      Victimas.CollectionChanged += delegate
      {
          Victimas.ToList().ForEach(x => x.AnexoPadre = this);
        ReportarCambioPropiedad("Victimas");
      };

      InformacionJefeGrupo = new clsAnexo_JefeDeGrupo();

      FechaYLugar = new clsAnexo_FechaYLugar();

      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

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
        ReportarCambioPropiedad("FechaYLugar");
      }
    }

    #endregion

    #region PREGUNTA 2

    private ObservableCollection<int> _NiñosNacidosPorAbusoSexual;
    [DataMember]
    public ObservableCollection<int> NiñosNacidosPorAbusoSexual
    {
      get { return _NiñosNacidosPorAbusoSexual; }
      set
      {
        _NiñosNacidosPorAbusoSexual = value;
        ReportarCambioPropiedad("NiñosNacidosPorAbusoSexual");
      }
    }

    #endregion

    #region PREGUNTAS 3 A 8

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

    #region PREGUNTAS 9 A 14


    private ObservableCollection<clsAnexo03_Victima> _Victimas;
    [DataMember]
    public ObservableCollection<clsAnexo03_Victima> Victimas
    {
      get { return _Victimas; }
      set
      {
        _Victimas = value;
        ReportarCambioPropiedad("Victimas");
      }
    }

    #endregion

    #region IAnexo

    [System.Xml.Serialization.XmlIgnore]
    public string Nombre
    {
      get { return "3. Delitos contra la libertad y la integridad sexual en desarrollo del conflicto armado"; }
    }

    [System.Xml.Serialization.XmlIgnore]
    public int Numero
    {
      get { return 3; }
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

    public string Scope
    {
        get { return "Anexo 03"; }
    }
  }
}
