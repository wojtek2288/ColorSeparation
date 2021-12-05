using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GK_Lab3.DirBitmap;

namespace GK_Lab3.Colors
{
    public abstract class CalculateColors
    {
        public abstract void IterateBitmap(DirectBitmap Img, DirectBitmap[] ResImg);
        public abstract void FirstComponent(int x, int y, Color PixelColor, DirectBitmap ResImg);
        public abstract void SecondComponent(int x, int y, Color PixelColor, DirectBitmap ResImg);
        public abstract void ThirdComponent(int x, int y, Color PixelColor, DirectBitmap ResImg);
    }
}
