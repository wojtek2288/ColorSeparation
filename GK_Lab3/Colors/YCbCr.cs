using GK_Lab3.DirBitmap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Lab3.Colors
{
    public class YCbCr : CalculateColors
    {
        public double Y;
        public double Cb;
        public double Cr;
        public override void IterateBitmap(DirectBitmap Img, DirectBitmap[] ResImg)
        {
            for(int i = 0; i < Img.Width; i++)
            {
                for(int j = 0; j < Img.Height; j++)
                {
                    Color PixelColor = Img.GetPixel(i, j);
                    FirstComponent(i, j, PixelColor, ResImg[0]);
                    SecondComponent(i, j, PixelColor, ResImg[1]);
                    ThirdComponent(i, j, PixelColor, ResImg[2]);
                }
            }
        }
        public override void FirstComponent(int x, int y, Color PixelColor, DirectBitmap ResImg)
        {
            Y = 0.299 * PixelColor.R + 0.587 * PixelColor.G + 0.114 * PixelColor.B;

            int c = (int)(Y);
            ResImg.SetPixel(x, y, Color.FromArgb(c, c, c));
        }
        public override void SecondComponent(int x, int y, Color PixelColor, DirectBitmap ResImg)
        {
            Cb = 128 - 0.169 * PixelColor.R - 0.331 * PixelColor.G + 0.5 * PixelColor.B;

            ResImg.SetPixel(x, y, Color.FromArgb(127, 255 - (int)Cb, (int)Cb));
        }
        public override void ThirdComponent(int x, int y, Color PixelColor, DirectBitmap ResImg)
        {
            Cr = 128 + 0.5*PixelColor.R - 0.419*PixelColor.G - 0.081*PixelColor.B;

            ResImg.SetPixel(x, y, Color.FromArgb((int)Cr, 255 - (int)Cr, 127));
        }

        public Color YCbCrConverter(int y, int cb, int cr)
        {
            double Y = (double)y;
            double Cb = (double)cb;
            double Cr = (double)cr;

            int r = (int)(Y + 1.40200 * (Cr - 128));
            int g = (int)(Y - 0.34414 * (Cb - 128) - 0.71414 * (Cr - 128));
            int b = (int)(Y + 1.77200 * (Cb - 128));

            (r, g, b) = MapTo_0_255(r, g, b);

            return Color.FromArgb(r, g, b);
        }

        public static double Map_0_255_to_0_1(int RGB)
        {
            double ret = (double)RGB / 255;
            if (ret > 1) return 1;
            if (ret < 0) return 0;
            return ret;
        }

        public static int Map_0_1_to_0_255(double val)
        {
            int ret = (int)(val * 255);
            if (ret > 255) return 255;
            if (ret < 0) return 0;
            return ret;
        }

        public (int R, int G, int B) MapTo_0_255(int Rval, int Gval, int Bval)
        {
            int R = Math.Max(0, Rval);
            int G = Math.Max(0, Gval);
            int B = Math.Max(0, Bval);

            if (R > 255)
                R = 255;
            if (G > 255)
                G = 255;
            if (B > 255)
                B = 255;

            return (R, G, B);
        }
    }
}
