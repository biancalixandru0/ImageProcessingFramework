using Emgu.CV;
using Emgu.CV.Structure;

using System.Linq;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using Point = System.Windows.Point;
using Color = System.Drawing.Color;
using Brushes = System.Drawing.Brushes;
using FontFamily = System.Drawing.FontFamily;

using static Framework.Utilities.DataProvider;
using ImageConverter = Framework.Converters.ImageConverter;

namespace Framework.View
{
    public partial class MagnifierWindow : Window
    {
        private Point LastPoint { get; set; }

        public MagnifierWindow()
        {
            InitializeComponent();

            MagnifierOn = true;

            Application.Current.Windows.OfType<MainWindow>().First().Update();
            Update();
        }

        public void Update()
        {
            if (LastPoint != LastPosition)
            {
                DisplayGray();
                DisplayColor();

                LastPoint = LastPosition;
            }
        }

        private void DisplayColor()
        {
            if (ColorInitialImage != null)
                imageBoxOriginal.Source = GetImage(ColorInitialImage, (int)imageBoxOriginal.Width, (int)imageBoxOriginal.Height);
            if (ColorProcessedImage != null)
                imageBoxProcessed.Source = GetImage(ColorProcessedImage, (int)imageBoxOriginal.Width, (int)imageBoxOriginal.Height);
        }

        private void DisplayGray()
        {
            if (GrayInitialImage != null)
                imageBoxOriginal.Source = GetImage(GrayInitialImage, (int)imageBoxOriginal.Width, (int)imageBoxOriginal.Height);
            if (GrayProcessedImage != null)
                imageBoxProcessed.Source = GetImage(GrayProcessedImage, (int)imageBoxOriginal.Width, (int)imageBoxOriginal.Height);
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MagnifierOn = false;
            Application.Current.Windows.OfType<MainWindow>().First().Update();
        }

        private ImageSource GetImage(Image<Bgr, byte> image, int width, int height)
        {
            Bitmap flag = new Bitmap(width, height);
            Graphics flagGraphics = Graphics.FromImage(flag);

            int i = 0;
            int widthStep = width / 9;
            int heightStep = height / 9;
            int yStep = 0;
            for (int x = -4; x <= 4; ++x)
            {
                int xStep = 0;
                int j = 0;
                for (int y = -4; y <= 4; ++y)
                {
                    if (LastPosition.Y + y >= 0 && LastPosition.X + x >= 0 &&
                        LastPosition.Y + y < image.Height && LastPosition.X + x < image.Width)
                    {
                        int blueColor = image.Data[(int)LastPosition.Y + y,
                            (int)LastPosition.X + x, 0];
                        int greenColor = image.Data[(int)LastPosition.Y + y,
                            (int)LastPosition.X + x, 1];
                        int redColor = image.Data[(int)LastPosition.Y + y,
                            (int)LastPosition.X + x, 2];

                        string text = redColor + "\n" + greenColor + "\n" + blueColor;
                        Font font = new Font(new FontFamily("Arial"), 7, System.Drawing.FontStyle.Bold, GraphicsUnit.Point);
                        Rectangle rectangle = new Rectangle(yStep, xStep, widthStep, heightStep);
                        flagGraphics.FillRectangle(new SolidBrush(Color.FromArgb(redColor, greenColor, blueColor)), rectangle);
                        if (blueColor <= 128 & greenColor <= 128 & redColor <= 128)
                            flagGraphics.DrawString(text, font, Brushes.White, rectangle);
                        else
                            flagGraphics.DrawString(text, font, Brushes.Black, rectangle);
                    }
                    else
                        flagGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)),
                            new Rectangle(yStep, xStep, widthStep, heightStep));
                    xStep += widthStep;
                    ++j;
                }
                yStep += heightStep;
                ++i;
            }

            return ImageConverter.Convert(flag);
        }

        private ImageSource GetImage(Image<Gray, byte> image, int width, int height)
        {
            Bitmap flag = new Bitmap(width, height);
            Graphics flagGraphics = Graphics.FromImage(flag);

            int i = 0;
            int widthStep = width / 9;
            int heightStep = height / 9;
            int yStep = 0;
            for (int x = -4; x <= 4; ++x)
            {
                int xStep = 0;
                int j = 0;
                for (int y = -4; y <= 4; ++y)
                {
                    if (LastPosition.Y + y >= 0 && LastPosition.X + x >= 0 &&
                        LastPosition.Y + y < image.Height && LastPosition.X + x < image.Width)
                    {
                        int grayColor = image.Data[(int)LastPosition.Y + y,
                            (int)LastPosition.X + x, 0];

                        string text = "\n" + grayColor + "\n";
                        Font font = new Font(new FontFamily("Arial"), 7, System.Drawing.FontStyle.Bold, GraphicsUnit.Point);
                        Rectangle rectangle = new Rectangle(yStep, xStep, widthStep, heightStep);
                        flagGraphics.FillRectangle(new SolidBrush(Color.FromArgb(grayColor, grayColor, grayColor)),
                            rectangle);
                        if (grayColor <= 128)
                            flagGraphics.DrawString(text, font, Brushes.White, rectangle);
                        else
                            flagGraphics.DrawString(text, font, Brushes.Black, rectangle);
                    }
                    else
                        flagGraphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)),
                            new Rectangle(yStep, xStep, widthStep, heightStep));
                    xStep += widthStep;
                    ++j;
                }
                yStep += heightStep;
                ++i;
            }

            return ImageConverter.Convert(flag);
        }
    }
}