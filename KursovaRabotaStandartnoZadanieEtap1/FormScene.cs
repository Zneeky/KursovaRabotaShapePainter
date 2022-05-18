using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShapesLibrary;

using Rectangle = ShapesLibrary.Rectangle;

namespace KursovaRabotaShapePainter
{
    
    public partial class FormScene : Form,IGraphics
    {
        
        List<Control> panels;
        List<Control> buttons;

       
        private OpenFileDialog ofd;
        private SaveFileDialog sfd;
        private List<Shape> _shapes = new List<Shape>();
        private Rectangle _frameRectangle;
        private Square _frameSquare;
        private Triangle _frameTriangle;
        private Rhombus _frameRhombus;
        private Circle _frameCircle;
        private Point _mouseDownLocation;
        private bool moving;
        private bool darkMode;
        private MouseEventArgs mouseEventArgs;
        private int buttonIndex;
        private Graphics _onPaintGraphics;

       

        public FormScene()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true);
            KeyPreview = true;
            this.MouseMove += delegate (object s, MouseEventArgs e)
              {
                  label1.Text = $"x:{e.X} y:{e.Y}";
              };

           

        }

        public void DrawRectangle(Color colorBorder, Color colorFill, int x, int y, int width, int height)
        {
            if (_onPaintGraphics != null)
            {
                using (var brush = new SolidBrush(colorFill))
                {
                    _onPaintGraphics.FillRectangle(brush, x, y, width, height);
                }

                using (var pen = new Pen(colorBorder))
                {
                    pen.Width = 3;
                    _onPaintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    _onPaintGraphics.DrawRectangle(pen, x, y, width, height);
                }
            }
        }

        public void DrawCircle(Color colorBorder, Color colorFill, int x, int y, int radius)
        {
            if (_onPaintGraphics != null)
            {
                using (var brush = new SolidBrush(colorFill))
                {
                    _onPaintGraphics.FillEllipse(brush, x-radius, y-radius, radius * 2, radius * 2);
                }
                using (var pen = new Pen(colorBorder))
                {
                    pen.Width = 3;
                    _onPaintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    _onPaintGraphics.DrawEllipse(pen, x - radius, y - radius, radius * 2, radius * 2);
                }
            }
        }

        public void DrawTriangle(Color colorBorder, Color colorFill, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            if (_onPaintGraphics != null)
            {
                Point p1 = new Point { X = x1, Y = y1 };
                Point p2 = new Point { X = x2, Y = y2 };
                Point p3 = new Point { X = x3, Y = y3 };
                
                
                    using (var brush = new SolidBrush(colorFill))
                    {

                        Point[] triaglePoints = new Point[] { p1, p2, p3 };
                        _onPaintGraphics.FillPolygon(brush, triaglePoints);
                    }
                    using (var pen = new Pen(colorBorder))
                    {
                        pen.Width = 3;
                        _onPaintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        _onPaintGraphics.DrawLine(pen, p1, p2);
                        _onPaintGraphics.DrawLine(pen, p1, p3);
                        _onPaintGraphics.DrawLine(pen, p3, p2);

                   
                }
            }
        }
     
        public void DrawSquare(Color colorBorder, Color colorFill, int x, int y, int side)
        {
            if (_onPaintGraphics != null)
            {
                using (var brush = new SolidBrush(colorFill))
                {
                    _onPaintGraphics.FillRectangle(brush, x, y, side, side);
                }

                using (var pen = new Pen(colorBorder))
                {
                    pen.Width = 3;
                    _onPaintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    _onPaintGraphics.DrawRectangle(pen, x, y, side, side);
                }
            }
        }

        public void DrawRhombus(Color colorBorder, Color colorFill, int x, int y, int width, int height)
        {
            if (_onPaintGraphics != null)
            {
                using (GraphicsPath myPath = new GraphicsPath())
                {

                    myPath.AddLines(new[]
                    {
                      new Point(x, y),
                      new Point(x + (width/ 2), y+(height/2)),
                      new Point(x + (width), y),
                      new Point(x + (width/2), y-(height/2)),
                      new Point(x, y)
                    });
                    using (var brush = new SolidBrush(colorFill))
                        _onPaintGraphics.FillPath(brush, myPath);
                    using (Pen pen = new Pen(colorBorder))
                    {
                        pen.Width = 3;
                        _onPaintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        _onPaintGraphics.DrawPath(pen, myPath);
                    }


                }
            }
        }
        void Initilize_Add()
        {
        
            panels = new List<Control>();
            buttons = new List<Control>();

            panels.Add(panel1);
            

            buttons.Add(btn_triangle);
            buttons.Add(btn_rectangle);
            buttons.Add(btn_square);
            buttons.Add(btn_rhomb);
            buttons.Add(btn_circle);

        }

        void ApplyTheme(Color back, Color pan, Color btn, Color menueStrip, Color TextColor)
        {
            this.BackColor = back;
            menuStripSelection.BackColor = menueStrip;
            menuStripSelection.ForeColor = TextColor;

            foreach (Control item in panels)
            {
                item.BackColor = pan;
            }

            foreach (Control item in buttons)
            {               
                item.BackColor = btn;
                item.ForeColor = TextColor;
                
            }
           
        }

        private int CalculateShapesArea()
        {
            //var area = 0;
            //for (int s = 0; s < _shapes.Count; s++)
            //{
            //    area += _shapes[s].Area;
            //}

            int area = _shapes.Select(s => s.Area).Sum();
            return area;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _onPaintGraphics = e.Graphics;
            foreach (var s in _shapes)
                s.Paint(this);

           _frameRectangle?.Paint(this);
           _frameTriangle?.Paint(this);
           _frameSquare?.Paint(this);
           _frameRhombus?.Paint(this);
           _frameCircle?.Paint(this);

            _onPaintGraphics = null;
        }

       

        private void FormScene_MouseDown(object sender, MouseEventArgs e)
        {
            mouseEventArgs = e;
            Unselect();
            switch (e.Button)
            {
                case MouseButtons.Right:
                        _mouseDownLocation = e.Location;
                        switch (buttonIndex)
                        {
                            case 1:
                                { _frameTriangle = new Triangle(); _frameTriangle.ColorBorder = Color.Black; }
                                break;
                            case 2:
                                {_frameRectangle = new Rectangle(); _frameRectangle.ColorBorder = Color.Black; }
                                break;
                            case 3:
                                { _frameSquare = new Square(); _frameSquare.ColorBorder = Color.Black; }                              
                                break;
                            case 4:
                                { _frameRhombus = new Rhombus(); _frameRhombus.ColorBorder = Color.Black; }
                                break;
                            case 5:
                                {_frameCircle = new Circle(); _frameCircle.ColorBorder = Color.Black; }
                                break;
                            default:
                                break;
                        }
                        break;

                case MouseButtons.Left:
                        Unselect();
                        for (int s = _shapes.Count - 1; s >= 0; s--)
                        {
                            if (_shapes[s].PointInShape(e.Location))
                            {
                                _shapes[s].Selected = true;
                                break;
                            }
                        }                       
                    break;

                case MouseButtons.XButton1: 
                        Unselect();
                        for (int s = _shapes.Count - 1; s >= 0; s--)
                        {
                            if (_shapes[s].PointInShape(e.Location))
                            {
                                _shapes[s].Selected = true;
                                moving = true;
                                break;
                            }
                        }
                    break;

                 default:
                        Unselect();
                    break;
            }
          
            Invalidate();
        }

        private void FormScene_MouseMove(object sender, MouseEventArgs e)
        {
            //TO DO: optimaze the code below:
            if (moving)
            {
                for (int s = _shapes.Count - 1; s >= 0; s--)
                {
                   
                    if (_shapes[s].Selected == true)
                    {
                        if (_shapes[s].Tag == "triangle")
                        {
                            _mouseDownLocation = _shapes[s].Location;
                            _shapes[s].Move(_mouseDownLocation.X, _mouseDownLocation.Y, e.Location.X, e.Location.Y);
                           
                        }              
                        else
                        {
                            _shapes[s].Move(_mouseDownLocation.X, _mouseDownLocation.Y, e.Location.X, e.Location.Y);
                            _mouseDownLocation = e.Location;
                        }

                        Invalidate();                       
                        break;
                    }
                }
            }

            if (_frameRectangle != null)
            {
                _frameRectangle.Location = new Point
                {
                    X = Math.Min(_mouseDownLocation.X, e.Location.X),
                    Y = Math.Min(_mouseDownLocation.Y, e.Location.Y)
                };

                _frameRectangle.Width = Math.Abs(_mouseDownLocation.X - e.Location.X);
                _frameRectangle.Height = Math.Abs(_mouseDownLocation.Y - e.Location.Y);

                Invalidate();
            }
            else if (_frameTriangle != null)
            {  
                
                //end postion
                _frameTriangle.Location = PointToClient(MousePosition);

                //pointOfSymetry
                double xMid = (_mouseDownLocation.X + e.Location.X) / 2;

                _frameTriangle.p2 = new Point(_mouseDownLocation.X, e.Location.Y);
                _frameTriangle.p3 = new Point((int)xMid, _mouseDownLocation.Y);

                Invalidate();
            } else if (_frameRhombus != null)
            {
                _frameRhombus.Location = new Point
                {
                    X = Math.Min(_mouseDownLocation.X, e.Location.X),
                    Y = Math.Min(_mouseDownLocation.Y, e.Location.Y)
                };
                _frameRhombus.Width= Math.Abs(_mouseDownLocation.X - e.Location.X);
                _frameRhombus.Height = Math.Abs(_mouseDownLocation.Y - e.Location.Y);


                Invalidate();
            }
            else if (_frameCircle != null)
            {
                _frameCircle.Location = new Point
                {
                    X = Math.Min(_mouseDownLocation.X, e.Location.X),
                    Y = Math.Min(_mouseDownLocation.Y, e.Location.Y)
                };
                _frameCircle.Radius= Math.Abs(_mouseDownLocation.X - e.Location.X);


                Invalidate();
            }else if(_frameSquare!=null)
            {
                _frameSquare.Location = new Point
                {
                    X = Math.Min(_mouseDownLocation.X, e.Location.X),
                    Y = Math.Min(_mouseDownLocation.Y, e.Location.Y)
                };
                _frameSquare.Side = Math.Abs(_mouseDownLocation.X - e.Location.X);

                Invalidate();
            }
            else
            {
                return;
            }

        }

        private void FormScene_MouseUp(object sender, MouseEventArgs e)
        {
            
            var r = new Random();

            if (_frameRectangle != null)
            {
                _frameRectangle.ColorBorder = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));

                Unselect();
                _shapes.Add(_frameRectangle);
                _frameRectangle.Selected = true;
                _frameRectangle = null;

                toolStripStatusLabelArea.Text = CalculateShapesArea().ToString();

                Invalidate();
            }
            else if (_frameTriangle != null )
            {
                if (_frameTriangle.Location == _frameTriangle.p2 || _frameTriangle.p2 == _frameTriangle.p3)
                {
                    _frameTriangle = null;
                    return;
                }
                _frameTriangle.ColorBorder = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                
                Unselect();
                _shapes.Add(_frameTriangle);
                _frameTriangle.Selected = true;
                _frameTriangle = null;

                toolStripStatusLabelArea.Text = CalculateShapesArea().ToString();

                Invalidate();
            }
            else if (_frameRhombus != null)
            {

                if (_frameRhombus.Height==0 || _frameRhombus.Width == 0)
                {
                    _frameRhombus = null;
                    return;
                }
                _frameRhombus.ColorBorder = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                Unselect();
                _shapes.Add(_frameRhombus);
                _frameRhombus.Selected = true;
                _frameRhombus = null;

                toolStripStatusLabelArea.Text = CalculateShapesArea().ToString();

                Invalidate();
            }
            else if (_frameCircle != null)
            {
                _frameCircle.ColorBorder = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                
                Unselect();
                _shapes.Add(_frameCircle);
                _frameCircle.Selected = true;
                _frameCircle = null;

                toolStripStatusLabelArea.Text = CalculateShapesArea().ToString();

                Invalidate();
            }
            else if (_frameSquare != null)
            {
                _frameSquare.ColorBorder = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));

                Unselect();
                _shapes.Add(_frameSquare);
                _frameSquare.Selected = true;
                _frameSquare = null;

                toolStripStatusLabelArea.Text = CalculateShapesArea().ToString();

                Invalidate();
            }
            else
            {
                return;
            }

            
        }

        private void FormScene_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode != Keys.Delete)
            {
                return;
            }

                //for (int s = _shapes.Count - 1; s >= 0; s--)
                //{
                //    if (_shapes[s].Selected)
                //        _shapes.RemoveAt(s);
                //}
                _shapes=_shapes
                .Where(s => s.Selected != true)
                .ToList();

                toolStripStatusLabelArea.Text = CalculateShapesArea().ToString();
                Invalidate();
            
            
        }

        private void Unselect()
        {
            moving = false;
            _shapes.ForEach(s => s.Selected = false);
        }

        private void FormScene_DoubleClick(object sender, EventArgs e)
        {

            foreach (var s in _shapes)
            {

                if (mouseEventArgs.Button != MouseButtons.Right)
                {
                    if (s.Selected && s.Tag == "rectangle")
                    {
                        var fr = new FormRectangle();
                        if (darkMode == true) {
                            fr.DarkTheme();
                        }
                        fr.Rectangle = (Rectangle)s;
                        fr.ShowDialog();

                        break;
                    }

                    else if (s.Selected && s.Tag == "rhombus")
                    {
                        var fr = new FormRhombus();
                        if (darkMode == true)
                        {
                            fr.DarkTheme();
                        }
                        fr.Rhombus = (Rhombus)s;
                        fr.ShowDialog();

                        break;
                    }
                    else if (s.Selected && s.Tag == "circle")
                    {
                        var fr = new FormCircle();

                        if (darkMode == true)
                        {
                            fr.DarkTheme();
                        }
                        fr.Circle = (Circle)s;
                        fr.ShowDialog();

                        break;
                    }
                    else if (s.Selected && s.Tag == "triangle")
                    {
                        var fr = new FormTriangle();
                        if (darkMode == true)
                        {
                            fr.DarkTheme();
                        }
                        fr.Triangle = (Triangle)s;
                        fr.ShowDialog();

                        break;
                    }
                    else if (s.Selected && s.Tag == "square")
                    {
                        var fr = new FormSquare();
                        if (darkMode == true)
                        {
                            fr.DarkTheme();
                        }
                        fr.Square = (Square)s;
                        fr.ShowDialog();

                        break;
                    }
                }
            
                
            }
            Invalidate();
        }

        private void btn_triangle_Click(object sender, EventArgs e)
        {
            buttonIndex = 1;
        }

        private void btn_rectangle_Click(object sender, EventArgs e)
        {
            buttonIndex = 2;
        }

        private void btn_square_Click(object sender, EventArgs e)
        {
            buttonIndex = 3;
        }

        private void btn_rhomb_Click(object sender, EventArgs e)
        {
            buttonIndex = 4;
        }

        private void btn_circle_Click(object sender, EventArgs e)
        {
            buttonIndex = 5;
        }

        private void FormScene_Load(object sender, EventArgs e)
        {
            Initilize_Add();
            ofd = new OpenFileDialog();
            sfd = new SaveFileDialog();
        }

        Color zcolor(int r, int g, int b)
        {

            return Color.FromArgb(r, g, b);
        }

   
        private void SelectShapes(List<Shape> shapes)
        {
            foreach (var item in shapes)
            {
                item.Selected = true;
            }
            Invalidate();
        }
        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unselect();
            var centerPoint = new Point
            {
                X = Width / 2,
                Y = Height / 2
            };
            var selected = _shapes
                .Where(shape =>shape.PointInShape(centerPoint))
                .ToList();
            SelectShapes(selected);
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unselect();
            var selected = _shapes
                .Where(shape => shape.Location.X < Width / 2)
                .ToList();
            SelectShapes(selected);
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unselect();
            var selected = _shapes
                .Where(shape => shape.Location.X > Width / 2)
                .ToList();
            SelectShapes(selected);

        }

        private void colorfulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyTheme(zcolor(240, 240, 240), zcolor(240, 240, 240), zcolor(181, 181, 181), Color.Crimson, zcolor(240, 240, 240));
            panel1.BackColor = Color.LightPink;

            darkMode = false;
            btn_circle.BackColor = Color.DarkViolet;
            btn_rhomb.BackColor = Color.DarkTurquoise;
            btn_square.BackColor = Color.OliveDrab;
            btn_rectangle.BackColor = Color.Orchid;
            btn_triangle.BackColor = Color.Crimson;

            selectToolStripMenuItem.ForeColor = zcolor(240, 240, 240);
            interfaceToolStripMenuItem.ForeColor = zcolor(240, 240, 240);
            fileToolStripMenuItem.ForeColor = zcolor(240, 240, 240);
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {

            darkMode = false;
            ApplyTheme(Color.White, zcolor(240, 240, 240), zcolor(181, 181, 181), Color.White, Color.Black);
            selectToolStripMenuItem.ForeColor = Color.Black;
            interfaceToolStripMenuItem.ForeColor = Color.Black;
            fileToolStripMenuItem.ForeColor = Color.Black;
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkMode = true;
            statusStrip1.ForeColor = zcolor(240, 240, 240);
            ApplyTheme(zcolor(30, 30, 30), zcolor(45, 45, 48), zcolor(104, 104, 104), Color.Black, Color.White);
            toolStripStatusLabelArea.BackColor = zcolor(240, 240, 240); 
            toolStripStatusLabelArea.ForeColor = Color.Black;

            selectToolStripMenuItem.ForeColor = Color.White;
            interfaceToolStripMenuItem.ForeColor = Color.White;
            fileToolStripMenuItem.ForeColor = Color.White;

        }

        private void funnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_circle.Image = KursovaRabotaShapePainter.Properties.Resources.child_Circle;
            btn_rhomb.Image = KursovaRabotaShapePainter.Properties.Resources.child_Rhombus;
            btn_square.Image = KursovaRabotaShapePainter.Properties.Resources.child_SquareFixed_1_;
            btn_rectangle.Image = KursovaRabotaShapePainter.Properties.Resources.child_Rectangle;
            btn_rectangle.ImageAlign = ContentAlignment.TopCenter;
            btn_triangle.Image = KursovaRabotaShapePainter.Properties.Resources.child_Triangle;
        }

        private void woodenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_circle.Image = KursovaRabotaShapePainter.Properties.Resources.wooden_CircleTrue;
            btn_rhomb.Image = KursovaRabotaShapePainter.Properties.Resources.wooden_RhombusTrue;
            btn_square.Image = KursovaRabotaShapePainter.Properties.Resources.wooden_SquareTrue;
            btn_rectangle.Image = KursovaRabotaShapePainter.Properties.Resources.wooden_ReactangleTrue;
            btn_rectangle.ImageAlign = ContentAlignment.TopCenter;
            btn_triangle.Image = KursovaRabotaShapePainter.Properties.Resources.woodenTriangleTrue;
        }

        private void metallicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_circle.Image = KursovaRabotaShapePainter.Properties.Resources.metal_CircleTrue;
            btn_rhomb.Image = KursovaRabotaShapePainter.Properties.Resources.metal_RhombusTrue;
            btn_square.Image = KursovaRabotaShapePainter.Properties.Resources.metal_SquareTrue;
            btn_rectangle.Image = KursovaRabotaShapePainter.Properties.Resources.metal_RecTrue;
            btn_rectangle.ImageAlign = ContentAlignment.MiddleCenter;
            btn_triangle.Image = KursovaRabotaShapePainter.Properties.Resources.metal_TriangleTrue;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK) 
            {
                IFormatter formatter = new BinaryFormatter();

                using (var fs = new FileStream(sfd.FileName, FileMode.Create))
                    formatter.Serialize(fs, _shapes);
            }
           //// IFormatter formatter = new BinaryFormatter();

           // using (var fs = new FileStream("data", FileMode.Create))
           //     formatter.Serialize(fs, _shapes);
            Invalidate();
            
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();

                using (var fs = new FileStream(ofd.FileName, FileMode.Open))
                _shapes = (List<Shape>)formatter.Deserialize(fs);
                toolStripStatusLabelArea.Text = CalculateShapesArea().ToString();
                Invalidate();
            }
            //if (!File.Exists("data"))
            //{
            //    return;
            //}
            //IFormatter formatter = new BinaryFormatter();

            //using (var fs = new FileStream("data", FileMode.Open))
            //    _shapes=(List<Shape>)formatter.Deserialize(fs);

            Invalidate();
        }
    }
}
