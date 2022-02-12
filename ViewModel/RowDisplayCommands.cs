using System;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;
using ImageProcessingFramework.Model;
using OxyPlot;
using OxyPlot.Axes;
using LinearAxis = OxyPlot.Axes.LinearAxis;
using LineSeries = OxyPlot.Series.LineSeries;

namespace ImageProcessingFramework.ViewModel
{
    class RowDisplayCommands
    {
        public PlotModel plotImage { get; private set; }

        public String Xpos
        {
            get
            {
                return "X: " + ((int)(DataProvider.MousePosition.X)).ToString();
            }
        }

        public String Ypos
        {
            get
            {
                return "Y: " + ((int)(DataProvider.MousePosition.Y)).ToString();
            }
        }

        private LineSeries GenerateSeriesForColor(Image<Bgr, byte> colorImage, int chanel, String color)
        {
            var chanelValues = new List<int>();

            for (int x = 0; x < colorImage.Width; x++)
                chanelValues.Add(colorImage.Data[(int)DataProvider.MousePosition.Y, x, chanel]);

            if (color.Equals("Blue"))
            {
                var series = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    MarkerSize = 1,
                    MarkerStroke = OxyColors.Blue,
                    MarkerFill = OxyColors.Blue,
                    Color = OxyColors.Blue
                };

                for (int index = 0; index < chanelValues.Count; ++index)
                    series.Points.Add(new DataPoint(index, chanelValues[index]));

                return series;
            }

            if (color.Equals("Green"))
            {
                var series = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    MarkerSize = 1,
                    MarkerStroke = OxyColors.Green,
                    MarkerFill = OxyColors.Green,
                    Color = OxyColors.Green
                };

                for (int index = 0; index < chanelValues.Count; ++index)
                    series.Points.Add(new DataPoint(index, chanelValues[index]));

                return series;
            }

            if (color.Equals("Red"))
            {
                var series = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    MarkerSize = 1,
                    MarkerStroke = OxyColors.Red,
                    MarkerFill = OxyColors.Red,
                    Color = OxyColors.Red
                };

                for (int index = 0; index < chanelValues.Count; ++index)
                    series.Points.Add(new DataPoint(index, chanelValues[index]));

                return series;
            }

            return null;
        }

        private LineSeries GenerateSerieForGray(Image<Gray, byte> grayImage, int chanel, String color)
        {
            var chanelValues = new List<int>();

            for (int x = 0; x < grayImage.Width; x++)
                chanelValues.Add(grayImage.Data[(int)DataProvider.MousePosition.Y, x, chanel]);

            if (color.Equals("Gray"))
            {
                var series = new LineSeries
                {
                    MarkerType = MarkerType.None,
                    MarkerSize = 1,
                    MarkerStroke = OxyColors.Red,
                    MarkerFill = OxyColors.Red,
                    Color = OxyColors.Red
                };

                for (int index = 0; index < chanelValues.Count; ++index)
                    series.Points.Add(new DataPoint(index, chanelValues[index]));

                return series;
            }

            return null;
        }

        public PlotModel PlotColorImage(Image<Bgr, byte> colorImage)
        {
            plotImage = new PlotModel();
            plotImage.Series.Clear();
            plotImage.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Maximum = colorImage.Width + 30,
                Minimum = 0
            });

            plotImage.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 300,
                Minimum = 0
            });

            var seriesGreen = GenerateSeriesForColor(colorImage, 1, "Green");
            var seriesRed = GenerateSeriesForColor(colorImage, 2, "Red");
            var seriesBlue = GenerateSeriesForColor(colorImage, 0, "Blue");

            plotImage.Series.Add(seriesGreen);
            plotImage.Series.Add(seriesRed);
            plotImage.Series.Add(seriesBlue);

            return plotImage;
        }

        public PlotModel PlotGrayImage(Image<Gray, byte> grayImage)
        {
            plotImage = new PlotModel();
            plotImage.Series.Clear();
            plotImage.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Maximum = grayImage.Width + 30,
                Minimum = 0
            });

            plotImage.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 300,
                Minimum = 0
            });

            var seriesGray = GenerateSerieForGray(grayImage, 0, "Gray");

            plotImage.Series.Add(seriesGray);

            return plotImage;
        }
    }
}