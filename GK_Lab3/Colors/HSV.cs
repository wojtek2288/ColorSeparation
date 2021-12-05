using GK_Lab3.DirBitmap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Lab3.Colors
{
    public class HSV : CalculateColors
    {
        public double H;
        public double S;
        public double V;

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
            RGBToHSV(Map_0_255_to_0_1(PixelColor.R), Map_0_255_to_0_1(PixelColor.G), Map_0_255_to_0_1(PixelColor.B));

            int c = (int)(this.H * (255.0 / 360.0));
            ResImg.SetPixel(x, y, Color.FromArgb(c, c, c));
        }
        public override void SecondComponent(int x, int y, Color PixelColor, DirectBitmap ResImg)
        {
            int c = (int)(this.S * 255.0);
            ResImg.SetPixel(x, y, Color.FromArgb(c, c, c));
        }
        public override void ThirdComponent(int x, int y, Color PixelColor, DirectBitmap ResImg)
        {
            int c = (int)(this.V * 255.0);
            ResImg.SetPixel(x, y, Color.FromArgb(c, c, c));
        }

        public void RGBToHSV(double R, double G, double B)
        {
            double Min = Math.Min(R, G);
            Min = Math.Min(Min, B);

            double Max = Math.Max(R, G);
            Max = Math.Max(Max, B);

            this.V = Max;

            double Delta = Max - Min;

            if(Delta < 0.00001)
            {
                this.S = 0;
                this.H = 0;
                return;
            }

            if(Max > 0)
            {
                this.S = Delta / Max;
            }
            else
            {
                this.S = 0;
                this.H = 0;
                return;
            }

            if (R >= Max)
                this.H = (G - B) / Delta;
            else if (G >= Max)
                this.H = 2 + (B - R) / Delta;
            else
                this.H = 4 + (R - G) / Delta;

            this.H = this.H * 60;

            if (this.H < 0)
                this.H += 360;

            return;
        }

        public Color HSVToRGB(double H, double S, double V)
        {
            double R, G, B, hh, p, q, t, ff;
            long i;

            hh = H;
            if (hh >= 360) hh = 0;
            hh /= 60;
            i = (long)hh;

            ff = hh - i;
            p = V * (1 - S);
            q = V * (1 - (S * ff));
            t = V * (1 - (S * (1 - ff)));

            switch (i)
            {
                case 0:
                    R = V;
                    G = t;
                    B = p;
                    break;
                case 1:
                    R = q;
                    G = V;
                    B = p;
                    break;
                case 2:
                    R = p;
                    G = V;
                    B = t;
                    break;
                case 3:
                    R = p;
                    G = q;
                    B = V;
                    break;
                case 4:
                    R = t;
                    G = p;
                    B = V;
                    break;
                case 5:
                default:
                    R = V;
                    G = p;
                    B = q;
                    break;
            }

            return Color.FromArgb(Map_0_1_to_0_255(R), Map_0_1_to_0_255(G), Map_0_1_to_0_255(B));
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
    }
}
