using System.Linq;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Framework.Utilities
{
    internal class FileHelper
    {
        #region Show File Dialog for loading an image
        public static string LoadFileDialog(string title)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = "Image files (*.jpg, *.jpeg, *.jfif, *.jpe, *.bmp, *.png) | *.jpg; *.jpeg; *.jfif; *.jpe; *.bmp; *.png"
            };

            if (fileDialog.ShowDialog() == false || fileDialog.FileName.CompareTo("") == 0)
                return null;

            return fileDialog.FileName;
        }
        #endregion

        #region Show File Dialog for saving an image
        public static string SaveFileDialog(string imageName)
        {
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                FileName = imageName,
                Filter = "Image files(*.jpg, *.jpeg, *.jfif, *.jpe, *.bmp, *.png) | *.jpg; *.jpeg; *.jfif; *.jpe; *.bmp; *.png"
            };

            if (fileDialog.ShowDialog() == false || fileDialog.FileName.CompareTo("") == 0)
                return null;

            return fileDialog.FileName;
        }

        public static EncoderParameters GetEncoderParameter(Encoder encoder, long value)
        {
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(
                encoder,
                value
            );

            return encoderParams;
        }

        public static ImageCodecInfo GetJpegCodec(string mimeType)
        {
            var jpegCodec = (from codec in ImageCodecInfo.GetImageEncoders()
                             where codec.MimeType == mimeType
                             select codec).Single();

            return jpegCodec;
        }
        #endregion

        #region Open image file verifying the OS platform
        public static void OpenImage(string imagePath)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(imagePath);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", imagePath);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", imagePath);
            }
        }
        #endregion
    }
}