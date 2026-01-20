using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.WPF.Captura.Registro.Secciones
{
  public partial class FechaLugar_Imp : UserControl
  {
    public FechaLugar_Imp()
    {
      InitializeComponent();
    }

    #region ASIGNACION DE LOS DATOS.

    public clsAnexo_FechaYLugar FechaYLugar
    {
      get { return (clsAnexo_FechaYLugar)GetValue(FechaYLugarProperty); }
      set { SetValue(FechaYLugarProperty, value); }
    }

    public static readonly DependencyProperty FechaYLugarProperty =
        DependencyProperty.Register("FechaYLugar", typeof(clsAnexo_FechaYLugar),
        typeof(FechaLugar_Imp), new UIPropertyMetadata(null, FechaYLugarChanged));

    static void FechaYLugarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var FLI = d as FechaLugar_Imp;

      FLI.steTituloEntorno.DataContext = FLI.FechaYLugar;
      FLI.txtEntorno.DataContext = FLI.FechaYLugar;

      FLI.cifFecha.Fecha = null;
      FLI.txtPais.Text = null;
      FLI.txtDepartamento.Text = null;
      FLI.txtMunicipio.Text = null;
      //FLI.txtBarrioVereda.Text = null;
      //FLI.txtLocalidadCorregimiento.Text = null;

      var Valor = e.NewValue as clsAnexo_FechaYLugar;

      if (Valor == null) return;

      FLI.cifFecha.Fecha = Valor.HechosFecha;

      //Se valida que tenga seleccionado departamento para immprimir el Pais, esto debido a que por defecto el combo tienen Pais 50 'Colombia'
      if (Valor.HechosPais.HasValue && Valor.HechosDepartamento.HasValue) {
          var paisSeleccionado = RUV.I.InfoGeneral.ListaPaises.FirstOrDefault(x => x.Id == Valor.HechosPais.Value);
          FLI.txtPais.Text = paisSeleccionado != null ? paisSeleccionado.Nombre : string.Empty;
      }

      if (Valor.HechosDepartamento.HasValue) { 
          var departamentoSeleccionado = RUV.I.InfoGeneral.ListaDepartamentos(Valor.HechosPais.Value).FirstOrDefault(x => x.Id == Valor.HechosDepartamento.Value);
          FLI.txtDepartamento.Text = departamentoSeleccionado != null ? departamentoSeleccionado.Nombre : string.Empty;
      }

      if (Valor.HechosMunicipio.HasValue) {
          var municipioSeleccionado = RUV.I.InfoGeneral.ListaMunicipios(Valor.HechosDepartamento.Value).FirstOrDefault(x => x.Id == Valor.HechosMunicipio.Value);
          FLI.txtMunicipio.Text = municipioSeleccionado != null ? municipioSeleccionado.Nombre : string.Empty;
      }
              

      // El tipo de entorno.
      //if (Valor.TipoEntorno.HasValue)
      //{
      //  switch (Valor.TipoEntorno.Value)
      //  {
      //    case eTipoEntorno.Urbano:
      //      FLI.steBarrioVereda.Texto = "BARRIO";
      //      FLI.steLocalidadCorregimiento.Texto = "LOCALIDAD";
      //      break;
      //    case eTipoEntorno.Rural:
      //      FLI.steBarrioVereda.Texto = "VEREDA";
      //      FLI.steLocalidadCorregimiento.Texto = "CORREGIMIENTO";
      //      break;
      //  }
      //}
      //else
      //{
      //  FLI.steBarrioVereda.Texto = null;
      //  FLI.steLocalidadCorregimiento.Texto = null;
      //}

      // Los nombres del entorno.
      //clsPoblacion Poblacion = null;
      //string Nombre=null;
      //if (Valor.BarrioVeredaId != null)
      //{
      //  Poblacion = Sipod.I.InfoGeneral.ListaPoblaciones
      //    .FirstOrDefault(x => x.Key == Valor.BarrioVeredaId.Value).LazyValue.Value;
      //  if (Poblacion!=null) Nombre=Poblacion.Nombre;
      //}
      //FLI.txtBarrioVereda.Text = Nombre;

      //Nombre = null;
      //if (Valor.LocalidadCorregimientoId != null)
      //{
      //  Poblacion = Sipod.I.InfoGeneral.ListaPoblaciones
      //    .FirstOrDefault(x => x.Key == Valor.LocalidadCorregimientoId.Value).LazyValue.Value;
      //  if (Poblacion != null) Nombre = Poblacion.Nombre;
      //}
      //FLI.txtLocalidadCorregimiento.Text = Nombre;

    }

    #endregion

    public string Numero
    {
      set { steTitulo.Numero = value; }
    }

    public string TextoTitulo
    {
      set { steTitulo.Texto = value; }
    }

    private bool _CajaFechaEsVisible = true;
    /// <summary>
    /// Oculta o muestra la caja de ingreso de fecha.
    /// </summary>
    public bool CajaFechaEsVisible
    {
      get { return _CajaFechaEsVisible; }
      set
      {
        _CajaFechaEsVisible = value;
        if (!value)
        {
          steFecha.Visibility = System.Windows.Visibility.Collapsed;
          cifFecha.Visibility = System.Windows.Visibility.Collapsed;
          // La primera columna ya no tiene ancho.
          grdMain.ColumnDefinitions[0].Width = new GridLength(0d, GridUnitType.Star);
        }
      }
    }

    private bool _CajaPaisEsVisible = false;
    /// <summary>
    /// Oculta o muestra la caja de país.
    /// </summary>
    public bool CajaPaisEsVisible
    {
        get { return _CajaPaisEsVisible; }
        set
        {
            _CajaPaisEsVisible = value;
            if (!value)
            {
                stePais.Visibility = System.Windows.Visibility.Collapsed;
                txtPais.Visibility = System.Windows.Visibility.Collapsed;
                // La primera columna ya no tiene ancho.
                grdMain.ColumnDefinitions[1].Width = new GridLength(0d, GridUnitType.Star);
            }
        }
    }


  }
}
