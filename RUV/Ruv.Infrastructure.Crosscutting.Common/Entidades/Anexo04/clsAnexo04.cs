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
  public partial class clsAnexo04 : clsEntidadBase, IDataErrorInfo, IAnexo, IValidationEntity
  {
    public clsAnexo04()
    {
      Victimas = new ObservableCollection<clsAnexo04_Victima>();
      Victimas.CollectionChanged += delegate
      {
          Victimas.ToList().ForEach(x => x.AnexoPadre = this);
        ReportarCambioPropiedad("Victimas");
      };

      FechaYLugar = new clsAnexo_FechaYLugar();
      InformacionJefeGrupo = new clsAnexo_JefeDeGrupo();

      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

    #region PREGUNTA 1

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

    #region PREGUNTAS 2 A 7

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

    #region PREGUNTAS 8 A 13

    private ObservableCollection<clsAnexo04_Victima> _Victimas;
    [DataMember]
    public ObservableCollection<clsAnexo04_Victima> Victimas
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
      get { return "4. Desaparición Forzada"; }
    }

    [System.Xml.Serialization.XmlIgnore]
    public int Numero
    {
      get { return 4; }
    }
          
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
        get { return "Anexo 04"; }
    }
  }
}
