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
                return (int)(CalculateArea() / 1444);
            }
        }

        



        public override void Paint(IGraphics graphics)
        {
            var colorBorder = Selected ? Color.Red : ColorBorder;
            var colorFill = Color.FromArgb(100, ColorBorder);
            //using (var brush=new SolidBrush(colorFill))
            //{
            //    Point[] triaglePoints = new Point[] { p2, p3, Location };
            //    graphics.FillPolygon(brush, triaglePoints);
            //}
            //using(var pen = new Pen(colorBorder))
            //{
            //    pen.Width = 3;
            //    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //    graphics.DrawLine(pen, p2, p3);
            //    graphics.DrawLine(pen, p2, Location);
            //    graphics.DrawLine(pen, Location, p3);

            //}
            graphics.DrawTriangle(colorBorder, colorFill, p2.X, p2.Y, p3.X, p3.Y, Location.X, Location.Y);
        }

        public override void Move (int mouseX, int mouseY,int eX, int eY)
        {

            int X =eX-mouseX;
            int Y =eY-mouseY;
            int speedX = Math.Abs(mouseX - eX);
            int speedY = Math.Abs(mouseY - eY);


            if (X > 0)
            {
                Location = new Point
                {
                    X = Location.X + speedX,
                    Y = Location.Y
                };
                p2 = new Point
                {

                    X = p2.X + speedX,
                    Y = p2.Y
                };
                p3 = new Point
                {

                    X = p3.X + speedX,
                    Y = p3.Y
                };
            }
            else if (X < 0)
            {
                Location = new Point
                {
                    X = Location.X - speedX,
                    Y = Location.Y
                };
                p2 = new Point
                {

                    X = p2.X - speedX,
                    Y = p2.Y
                };
                p3 = new Point
                {

                    X = p3.X - speedX,
                    Y = p3.Y
                };
            }

            if (Y > 0)
            {
                Location = new Point
                {
                    X = Location.X,
                    Y = Location.Y + speedY
                };
                p2 = new Point
                {

                    X = p2.X,
                    Y = p2.Y + speedY
                };
                p3 = new Point
                {

                    X = p3.X,
                    Y = p3.Y + speedY
                };
            }
            else if (Y < 0)
            {
                Location = new Point
                {
                    X = Location.X,
                    Y = Location.Y - speedY
                };
                p2 = new Point
                {

                    X = p2.X,
                    Y = p2.Y - speedY
                };
                p3 = new Point
                {

                    X = p3.X,
                    Y = p3.Y - speedY
                };
            }

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
           
            double area = TriangleAreaFormula(Location.X,Location.Y,p2.X,p2.Y,p3.X,p3.Y);
            return area;
        }

       
        private double TriangleAreaFormula(int x1, int y1, int x2,
                       int y2, int x3, int y3)
        {
            return Math.Abs((x1 * (y2 - y3) +
                             x2 * (y3 - y1) +
                             x3 * (y1 - y2)) / 2.0);
        }
    }
}
