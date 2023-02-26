using Emgu.CV;
using Emgu.CV.Structure;

using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

using Framework.Model;
using Framework.ViewModel;
using static Framework.Utilities.DataProvider;
using static Framework.Utilities.UiHelper;
using static Framework.Utilities.DrawingHelper;

namespace Framework.View
{
    public partial class MainWindow : Window
    {
        private readonly MainVM _mainVM;

        public MainWindow()
        {
            InitializeComponent();

            _mainVM = new MainVM();
            DataContext = _mainVM;

            InitializeThemeMode();
        }

        public void Update()
        {
            double scaleValue = sliderZoom.Value;

            RemoveUiElements(canvasOriginalImage, canvasProcessedImage);
            DrawUiElements(canvasOriginalImage, canvasProcessedImage, scaleValue);
        }

        private void InitializeThemeMode()
        {
            if (_mainVM.Theme is LimeGreenTheme)
                (themeMenu.Items[0] as MenuItem).IsChecked = true;
            else if (_mainVM.Theme is ForestGreenTheme)
                (themeMenu.Items[1] as MenuItem).IsChecked = true;
            else if (_mainVM.Theme is PalePinkTheme)
                (themeMenu.Items[2] as MenuItem).IsChecked = true;
            else if (_mainVM.Theme is LavenderVioletTheme)
                (themeMenu.Items[3] as MenuItem).IsChecked = true;
            else if (_mainVM.Theme is CobaltBlueTheme)
                (themeMenu.Items[4] as MenuItem).IsChecked = true;
        }

        private void SetUiValues(Image<Gray, byte> grayImage, Image<Bgr, byte> colorImage, int x, int y)
        {
            _mainVM.XPos = x >= 0 ? "X: " + x.ToString() : "";
            _mainVM.YPos = y >= 0 ? "Y: " + y.ToString() : "";

            _mainVM.GrayValue = (grayImage != null && y >= 0 && y < grayImage.Height && x >= 0 && x < grayImage.Width) ?
                "Gray: " + grayImage.Data[y, x, 0] : "";
            _mainVM.BlueValue = (colorImage != null && y >= 0 && y < colorImage.Height && x >= 0 && x < colorImage.Width) ?
                "B: " + colorImage.Data[y, x, 0] : "";
            _mainVM.GreenValue = (colorImage != null && y >= 0 && y < colorImage.Height && x >= 0 && x < colorImage.Width) ?
                "G: " + colorImage.Data[y, x, 1] : "";
            _mainVM.RedValue = (colorImage != null && y >= 0 && y < colorImage.Height && x >= 0 && x < colorImage.Width) ?
                "R: " + colorImage.Data[y, x, 2] : "";
        }

        private void ImageMouseMove(object sender, MouseEventArgs e)
        {
            if (sender == initialImage)
            {
                var position = e.GetPosition(initialImage);
                SetUiValues(GrayInitialImage, ColorInitialImage, (int)position.X, (int)position.Y);
            }
            else if (sender == processedImage)
            {
                var position = e.GetPosition(processedImage);
                SetUiValues(GrayProcessedImage, ColorProcessedImage, (int)position.X, (int)position.Y);
            }
        }

        private void ImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender == initialImage)
                MousePosition = e.GetPosition(initialImage);
            else if (sender == processedImage)
                MousePosition = e.GetPosition(processedImage);

            if (LastPosition != MousePosition)
            {
                VectorOfMousePosition.Add(MousePosition);
                LastPosition = MousePosition;
            }
        }

        private void ImageMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            VectorOfMousePosition.Clear();
        }

        private void CanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Update();

            if (MagnifierOn == true)
                Application.Current.Windows.OfType<MagnifierWindow>().First().Update();
            if (RowColorLevelsOn == true || ColumnColorLevelsOn == true)
                Application.Current.Windows.OfType<ColorLevelsWindow>().All(window => { window.Update(); return true; });
        }

        private void SliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double scaleValue = sliderZoom.Value;

            UpdateShapesProperties(canvasOriginalImage, scaleValue);
            UpdateShapesProperties(canvasProcessedImage, scaleValue);

            if (_mainVM != null)
            {
                _mainVM.OriginalCanvasWidth = initialImage.ActualWidth * scaleValue;
                _mainVM.OriginalCanvasHeight = initialImage.ActualHeight * scaleValue;

                _mainVM.ProcessedCanvasWidth = processedImage.ActualWidth * scaleValue;
                _mainVM.ProcessedCanvasHeight = processedImage.ActualHeight * scaleValue;
            }
        }

        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender == scrollViewerInitial)
            {
                scrollViewerProcessed.ScrollToVerticalOffset(e.VerticalOffset);
                scrollViewerProcessed.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
            else
            {
                scrollViewerInitial.ScrollToVerticalOffset(e.VerticalOffset);
                scrollViewerInitial.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseWindows();
        }

        private void ThemeModeSelector(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in themeMenu.Items)
            {
                item.IsChecked = false;
            }

            var selectedItem = sender as MenuItem;
            selectedItem.IsChecked = true;

            string selectedTheme = selectedItem.Header.ToString();

            _mainVM.SetThemeMode(selectedTheme);

            Properties.Settings.Default.Theme = selectedTheme;
            Properties.Settings.Default.Save();
        }
    }
}