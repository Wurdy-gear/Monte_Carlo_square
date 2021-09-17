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

        private void CalculateAreaUnderCurve()
        {

        }

        // Метод Монте Карло для расчёта PI

        //Квадрат имеет площадь 1, а круг должен иметь площадь PI * R^2 радиус нам дан и равен 0,5 решить данное уравнение относительно PI
        private void MonteCarloPi()
        {
            long N = Convert.ToInt64(TrackBar.Value);
            Random rand = new Random();

            // Создание битмапа для отрисовки
            int wid = pictureBox1.ClientSize.Width;
            int hgt = pictureBox1.ClientSize.Height;
            Bitmap bm = new Bitmap(wid, hgt);

            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.White);
                gr.DrawEllipse(Pens.Black, 0, 0, wid - 1, hgt - 1);
            }

            int numHits = 0;
            for (int i = 0; i < N; i++)
            {
                // Создаём случайную точку от 0 до 1
                double x = rand.NextDouble();
                double y = rand.NextDouble();

                // Проверка на попадание в круг
                double dx = x - 0.5;
                double dy = y - 0.5;
                if (dx * dx + dy * dy < 0.25) numHits++;

                // отрисовка точек
                if (i < 1000000)
                {
                    int ix = (int)(wid * x);
                    int iy = (int)(hgt * y);
                    if (dx * dx + dy * dy < 0.25)
                        bm.SetPixel(ix, iy, Color.Gray);
                    else
                        bm.SetPixel(ix, iy, Color.Red);
                }
            }

            // Отображение точек
            pictureBox1.Image = bm;


            // Доля попаданий
            double fraction = numHits / (double)N;

            // Результат
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
