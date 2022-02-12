using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows;
using System.Windows.Controls;
using PointCollection = System.Windows.Media.PointCollection;

namespace ImageProcessingFramework.Model
{
    class DataProvider
    {
        public static Point MousePosition
        {
            get;
            set;
        }

        public static Point LastPosition
        {
            get;
            set;
        }

        public static Image<Gray, byte> GrayInitialImage
        {
            get;
            set;
        }

        public static Image<Bgr, byte> ColorInitialImage
        {
            get;
            set;
        }

        public static Image<Bgr, byte> ColorProcessedImage
        {
            get;
            set;
        }

        public static Image<Gray, byte> GrayProcessedImage
        {
            get;
            set;
        }

        public static bool MagnifierOn
        {
            get;
            set;
        }

        public static bool GLevelsrowOn
        {
            get;
            set;
        }

        public static PointCollection VectorOfMousePosition
        {
            get;
            set;
        }

        public static Image InitialImageUi
        {
            get;
            set;
        }

        public static Image ProcessedImageUi
        {
            get;
            set;
        }

        public static Canvas InitialCanvas
        {
            get;
            set;
        }

        public static Canvas ProcessedCanvas
        {
            get;
            set;
        }
    }
}