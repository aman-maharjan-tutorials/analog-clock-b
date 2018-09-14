using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalogClock
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Paint(
            object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int r = Math.Min(
                this.ClientSize.Height / 2 - 10,
                this.ClientSize.Width / 2 - 10);
            int d = 2 * r;
            int x = ClientSize.Width / 2 - r;
            int y = ClientSize.Height / 2 - r;
            g.DrawEllipse(Pens.Black, x, y, d, d);
        }

        private void MainForm_Resize(
            object sender, EventArgs e)
        {
            Invalidate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
