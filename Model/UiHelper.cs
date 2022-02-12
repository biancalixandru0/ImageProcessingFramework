using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using static ImageProcessingFramework.Model.DataProvider;
using System.Windows;
using Ellipse = System.Windows.Shapes.Ellipse;

namespace ImageProcessingFramework.Model
{
    class UiHelper
    {
        public static Rectangle InitialRectangle { get; set; }
        public static Rectangle ProcessedRectangle { get; set; }

        public static Line InitialLine { get; set; }
        public static Line ProcessedLine { get; set; }

        public static Ellipse InitialEllipse { get; set; }
        public static Ellipse ProcessedEllipse { get; set; }

        public static Polygon InitialPolygon { get; set; }
        public static Polygon ProcessedPolygon { get; set; }

        public static void ResizeCanvas(Canvas canvas, double scaleValue)
        {
            if (GrayInitialImage != null)
            {
                canvas.Height = GrayInitialImage.Height * scaleValue;
                canvas.Width = GrayInitialImage.Width * scaleValue;
            }

            if (ColorInitialImage != null)
            {
                canvas.Height = ColorInitialImage.Height * scaleValue;
                canvas.Width = ColorInitialImage.Width * scaleValue;
            }
        }

        public static void SetUiValues(TextBlock xPos,
            TextBlock yPos,
            TextBlock grayValue,
            TextBlock bValue,
            TextBlock gValue,
            TextBlock rValue,
            Image<Gray, byte> grayImage,
            Image<Bgr, byte> colorImage,
            int x, int y)
        {
            xPos.Text = "X: " + x.ToString() + " ";
            yPos.Text = "Y: " + y.ToString();

            grayValue.Text = (grayImage != null && y != grayImage.Height && x != grayImage.Width) ?
                "Gray: " + grayImage.Data[y, x, 0] : "Gray: 0";
            bValue.Text = (colorImage != null && y != colorImage.Height && x != colorImage.Width) ?
                "B: " + colorImage.Data[y, x, 0] : "B: 0";
            gValue.Text = (colorImage != null && y != colorImage.Height && x != colorImage.Width) ?
                "G: " + colorImage.Data[y, x, 1] : "G: 0";
            rValue.Text = (colorImage != null && y != colorImage.Height && x != colorImage.Width) ?
                "R: " + colorImage.Data[y, x, 2] : "R: 0";
        }

        public static Rectangle GetRectangle(Canvas canvas, int x, int y, double scaleValue)
        {
            var rectangle = new Rectangle
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            rectangle.Width = 9;
            rectangle.Height = 9;
            Canvas.SetLeft(rectangle, x * scaleValue);
            Canvas.SetTop(rectangle, y * scaleValue);
            canvas.Children.Add(rectangle);

            return rectangle;
        }

        public static Line GetLine(Canvas canvas, Image image, double scaleValue)
        {
            var line = new Line
            {
                X1 = 0,
                Y1 = MousePosition.Y * scaleValue,
                X2 = image.ActualWidth * scaleValue,
                Y2 = MousePosition.Y * scaleValue,
                StrokeThickness = 2,
                Stroke = Brushes.Red
            };
            canvas.Children.Add(line);

            return line;
        }

        public static void RemoveUiElements(Canvas initialCanvas,
            Canvas processedCanvas,
            UIElement initialElement,
            UIElement processedElement)
        {
            initialCanvas.Children.Remove(initialElement);
            processedCanvas.Children.Remove(processedElement);
        }
    }
}