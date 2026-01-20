using System;
using System.Collections.Generic;
using System.IO;
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
using w = Ruv.Infrastructure.Crosscutting.Utilities.Wacom;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.FirmaDeclaracion;

namespace Ruv.WPF.Captura.Controles.Wacom
{
    /// <summary>
    /// Interaction logic for ucFirmaDigital.xaml
    /// </summary>
    public partial class ucFirmaDigital : UserControl
    {
        #region Attributes

        private w::Capture cap = null;
        private SignatureHost sh = null;

        #endregion
        #region Constructor

        public ucFirmaDigital()
        {
            InitializeComponent();

            sh = new SignatureHost();
            cvn.Children.Add(sh);

            cap = new w::Capture();
            cap.DrawLine += new EventHandler<w::LineEventArgs>(cap_DrawLine);
            cap.DrawRectangle += new EventHandler<w.RectangleEventArgs>(cap_DrawRectangle);
        }

        #endregion
        #region Public Methods

        public void DisableCapture()
        {
            if (cap != null)
            {
                cap.Dispose();
                cap = null;
            }
        }

        #endregion
        #region Private Methods

        private bool TestWacom()
        {
            w::Info info = new w::Info();
            bool bActive = info.IsActive;

            lblConectado.Content = bActive ? "SI" : "NO";

            return bActive;
        }

        #region Events

        private void cap_DrawRectangle(object sender, w.RectangleEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("X: {0}, Y: {1}, Width: {2}, Height: {3}", new object[] { e.X, e.Y, e.Width, e.Height }));
            Pen pen = new Pen { Thickness = (double)e.MPen.Width, Brush = Brushes.Black };
            sh.DrawRectangle(pen, e.X, e.Y, e.Width, e.Height);
        }

        private void cap_DrawLine(object sender, w.LineEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("Point1: {0}, Point2: {1}", new object[] { e.Point1.ToString(), e.Point2.ToString() }));
            Pen pen = new Pen { Thickness = (double)e.MPen.Width, Brush = Brushes.Black };
            Point pt1 = new Point(e.Point1.X.Value, e.Point1.Y.Value);
            Point pt2 = new Point(e.Point2.X.Value, e.Point2.Y.Value);
            sh.DrawLine(pen, pt1, pt2);
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            if (TestWacom())
            {
                cap.Enable((int)cvn.ActualWidth, (int)cvn.ActualHeight);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MemoryStream file = new MemoryStream())
                {
                    // Save current canvas transform
                    Transform transform = cvn.LayoutTransform;
                    GeneralTransform gt = cvn.TransformToAncestor((Visual)cvn.Parent);
                    // reset current transform (in case it is scaled or rotated)
                    cvn.LayoutTransform = null;
                    cvn.RenderTransform = null;
                    // Get the size of canvas
                    Size size = new Size(cvn.Width, cvn.Height);
                    //Size size = new Size(cap.MaxPt.X.Value - cap.MinPt.X.Value, cap.MaxPt.Y.Value - cap.MinPt.Y.Value);
                    // Measure and arrange the surface
                    // VERY IMPORTANT
                    cvn.Measure(size);
                    cvn.Arrange(new Rect(size));

                    int marg = int.Parse(cvn.Margin.Left.ToString());
                    RenderTargetBitmap rtb = new RenderTargetBitmap((int)this.cvn.ActualWidth - marg,
                                    (int)this.cvn.ActualHeight - marg, 0, 0, PixelFormats.Default);
                    //RenderTargetBitmap rtb = new RenderTargetBitmap(cap.MaxPt.X.Value - cap.MinPt.X.Value - marg,
                    //                cap.MaxPt.Y.Value - cap.MinPt.Y.Value - marg, 0, 0, PixelFormats.Default);
                    rtb.Render(cvn);
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(file);
                    clsFirma f = (clsFirma)DataContext;
                    f.firma = file.GetBuffer();
                    file.Close();

                    // Restore previously saved layout
                    cvn.LayoutTransform = transform;
                    cvn.RenderTransform = (Transform)gt;
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message, Title);
            }
            finally
            {
                if (TestWacom())
                {
                    cap.Disable();
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            cvn.Children.Clear();
            sh = new SignatureHost();
            cvn.Children.Add(sh);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TestWacom();
        }

        #endregion

        #endregion
    }
}
