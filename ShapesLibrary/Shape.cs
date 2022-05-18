using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShapesLibrary
{
    [Serializable]
    public abstract class Shape
    {
        public Point Location { get; set; }
        public Color ColorBorder { get; set; }
        public virtual string Tag { get; }
        public abstract int Area { get; }

        [NonSerialized]
        private bool _selected;   
        public bool Selected { 
            get=>_selected;
            set => _selected = value;
        }
        public virtual void  Move(int X,int Y , int X1, int Y1)
        {
            Location = new Point
            {
                X = Math.Min(X, X1),
                Y = Math.Min(Y, Y1)
            };
        }
        public abstract void Paint(IGraphics graphics);

        protected abstract double CalculateArea();

        public abstract bool PointInShape(Point point);



       
    }
}
