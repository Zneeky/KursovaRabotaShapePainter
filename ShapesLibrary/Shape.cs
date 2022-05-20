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
        public virtual void  Move(int mouseX, int mouseY, int eX, int eY)
        {
            int changeX = eX - mouseX;
            int changeY = eY - mouseY;

            int speedX = Math.Abs(eX - mouseX);
            int speedY = Math.Abs(eY - mouseY);

            if (changeX > 0)
            {
                Location = new Point
                {
                    X = Location.X + speedX,
                    Y = Location.Y
                };
            }
            else if (changeX < 0)
            {
                Location = new Point
                {
                    X = Location.X - speedX,
                    Y = Location.Y
                };
            }

            if (changeY > 0)
            {
                Location = new Point
                {
                    X = Location.X,
                    Y = Location.Y + speedY
                };
            }
            else if (changeY < 0)
            {
                Location = new Point
                {
                    X = Location.X,
                    Y = Location.Y - speedY
                };
            }

        }
        public abstract void Paint(IGraphics graphics);

        protected abstract double CalculateArea();

        public abstract bool PointInShape(Point point);



       
    }
}
