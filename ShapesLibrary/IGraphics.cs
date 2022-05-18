using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesLibrary
{
    public interface IGraphics
    {

       void DrawRectangle(Color colorBorder,Color colorFill, int x, int y, int width, int height);

        void DrawSquare(Color colorBorder, Color colorFill, int x, int y, int side);

        void DrawRhombus(Color colorBorder, Color colorFill, int x, int y, int width, int height);

        void DrawCircle(Color colorBorder, Color colorFill, int x, int y, int radius);

        void DrawTriangle(Color colorBorder, Color colorFill, int x1, int y1, int x2, int y2, int x3,int y3);
    }
}
