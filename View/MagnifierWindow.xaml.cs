using ImageProcessingFramework.ViewModel;
using System.Windows;
using static ImageProcessingFramework.Model.DataProvider;

namespace ImageProcessingFramework.View
{
    public partial class MagnifierWindow : Window
    {
        public MagnifierWindow()
        {
            InitializeComponent();

            if (ColorInitialImage != null)
                DisplayColor();

            if (GrayInitialImage != null)
                DisplayGray();

            LastPosition = MousePosition;
        }

        private void DisplayColor()
        {
            var magnifierCommands = new MagnifierCommands();
            if (ColorInitialImage != null)
                imageBoxOriginal.Source = magnifierCommands.GetImage(ColorInitialImage, (int)imageBoxOriginal.Width, (int)imageBoxOriginal.Height);
            if (ColorProcessedImage != null)
                imageBoxProcessed.Source = magnifierCommands.GetImage(ColorProcessedImage, (int)imageBoxOriginal.Width, (int)imageBoxOriginal.Height);
            if (GrayProcessedImage != null)
                imageBoxProcessed.Source = magnifierCommands.GetImageGray(GrayProcessedImage, (int)imageBoxOriginal.Width, (int)imageBoxOriginal.Height);
        }

        private void DisplayGray()
        {
            var magnifierCommands = new MagnifierCommands();
            if (GrayInitialImage != null)
                imageBoxOriginal.Source = magnifierCommands.GetImageGray(GrayInitialImage, (int)imageBoxOriginal.Width, (int)imageBoxOriginal.Height);
            if (GrayProcessedImage != null)
                imageBoxProcessed.Source = magnifierCommands.GetImageGray(GrayProcessedImage, (int)imageBoxOriginal.Width, (int)imageBoxOriginal.Height);
        }

        private void MagnifierClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MagnifierOn = false;
        }

        public object Controls { get; private set; }

        private void MagnifierUpdate(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (LastPosition != MousePosition)
            {
                if (GrayInitialImage != null)
                    DisplayGray();

                if (ColorInitialImage != null)
                    DisplayColor();

                LastPosition = MousePosition;
            }
        }
    }
}