using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Framework.Utilities
{
    public class DrawingHelper
    {
        public static Collection<Line> VectorOfLines { get; set; }
        public static Collection<Rectangle> VectorOfRectangles { get; set; }
        public static Collection<Ellipse> VectorOfEllipses { get; set; }
        public static Collection<Polygon> VectorOfPolygons { get; set; }

        private static Dictionary<Shape, (double, double)> ShapesProperties { get; }

        static DrawingHelper()
        {
            VectorOfLines = new Collection<Line>();
            VectorOfRectangles = new Collection<Rectangle>();
            VectorOfEllipses = new Collection<Ellipse>();
            VectorOfPolygons = new Collection<Polygon>();

            ShapesProperties = new Dictionary<Shape, (double, double)>();
        }

        private static void AddShapeProperties(
            Canvas canvas, Shape shape, double leftProperty, double topProperty, double scaleValue)
        {
            canvas.Children.Add(shape);
            ShapesProperties.Add(shape, (leftProperty, topProperty));

            Canvas.SetLeft(shape, leftProperty * scaleValue);
            Canvas.SetTop(shape, topProperty * scaleValue);

            shape.LayoutTransform = new ScaleTransform
            {
                ScaleX = scaleValue,
                ScaleY = scaleValue
            };
        }

        public static void UpdateShapesProperties(
            Canvas canvas, double scaleValue)
        {
            if (canvas != null)
            {
                foreach (Shape shape in ShapesProperties.Keys)
                {
                    Canvas.SetLeft(shape, ShapesProperties[shape].Item1 * scaleValue);
                    Canvas.SetTop(shape, ShapesProperties[shape].Item2 * scaleValue);

                    (shape.LayoutTransform as ScaleTransform).ScaleX = scaleValue;
                    (shape.LayoutTransform as ScaleTransform).ScaleY = scaleValue;
                }
            }
        }

        public static Line DrawLine(
            Canvas canvas, Point start, Point end, int thickness, Brush color, double scaleValue)
        {
            Line line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                StrokeThickness = thickness,
                Stroke = color
            };

            VectorOfLines.Add(line);
            AddShapeProperties(canvas, line, Canvas.GetLeft(line), Canvas.GetTop(line), scaleValue);

            return line;
        }

        public static Rectangle DrawRectangle(
            Canvas canvas, Point leftTop, Point rightBottom, int thickness, Brush color, double scaleValue)
        {
            Rectangle rectangle = new Rectangle
            {
                Stroke = color,
                StrokeThickness = thickness,
                Width = Math.Abs(rightBottom.X - leftTop.X),
                Height = Math.Abs(rightBottom.Y - leftTop.Y)
            };

            VectorOfRectangles.Add(rectangle);
            AddShapeProperties(canvas, rectangle, leftTop.X, leftTop.Y, scaleValue);

            return rectangle;
        }

        public static Ellipse DrawEllipse(
            Canvas canvas, Point center, double width, double height, int thickness, Brush color, double scaleValue)
        {
            Ellipse ellipse = new Ellipse
            {
                Stroke = color,
                StrokeThickness = thickness,
                Width = width,
                Height = height
            };

            VectorOfEllipses.Add(ellipse);
            AddShapeProperties(canvas, ellipse, center.X - width / 2.0, center.Y - height / 2.0, scaleValue);

            return ellipse;
        }

        public static Polygon DrawPolygon(
            Canvas canvas, PointCollection vectorOfPoints, int thickness, Brush color, double scaleValue)
        {
            Polygon polygon = new Polygon
            {
                Stroke = color,
                StrokeThickness = thickness,
                Points = vectorOfPoints.Clone()
            };

            VectorOfPolygons.Add(polygon);
            AddShapeProperties(canvas, polygon, Canvas.GetLeft(polygon), Canvas.GetTop(polygon), scaleValue);

            return polygon;
        }

        public static bool RemoveUiElement(Canvas canvas, UIElement element)
        {
            if (element != null)
            {
                canvas.Children.Remove(element);
                ShapesProperties.Remove(element as Shape);

                if (element is Line line)
                    return VectorOfLines.Remove(line);
                else if (element is Rectangle rectangle)
                    return VectorOfRectangles.Remove(rectangle);
                else if (element is Ellipse ellipse)
                    return VectorOfEllipses.Remove(ellipse);
                else if (element is Polygon polygon)
                    return VectorOfPolygons.Remove(polygon);
            }

            return false;
        }

        public static void RemoveUiElements<TElement>(Canvas canvas)
            where TElement : UIElement
        {
            var items = canvas.Children.OfType<TElement>().ToList();
            items.RemoveAll(item =>
            {
                return RemoveUiElement(canvas, item);
            });
        }

        public static void RemoveUiElements(Canvas canvas)
        {
            RemoveUiElements<Line>(canvas);
            RemoveUiElements<Rectangle>(canvas);
            RemoveUiElements<Ellipse>(canvas);
            RemoveUiElements<Polygon>(canvas);
        }
    }
}