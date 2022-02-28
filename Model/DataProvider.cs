using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Ellipse = System.Windows.Shapes.Ellipse;
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

        public static Collection<Line> VectorOfLines
        {
            get;
            set;
        }

        public static Collection<Rectangle> VectorOfRectangles
        {
            get;
            set;
        }

        public static Collection<Ellipse> VectorOfEllipses
        {
            get;
            set;
        }

        public static Collection<Polygon> VectorOfPolygons
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