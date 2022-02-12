using System.Windows;
using ImageProcessingFramework.ViewModel;
using OxyPlot;
using static ImageProcessingFramework.Model.DataProvider;

namespace ImageProcessingFramework
{
    public partial class RowDisplayWindow : Window
    {
        private Point LastPosition;

        public RowDisplayWindow()
        {
            InitializeComponent();

            if (ColorInitialImage != null)
                DisplayColor();

            if (GrayInitialImage != null)
                DisplayGray();
        }

        private void RowDisplayUpdate(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (LastPosition != MousePosition)
            {
                LastPosition = MousePosition;
                xPos.Text = "X: " + ((int)(MousePosition.X)).ToString();
                yPos.Text = "Y: " + ((int)(MousePosition.Y)).ToString();
                if (GrayInitialImage != null)
                    DisplayGray();

                if (ColorInitialImage != null)
                    DisplayColor();

                checkBoxBlue.IsChecked = true;
                checkBoxGreen.IsChecked = true;
                checkBoxRed.IsChecked = true;
            }
        }

        private void DisplayGray()
        {
            var rowDisplayCommands = new RowDisplayCommands();
            if (GrayInitialImage != null)
                originalImageView.Model = rowDisplayCommands.PlotGrayImage(GrayInitialImage);
            if (GrayProcessedImage != null)
                processedImageView.Model = rowDisplayCommands.PlotGrayImage(GrayProcessedImage);
        }

        private void DisplayColor()
        {
            var rowDisplayCommands = new RowDisplayCommands();
            originalImageView.Model = rowDisplayCommands.PlotColorImage(ColorInitialImage);
            if (ColorProcessedImage != null)
                processedImageView.Model = rowDisplayCommands.PlotColorImage(ColorProcessedImage);
            checkBoxBlue.Visibility = Visibility.Visible;
            checkBoxGreen.Visibility = Visibility.Visible;
            checkBoxRed.Visibility = Visibility.Visible;

            DisplayGray();
        }

        private void AddGreenSeries(object sender, RoutedEventArgs e)
        {
            if (ColorInitialImage != null)
                SetVisibility(originalImageView.Model, 0, true);
            if (ColorProcessedImage != null)
                SetVisibility(processedImageView.Model, 0, true);
        }

        private void RemoveGreenSeries(object sender, RoutedEventArgs e)
        {
            if (ColorInitialImage != null)
                SetVisibility(originalImageView.Model, 0, false);
            if (ColorProcessedImage != null)
                SetVisibility(processedImageView.Model, 0, false);
        }

        private void AddRedSeries(object sender, RoutedEventArgs e)
        {
            if (ColorInitialImage != null)
                SetVisibility(originalImageView.Model, 1, true);
            if (ColorProcessedImage != null)
                SetVisibility(processedImageView.Model, 1, true);
        }

        private void RemoveRedSeries(object sender, RoutedEventArgs e)
        {
            if (ColorInitialImage != null)
                SetVisibility(originalImageView.Model, 1, false);
            if (ColorProcessedImage != null)
                SetVisibility(processedImageView.Model, 1, false);
        }

        private void AddBlueSeries(object sender, RoutedEventArgs e)
        {
            if (ColorInitialImage != null)
                SetVisibility(originalImageView.Model, 2, true);
            if (ColorProcessedImage != null)
                SetVisibility(processedImageView.Model, 2, true);
        }

        private void RemoveBlueSeries(object sender, RoutedEventArgs e)
        {
            if (ColorInitialImage != null)
                SetVisibility(originalImageView.Model, 2, false);
            if (ColorProcessedImage != null)
                SetVisibility(processedImageView.Model, 2, false);
        }

        private void SetVisibility(PlotModel model, int indexSeries, bool isVisible)
        {
            if (model != null)
            {
                model.Series[indexSeries].IsVisible = isVisible;
                model.InvalidatePlot(true);
            }
        }

        private void RowDisplayClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GLevelsrowOn = false;
        }
    }
}