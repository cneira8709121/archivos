using System.IO;
using System.IO.Packaging;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Xml;
using Ruv.WPF.Captura.Registro.Secciones;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Ruv.WPF.Captura.Infrastructure;

namespace Ruv.WPF.Captura.Tmp
{
  /// <summary>
  /// Lógica de interacción para Window1.xaml
  /// </summary>
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(Window1_Loaded);
    }

    void Window1_Loaded(object sender, RoutedEventArgs e)
    {
      Bienes.Add(new clsAnexo01_Victima_Bien { TipoBien = 1, CalidadDeLaVictima = 1, Descripcion = "Mueblecito" });
      Bienes[0].EstadoRegistro = eEstadoRegistro.SinModificaciones;

      DataContext = this;
    }

    private ObservableCollection<clsAnexo01_Victima_Bien> _Bienes;
    public ObservableCollection<clsAnexo01_Victima_Bien> Bienes
    {
      get
      {
        if (_Bienes == null)
          _Bienes = new ObservableCollection<clsAnexo01_Victima_Bien>();
        return _Bienes;
      }
      set { _Bienes = value; }
    }

    IEditableCollectionView _BienesVistaEditable;
    ICollectionView _BienesICV = null;
    /// <summary>
    /// Vista de las entidades con capacidad de edición.
    /// </summary>
    public IEditableCollectionView BienesVistaEditable
    {
      get
      {
        if (_BienesVistaEditable == null)
        {
          _BienesICV = CollectionViewSource.GetDefaultView(Bienes);
          _BienesICV.Filter = new Predicate<object>(MetodoFiltro);
          _BienesVistaEditable = (IEditableCollectionView)_BienesICV;
        }
        return _BienesVistaEditable;
      }
      set { _BienesVistaEditable = value; }
    }

    /// <summary>
    /// El filtro para las entidades no-eliminadas.
    /// </summary>
    /// <param name="Bien"></param>
    /// <returns></returns>
    Boolean MetodoFiltro(object Bien)
    {
      return (Bien as clsAnexo01_Victima_Bien).EstadoRegistro != eEstadoRegistro.Eliminado;
    }

    private List<Elemento> _TiposBienes;
    public List<Elemento> TiposBienes
    {
      get
      {
        if (_TiposBienes == null)
        {
          _TiposBienes = new List<Elemento>();
          _TiposBienes.Add(new Elemento { Nombre = "Mueble", Id = 1 });
          _TiposBienes.Add(new Elemento { Nombre = "Inmueble", Id = 2 });
        }
        return _TiposBienes;
      }
      set { _TiposBienes = value; }
    }

    private List<Elemento> _CalidadVictima;
    public List<Elemento> CalidadVictima
    {
      get
      {
        if (_CalidadVictima == null)
        {
          _CalidadVictima = new List<Elemento>();
          _CalidadVictima.Add(new Elemento { Id = 1, Nombre = "Propietario" });
          _CalidadVictima.Add(new Elemento { Id = 2, Nombre = "Poseedor" });
          _CalidadVictima.Add(new Elemento { Id = 3, Nombre = "Arrendatario o tenedor" });
          _CalidadVictima.Add(new Elemento { Id = 4, Nombre = "Conductor" });
        }
        return _CalidadVictima;
      }
      set { _CalidadVictima = value; }
    }

    /// <summary>
    /// Agregar una nueva entidad.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AgregarEntidad(object sender, RoutedEventArgs e)
    {
      var NuevoBien = BienesVistaEditable.AddNew();
      (NuevoBien as clsAnexo01_Victima_Bien).EstadoRegistro = eEstadoRegistro.Insertar;

      // Todo: Agregarle el ID correspondiente y ponerlo en la lista.
    }

    /// <summary>
    /// Borrar una entidad.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OperacionBorrarEntidad(object sender, RoutedEventArgs e)
    {
      if (dgEntidad.SelectedItem != null)
      {
        var Elemento = dgEntidad.SelectedItem as clsAnexo01_Victima_Bien;
        OperacionBorrarEntidad(Elemento);
        //if (Elemento.EstadoRegistro == eEstadoRegistro.Insertar)
        //  BienesVistaEditable.Remove(Elemento);
        //else
        //  Elemento.EstadoRegistro = eEstadoRegistro.Eliminado;
        //_BienesICV.Refresh();
        //ReportarCambioPropiedad("BienesVistaEditable");
      }
    }

    private void ListarCambios(object sender, RoutedEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("==========================");
      foreach (var item in Bienes)
      {
        System.Diagnostics.Debug.WriteLine(string.Format("{0} : {1}",
          item.Descripcion,
          item.EstadoRegistro));
      }
      System.Diagnostics.Debug.WriteLine("==========================");
    }

    /// <summary>
    /// Marca o borra una entidad.
    /// </summary>
    /// <param name="bien"></param>
    void OperacionBorrarEntidad(clsAnexo01_Victima_Bien bien)
    {
      if (bien.EstadoRegistro == eEstadoRegistro.Insertar)
      {
        BienesVistaEditable.CancelNew();
        BienesVistaEditable.Remove(bien);
      }
      else
      {
        BienesVistaEditable.CancelEdit();
        bien.EstadoRegistro = eEstadoRegistro.Eliminado;
      }
      _BienesICV.Refresh();
    }

    /// <summary>
    /// Se lanza al postear un cambio en una fila de la grilla.
    /// Sucede cuando la fila editada o insertada pierde el foco.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void dgEntidad_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
    {
      if (e.EditAction == DataGridEditAction.Commit)
      {
        // Si la entidad no está correctamente ingresada, borrarla.
        var Entidad = e.Row.Item as clsAnexo01_Victima_Bien;
        List<eEstadoValidacion> Requeridas = Ruv.WPF.Captura.Infrastructure.clsUtil.ValidacionesRequeridas();
        int validacionesSaltadas = 0;
        if (!RUV.I.ValidadorEntidades.EntidadEsValida(Entidad, Requeridas, ref validacionesSaltadas))
        {
          e.Cancel = true;
          OperacionBorrarEntidad(Entidad);
        }
      }
    }
  }

  public class Elemento
  {
    public int Id { get; set; }
    public string Nombre { get; set; }
  }

}

