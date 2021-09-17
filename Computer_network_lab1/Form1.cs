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

        //0.810496 ответ
       
        private void MonteCarloForCurve()
        {
            long N = Convert.ToInt64(TrackBar.Value);

            Random rand = new Random();

            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Width);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.White);
            }

            int numHits = 0;
            for (int i = 0; i < N; i++)
            {
                // Создание рандомных точек
                double x = rand.NextDouble();
                double y = rand.NextDouble();

                //Проверка попала ли точка в площадь фигуры
                if (y >= 1 - (1.0 / (Math.Sqrt(2.0 * Math.Pow(x, 2) + 1.0)))) numHits++;

                // Отрисовка точек
                if (i < 100000)
                {
                    int ix = (int)(pictureBox1.Width * x);
                    int iy = (int)(pictureBox1.Width * y);
                    if (y <= 1 - (1.0 / (Math.Sqrt(2.0 * Math.Pow(x, 2) + 1.0))))
                        bm.SetPixel(ix, iy, Color.Gray);
                    else
                        bm.SetPixel(ix, iy, Color.Red);
                }
            }
            
            //Отрисовка кривой
            double xForCalc = 0;
            double yForGraph = 0;
            for (int xForGraph = 0; xForGraph < pictureBox1.Width; xForGraph++)
            {
                xForCalc = (double)xForGraph / (pictureBox1.Width-1);
                double y = (1.0 / (Math.Sqrt(2.0 * Math.Pow(xForCalc, 2) + 1.0)));

                yForGraph = (pictureBox1.Width-1) - (y * (pictureBox1.Width-1));

                bm.SetPixel(xForGraph, (int)yForGraph, Color.Black);
            }

            //Отображаем точки
            pictureBox1.Image = bm;

            // Вычисляем интеграл
             double output = (double) numHits / N;
             label1.Text = output.ToString();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            MonteCarloForCurve();
        }
    }
}
