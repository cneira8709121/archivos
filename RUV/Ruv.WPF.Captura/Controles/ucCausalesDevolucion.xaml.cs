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
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Devolucion;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.Infrastructure.Crosscutting.Resources.Globalization;

namespace Ruv.WPF.Captura
{
	/// <summary>
	/// Interaction logic for ucCausalesDevolucion.xaml
	/// </summary>
	public partial class ucCausalesDevolucion : UserControl
    {
        #region RoutedEvents

        public static readonly RoutedEvent AceptarClickEvent = EventManager.RegisterRoutedEvent("AceptarClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ucCausalesDevolucion));
        public static readonly RoutedEvent CancelarClickEvent = EventManager.RegisterRoutedEvent("CancelarClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ucCausalesDevolucion));

        #endregion
        #region DependencyProperties

        public static readonly DependencyProperty IdUsuarioProperty = DependencyProperty.Register("IdUsuario", typeof(int?), typeof(ucCausalesDevolucion), new UIPropertyMetadata(null));
        public static readonly DependencyProperty IdDeclaracionProperty = DependencyProperty.Register("IdDeclaracion", typeof(int?), typeof(ucCausalesDevolucion), new UIPropertyMetadata(null));
        public static readonly DependencyProperty IdRadicacionProperty = DependencyProperty.Register("IdRadicacion", typeof(int?), typeof(ucCausalesDevolucion), new UIPropertyMetadata(null));
        public static readonly DependencyProperty IdEntidadMunicipioProperty = DependencyProperty.Register("IdEntidadMunicipio", typeof(int?), typeof(ucCausalesDevolucion), new UIPropertyMetadata(null));

        #endregion
        #region Events

        public event RoutedEventHandler AceptarClick
        {
            add { AddHandler(AceptarClickEvent, value); }
            remove { RemoveHandler(AceptarClickEvent, value); }
        }

        public event RoutedEventHandler CancelarClick
        {
            add { AddHandler(CancelarClickEvent, value); }
            remove { RemoveHandler(CancelarClickEvent, value); }
        }

        #endregion
        #region Attributes

        private eTipoParametros _eParametroTipoCausal;

        #endregion
        #region Properties

        public int? IdUsuario
        {
            get { return (int?)GetValue(IdUsuarioProperty); }
            set { SetValue(IdUsuarioProperty, value); }
        }

        public int? IdDeclaracion
        {
            get { return (int?)GetValue(IdDeclaracionProperty); }
            set { SetValue(IdDeclaracionProperty, value); }
        }

        public int? IdRadicacion
        {
            get { return (int?)GetValue(IdRadicacionProperty); }
            set { SetValue(IdRadicacionProperty, value); }
        }

        public int? IdEntidadMunicipio
        {
            get { return (int?)GetValue(IdEntidadMunicipioProperty); }
            set { SetValue(IdEntidadMunicipioProperty, value); }
        }

        public eTipoParametros EParametroTipoCausal
        {
            get { return _eParametroTipoCausal; }
            set
            {
                if (!(value == eTipoParametros.CausalesTodos ||
                      value == eTipoParametros.CausalesLiderRadicacion ||
                      value == eTipoParametros.CausalesCriticaN ||
                      value == eTipoParametros.CausalesGlosas ||
                      value == eTipoParametros.CausalesValoracion)) 
                    _eParametroTipoCausal = eTipoParametros.CausalesTodos;
                else
                    _eParametroTipoCausal = value;

                ucListaOpciones.TipoParametros = _eParametroTipoCausal;
            }
        }

        #endregion
        #region Constructor

        public ucCausalesDevolucion()
		{
			this.InitializeComponent();
		}

        #endregion

        #region Private methods

        #region Events

        private void AceptarButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ucListaOpciones.ValoresUsuario.Count == 0 && txtObservaciones.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Informacion.ObservacionesRequeridasDevolucion, Informacion.Titulo, MessageBoxButton.OK);
                return;
            }

            if (AceptarClickEvent != null)
            {
                RaiseEvent(new RoutedEventArgs(AceptarClickEvent, this));
            }
        }

        private void CancelarButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CancelarClickEvent != null)
            {
                RaiseEvent(new RoutedEventArgs(CancelarClickEvent, this));
            }
        }

        #endregion

        #endregion
    }
}