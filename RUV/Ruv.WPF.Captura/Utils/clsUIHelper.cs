using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace System
{
    public delegate FrameworkElementItem DelegadoCriterio(DependencyObject objeto);

    public class FrameworkElementItem : INotifyPropertyChanged
    {
        public FrameworkElement SourceControl { get; set; }
        public string Description { get; set; }

        private bool _Seleccionado;
        public bool Seleccionado
        {
            get { return _Seleccionado; }
            set
            {
                _Seleccionado = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Seleccionado"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class clsUIHelper
    {
        #region BÚSQUEDA DE CONTROLES EN EL VISUALTREE

        List<FrameworkElementItem> AllChildren;

        DelegadoCriterio Criterio;
        DependencyObject OneChild;
        bool SoloElPrimero;
        bool UnoEncontrado;
        bool UsarVisualTree;
        bool OmitirHijosConPadre;

        /// <summary>
        /// Retorna todos los hijos de un FrameworkElement.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="criteria">Criterio de búsqueda.</param>
        /// <returns></returns>
        /// <param name="omitirHijosConPadre">Si el padre no cumple con el criterio, no se hace búsqueda de sus hijos.</param>
        /// <param name="usarVisualTree">True: Se utliza el VisualTree (más resumido) y no el LogicalTree (más compacto).</param>
        public List<FrameworkElementItem> GetChildren(
          DependencyObject parent, DelegadoCriterio criteria, bool soloElPrimero = false,
          bool usarVisualTree = true,
          bool omitirHijosConPadre = false)
        {
            SoloElPrimero = soloElPrimero;
            UsarVisualTree = usarVisualTree;
            OmitirHijosConPadre = omitirHijosConPadre;


            if (AllChildren == null)
                AllChildren = new List<FrameworkElementItem>();
            else
                AllChildren.Clear();
            UnoEncontrado = false;

            Criterio = criteria;
            GetChildrenRecursive(parent);

            return AllChildren;
        }

        void GetChildrenRecursive(DependencyObject parent)
        {
            if (UnoEncontrado && SoloElPrimero) return;
            if (parent == null) return;
            List<UIElement> ListaHijos = new List<UIElement>();
            int childrenCount = 0; // VisualTreeHelper.GetChildrenCount(parent);
            bool ControlCumple = false;

            if (UsarVisualTree)
            {
                childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                if (childrenCount == 0) return;
                for (int i = 0; i < childrenCount; i++)
                    ListaHijos.Add(VisualTreeHelper.GetChild(parent, i) as UIElement);
            }
            else
            {
                // Determinar si se debe utilizar el VisualTree o el LogicalTree.
                //if (childrenCount == 0)
                //{
                ListaHijos = LogicalTreeHelper.GetChildren(parent).OfType<UIElement>().ToList();
                if (!ListaHijos.Any()) return;
                childrenCount = ListaHijos.Count;
                //}
                //else
                //{
                //  ListaHijos = new List<UIElement>();
                //  for (int i = 0; i < childrenCount; i++)
                //    ListaHijos.Add(VisualTreeHelper.GetChild(parent, i) as UIElement);
                //}
            }

            for (int i = 0; i < childrenCount; i++)
            {
                OneChild = ListaHijos[i];
                FrameworkElementItem Item = null;
                if (Criterio == null)
                {
                    AllChildren.Add(new FrameworkElementItem
                    {
                        SourceControl = ListaHijos[i] as FrameworkElement,
                        Description = null
                    });
                    ControlCumple = true;
                    UnoEncontrado = true;
                }
                else
                {
                    if ((Item = Criterio(OneChild as FrameworkElement)) != null)
                    {
                        AllChildren.Add(Item);
                        UnoEncontrado = true;
                        ControlCumple = true;
                    }
                }

                if (OmitirHijosConPadre == false || !ControlCumple)
                    GetChildrenRecursive(OneChild);
            }
        }

        #endregion
    }
}