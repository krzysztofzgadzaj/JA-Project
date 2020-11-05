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
        public static extern unsafe void filterProc(Byte* srcPtr, Byte* dstPtr, float ratio, int dstWidth, int numberOfLines, int indexOfLine, int srcWidth);
        //public static extern unsafe void filterProc(Byte* srcPtr, Byte* dstPtr, float ratio, int size);

        unsafe public static long Extend(ref Bitmap dst, string originalFilePath, float scale, bool ifAsm, bool ifCsharp, int threadsNumber)
        {
            var src = new Bitmap(originalFilePath);            
            var ratio = (float)(1 / scale);

            var srcByteArray = src.Bitmap2ByteArray();
            var dstByteArray = dst.Bitmap2ByteArray();

            var partForThread = dst.Height / threadsNumber;
            var index = 0;

            if (ifCsharp == true)
            {
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

                        var watch = System.Diagnostics.Stopwatch.StartNew();

                        int size = dstByteArray.GetLength(0) * dstByteArray.GetLength(1);

                        filterProc(srcPtr, dstPtr, ratio, dstByteArray.GetLength(0), dstByteArray.GetLength(1), 0, srcByteArray.GetLength(0));

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
