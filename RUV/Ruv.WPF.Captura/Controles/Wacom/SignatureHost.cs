using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Ruv.WPF.Captura.Controles.Wacom
{
    public class SignatureHost : FrameworkElement
    {
        #region Attributes

        private VisualCollection _children;

        #endregion
        #region Constructor

        public SignatureHost()
        {
            _children = new VisualCollection(this);
        }

        #endregion
        #region Public methods

        public void DrawRectangle(Pen pen, int X, int Y, int Width, int Height)
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            // Create a rectangle and draw it in the DrawingContext.
            Rect rect = new Rect(X, Y, Width, Height);
            drawingContext.DrawRectangle(null, pen, rect);

            // Persist the drawing content.
            drawingContext.Close();

            _children.Add(drawingVisual);
        }

        public void DrawRectangle(Rect rect, Brush brush)
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            // Create a rectangle and draw it in the DrawingContext.
            drawingContext.DrawRectangle(brush, (Pen)null, rect);

            // Persist the drawing content.
            drawingContext.Close();

            _children.Add(drawingVisual);
        }

        public void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            // Draw a new line in the DrawingContext.
            drawingContext.DrawLine(pen, pt1, pt2);

            // Persist the drawing content.
            drawingContext.Close();

            _children.Add(drawingVisual);
        }

        #endregion
        #region Protected methods

        // Provide a required override for the VisualChildrenCount property.
        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }

        // Provide a required override for the GetVisualChild method.
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }

        #endregion
    }
}
