using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Siema
{
    public class ImageExtender
    {
        public static Bitmap Extend(string filePath, float scalar)
        {
            Bitmap src = new Bitmap(filePath);
            Bitmap dst = new Bitmap((int)(src.Width * scalar), (int)(src.Height * scalar));

            double ratio = 1 / scalar;

            for (int x = 0; x < dst.Width; x++)
            {
                for (int y = 0; y < dst.Height; y++)
                {
                    Color color = src.GetPixel((int)(x * ratio), (int)(y * ratio));
                    dst.SetPixel(x, y, color);
                }
            }

            return dst;
        }
    }
}
