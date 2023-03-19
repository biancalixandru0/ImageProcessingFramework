using Emgu.CV;
using Emgu.CV.Structure;
using System;

namespace Algorithms.Tools
{
    public class Tools
    {
        #region Copy
        public static Image<Gray, byte> Copy(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = inputImage.Clone();
            return result;
        }

        public static Image<Bgr, byte> Copy(Image<Bgr, byte> inputImage)
        {
            Image<Bgr, byte> result = inputImage.Clone();
            return result;
        }
        #endregion

        #region Invert
        public static Image<Gray, byte> Invert(Image<Gray, byte> inputImage)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);
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
            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Size);
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
        #endregion

        #region Convert color image to grayscale image
        public static Image<Gray, byte> Convert(Image<Bgr, byte> inputImage)
        {
            Image<Gray, byte> result = inputImage.Convert<Gray, byte>();
            return result;
        }
        #endregion

        #region Rotate
        public static Image<Gray, byte> RotateClockwise(Image<Gray, byte> inputImage)
        {
            int resultWidth = inputImage.Height;
            int resultHeight = inputImage.Width;

            Image<Gray, byte> result = new Image<Gray, byte>(resultWidth, resultHeight);

            for (int x = 0; x < resultWidth; x++)
            {
                for (int y = 0; y < resultHeight; y++)
                {
                    result.Data[y, result.Width - x - 1, 0] = inputImage.Data[x, y, 0];
                }
            }

            return result;
        }

        public static Image<Bgr, byte> RotateClockwise(Image<Bgr, byte> inputImage)
        {
            int resultWidth = inputImage.Height;
            int resultHeight = inputImage.Width;

            Image<Bgr, byte> result = new Image<Bgr, byte>(resultWidth, resultHeight);

            for (int x = 0; x < resultWidth; x++)
            {
                for (int y = 0; y < resultHeight; y++)
                {
                    result.Data[y, result.Width - x - 1, 0] = inputImage.Data[x, y, 0];
                    result.Data[y, result.Width - x - 1, 1] = inputImage.Data[x, y, 1];
                    result.Data[y, result.Width - x - 1, 2] = inputImage.Data[x, y, 2];
                }
            }

            return result;
        }

        public static Image<Gray, byte> RotateAntiClockwise(Image<Gray, byte> inputImage)
        {
            int resultWidth = inputImage.Height;
            int resultHeight = inputImage.Width;

            Image<Gray, byte> result = new Image<Gray, byte>(resultWidth, resultHeight);

            for (int y = 0; y < resultHeight; y++)
            {
                for (int x = 0; x < resultWidth; x++)
                {
                    result.Data[result.Height - y - 1, x, 0] = inputImage.Data[x, y, 0];
                }
            }

            return result;
        }

        public static Image<Bgr, byte> RotateAntiClockwise(Image<Bgr, byte> inputImage)
        {
            int resultWidth = inputImage.Height;
            int resultHeight = inputImage.Width;

            Image<Bgr, byte> result = new Image<Bgr, byte>(resultWidth, resultHeight);

            for (int y = 0; y < resultHeight; y++)
            {
                for (int x = 0; x < resultWidth; x++)
                {
                    result.Data[result.Height - y - 1, x, 0] = inputImage.Data[x, y, 0];
                    result.Data[result.Height - y - 1, x, 1] = inputImage.Data[x, y, 1];
                    result.Data[result.Height - y - 1, x, 2] = inputImage.Data[x, y, 2];
                }
            }

            return result;
        }
        #endregion

        #region Mirror image verticaly

        public static Image<Gray, byte> MirorImage(Image<Gray, byte> inputImage)
        {
     
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    result.Data[y,x,0] = inputImage.Data[y,result.Width - x -1, 0];

                }
            }

            return result;
        }

        public static Image<Bgr, byte> MirorImage(Image<Bgr, byte> inputImage)
        {

            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Size);


            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    result.Data[y, x, 0] = inputImage.Data[y, result.Width - x - 1, 0];
                    result.Data[y, x, 1] = inputImage.Data[y, result.Width - x - 1, 1];
                    result.Data[y, x, 2] = inputImage.Data[y, result.Width - x - 1, 2];

                }
            }

            return result;
        }

        #endregion

        #region Mirror image verticaly

        public static Image<Gray, byte> BinarizareImagine(Image<Gray, byte> inputImage, int threshold)
        {

            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    if (inputImage.Data[y, x, 0] <= threshold)
                    {
                        result.Data[y, x, 0] = 0;
                    }
                    else
                    {
                        result.Data[y, x, 0] = 255;
                    }
                }
            }

            return result;
        }

        public static Image<Bgr, byte> BinarizareImagine(Image<Bgr, byte> inputImage, int threshold)
        {

            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Size);

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    if ((inputImage.Data[y, x, 0] + inputImage.Data[y, x, 1] + inputImage.Data[y, x, 2]) / 3 <= threshold)
                    {
                        result.Data[y, x, 0] = 0;
                        result.Data[y, x, 1] = 0;
                        result.Data[y, x, 2] = 0;
                    }
                    else
                    {
                        result.Data[y, x, 0] = 255;
                        result.Data[y, x, 1] = 255;
                        result.Data[y, x, 2] = 255;
                    }

                }
            }

            return result;
        }
        #endregion

        #region Crop image

        public static Image<Gray, byte> Crop(Image<Gray, byte> grayInitialImage, Tuple<int, int> top, Tuple<int, int> bottom)
        {
            int width = bottom.Item1 - top.Item1;
            int height = bottom.Item2 - top.Item2;

            Image<Gray, byte> result = new Image<Gray, byte>(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)

                    result.Data[y, x, 0] = grayInitialImage.Data[y + top.Item2, x + top.Item1, 0];

            }

            return result;
        }

        public static Image<Bgr, byte> Crop(Image<Bgr, byte> colorInitialImage, Tuple<int, int> top, Tuple<int, int> bottom)
        {
            int width = bottom.Item1 - top.Item1;
            int height = bottom.Item2 - top.Item2;

            Image<Bgr, byte> result = new Image<Bgr, byte>(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result.Data[y, x, 0] = colorInitialImage.Data[y + top.Item2, x + top.Item1, 0];
                    result.Data[y, x, 1] = colorInitialImage.Data[y + top.Item2, x + top.Item1, 1];
                    result.Data[y, x, 2] = colorInitialImage.Data[y + top.Item2, x + top.Item1, 2];
                }
            }

            return result;
        }

        #endregion
    }
}