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
    public class Square:Shape
    {

        private int _side;
        public int Side { 
            get=>_side;
            set 
            {
                if (value < 0)
                    throw new InvalidValueException("Negative value for side is not allowed!");

                _side = value;
            }
        }


        public override string Tag
        {
            get
            {
                return "square";
            }
        }

        public override int Area
        {
            get
            {
                return (int)(CalculateArea() /1444);
            }
        }

        


        public override void Paint(IGraphics graphics)
        {
            var colorBorder = Selected ? Color.Red : ColorBorder;
            var colorFill = Color.FromArgb(100, ColorBorder);
            //using (var brush = new SolidBrush(colorFill))
            //{
            //    graphics.FillRectangle(brush, Location.X, Location.Y, Side, Side);
            //}

            //using (var pen = new Pen(colorBorder))
            //{
            //    pen.Width = 3;
            //    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //    graphics.DrawRectangle(pen, Location.X, Location.Y, Side, Side);
            //}
            graphics.DrawSquare(colorBorder, colorFill, Location.X, Location.Y, Side);
        }

        public override bool PointInShape(Point point)
        {
            return Location.X <= point.X && point.X <= Location.X + Side &&
                   Location.Y <= point.Y && point.Y <= Location.Y + Side;
        }
        protected override double CalculateArea()
        {
            return (Side * Side);
        }

        
    }
}
