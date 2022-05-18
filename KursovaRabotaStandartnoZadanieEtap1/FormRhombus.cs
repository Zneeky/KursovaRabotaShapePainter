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
    public partial class FormRhombus : Form
    {
        private Rhombus _rhombus;

        public Rhombus Rhombus
        {
            get
            {
                return _rhombus;
            }
            set
            {
                _rhombus = value;

                textBoxWidth.Text = _rhombus.Width.ToString();
                textBoxHeight.Text = _rhombus.Height.ToString();

                buttonColor.BackColor = _rhombus.ColorBorder;
            }
        }
        public FormRhombus()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                Rhombus.Width = int.Parse(textBoxWidth.Text);
                Rhombus.Height = int.Parse(textBoxHeight.Text);
                Rhombus.ColorBorder = buttonColor.BackColor;
            }
            catch
            {
                MessageBox.Show("Invalid value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
            }
        }
        Color zcolor(int r, int g, int b)
        {

            return Color.FromArgb(r, g, b);
        }

        public void DarkTheme()
        {
            this.BackColor = zcolor(48, 48, 51);
            this.ForeColor = zcolor(255, 183, 0);

            textBoxHeight.ForeColor = zcolor(255, 183, 0);
            textBoxHeight.BackColor = zcolor(100, 100, 107);

            textBoxWidth.ForeColor = zcolor(255, 183, 0);
            textBoxWidth.BackColor = zcolor(100, 100, 107);

            buttonCancel.BackColor = zcolor(100, 100, 107);
            buttonOK.BackColor = zcolor(100, 100, 107);


        }
    }
}
