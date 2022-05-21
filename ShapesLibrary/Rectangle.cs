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
    public class Rectangle:Shape
    {

        private int _height;
        public int Height
        {
            get => _height;
            set
            {
                if (value < 0)
                 throw new InvalidValueException("Negative value for height not allowed!");
               
                _height = value;              
            }
        }

        private int _width;
        public int Width { 
            get=>_width;
            set
            {
                if (value < 0)
                    throw new InvalidValueException("Negative value for width now allowed!");

                _width = value;
            } 
        }

        public override string Tag 
        {
            get
            {
                return "rectangle";
            }
        }

        public override int Area
        {
            get
            {
                return (int)(CalculateArea()/1428); 
            }
        }

       


        public override void Paint(IGraphics graphics)
        {
            var colorBorder = Selected ? Color.Red : ColorBorder;
            var colorFill = Color.FromArgb(100, ColorBorder);
            graphics.DrawRectangle(colorBorder, colorFill, Location.X, Location.Y, Width, Height);
        }

        public override bool PointInShape(Point point)
        {
            return Location.X <= point.X && point.X <= Location.X + Width &&
                   Location.Y <= point.Y && point.Y <= Location.Y + Height;
        }
        protected override double CalculateArea()
        {
            return (Height * Width);
        }

        //public bool IsRectangle(Shape shape)
        //{

        //    return ((this==shape)&&(this!=null && shape!=null));
        //}

      
    }
}
