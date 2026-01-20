using System;

namespace Ruv.Infrastructure.Crosscutting.Utilities.Wacom
{
    public class LineEventArgs : EventArgs
    {
        public Pen MPen { get; set; }
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
    }

    public class RectangleEventArgs : EventArgs
    {
        public Pen MPen { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}