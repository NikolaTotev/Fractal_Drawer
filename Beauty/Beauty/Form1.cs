using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Beauty
{
    public partial class Form1 : Form
    {
        // Spiral variables
        private float amount;
        private float n = 0;
        private float c = 4;
        private float angle;
        private float r;
        private float size = 2;
        private float x;
        private float y;


        //Fractal Variables

        //Circle Variables
        private float posX;
        private float posY;
        private float radius = 800;

        // Squares 
        private float sO = 424;
        private int colGen;
        private int pictureNumber;
        private int sessionNumber;
        private int itterations = 1;
        private int prev = 0;
        private Random newRandom = new Random();
        private Color col1 = Color.MediumAquamarine;
        private Color col2 = Color.CornflowerBlue;
        private Color col3 = Color.DodgerBlue;
        private Color col4 = Color.LightBlue;
        private Color col5 = Color.DarkCyan;
        private Color col6 = Color.RoyalBlue;
        private Color col7 = Color.Orange;
        private Color col8 = Color.LightCoral;
        private Color col9 = Color.DarkOrange;
        private Color col10 = Color.OrangeRed;
        private Color[] colors;
        private Graphics g = null;
        private Bitmap bmp;
        private SolidBrush sBrush = new SolidBrush(Color.White);
        private Pen myPen = new Pen(Color.White);
        private string saveDir;

        public Form1()
        {
            InitializeComponent();

            g = panel1.CreateGraphics();

            posX = panel1.Width;
            posY = panel1.Height;

            bmp = new Bitmap(panel1.Width, panel1.Height);

            // col1 = Color.FromArgb(newRandom.Next(0, 255), newRandom.Next(0, 255), newRandom.Next(0, 255));
            // col2 = Color.FromArgb(newRandom.Next(0, 255), newRandom.Next(0, 255), newRandom.Next(0, 255));
            colors = new Color[] { col1, col2, col3, col4, col5, col6, col7, col8, col9, col10 };
            pictureNumber = Properties.Settings.Default.pictureNumber;
            sessionNumber = Properties.Settings.Default.sessionNumber;



        }

        private void drawPhl_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(panel1.Width, panel1.Height);

            //col1 = Color.FromArgb(newRandom.Next(0, 255), newRandom.Next(0, 255), newRandom.Next(0, 255));
            // col2 = Color.FromArgb(newRandom.Next(0, 255), newRandom.Next(0, 255), newRandom.Next(0, 255));
            colors = new Color[] { col1, col2 };

            panel1.Refresh();
            g = panel1.CreateGraphics();
            r = 0;
            x = 0;
            y = 0;
            angle = 0;
            n = 0;
            size = 3;

            for (int i = 0; i < 15000; i++)
            {
                colGen = newRandom.Next(0, 1000);

                if (colGen % 2 == 0)
                {
                    sBrush.Color = colors[0];
                }

                if (colGen % 2 != 0)
                {
                    sBrush.Color = colors[1];
                }

                // Color c1 = Color.FromArgb(newRandom.Next(0, 255), newRandom.Next(0, 255), newRandom.Next(0, 255));
                //sBrush.Color = c1;

                angle = n * 137.3f * .017453292519f;
                r = (float)(c * Math.Sqrt(n));
                x = (float)(r * Math.Cos(angle) + panel1.Width / 2);
                y = (float)(r * Math.Sin(angle) + panel1.Height / 2);


                g.FillEllipse(sBrush, x, y, size, size);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                size += 0.0005f;
                n++;
            }

            g = Graphics.FromImage(bmp);
            Rectangle rect = panel1.RectangleToScreen(panel1.ClientRectangle);
            g.CopyFromScreen(rect.Location, Point.Empty, panel1.Size);
            string fileName = saveDir + string.Format("\\Fractal{0}.png", pictureNumber.ToString());
            bmp.Save(fileName);
            pictureNumber++;
            Properties.Settings.Default.pictureNumber = this.pictureNumber;
            Properties.Settings.Default.Save();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            itterations = 1;
            SetDirectory();
            //col1 = Color.FromArgb(newRandom.Next(0, 255), newRandom.Next(0, 255), newRandom.Next(0, 255));
            // col2 = Color.FromArgb(newRandom.Next(0, 255), newRandom.Next(0, 255), newRandom.Next(0, 255));

            //colors = new Color[] { col1, col2 };


            panel1.Refresh();
            g = panel1.CreateGraphics();
            posX = panel1.Width;
            posY = panel1.Height;
            DrawCircle(posX / 2, posY / 2, radius);
        }

        /// <summary>
        /// Draws a circle fractal based on given parameters == Reccursive function ==
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="d"></param>

        #region fractals
        private void DrawCircle(float pX, float pY, float d)
        {
            colGen = newRandom.Next(0, 1000);

       
            myPen.Color = colors[newRandom.Next(0, colors.Length)];
            sBrush.Color = colors[newRandom.Next(0, colors.Length)];


            g.DrawEllipse(myPen, pX - d / 2, pY - d / 2, d, d);
           //g.FillEllipse(sBrush, pX - d / 2, pY - d / 2, d, d);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (d > 1)
            {
                DrawCircle(pX + d * 0.5f, pY, d * 0.5f);
                DrawCircle(pX - d * 0.5f, pY, d * 0.5f);
                // DrawCircle(pX, pY + d * 0.5f, d * 0.5f);
                // DrawCircle(pX , pY - d * 0.5f, d * 0.5f);

            }
            itterations++;
            if (itterations == 2048)
            {

                g = Graphics.FromImage(bmp);
                Rectangle rect = panel1.RectangleToScreen(panel1.ClientRectangle);
                g.CopyFromScreen(rect.Location, Point.Empty, panel1.Size);
                string fileName = saveDir + string.Format("\\Fractal{0}.png", pictureNumber.ToString());
                bmp.Save(fileName);
                pictureNumber++;
                Properties.Settings.Default.pictureNumber = this.pictureNumber;
                Properties.Settings.Default.Save();
            }


        }

        private void Squares(float pX1, float pY2, float s)
        {
            if (itterations != 1)
            {
                myPen.Color = colors[newRandom.Next(0, colors.Length)];
                sBrush.Color = colors[newRandom.Next(0, colors.Length)];
            }
            else
            {
                myPen.Color = Color.Azure;
                sBrush.Color = Color.Azure;
            }

            


            //g.DrawRectangle(myPen, pX1 - s / 2, pY2 - s / 2, s, s);
            g.FillEllipse(sBrush, pX1 - s / 2, pY2 - s / 2, s, s);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (s > 1)
            {

                Squares((float)(pX1 + s * 0.5f * Math.Cos(60)), (float)(pY2 * Math.Sin(30)), s * 0.5f);
                Squares((float)(pX1 - s * 0.5f * Math.Sin(30)), (float)(pY2 * Math.Sin(30)), s * 0.5f);
                Squares(pX1, pY2 + s * 0.5f, s * 0.5f);
                //Squares(pX1, pY2 - s * 0.5f, s * 0.5f);
            }
            itterations++;

            if (itterations > prev)
            {
                prev = itterations;

            }
            else if (prev == itterations)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            itterations = 1;
            SetDirectory();
            //col1 = Color.FromArgb(newRandom.Next(0, 255), newRandom.Next(0, 255), newRandom.Next(0, 255));
            //col2 = Color.FromArgb(newRandom.Next(0, 255), newRandom.Next(0, 255), newRandom.Next(0, 255));

            //colors = new Color[] { col1, col2 };

            panel1.Refresh();
            g = panel1.CreateGraphics();
            Squares(panel1.Width / 2, panel1.Height / 2, sO);
        }

        #endregion
        private void button3_Click(object sender, EventArgs e)
        {
            SetDirectory();

            for (int i = 0; i < 2; i++)
            {
                drawPhl_Click(sender, e);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string test = saveFileDialog1.FileName;
            textBox1.Text = test;
        }

        private void SetDirectory()
        {
            saveDir = @"D:\Desktop\Fractals\Session" + sessionNumber.ToString();
            Directory.CreateDirectory(saveDir);
            sessionNumber++;
            Properties.Settings.Default.sessionNumber = this.sessionNumber;
            Properties.Settings.Default.Save();
            bmp = new Bitmap(panel1.Width, panel1.Height);
        }
    }
}
