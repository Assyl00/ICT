using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    enum Tool
    {
        Pen,
        Line,
        Rectangle,
        Ellipse,
        Fill,
        FillGdi,
        Eraser,
        FilledRect
    }
    public partial class Form1 : Form
    {
        Bitmap bitmap = default(Bitmap);
        Graphics graphics = default(Graphics);
        Pen pen = new Pen(Color.Black);
        Point prevPoint = default(Point);
        Point currentPoint = default(Point);
        bool isMousePressed = false;
        Tool currentTool = Tool.Pen;
        Pen eraser = new Pen(Color.Transparent);

        SolidBrush solidBrush = new SolidBrush(Color.Black);


        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            pictureBox1.Image = bitmap;
            graphics.Clear(Color.White);
            открытьToolStripMenuItem.Click += ОткрытьToolStripMenuItem_Click;
            сохранитьToolStripMenuItem.Click += СохранитьToolStripMenuItem_Click;
        }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap = Bitmap.FromFile(openFileDialog1.FileName) as Bitmap;
                pictureBox1.Image = bitmap;
                graphics = Graphics.FromImage(bitmap);
            }
        }

        private void СохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    bitmap.Save(saveFileDialog1.FileName);
            //}
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Pen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Line;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Rectangle;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Ellipse;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // установка цвета
            pen.Color = colorDialog1.Color;
            solidBrush.Color = colorDialog1.Color;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Fill;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            currentTool = Tool.FillGdi;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Eraser;
            eraser.Color = Color.White;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            currentTool = Tool.FilledRect;
        }
        Rectangle GetMyRectangle(Point prevPoint, Point curPoint)
        {

            return new Rectangle
            {
                X = Math.Min(prevPoint.X, curPoint.X),
                Y = Math.Min(prevPoint.Y, curPoint.Y),
                Width = Math.Abs(prevPoint.X - curPoint.X),
                Height = Math.Abs(prevPoint.Y - curPoint.Y)
            };
        }

        Rectangle GetMyEllipse(Point prevPoint, Point curPoint)
        {
            return new Rectangle {
                X = Math.Min(prevPoint.X, curPoint.X),
                Y = Math.Min(prevPoint.Y, curPoint.Y),
                Width = Math.Abs(prevPoint.X - curPoint.X),
                Height = Math.Abs(prevPoint.Y - curPoint.Y)
            };
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Location.ToString();
            if (isMousePressed)
            {
                switch (currentTool)
                {
                    case Tool.Line:
                    case Tool.Rectangle:
                    case Tool.Ellipse:
                    case Tool.FilledRect:
                        currentPoint = e.Location;
                        break;
                    case Tool.Pen:
                        prevPoint = currentPoint;
                        currentPoint = e.Location;
                        graphics.DrawLine(pen, prevPoint, currentPoint);
                        break;
                    case Tool.Eraser:
                        prevPoint = currentPoint;
                        currentPoint = e.Location;
                        graphics.DrawLine(eraser, prevPoint, currentPoint);
                        break;
                    default:
                        break;
                }

                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
            currentPoint = e.Location;
            isMousePressed = true;
            switch (currentTool)
            {
                case Tool.Pen:
                    break;
                case Tool.Line:
                    break;
                case Tool.Rectangle:
                    break;
                case Tool.Ellipse:
                    break;
                case Tool.Fill:
                    currentPoint = e.Location;
                    bitmap = Utils.Fill(bitmap, currentPoint, bitmap.GetPixel(e.X, e.Y), colorDialog1.Color);
                    graphics = Graphics.FromImage(bitmap);
                    pictureBox1.Image = bitmap;
                    pictureBox1.Refresh();
                    break;
                case Tool.FillGdi:
                    MapFill mf = new MapFill();
                    mf.Fill(graphics, currentPoint, colorDialog1.Color, ref bitmap);
                    graphics = Graphics.FromImage(bitmap);
                    pictureBox1.Image = bitmap;
                    pictureBox1.Refresh();
                    break;
                default:
                    break;
            }
        }
        

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;

            switch (currentTool)
            {
                case Tool.Line:
                    graphics.DrawLine(pen, prevPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    graphics.DrawRectangle(pen, GetMyRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Pen:
                    break;
                case Tool.Ellipse:
                    graphics.DrawEllipse(pen, GetMyEllipse(prevPoint, currentPoint));
                    break;
                case Tool.FilledRect:
                    graphics.FillRectangle(solidBrush, GetMyRectangle(prevPoint, currentPoint));
                    break;
                default:
                    break;
            }
            prevPoint = e.Location;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            switch (currentTool)
            {
                case Tool.Line:
                    e.Graphics.DrawLine(pen, prevPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    e.Graphics.DrawRectangle(pen, GetMyRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Pen:
                    break;
                case Tool.Ellipse:
                    e.Graphics.DrawEllipse(pen, GetMyEllipse(prevPoint, currentPoint));
                    break;
                case Tool.FilledRect:
                    e.Graphics.FillRectangle(solidBrush, GetMyRectangle(prevPoint, currentPoint));
                    break;
                default:
                    break;
            }
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = (float)numericUpDown1.Value;
            eraser.Width = (float)numericUpDown1.Value;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
