using Emgu.CV;

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

using static Framework.Utilities.DataProvider;
using static Framework.Utilities.DrawingHelper;

namespace Framework.Utilities
{
    class UiHelper
    {
        private static Rectangle _initialSquare;
        private static Rectangle _processedSquare;

        private static Line _initialRowLine;
        private static Line _processedRowLine;

        private static Line _initialColumnLine;
        private static Line _processedColumnLine;

        public static void DrawUiElements(Canvas initialCanvas, Canvas processedCanvas, double scaleValue)
        {
            if (MagnifierOn == true)
            {
                DrawInitialSquare(initialCanvas, scaleValue);
                DrawProcessedSquare(processedCanvas, scaleValue);
            }

            if (RowColorLevelsOn == true || MagnifierOn == true)
            {
                DrawInitialRowLine(initialCanvas, scaleValue);
                DrawProcessedRowLine(processedCanvas, scaleValue);
            }

            if (ColumnColorLevelsOn == true || MagnifierOn == true)
            {
                DrawInitialColumnLine(initialCanvas, scaleValue);
                DrawProcessedColumnLine(processedCanvas, scaleValue);
            }
        }

        public static void RemoveUiElements(Canvas initialCanvas, Canvas processedCanvas)
        {
            RemoveUiElement(initialCanvas, _initialSquare);
            RemoveUiElement(processedCanvas, _processedSquare);

            RemoveUiElement(initialCanvas, _initialRowLine);
            RemoveUiElement(processedCanvas, _processedRowLine);

            RemoveUiElement(initialCanvas, _initialColumnLine);
            RemoveUiElement(processedCanvas, _processedColumnLine);
        }

        private static Rectangle GetSquare(Canvas canvas, Point point, double scaleValue)
        {
            var leftTop = new Point(point.X - 5, point.Y - 5);
            var rightBottom = new Point(point.X + 5, point.Y + 5);

            return DrawRectangle(canvas, leftTop, rightBottom, 1, Brushes.Red, scaleValue);
        }

        private static Line GetRowLine(Canvas canvas, IImage image, Point point, double scaleValue)
        {
            var start = new Point(0, point.Y);
            var end = new Point(image.Size.Width, point.Y);

            return DrawLine(canvas, start, end, 1, Brushes.Red, scaleValue);
        }

        private static Line GetColumnLine(Canvas canvas, IImage image, Point point, double scaleValue)
        {
            var start = new Point(point.X, 0);
            var end = new Point(point.X, image.Size.Height);

            return DrawLine(canvas, start, end, 1, Brushes.Red, scaleValue);
        }

        private static void DrawInitialRowLine(Canvas canvas, double scaleValue)
        {
            if (GrayInitialImage != null)
                _initialRowLine = GetRowLine(canvas, GrayInitialImage, LastPosition, scaleValue);
            else if (ColorInitialImage != null)
                _initialRowLine = GetRowLine(canvas, ColorInitialImage, LastPosition, scaleValue);
        }

        private static void DrawProcessedRowLine(Canvas canvas, double scaleValue)
        {
            if (GrayProcessedImage != null)
                _processedRowLine = GetRowLine(canvas, GrayProcessedImage, LastPosition, scaleValue);
            else if (ColorProcessedImage != null)
                _processedRowLine = GetRowLine(canvas, ColorProcessedImage, LastPosition, scaleValue);
        }

        private static void DrawInitialColumnLine(Canvas canvas, double scaleValue)
        {
            if (GrayInitialImage != null)
                _initialColumnLine = GetColumnLine(canvas, GrayInitialImage, LastPosition, scaleValue);
            else if (ColorInitialImage != null)
                _initialColumnLine = GetColumnLine(canvas, ColorInitialImage, LastPosition, scaleValue);
        }

        private static void DrawProcessedColumnLine(Canvas canvas, double scaleValue)
        {
            if (GrayProcessedImage != null)
                _processedColumnLine = GetColumnLine(canvas, GrayProcessedImage, LastPosition, scaleValue);
            else if (ColorProcessedImage != null)
                _processedColumnLine = GetColumnLine(canvas, ColorProcessedImage, LastPosition, scaleValue);
        }

        private static void DrawInitialSquare(Canvas canvas, double scaleValue)
        {
            if (GrayInitialImage != null || ColorInitialImage != null)
                _initialSquare = GetSquare(canvas, LastPosition, scaleValue);
        }

        private static void DrawProcessedSquare(Canvas canvas, double scaleValue)
        {
            if (GrayProcessedImage != null || ColorProcessedImage != null)
                _processedSquare = GetSquare(canvas, LastPosition, scaleValue);
        }
    }
}