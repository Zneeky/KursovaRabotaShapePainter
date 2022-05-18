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


namespace KursovaRabotaStandartnoZadanieEtap1
{
    public partial class FormCircle : Form
    {
        private Circle _circle;

        public Circle Circle
        {
            get
            {
                return _circle;
            }
            set
            {
                _circle = value;
                textBoxRadius.Text = _circle.Radius.ToString();
                buttonColor.BackColor = _circle.ColorBorder;
            }
        }
        public FormCircle()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                Circle.Radius = int.Parse(textBoxRadius.Text);
                Circle.ColorBorder = buttonColor.BackColor;
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

            textBoxRadius.ForeColor = zcolor(255, 183, 0);
            textBoxRadius.BackColor = zcolor(100, 100, 107);
           
            buttonCancel.BackColor = zcolor(100, 100, 107);
            buttonOK.BackColor = zcolor(100, 100, 107);


        }
    }
}
