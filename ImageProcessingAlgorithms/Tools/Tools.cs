using Emgu.CV;
using Emgu.CV.Structure;

namespace ImageProcessingAlgorithms.Tools
{
    public class Tools
    {
        public static Image<Gray, byte> Invert(Image<Gray, byte> inputImage)
        {
            var result = new Image<Gray, byte>(inputImage.Size);
            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    result.Data[y, x, 0] = (byte)(255 - inputImage.Data[y, x, 0]);
                }
            }
            return result;
        }

        public static Image<Bgr, byte> Invert(Image<Bgr, byte> inputImage)
        {
            var result = new Image<Bgr, byte>(inputImage.Size);
            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    result.Data[y, x, 0] = (byte)(255 - inputImage.Data[y, x, 0]);
                    result.Data[y, x, 1] = (byte)(255 - inputImage.Data[y, x, 1]);
                    result.Data[y, x, 2] = (byte)(255 - inputImage.Data[y, x, 2]);
                }
            }
            return result;
        }

        public static Image<Bgr, byte> Copy(Image<Bgr, byte> image)
        {
            var result = new Image<Bgr, byte>(image.Size);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    result.Data[y, x, 0] = (byte)(image.Data[y, x, 0]);
                }
            }
            return image;
        }

        public static Image<Gray, byte> Copy(Image<Gray, byte> image)
        {
            var result = new Image<Gray, byte>(image.Size);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    result.Data[y, x, 0] = (byte)(image.Data[y, x, 0]);
                }
            }
            return image;
        }

        public static Image<Gray, byte> Convert(Image<Bgr, byte> coloredImage)
        {
            var grayImage = coloredImage.Convert<Gray, byte>();

            return grayImage;
        }
    }
}