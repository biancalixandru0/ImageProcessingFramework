using Emgu.CV;
using Emgu.CV.Structure;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ImageProcessingFramework.Model;
using System.Drawing;
using System.Windows.Interop;
using System;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ImageProcessingFramework.ViewModel
{
    class MagnifierCommands
    {
        public System.Windows.Media.ImageSource GetImage(Image<Bgr, byte> image, int width, int height)
        {
            Bitmap flag = new Bitmap(width, height);
            Graphics flagGraphics = Graphics.FromImage(flag);

            int i = 0;
            int widthStep = width / 9;
            int heightStep = height / 9;
            int yStep = 0;
            for (int x = -4; x <= 4; ++x)
            {
                int xStep = 0;
                int j = 0;
                for (int y = -4; y <= 4; ++y)
                {
                    if (DataProvider.MousePosition.Y + y >= 0 && DataProvider.MousePosition.X + x >= 0)
                    {
                        int blueColor = GetColorValue(image, (int)DataProvider.MousePosition.Y + y,
                            (int)DataProvider.MousePosition.X + x, 0);
                        int greenColor = GetColorValue(image, (int)DataProvider.MousePosition.Y + y,
                            (int)DataProvider.MousePosition.X + x, 1);
                        int redColor = GetColorValue(image, (int)DataProvider.MousePosition.Y + y,
                            (int)DataProvider.MousePosition.X + x, 2);

                        string text = redColor + "\n" + greenColor + "\n" + blueColor;
                        Font font = new Font(new FontFamily("Arial"), 7, System.Drawing.FontStyle.Bold, GraphicsUnit.Point);
                        Rectangle rectangle = new Rectangle(yStep, xStep, widthStep, heightStep);
                        flagGraphics.FillRectangle(new SolidBrush(Color.FromArgb(redColor, greenColor, blueColor)), rectangle);
                        if (blueColor <= 100 & greenColor <= 100 & redColor <= 100)
                            flagGraphics.DrawString(text, font, Brushes.White, rectangle);
                        else
                            flagGraphics.DrawString(text, font, Brushes.Black, rectangle);
                    }
                    else
                        flagGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)),
                            new Rectangle(yStep, xStep, widthStep, heightStep));
                    xStep += widthStep;
                    ++j;
                }
                yStep += heightStep;
                ++i;
            }

            return GetBitmap(flag);
        }

        public System.Windows.Media.ImageSource GetImageGray(Image<Gray, byte> image, int width, int height)
        {
            Bitmap flag = new Bitmap(width, height);
            Graphics flagGraphics = Graphics.FromImage(flag);

            int i = 0;
            int widthStep = width / 9;
            int heightStep = height / 9;
            int yStep = 0;
            for (int x = -4; x <= 4; ++x)
            {
                int xStep = 0;
                int j = 0;
                for (int y = -4; y <= 4; ++y)
                {
                    if (DataProvider.MousePosition.Y + y >= 0 && DataProvider.MousePosition.X + x >= 0)
                    {
                        int grayColor = GetGrayValue(image, (int)DataProvider.MousePosition.Y + y,
                            (int)DataProvider.MousePosition.X + x, 0);

                        string text = "\n" + grayColor + "\n";
                        Font font = new Font(new FontFamily("Arial"), 7, System.Drawing.FontStyle.Bold, GraphicsUnit.Point);
                        Rectangle rectangle = new Rectangle(yStep, xStep, widthStep, heightStep);
                        flagGraphics.FillRectangle(new SolidBrush(Color.FromArgb(grayColor, grayColor, grayColor)),
                            rectangle);
                        if (grayColor <= 100)
                            flagGraphics.DrawString(text, font, Brushes.White, rectangle);
                        else
                            flagGraphics.DrawString(text, font, Brushes.Black, rectangle);
                    }
                    else
                        flagGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)),
                            new Rectangle(yStep, xStep, widthStep, heightStep));
                    xStep += widthStep;
                    ++j;
                }
                yStep += heightStep;
                ++i;
            }

            return GetBitmap(flag);
        }

        private int GetColorValue(Image<Bgr, byte> image, int x, int y, int channel)
        {
            return image.Data[x, y, channel];
        }

        private int GetGrayValue(Image<Gray, byte> image, int x, int y, int channel)
        {
            return image.Data[x, y, channel];
        }

        private System.Windows.Media.ImageSource GetBitmap(Bitmap bitmap)
        {
             return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}