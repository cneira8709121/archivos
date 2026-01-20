namespace Ruv.Infrastructure.Crosscutting.Utilities.Wacom
{
    public struct Pen
    {
        public int Color;
        public float Width;
        
        public Pen(int color)
        {
            this.Color = color;
            this.Width = 1f;
        }
    }

    public struct Point
    {
        public static Point Empty;
        public int? X;
        public int? Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        #region Methods

        #region Public

        public override bool Equals(object obj)
        {
            Point pt = (Point)obj;
            return pt.X == this.X && pt.Y == this.Y;
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", new object[] { X, Y });
        }

        #region Statics

        public static Point operator <(Point pt1, Point pt2)
        {
            Point minPt = new Point();

            if (pt1.X < pt2.X) minPt.X = pt1.X;
            else minPt.X = pt2.X;

            if (pt1.Y < pt2.Y) minPt.Y = pt1.Y;
            else minPt.Y = pt2.Y;

            return minPt;
        }

        public static Point operator >(Point pt1, Point pt2)
        {
            Point maxPt = new Point();

            if (pt1.X > pt2.X) maxPt.X = pt1.X;
            else maxPt.X = pt2.X;

            if (pt1.Y > pt2.Y) maxPt.Y = pt1.Y;
            else maxPt.Y = pt2.Y;

            return maxPt;
        }

        #endregion

        #endregion

        #endregion
    }
}