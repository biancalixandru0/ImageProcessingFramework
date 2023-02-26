using Emgu.CV;

using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Framework.Converters
{
    class ImageConverter
    {
        public static ImageSource Convert<TColor>(Image<TColor, byte> image)
            where TColor : struct, IColor
        {
            return Convert(image.Bitmap);
        }

        public static ImageSource Convert(Bitmap bitmap)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
               IntPtr.Zero,
               Int32Rect.Empty,
               BitmapSizeOptions.FromEmptyOptions());
        }
    }
}