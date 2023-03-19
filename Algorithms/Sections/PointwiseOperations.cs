using Emgu.CV.Structure;
using Emgu.CV;
using System.Diagnostics.Contracts;

namespace Algorithms.Sections
{
    public class PointwiseOperations
    {

        #region Color contrast stretching

        public static Image<Hsv, byte> ColorContrastStretchingImg(Image<Bgr, byte> inputImage)
        {
            // Convertim imaginea RGB la HSV
            Image<Hsv, byte> hsvImage = inputImage.Convert<Hsv, byte>();

            // Returnăm imaginea rezultată
            return hsvImage;
        }
        #endregion

    }
}