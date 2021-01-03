using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace JA_Project.Helpers
{
    unsafe public static class BitmapHelpers
    {
        public static BitmapImage Bitmap2BitmapImage(this Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        unsafe public static Byte[,,] Bitmap2ByteArray (this Bitmap bitmap)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;

            var byteArray = new Byte[width, height, 4];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    byteArray[i, j, 0] = bitmap.GetPixel(i, j).A;
                    byteArray[i, j, 1] = bitmap.GetPixel(i, j).R;
                    byteArray[i, j, 2] = bitmap.GetPixel(i, j).G;
                    byteArray[i, j, 3] = bitmap.GetPixel(i, j).B;
                }
            }

            return byteArray;
        }

        unsafe public static Bitmap ByteArray2Bitmap (this Byte[,,] byteArray)
        {
            var width = byteArray.GetLength(0);
            var height = byteArray.GetLength(1);

            var bitmap = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    var a = byteArray[i, j, 0];
                    var r = byteArray[i, j, 1];
                    var g = byteArray[i, j, 2];
                    var b = byteArray[i, j, 3];

                    var color = Color.FromArgb(a, r, g, b);

                    bitmap.SetPixel(i, j, color);
                }
            }

            return bitmap;

        }
    }
}
