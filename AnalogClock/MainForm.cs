using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AnalogClock
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Resize(
            object sender, EventArgs e)
        {
            Invalidate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void MainForm_Paint(
            object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int faceRadius = Math.Min(
                this.ClientSize.Height / 2 - 10,
                this.ClientSize.Width / 2 - 10);
            DrawClockFace(g, faceRadius);
            DrawHands(g, faceRadius);
        }

        private void DrawClockFace(
            Graphics g, int faceRadius)
        {
            int r1 = faceRadius;
            int r2 = (int)(0.95 * faceRadius);
            double thetaInterval = DegreeToRadian(6);
            Pen pen = new Pen(Color.Red, 3);
            DrawIndicators(g, pen, r1, r2, thetaInterval);

            r1 = faceRadius;
            r2 = (int)(0.85 * faceRadius);
            thetaInterval = DegreeToRadian(30);
            pen = new Pen(Color.Blue, 5);
            DrawIndicators(g, pen, r1, r2, thetaInterval);

            int d = 2 * faceRadius;
            int x = ClientSize.Width / 2 - faceRadius;
            int y = ClientSize.Height / 2 - faceRadius;
            pen = new Pen(Color.Blue, 5);
            g.DrawEllipse(pen, x, y, d, d);
        }

        private double DegreeToRadian(double degrees)
        {
            return degrees * Math.PI / 180; 
        }

        private Point Center()
        {
            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;
            return new Point(x, y);
        }

        private Point PolarToCartesian(
            int r, double theta)
        {
            Point center = Center();
            int x = (int)(center.X + r * Math.Cos(theta));
            int y = (int)(center.Y - r * Math.Sin(theta));
            return new Point(x, y);
        }

        private void DrawIndicator(
            Graphics g, Pen pen,
            int r1, int r2, double theta)
        {
            Point p1 = PolarToCartesian(r1, theta);
            Point p2 = PolarToCartesian(r2, theta);
            g.DrawLine(pen, p1, p2);
        }

        private void DrawIndicators(
            Graphics g, Pen pen,
            int r1, int r2, 
            double thetaInterval)
        {
            for(double theta = 0; 
                theta < 2 * Math.PI;
                theta += thetaInterval
                )
            {
                DrawIndicator(g, pen, r1, r2, theta);
            }
        }

        private void DrawHand(
            Graphics g, Pen pen, 
            int r, double theta)
        {
            Point p1 = Center();
            Point p2 = PolarToCartesian(r, theta);
            g.DrawLine(pen, p1, p2);
        }

        private void DrawHands(
            Graphics g, int faceRadius)
        {
            int h = DateTime.Now.Hour;
            int m = DateTime.Now.Minute;
            int s = DateTime.Now.Second;

            int r = (int)(0.8 * faceRadius);
            double theta = 90 - 6 * s;
            theta = DegreeToRadian(theta);
            Pen pen = new Pen(Color.Red, 2);
            DrawHand(g, pen, r, theta);

            r = (int)(0.7 * faceRadius);
            theta = 90 - 6 * m - 0.1 * s;
            theta = DegreeToRadian(theta);
            pen = new Pen(Color.Blue, 3.5f);
            DrawHand(g, pen, r, theta);

            r = (int)(0.6 * faceRadius);
            theta = 90 - 30 * h - 0.5 * m - 1 / 120 * s;
            theta = DegreeToRadian(theta);
            pen = new Pen(Color.DarkBlue, 5);
            DrawHand(g, pen, r, theta);
        }

    }
}
