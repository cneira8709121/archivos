using System;
using System.Collections.Generic;
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

namespace Ruv.WPF.Captura.Controles
{
	/// <summary>
	/// Interaction logic for ImageButton.xaml
	/// </summary>
	public partial class ImageButton : UserControl
	{
        #region RoutedEvents

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ImageButton));

        #endregion
        #region Events

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        #endregion
        #region DependencyProperties

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ImageButton), new UIPropertyMetadata(""));
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton), new UIPropertyMetadata(null));

        #endregion
        #region Properties

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        #endregion
        #region Constructor

        public ImageButton()
        {
            this.InitializeComponent();
        }

        #endregion
        #region Private methods

        #region Events

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ClickEvent != null)
            {
                RaiseEvent(new RoutedEventArgs(ClickEvent, this));
            }
        }

        #endregion

        #endregion
	}
}