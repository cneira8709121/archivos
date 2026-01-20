using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Ruv.Infrastructure.Crosscutting.Common.Entidades
{
  [DataContract]
  public partial class clsPersonasAfectadas : clsEntidadBase, IDataErrorInfo, IValidationEntity
  {
    #region CONSTRUCTOR

    private clsDeclaracion _Declaracion;
    /// <summary>
    /// Referencia a la declaración padre.
    /// No requiere almacenamiento.
    /// </summary>
    [System.Xml.Serialization.XmlIgnore]
    public clsDeclaracion Declaracion
    {
      get { return _Declaracion; }
      set { _Declaracion = value; }
    }

    public clsPersonasAfectadas()
    {
      ConstructorGeneral();
    }

    private void ConstructorGeneral()
    {
      // Inicializar la lista de personas.
      //_ListaPersonasOC = new ObservableCollection<clsPersonaAfectada>();
      //_ListaPersonasOC.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_ListaPersonasOC_CollectionChanged);
      //_ListaPersonasICV = CollectionViewSource.GetDefaultView(_ListaPersonasOC);
      //_ListaPersonasICV.SortDescriptions.Add(
      //  new SortDescription("NombreCompleto", ListSortDirection.Ascending));
      //_ListaPersonasICV.Filter = new Predicate<object>(FiltroOmitirEliminados);

      //ReportarCambioPropiedad("ListaPersonasOrdenada");
      //ReportarCambioPropiedad("ListaPersonas");

      _ListaPersonasOC = new ObservableCollection<clsPersonaAfectada>();
      _ListaPersonasOC.CollectionChanged 
        += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_ListaPersonasOC_CollectionChanged);

      _EstadoRegistro = eEstadoRegistro.Insertar;
    }

    #endregion

    #region LA LISTA DE LAS PERSONAS AFECTADAS

    void _ListaPersonasOC_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      ReportarCambioPropiedad("ListaPersonasOrdenada");
      ReportarCambioPropiedad("ListaPersonas");
    }

    /// <summary>
    /// La vista que permite ordenar la lista de personas.
    /// </summary>
    ICollectionView _ListaPersonasICV;

    /// <summary>
    /// El contenedor de la lista de personas.
    /// </summary>
    ObservableCollection<clsPersonaAfectada> _ListaPersonasOC;

    /// <summary>
    /// Lista de personas afectadas, en orden alfabético.
    /// </summary>
    [System.Xml.Serialization.XmlIgnore]
    public ICollectionView ListaPersonasOrdenada
    {
      get
      {
        if (_ListaPersonasICV == null)
        {
          // Inicializar la lista de personas.
          _ListaPersonasICV = CollectionViewSource.GetDefaultView(_ListaPersonasOC);
          _ListaPersonasICV.SortDescriptions.Add(
            new SortDescription("NombreCompleto", ListSortDirection.Ascending));
          _ListaPersonasICV.Filter = new Predicate<object>(FiltroOmitirEliminados);

            

          ReportarCambioPropiedad("ListaPersonasOrdenada");
          ReportarCambioPropiedad("ListaPersonas");
        }
        return _ListaPersonasICV;
      }
      set { }
    }

    /// <summary>
    /// La lista de personas, modificable.
    /// </summary>
    [DataMember]
    public ObservableCollection<clsPersonaAfectada> ListaPersonas
    {
      get
      {
        return _ListaPersonasOC;
      }
      set
      {
        _ListaPersonasOC = value;
        ReportarCambioPropiedad("ListaPersonasOrdenada");
        ReportarCambioPropiedad("ListaPersonas");
      }
    }

    #endregion



    public string Scope
    {
        get { return "HOJA 2"; }
    }
  }
}
