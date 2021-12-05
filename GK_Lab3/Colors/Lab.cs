using GK_Lab3.DirBitmap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Lab3.Colors
{
    public class Lab : CalculateColors
    {
        public double X;
        public double Y;
        public double Z;

        public double L;
        public double a;
        public double b;

        public override void IterateBitmap(DirectBitmap Img, DirectBitmap[] ResImg)
        {
            for (int i = 0; i < Img.Width; i++)
            {
                for (int j = 0; j < Img.Height; j++)
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
            RGBToXYZ(PixelColor.R, PixelColor.G, PixelColor.B);
            XYZToLab(this.X, this.Y, this.Z);

            int c = (int)(this.L * 255.0 / 100.0);
            ResImg.SetPixel(x, y, Color.FromArgb(c, c, c));
        }
        public override void SecondComponent(int x, int y, Color PixelColor, DirectBitmap ResImg)
        {
            int c = (int)(this.a + 128);
            ResImg.SetPixel(x, y, Color.FromArgb(c, 255 - c, 127));
        }
        public override void ThirdComponent(int x, int y, Color PixelColor, DirectBitmap ResImg)
        {
            int c = (int)(this.b + 128);
            ResImg.SetPixel(x, y, Color.FromArgb(c, 127, 255 - c));
        }

        public void RGBToXYZ(int R, int G, int B)
        {
            double var_R = R / 255.0;
            double var_G = G / 255.0;
            double var_B = B / 255.0;

            if (var_R > 0.04045)
                var_R = Math.Pow((var_R + 0.055) / 1.055, 2.4);
            else
                var_R = var_R / 12.92;

            if (var_G > 0.04045)
                var_G = Math.Pow((var_G + 0.055) / 1.055, 2.4);
            else
                var_G = var_G / 12.92;

            if (var_B > 0.04045)
                var_B = Math.Pow((var_B + 0.055) / 1.055, 2.4);
            else
                var_B = var_B / 12.92;

            var_R = var_R * 100;
            var_G = var_G * 100;
            var_B = var_B * 100;

            this.X = var_R * 0.4124 + var_G * 0.3576 + var_B * 0.1805;
            this.Y = var_R * 0.2126 + var_G * 0.7152 + var_B * 0.0722;
            this.Z = var_R * 0.0193 + var_G * 0.1192 + var_B * 0.9505;

            return;
        }

        public void XYZToLab(double X, double Y, double Z)
        {
            double XR = 94.81;
            double YR = 100.0;
            double ZR = 107.3;

            if (Y / YR > 0.008856)
                this.L = 116 * Math.Cbrt(Y / YR) - 16;
            else
                this.L = 903.3 * (Y / YR);

            this.a = 500 * (Math.Cbrt(X / XR) - Math.Cbrt(Y / YR));
            this.b = 200 * (Math.Cbrt(Y / YR) - Math.Cbrt(Z / ZR));

            return;
        }
    }
}
