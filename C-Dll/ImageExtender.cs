using System;

namespace Siema
{
    public class ImageExtender
    {
        public static void Extend(ref Byte[,,] srcByteArray, ref Byte[,,] dstByteArray, int width, float ratio, int currentIndex, int partForThread)
        {

            int newXParam;
            int newYParam;

            for (int x = 0; x < width; x++)
            {
                for (int y = currentIndex; y < currentIndex + partForThread; y++)
                {
                    newXParam = (int) (x * ratio);
                    newYParam = (int) (y * ratio);
                    
                    dstByteArray[x, y, 0] = srcByteArray[newXParam, newYParam, 0];
                    dstByteArray[x, y, 1] = srcByteArray[newXParam, newYParam, 1];
                    dstByteArray[x, y, 2] = srcByteArray[newXParam, newYParam, 2];
                    dstByteArray[x, y, 3] = srcByteArray[newXParam, newYParam, 3];
                }
            }
        }
    }
}
