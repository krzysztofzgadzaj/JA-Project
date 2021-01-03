using JA_Project.Helpers;
using Siema;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JA_Project
{
    public class ExtenderHelpers
    {

        [DllImport(@"..\..\..\x64\Debug\AsmDll2.dll")]
        public static extern unsafe void ImageExtenderAsm(Byte* srcPtr, Byte* dstPtr, float ratio, float nvm, int srcHeight, int dstHeight, int indexOfLine, int numberOfLines);

        unsafe public static long Extend(ref Bitmap dst, string originalFilePath, float scale, bool ifAsm, bool ifCsharp, int threadsNumber)
        {
            var src = new Bitmap(originalFilePath);            
            var ratio = (float)(1 / scale);

            var srcByteArray = src.Bitmap2ByteArray();
            var dstByteArray = dst.Bitmap2ByteArray();

            if (ifCsharp == true)
            {
                var partForThread = dst.Height / threadsNumber;
                var index = 0;

                var watch = System.Diagnostics.Stopwatch.StartNew();

                Parallel.For(0, threadsNumber, i =>
                {
                    index = i * partForThread;
                    ImageExtender.Extend(ref srcByteArray, ref dstByteArray, dstByteArray.GetLength(0), ratio, index, partForThread);

                });

                watch.Stop();
                
                dst = dstByteArray.ByteArray2Bitmap();

                return watch.ElapsedMilliseconds;
            }
            else if ( ifAsm == true )
            {
                fixed(Byte* srcPtr = &srcByteArray[0,0,0])
                {
                    fixed(Byte* dstPtr = &dstByteArray[0, 0, 0])
                    {
                        Byte* p1 = srcPtr;
                        Byte* p2 = dstPtr;

                        var partForThread = dst.Width / threadsNumber;
                        var index = 0;

                        var watch = System.Diagnostics.Stopwatch.StartNew();

                        Parallel.For(0, threadsNumber, i =>
                        {
                            index = i * partForThread;
                            ImageExtenderAsm(p1, p2, ratio, 0, srcByteArray.GetLength(1), dstByteArray.GetLength(1), index, partForThread);

                        });

                        watch.Stop();

                        dst = dstByteArray.ByteArray2Bitmap();

                        return watch.ElapsedMilliseconds;

                    }
                }   
            }
            else
            {
                return 0;
            }
        }
    }
}
