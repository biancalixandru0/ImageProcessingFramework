using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Emgu.CV;

namespace ImageProcessingFramework.Model
{
    class ImageConverter
    {
        public static ImageSource Convert<TColor>(Image<TColor, byte> image)
            where TColor : struct, IColor
        {
            return Imaging.CreateBitmapSourceFromHBitmap(image.Bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}