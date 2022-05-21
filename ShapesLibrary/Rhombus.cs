using System;
using System.Collections.Generic;
using System.Drawing;  using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapesLibrary.Exceptions;


namespace ShapesLibrary
{
    [Serializable]
    public class Rhombus : Shape
    {

        private int _height;
        public  int Height { get=>_height; set 
            {
                if (value < 0)
                    throw new InvalidValueException("Negative value for height not allowed!");

                _height = value;

            } 
        }

        private int _width;
        public  int Width { get=>_width; set
            {
                if (value < 0)
                    throw new InvalidValueException("Negative value for width not allowed!");

                _width = value;
            }
        }

        private Point A = Point.Empty;
        private Point B = Point.Empty;
        private Point C = Point.Empty;
        private Point D = Point.Empty;



        public override string Tag
        {
            get
            {
                return "rhombus";
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
            graphics.DrawRhombus(colorBorder, colorFill, Location.X, Location.Y, Width, Height);
        }
        public override bool PointInShape(Point point)
        {
            
             A = new Point(Location.X, Location.Y);
             B = new Point(Location.X + (Width / 2), Location.Y + (Height / 2));
             C = new Point(Location.X + Width, Location.Y);
             D = new Point(Location.X + (Width / 2), Location.Y - Height/2);

            double areaOfRhombus = CalculateArea();
            double area1 = TriangleAreaFormula(A.X, A.Y, B.X, B.Y, point.X, point.Y);
            double area2 = TriangleAreaFormula(B.X, B.Y, C.X, C.Y, point.X, point.Y);
            double area3 = TriangleAreaFormula(C.X, C.Y, D.X, D.Y, point.X, point.Y);
            double area4 = TriangleAreaFormula(A.X, A.Y, D.X, D.Y, point.X, point.Y);


            return area1+area2+area3+area4==areaOfRhombus;
            
        }

        protected override double CalculateArea()
        {
            A = new Point(Location.X, Location.Y);
            B = new Point(Location.X + (Width / 2), Location.Y + (Height / 2));
            C = new Point(Location.X + Width, Location.Y);
            D = new Point(Location.X + (Width / 2), Location.Y - Height / 2);



            double area1 = TriangleAreaFormula(A.X, A.Y, B.X, B.Y, Location.X+Width/2, Location.Y+Height/2);
            double area2 = TriangleAreaFormula(B.X, B.Y, C.X, C.Y, Location.X +Width/2, Location.Y + Height / 2);
            double area3 = TriangleAreaFormula(C.X, C.Y, D.X, D.Y, Location.X + Width / 2, Location.Y + Height / 2);
            double area4 = TriangleAreaFormula(A.X, A.Y, D.X, D.Y, Location.X +Width/ 2, Location.Y + Height / 2);

            return (area1 + area2 + area3 + area4);

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
