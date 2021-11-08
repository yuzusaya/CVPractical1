using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using Image = System.Drawing.Image;

namespace Practical1
{
    public partial class Form1 : Form
    {
        private Bitmap _image;
        private Bitmap _grayImage;
        private int threshold = 128;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                _image = (Bitmap)Image.FromFile(Openfile.FileName);
                pictureBox1.Image = _image;
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Grayscale grayFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            _grayImage = grayFilter.Apply(_image);
            pictureBox2.Image = _grayImage;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                try
                {
                    Color color = _grayImage.GetPixel(e.X, e.Y);
                    CoordinateLabel.Text = $"({e.X},{e.Y}) : {color.ToString()}";
                }
                catch (ArgumentOutOfRangeException arg)
                {
                    //ignore the error
                }
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (_grayImage == null)
                return;

            if (int.TryParse(lblThreshold.Text, out threshold))
            {
                if(threshold < 0 || threshold > 255)
                {
                    return;
                }
                Threshold thresholdValue = new Threshold(threshold);
                Bitmap binaryImage = thresholdValue.Apply(_grayImage);
                pictureBox3.Image = binaryImage;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (_grayImage == null)
                return;
            HistogramEqualization histEq = new HistogramEqualization();
            Bitmap histEqImage = histEq.Apply(_grayImage);
            pictureBox4.Image = histEqImage;
        }
    }
}
