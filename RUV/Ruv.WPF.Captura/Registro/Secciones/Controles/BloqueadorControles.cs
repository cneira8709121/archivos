using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Registro.Secciones.Controles
{
    /// <summary>
    /// Permite deshabilitar el contenido de un control, permitiendo una excepción.
    /// </summary>
    [ContentProperty("ControlesABloquear")]
    public class BloqueadorControles : ItemsControl
    {
        #region CONSTRUCTOR
        public BloqueadorControles()
        {
            ControlesABloquear = new ObservableCollection<ControlIndividual>();
            ControlesABloquear.CollectionChanged += ControlesABloquear_CollectionChanged;
            _ValoresQueDesbloquean = new ObservableCollection<ValorParametroHabilitante>();
            _ValoresQueBloquean = new ObservableCollection<ValorParametroBloqueante>();
            this.Loaded += new RoutedEventHandler(BloqueadorControles_Loaded);
        }

        /// <summary>
        /// Una vez se cargue este control se deben actualizar los bloqueos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BloqueadorControles_Loaded(object sender, RoutedEventArgs e)
        {
            ActualizarBloqueo();
        }

        /// <summary>
        /// Al agregar controles a la colección se les debe agregar al logical tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ControlesABloquear_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems)
                {
                    AddLogicalChild(item);
                }
        }

        #endregion

        #region PROPIEDADES

        /// <summary>
        /// El control cuyo contenido debe habilitarse o deshabilitarse.
        /// </summary>
        public UIElement Contenedor
        {
            get { return (UIElement)GetValue(ContenedorProperty); }
            set { SetValue(ContenedorProperty, value); }
        }

        public static readonly DependencyProperty ContenedorProperty =
            DependencyProperty.Register("Contenedor", typeof(UIElement),
            typeof(BloqueadorControles), new UIPropertyMetadata(null, ValorChanged));

        /// <summary>
        /// Control que no debe deshabilitarse.
        /// </summary>
        public UIElement ControlExcepto
        {
            get { return (UIElement)GetValue(ControlExceptoProperty); }
            set { SetValue(ControlExceptoProperty, value); }
        }

        public static readonly DependencyProperty ControlExceptoProperty =
            DependencyProperty.Register("ControlExcepto", typeof(UIElement),
            typeof(BloqueadorControles), new UIPropertyMetadata(null, ValorChanged));

        /// <summary>
        /// El valor a monitorear para determinar si el contenido del Contenedor debe 
        /// deshabilitarse. 1=Habilitar, 0=Deshabilitar, Null=No hacer nada.
        /// </summary>
        public int? Valor
        {
            get { return (int?)GetValue(ValorProperty); }
            set { SetValue(ValorProperty, value); }
        }

        public static readonly DependencyProperty ValorProperty =
            DependencyProperty.Register("Valor", typeof(int?),
            typeof(BloqueadorControles), new UIPropertyMetadata(null, ValorChanged));

        static void ValorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as BloqueadorControles).ActualizarBloqueo();
        }

        private ObservableCollection<ControlIndividual> _ControlesABloquear;
        /// <summary>
        /// La específica lista de los controles a bloquear.
        /// </summary>
        public ObservableCollection<ControlIndividual> ControlesABloquear
        {
            get { return _ControlesABloquear; }
            set { _ControlesABloquear = value; }
        }

        private ObservableCollection<ValorParametroHabilitante> _ValoresQueDesbloquean;
        /// <summary>
        /// La lista de posibles valores que desbloquean los controles de la interfase.
        /// </summary>
        public ObservableCollection<ValorParametroHabilitante> ValoresQueDesbloquean
        {
            get { return _ValoresQueDesbloquean; }
            set
            {
                _ValoresQueDesbloquean = value;
                ActualizarBloqueo();
            }
        }

        private ObservableCollection<ValorParametroBloqueante> _ValoresQueBloquean;
        /// <summary>
        /// La lista de posibles valores que desbloquean los controles de la interfase.
        /// </summary>
        public ObservableCollection<ValorParametroBloqueante> ValoresQueBloquean
        {
            get { return _ValoresQueBloquean; }
            set
            {
                _ValoresQueBloquean = value;
                ActualizarBloqueo();
            }
        }
        #endregion

        #region REALIZACIÓN DEL BLOQUEO DE LA INTERFASE

        /// <summary>
        /// Cambia el estado de habilitación de los controles.
        /// </summary>
        void ActualizarBloqueo()
        {
            if (ControlesABloquear.Any() && Contenedor != null)
                throw new ApplicationException("No se pueden establecer 'ControlesABloquear' y 'Contenedor' al mismo tiempo.");

            if (ValoresQueDesbloquean.Any() && ValoresQueBloquean.Any())
                throw new ApplicationException("No se pueden establecer 'ValoresQueDesbloquean' y 'ValoresQueBloquean' al mismo tiempo.");

            clsUIHelper UIH = new clsUIHelper();
            List<int?> ListaTemp = new List<int?>();

            if (!ValoresQueBloquean.Any())
            {
                // Armar la lista de valores positivos (que desbloquean).
                if (!ValoresQueDesbloquean.Any())
                    ListaTemp.Add(1);
                else
                    ValoresQueDesbloquean.ToList().ForEach(x => ListaTemp.Add((int)x.Valor.Value));

                if (Contenedor != null)
                {
                    var Controles = UIH.GetChildren(Contenedor, CriterioBusqueda,
                    usarVisualTree: false, omitirHijosConPadre: true);
                    Controles.ForEach(x => x.SourceControl.IsEnabled = ListaTemp.Any(y => y == Valor));
                }
                else
                {
                    ControlesABloquear.ToList().ForEach(x =>
                    { if (x.Elemento != null) x.Elemento.IsEnabled = ListaTemp.Any(y => y == Valor); });
                }

            }
            else
            {
                ValoresQueBloquean.ToList().ForEach(x => ListaTemp.Add(x.Valor));

                if (Contenedor != null)
                {
                    var Controles = UIH.GetChildren(Contenedor, CriterioBusqueda,
                    usarVisualTree: false, omitirHijosConPadre: true);
                    Controles.ForEach(x => x.SourceControl.IsEnabled = !(ListaTemp.Any(y => y == Valor)));
                }
                else
                {
                    ControlesABloquear.ToList().ForEach(x =>
                    { if (x.Elemento != null) x.Elemento.IsEnabled = !(ListaTemp.Any(y => y == Valor)); });
                }
            }

            if (ControlExcepto != null)
                ControlExcepto.IsEnabled = true;
        }

        #endregion

        FrameworkElementItem CriterioBusqueda(DependencyObject child)
        {
            FrameworkElementItem Resultado = null;

            if (child is UIElement && child != ControlExcepto && !(child is Panel))
            {
                Resultado = new FrameworkElementItem
                {
                    Description = null,
                    SourceControl = child as FrameworkElement
                };
            }

            return Resultado;
        }

    }

    /// <summary>
    /// Permite referenciar un control a bloquear.
    /// </summary>
    public class ControlIndividual : FrameworkElement
    {
        public UIElement Elemento
        {
            get { return (UIElement)GetValue(ElementoProperty); }
            set { SetValue(ElementoProperty, value); }
        }

        public static readonly DependencyProperty ElementoProperty =
            DependencyProperty.Register("Elemento", typeof(UIElement),
            typeof(ControlIndividual), new UIPropertyMetadata(null));
    }

    /// <summary>
    /// Permite referenciar un parámetro
    /// </summary>
    public class ValorParametroHabilitante : FrameworkElement
    {
        public eParametrosHabilitantes? Valor
        {
            get { return (eParametrosHabilitantes?)GetValue(ElementoProperty); }
            set { SetValue(ElementoProperty, value); }
        }

        public static readonly DependencyProperty ElementoProperty =
            DependencyProperty.Register("Valor", typeof(eParametrosHabilitantes?),
            typeof(ValorParametroHabilitante), new UIPropertyMetadata(null));
    }

    /// <summary>
    /// Permite referenciar un parámetro
    /// </summary>
    public class ValorParametroBloqueante : FrameworkElement
    {
        public int? Valor
        {
            get { return (int?)GetValue(ElementoProperty); }
            set { SetValue(ElementoProperty, value); }
        }

        public static readonly DependencyProperty ElementoProperty =
            DependencyProperty.Register("Valor", typeof(int?),
            typeof(ValorParametroBloqueante), new UIPropertyMetadata(null));
    }

}