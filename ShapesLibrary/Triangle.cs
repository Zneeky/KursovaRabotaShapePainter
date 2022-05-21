using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesLibrary
{

    [Serializable]
    public class Triangle:Shape
    {

        public Point p2 { get; set; }
        public Point p3 { get; set; }

        public override string Tag
        {
            get
            {
                return "triangle";
            }
        }
        public override int Area
        {
            get
            {
                return (int)(CalculateArea() / 1428);
            }
        }

        



        public override void Paint(IGraphics graphics)
        {
            var colorBorder = Selected ? Color.Red : ColorBorder;
            var colorFill = Color.FromArgb(100, ColorBorder);
            graphics.DrawTriangle(colorBorder, colorFill, p2.X, p2.Y, p3.X, p3.Y, Location.X, Location.Y);
        }

        public override void Move (int mDX, int mDY,int eX, int eY)
        {

            int changeX =eX- mDX;
            int changeY =eY- mDY;

            Location = new Point
            {
                X = Location.X + changeX,
                Y = Location.Y + changeY
            };
            p2 = new Point
            {

                X = p2.X + changeX,
                Y = p2.Y + changeY
            };
            p3 = new Point
            {

                X = p3.X + changeX,
                Y = p3.Y + changeY
            };
        }
        public override bool PointInShape(Point point)
        {

            var s = (p2.X - Location.X) * (point.Y - Location.Y) - (p2.Y - Location.Y) * (point.X - Location.X);
            var t = (p3.X - p2.X) * (point.Y - p2.Y) - (p3.Y - p2.Y) * (point.X - p2.X);

            if ((s < 0) != (t < 0) && s != 0 && t != 0)
                return false;

            var d = (Location.X - p3.X) * (point.Y - p3.Y) - (Location.Y - p3.Y) * (point.X - p3.X);
            return d == 0 || (d < 0) == (s + t <= 0);
        }

        protected override double CalculateArea()
        {
            double height = Math.Abs(Location.Y - p3.Y);
            double width = Math.Abs(Location.X - p2.X);
            return (height * width) / 2;
        }

       
    }
}
