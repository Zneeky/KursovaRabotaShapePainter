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
        public virtual void  Move(int mDX, int mDY, int eX, int eY)
        {
            int changeX = eX - mDX;
            int changeY = eY - mDY;

            Location = new Point
            {
                X = Location.X + changeX,
                Y = Location.Y + changeY
            };

        }
        public abstract void Paint(IGraphics graphics);

        protected abstract double CalculateArea();

        public abstract bool PointInShape(Point point);



       
    }
}
