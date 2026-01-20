using System.Windows;
using System.Windows.Controls;
using Ruv.WPF.Captura.Controles;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows;
//using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Ruv.WPF.Captura.Registro.Secciones
{
  public partial class A11 : UserControl, ISeccionRegistro
  {
    #region CONSTRUCTOR

    public A11()
    {
      InitializeComponent();
      this.Loaded += new RoutedEventHandler(A11_Loaded);
    }

    void A11_Loaded(object sender, RoutedEventArgs e)
    { Inicializar(); }

    EditorObservableCollection<clsAnexo11_BienInmueble> EditorInmuebles;
    EditorObservableCollection<clsAnexo11_BienMueble> EditorMuebles;
    EditorObservableCollection<clsAnexo11_CreditoPasivo> EditorCreditosPasivos;

    void Inicializar()
    {
      // Inicializar los editores para las colecciones.
      EditorInmuebles = new EditorObservableCollection<clsAnexo11_BienInmueble>()
      {
        ListaDatos = EsteAnexo.BienesInmuebles,
        BotonAgregar = btnAgregarBienInmueble,
        BotonQuitar = btnQuitarBienInmueble
      };
      dgrBienesInmuebles.ItemsSource = EditorInmuebles.ListaDatosVista;
      dgrBienesInmuebles.LostFocus += delegate
      {
        EditorInmuebles.PostearCambios();
      };

      EditorMuebles = new EditorObservableCollection<clsAnexo11_BienMueble>()
      {
        ListaDatos = EsteAnexo.BienesMuebles,
        BotonAgregar = btnAgregarBienMueble,
        BotonQuitar = btnQuitarBienMueble
      };
      dgrBienesMuebles.ItemsSource = EditorMuebles.ListaDatosVista;
      dgrBienesMuebles.LostFocus += delegate
      {
          EditorMuebles.PostearCambios();
      };

      EditorCreditosPasivos = new EditorObservableCollection<clsAnexo11_CreditoPasivo>()
      {
        ListaDatos = EsteAnexo.CreditosPasivos,
        BotonAgregar = btnAgregarCreditoPasivo,
        BotonQuitar = btnQuitarCreditoPasivo
      };
      dgrCreditosPasivos.ItemsSource = EditorCreditosPasivos.ListaDatosVista;
      dgrCreditosPasivos.LostFocus += delegate
      {
          EditorCreditosPasivos.PostearCambios();
      };

        if(EditorInmuebles.ListaDatosVista != null)
            EditorInmuebles.ListaDatosVista.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);

        if (EditorMuebles.ListaDatosVista != null)
            EditorMuebles.ListaDatosVista.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);

        if (EditorCreditosPasivos.ListaDatosVista != null)
            EditorCreditosPasivos.ListaDatosVista.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);
            
    }

    void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        // Forzar la validación.
        RUV.I.UIPrincipal.ValidadorActual.Validar();
    }

    #endregion

    #region ISeccionRegistro

    public eSeccionRegistro Seccion
    { get { return eSeccionRegistro.A11; } }

    public bool RequireScrollBars { get { return false; } }

    public void MostrarEnInterfase()
    { }

    #endregion

    #region PROPIEDADES & CAMPOS

    /// <summary>
    /// El DataContext de este anexo.
    /// </summary>
    public clsAnexo11 EsteAnexo
    {
      get
      {
        return DataContext as clsAnexo11;
      }
    }

    #endregion

  
  


  }
}
