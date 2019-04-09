using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV;


namespace DETEKSI_WAJAH
{
    public partial class Form1 : Form
    {
        Capture capture;
        bool captureprogress;
        HaarCascade Haar;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Capture == null)
            {
                try
                {
                    capture = new Capture();
                }
                catch (NullReferenceException exc)
                {
                    MessageBox.Show("camera tidak aktif");
                }

            }
            if (Capture != null)
            {
                if (captureprogress)
                {
                    Application.Idle -= prosesframe;

                }
                captureprogress = !captureprogress;
            }
        }
        private void prosesframe(object sender, EventArgs argh)
        {
            Image<Bgr, Byte> imageframe = capture.QueryFrame();
            if (imageframe != null)
            {
                Image<Gray, Byte> grayframe = imageframe.Convert<Gray, Byte>();
                var face = grayframe.DetectHaarCascade(Haar, 1.4, 4, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20))[0];
                foreach (var faces in face)
                {
                    imageframe.Draw(faces.rect, new Bgr(Color.Red), 3);
                }
            }
            imageBox1.Image = imageframe;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Haar = new HaarCascade("haarcascade_frontalface_default.xml");
        }
    }
}

