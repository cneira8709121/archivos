using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Ruv.Infrastructure.Crosscutting.Common;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

namespace Ruv.WPF.Captura.Controles
{
  public class EditorObservableCollection<T1> : DependencyObject, IDisposable, INotifyPropertyChanged where T1 : class
  {
    #region PROPIEDADES

    private ObservableCollection<T1> _ListaDatos;
    /// <summary>
    /// La lista original de los datos.
    /// </summary>
    public ObservableCollection<T1> ListaDatos
    {
      get { return _ListaDatos; }
      set
      {
        _ListaDatos = value;
        if (value != null)
        {
          _ListaDatosVista = CollectionViewSource.GetDefaultView(value);
          if (_ListaDatosVista.Filter == null)
            _ListaDatosVista.Filter = 
              new Predicate<object>(RUV.I.Util.FiltroEntidadNoEliminada);
        }
      }
    }

    ICollectionView _ListaDatosVista = null;
    /// <summary>
    /// La vista para presentar los datos.
    /// </summary>
    public ICollectionView ListaDatosVista
    {
      get { return _ListaDatosVista; }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region BOTONES DE ACCION

    private Button _BotonAgregar;
    public Button BotonAgregar
    {
      get { return _BotonAgregar; }
      set
      {
        if (_BotonAgregar == null && value != null)
          value.Click += delegate { AgregarItem(); };
        _BotonAgregar = value;
      }
    }

    private Button _BotonQuitar;
    public Button BotonQuitar
    {
      get { return _BotonQuitar; }
      set
      {
          if (_BotonQuitar == null && value != null)
          {
              value.Click -= delegate { QuitarItem(); };
              value.Click += delegate { QuitarItem(); };
          }
        _BotonQuitar = value;
      }
    }

    #endregion

    #region AGREGAR UN NUEVO ITEM

    /// <summary>
    /// Agrega un nuevo elemento a la lista.
    /// </summary>
    void AgregarItem()
    {
      T1 NuevoDato = Activator.CreateInstance<T1>();

      if (ListaDatos == null) ListaDatos = new ObservableCollection<T1>();

      RUV.I.Util.EntidadEstablecerSiguienteId(
        ListaDatos.Cast<clsEntidadBase>(),
        NuevoDato as clsEntidadBase);

      (NuevoDato as clsEntidadBase).EstadoRegistro
        = eEstadoRegistro.Insertar;

      ListaDatos.Add(NuevoDato);
      ListaDatosVista.MoveCurrentTo(NuevoDato);
    }

    #endregion

    #region QUITAR UN ITEM

    /// <summary>
    /// Quitar un elemento de la lista.
    /// </summary>
    void QuitarItem()
    {
      Ruv.Infrastructure.Crosscutting.Common.Entidades.clsEntidadBase Entidad =
        ListaDatosVista.CurrentItem as
        Ruv.Infrastructure.Crosscutting.Common.Entidades.clsEntidadBase;

      if (Entidad == null)
      {
        RUV.I.UIPrincipal.ReportarInformacionDeUsuario("Para quitar un elemento primero debe seleccionarlo.");
        return;
      }

      if (Entidad.EstadoRegistro == eEstadoRegistro.Insertar)
          ListaDatos.Remove(Entidad as T1);
      else
      {
          Entidad.EstadoRegistro = eEstadoRegistro.Eliminado;
          ListaDatosVista.Refresh();
      }
    }

    #endregion

    public void PostearCambios()
    {
      if (_ListaDatosVista == null) return;

      var LCV = _ListaDatosVista as ListCollectionView;
      if (LCV.IsAddingNew)
        LCV.CommitNew();
      else if (LCV.IsEditingItem)
        LCV.CommitEdit();
    }

    #region IDISPOSABLE

    public void Dispose()
    {
      _ListaDatosVista = null;
      ListaDatos = null;
      BotonAgregar = null;
      BotonQuitar = null;
    }

    #endregion

  }
}
