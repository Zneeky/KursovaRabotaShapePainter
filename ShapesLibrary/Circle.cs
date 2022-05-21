using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapesLibrary.Exceptions;

namespace ShapesLibrary
{
    [Serializable]
    public class Circle:Shape
    {

        private int _radius;
        public int Radius { 
            get=>_radius;
            set 
            {
                if (value < 0)
                    throw new InvalidValueException("Negative value for radius not allowed!");

                _radius = value;
            }
        }
        private Point center;

        public override string Tag
        {
            get
            {
                return "circle";
            }
        }
        public override double Area
        {
            get
            {
                return CalculateArea() / 1428;
            }
        }


        public override void Paint(IGraphics graphics)
        {
            var colorBorder = Selected ? Color.Red : ColorBorder;
            var colorFill = Color.FromArgb(100, ColorBorder);

            graphics.DrawCircle(colorBorder, colorFill, Location.X, Location.Y, Radius);
        }

        public override bool PointInShape(Point point)
        {
            center = new Point
            {
                X = Location.X ,
                Y = Location.Y
            };

            int dx = Math.Abs(point.X - center.X);
            int dy = Math.Abs(point.Y - center.Y);
            return dx*dx+dy*dy<=Radius*Radius;
        }

        protected override double CalculateArea()
        {
            return (Radius*Radius*Math.PI);
        }
        


    }
}
