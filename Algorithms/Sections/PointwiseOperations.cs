using Emgu.CV.Structure;
using Emgu.CV;
using System.Diagnostics.Contracts;
using System;

namespace Algorithms.Sections
{
    public class PointwiseOperations
    {

        #region Color contrast stretching

        public static Image<Hsv, byte> ColorContrastStretching(Image<Bgr, byte> inputImage)
        {
            //convertire imagine din Bgr in Hsv
            Image<Hsv, byte> hsvImage = new Image<Hsv, byte>(inputImage.Width, inputImage.Height);
            CvInvoke.CvtColor(inputImage, hsvImage, Emgu.CV.CvEnum.ColorConversion.Bgr2Hsv);
            hsvImage.Data = inputImage.Data;

            //calculare interval Lmin si Lmax ptr. componenta V
            byte minV = byte.MaxValue;
            byte maxV = byte.MinValue;

            for(int y = 0; y<hsvImage.Height;  y++)
            {
                for(int x = 0; x<hsvImage.Width; x++)
                {
                    byte componentaV = hsvImage.Data[y, x, 2];

                    if (componentaV < minV)
                    {
                        minV = componentaV;
                    }

                    if (componentaV >maxV)
                    {
                        maxV = componentaV;
                    }
                }
            }

            //mapare interval Lmin si Lmax ptr componenta V

            double a = 255.0 / (maxV - minV);
            double b = -a * minV;

            Image<Hsv, byte> hsvImageDublura = new Image<Hsv, byte>(inputImage.Width, inputImage.Height);
            hsvImageDublura = hsvImage;

            for (int y=0; y<hsvImage.Height;y++)
            {
                for(int x=0; x<hsvImage.Width; x++)
                {
                    double v = hsvImage.Data[y, x, 2];

                    double transformatV = a * v + b;

                    byte transformatVByte = Convert.ToByte(transformatV);

                    hsvImageDublura.Data[y, x, 2] = transformatVByte;
                }
            }

            return hsvImageDublura;
        }
        #endregion

    }
}