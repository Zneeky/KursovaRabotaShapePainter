using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShapesLibrary;


namespace KursovaRabotaShapePainter
{
    public partial class FormTriangle : Form
    {
        private Triangle _triangle;
        int width;
        int height;
        

        public Triangle Triangle
        {
            get
            {
                return _triangle;
            }
            set
            {
                _triangle = value;
                textBoxWidth.Text =Math.Abs(_triangle.Location.X - _triangle.p2.X).ToString();
                width = Math.Abs(_triangle.Location.X - _triangle.p2.X);
                
                textBoxHeight.Text = Math.Abs(_triangle.Location.Y - _triangle.p3.Y).ToString();
                height = Math.Abs(_triangle.Location.Y - _triangle.p3.Y);

                buttonColor.BackColor = _triangle.ColorBorder;
            }
        }
        public FormTriangle()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBoxWidth.Text) < 0 || int.Parse(textBoxHeight.Text) < 0)
            {
                MessageBox.Show("Invalid value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Triangle.Location.X > Triangle.p2.X)
            {
                if (width > int.Parse(textBoxWidth.Text))
                {
                    Triangle.Location = new Point
                    {
                        X = Triangle.Location.X - ((width - int.Parse(textBoxWidth.Text)) / 2),
                        Y = Triangle.Location.Y,
                    };

                    Triangle.p2 = new Point
                    {
                        X = Triangle.p2.X + ((width - int.Parse(textBoxWidth.Text)) / 2),
                        Y = Triangle.Location.Y,
                    };
                }
                else if (width < int.Parse(textBoxWidth.Text))
                {
                    Triangle.Location = new Point
                    {
                        X = Triangle.Location.X + Math.Abs((width - int.Parse(textBoxWidth.Text)) / 2),
                        Y = Triangle.Location.Y,
                    };

                    Triangle.p2 = new Point
                    {
                        X = Triangle.p2.X - Math.Abs((width - int.Parse(textBoxWidth.Text)) / 2),
                        Y = Triangle.Location.Y,
                    };
                }
            }else if(Triangle.Location.X < Triangle.p2.X)
            {
                if (width > int.Parse(textBoxWidth.Text))
                {
                    Triangle.Location = new Point
                    {
                        X = Triangle.Location.X + ((width - int.Parse(textBoxWidth.Text)) / 2),
                        Y = Triangle.Location.Y,
                    };

                    Triangle.p2 = new Point
                    {
                        X = Triangle.p2.X - ((width - int.Parse(textBoxWidth.Text)) / 2),
                        Y = Triangle.Location.Y,
                    };
                }
                else if (width < int.Parse(textBoxWidth.Text))
                {
                    Triangle.Location = new Point
                    {
                        X = Triangle.Location.X - Math.Abs((width - int.Parse(textBoxWidth.Text)) / 2),
                        Y = Triangle.Location.Y,
                    };

                    Triangle.p2 = new Point
                    {
                        X = Triangle.p2.X + Math.Abs((width - int.Parse(textBoxWidth.Text)) / 2),
                        Y = Triangle.Location.Y,
                    };
                }
            }

            if (Triangle.p3.Y < Triangle.Location.Y)
            {
                if(height > int.Parse(textBoxHeight.Text))
                {
                   Triangle.Location = new Point
                   {
                     X = Triangle.Location.X,
                     Y = Triangle.Location.Y-(height-int.Parse(textBoxHeight.Text)),
                   };                   
                   Triangle.p2 = new Point
                   {
                     X = Triangle.p2.X,
                     Y = Triangle.p2.Y - (height - int.Parse(textBoxHeight.Text)),
                    };


                }
                else if(height < int.Parse(textBoxHeight.Text))
                {
                    Triangle.Location = new Point
                    {
                        X = Triangle.Location.X,
                        Y = Triangle.Location.Y + Math.Abs(height - int.Parse(textBoxHeight.Text)),
                    };
                    Triangle.p2 = new Point
                    {
                        X = Triangle.p2.X,
                        Y = Triangle.p2.Y + Math.Abs(height - int.Parse(textBoxHeight.Text)),
                    };
                }
            }
            else if(Triangle.p3.Y > Triangle.Location.Y)
            {
                if (height > int.Parse(textBoxHeight.Text))
                {
                    Triangle.Location = new Point
                    {
                        X = Triangle.Location.X,
                        Y = Triangle.Location.Y + (height - int.Parse(textBoxHeight.Text)),
                    };
                    Triangle.p2 = new Point
                    {
                        X = Triangle.p2.X,
                        Y = Triangle.p2.Y + (height - int.Parse(textBoxHeight.Text)),
                    };




                }
                else if (height < int.Parse(textBoxHeight.Text))
                {
                    Triangle.Location = new Point
                    {
                        X = Triangle.Location.X,
                        Y = Triangle.Location.Y - Math.Abs(height - int.Parse(textBoxHeight.Text)),
                    };
                    Triangle.p2 = new Point
                    {
                        X = Triangle.p2.X,
                        Y = Triangle.p2.Y - Math.Abs(height - int.Parse(textBoxHeight.Text)),
                    };
                }
            }



            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                
                buttonColor.BackColor = cd.Color;
                Triangle.ColorBorder = buttonColor.BackColor;
            }
        }

        Color zcolor(int r, int g, int b)
        {

            return Color.FromArgb(r, g, b);
        }

        public void DarkTheme()
        {
            List<Control> texboxes = new List<Control>();
            texboxes.Add(textBoxWidth);
            texboxes.Add(textBoxHeight); 


            this.BackColor = zcolor(48, 48, 51);
            this.ForeColor = zcolor(255, 183, 0);

            foreach(var item in texboxes)
            {
                item.ForeColor = zcolor(255, 183, 0);
                item.BackColor = zcolor(100, 100, 107);
            }
           
            buttonCancel.BackColor = zcolor(100, 100, 107);
            buttonOK.BackColor = zcolor(100, 100, 107);


        }

        
    }
}
