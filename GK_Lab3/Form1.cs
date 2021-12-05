using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GK_Lab3.DirBitmap;
using GK_Lab3.Colors;

namespace GK_Lab3
{
    public partial class Form1 : Form
    {
        public DirectBitmap ImgBitmap;
        public DirectBitmap Bitmap1;
        public DirectBitmap Bitmap2;
        public DirectBitmap Bitmap3;
        DirectBitmap[] Bitmaps;
        YCbCr YCbCrColorProfile;
        HSV HSVColorProfile;
        Lab LabColorProfile;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            ImgBitmap = new DirectBitmap(ImagePictureBox.Width, ImagePictureBox.Height);
            Bitmap1 = new DirectBitmap(ImagePictureBox.Width, ImagePictureBox.Height);
            Bitmap2 = new DirectBitmap(ImagePictureBox.Width, ImagePictureBox.Height);
            Bitmap3 = new DirectBitmap(ImagePictureBox.Width, ImagePictureBox.Height);

            using (var bmt = new Bitmap(System.IO.Path.Combine(Application.StartupPath, "..\\..\\..\\Img\\tatry.jpg")))
            {
                for (int i = 0; i < ImgBitmap.Width; i++)
                {
                    for (int j = 0; j < ImgBitmap.Height; j++)
                    {
                        ImgBitmap.SetPixel(i, j, bmt.GetPixel(i % bmt.Width, j % bmt.Height));
                    }
                }
            }

            YCbCrColorProfile = new YCbCr();
            HSVColorProfile = new HSV();
            LabColorProfile = new Lab();

            Bitmaps = new DirectBitmap[3];

            Bitmaps[0] = Bitmap1;
            Bitmaps[1] = Bitmap2;
            Bitmaps[2] = Bitmap3;

            YCbCrColorProfile.IterateBitmap(ImgBitmap, Bitmaps);

            ImagePictureBox.Image = ImgBitmap.Bitmap;

            pictureBox2.Image = Bitmap1.Bitmap;
            pictureBox3.Image = Bitmap2.Bitmap;
            pictureBox4.Image = Bitmap3.Bitmap;
        }

        private void ChangePicture(string FileName)
        {
            using (var bmt = new Bitmap(System.IO.Path.Combine(Application.StartupPath, "..\\..\\..\\Img\\" + FileName)))
            {
                for (int i = 0; i < ImgBitmap.Width; i++)
                {
                    for (int j = 0; j < ImgBitmap.Height; j++)
                    {
                        ImgBitmap.SetPixel(i, j, bmt.GetPixel(i % bmt.Width, j % bmt.Height));
                    }
                }
            }
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                ChangePicture("tatry.jpg");
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                ChangePicture("donut.jpeg");
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                ChangePicture("mountain.png");
            }

            if(radioButton1.Checked)
            {
                YCbCrColorProfile.IterateBitmap(ImgBitmap, Bitmaps);
            }
            else if(radioButton2.Checked)
            {
                HSVColorProfile.IterateBitmap(ImgBitmap, Bitmaps);
            }
            else if(radioButton3.Checked)
            {
                LabColorProfile.IterateBitmap(ImgBitmap, Bitmaps);
            }

            ImagePictureBox.Image = ImgBitmap.Bitmap;

            pictureBox2.Image = Bitmap1.Bitmap;
            pictureBox3.Image = Bitmap2.Bitmap;
            pictureBox4.Image = Bitmap3.Bitmap;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                label2.Text = "Y";
                label3.Text = "Cb";
                label4.Text = "Cr";

                YCbCrColorProfile.IterateBitmap(ImgBitmap, Bitmaps);
                pictureBox2.Image = Bitmap1.Bitmap;
                pictureBox3.Image = Bitmap2.Bitmap;
                pictureBox4.Image = Bitmap3.Bitmap;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            {
                label2.Text = "H";
                label3.Text = "S";
                label4.Text = "V";

                HSVColorProfile.IterateBitmap(ImgBitmap, Bitmaps);
                pictureBox2.Image = Bitmap1.Bitmap;
                pictureBox3.Image = Bitmap2.Bitmap;
                pictureBox4.Image = Bitmap3.Bitmap;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked)
            {
                label2.Text = "L";
                label3.Text = "a";
                label4.Text = "b";

                LabColorProfile.IterateBitmap(ImgBitmap, Bitmaps);
                pictureBox2.Image = Bitmap1.Bitmap;
                pictureBox3.Image = Bitmap2.Bitmap;
                pictureBox4.Image = Bitmap3.Bitmap;
            }
        }
    }
}
