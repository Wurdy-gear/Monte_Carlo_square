using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_network_lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double a = 1;
        double b = 1;

        int h = 0;
        private void CalculateAreaUnderCurve()
        {

        }

        // y = f(x) кривая

        //Сгенерировать случайную точку на промежутке от 0 до 1

        //Проверить попала ли точка в площадь фигуры y<=f(x), если да то h+1

        //Пройдя N итераций вычисляем h/N это и есть интеграл
        // Use Monte Carlo simulation to estimate pi.
        private void MonteCarloPi()
        {
            long N = Convert.ToInt64(TrackBar.Value);
            Random rand = new Random();

            // Make a bitmap to show points.
            int wid = pictureBox1.ClientSize.Width;
            int hgt = pictureBox1.ClientSize.Height;
            Bitmap bm = new Bitmap(wid, hgt);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.White);
                gr.DrawEllipse(Pens.Black, 0, 0, wid - 1, hgt - 1);
            }

            // Make the random points.
            int numHits = 0;
            for (int i = 0; i < N; i++)
            {
                // Make a random point 0 <= x < 1.
                double x = rand.NextDouble();
                double y = rand.NextDouble();

                // See how far the point is from (0.5, 0.5).
                double dx = x - 0.5;
                double dy = y - 0.5;
                if (dx * dx + dy * dy < 0.25) numHits++;

                // Plots up to 10,000 points.
                if (i < 10000)
                {
                    int ix = (int)(wid * x);
                    int iy = (int)(hgt * y);
                    if (dx * dx + dy * dy < 0.25)
                        bm.SetPixel(ix, iy, Color.Gray);
                    else
                        bm.SetPixel(ix, iy, Color.Black);
                }
            }

            // Display the plotted points.
            pictureBox1.Image = bm;


            // Get the hit fraction.
            double fraction = numHits / (double)N;

            // Estimate pi.
            double output = 4.0 * fraction;
            label1.Text = output.ToString();

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Thread ChecksumsThread = new Thread(new ThreadStart(MonteCarloPi));
            ChecksumsThread.Priority = ThreadPriority.Highest;
            ChecksumsThread.Start();
        }
    }
}
