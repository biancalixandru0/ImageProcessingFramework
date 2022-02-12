using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System;
using Ellipse = System.Windows.Shapes.Ellipse;
using PointCollection = System.Windows.Media.PointCollection;

namespace ImageProcessingFramework.Model
{
    public class DrawHelper
    {
        public static void SetPixelGray(Image imageSource, Image<Gray, byte> inputImage, int x, int y, int gray)
        {
            inputImage.Data[y, x, 0] = (byte)gray;
            imageSource.Source = ImageConverter.Convert(inputImage);
        }

        public static void SetPixelColor(Image imageSource, Image<Bgr, byte> inputImage, int x, int y, int red, int green, int blue)
        {
            inputImage.Data[y, x, 0] = (byte)blue;
            inputImage.Data[y, x, 1] = (byte)green;
            inputImage.Data[y, x, 2] = (byte)red;
            imageSource.Source = ImageConverter.Convert(inputImage);
        }

        public static Line DrawLine(Canvas canvas, int startX, int startY, int endX, int endY, int thickness, Brush color)
        {
            var line = new Line
            {
                X1 = startX,
                Y1 = startY,
                X2 = endX,
                Y2 = endY,
                StrokeThickness = thickness,
                Stroke = color
            };
            canvas.Children.Add(line);

            return line;
        }

        public static Rectangle DrawRectangle(Canvas canvas, int leftTopX, int leftTopY, int rightBottomX, int rightBottomY,
            int thickness, Brush color)
        {
            var rectangle = new Rectangle
            {
                Stroke = color,
                StrokeThickness = thickness,
                Width = Math.Abs(rightBottomX - leftTopX),
                Height = Math.Abs(rightBottomY - leftTopY)
            };
            Canvas.SetLeft(rectangle, leftTopX);
            Canvas.SetTop(rectangle, leftTopY);
            canvas.Children.Add(rectangle);

            return rectangle;
        }

        public static Ellipse DrawElipse(Canvas canvas, int centerX, int centerY, int width, int height, int thickness, Brush color)
        {
            var ellipse = new Ellipse
            {
                Stroke = color,
                StrokeThickness = thickness,
                Width = width,
                Height = height
            };
            Canvas.SetLeft(ellipse, centerX - width / 2.0);
            Canvas.SetTop(ellipse, centerY - height / 2.0);
            canvas.Children.Add(ellipse);

            return ellipse;
        }

        public static Polygon DrawPolygon(Canvas canvas, PointCollection vectorOfPoints, int thickness, Brush color)
        {
            var polygon = new Polygon
            {
                Stroke = color,
                StrokeThickness = thickness,
                Points = vectorOfPoints
            };
            canvas.Children.Add(polygon);

            return polygon;
        }
    }
}