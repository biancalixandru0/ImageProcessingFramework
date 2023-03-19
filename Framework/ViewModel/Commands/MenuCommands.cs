using Emgu.CV;
using Emgu.CV.Structure;

using System.Windows;
using System.Drawing.Imaging;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

using Framework.View;
using static Framework.Utilities.DataProvider;
using static Framework.Utilities.FileHelper;
using static Framework.Utilities.DrawingHelper;
using static Framework.Converters.ImageConverter;
using Algorithms.Tools;
using Algorithms.Utilities;
using System.Collections.Generic;
using System;
using Algorithms.Sections;

namespace Framework.ViewModel
{
    public class MenuCommands : BaseVM
    {
        private readonly MainVM _mainVM;

        public MenuCommands(MainVM mainVM)
        {
            _mainVM = mainVM;
        }

        private ImageSource InitialImage
        {
            get => _mainVM.InitialImage;
            set => _mainVM.InitialImage = value;
        }

        private ImageSource ProcessedImage
        {
            get => _mainVM.ProcessedImage;
            set => _mainVM.ProcessedImage = value;
        }

        private double ScaleValue
        {
            get => _mainVM.ScaleValue;
            set => _mainVM.ScaleValue = value;
        }

        #region File

        #region Load grayscale image
        private RelayCommand _loadGrayImageCommand;
        public RelayCommand LoadGrayImageCommand
        {
            get
            {
                if (_loadGrayImageCommand == null)
                    _loadGrayImageCommand = new RelayCommand(LoadGrayImage);
                return _loadGrayImageCommand;
            }
        }

        private void LoadGrayImage(object parameter)
        {
            Clear(parameter);

            string fileName = LoadFileDialog("Select a gray picture");
            if (fileName != null)
            {
                GrayInitialImage = new Image<Gray, byte>(fileName);
                InitialImage = Convert(GrayInitialImage);
            }
        }
        #endregion

        #region Load color image
        private ICommand _loadColorImageCommand;
        public ICommand LoadColorImageCommand
        {
            get
            {
                if (_loadColorImageCommand == null)
                    _loadColorImageCommand = new RelayCommand(LoadColorImage);
                return _loadColorImageCommand;
            }
        }

        private void LoadColorImage(object parameter)
        {
            Clear(parameter);

            string fileName = LoadFileDialog("Select a color picture");
            if (fileName != null)
            {
                ColorInitialImage = new Image<Bgr, byte>(fileName);
                InitialImage = Convert(ColorInitialImage);
            }
        }
        #endregion

        #region Save processed image
        private ICommand _saveProcessedImageCommand;
        public ICommand SaveProcessedImageCommand
        {
            get
            {
                if (_saveProcessedImageCommand == null)
                    _saveProcessedImageCommand = new RelayCommand(SaveProcessedImage);
                return _saveProcessedImageCommand;
            }
        }

        private void SaveProcessedImage(object parameter)
        {
            if (GrayProcessedImage == null && ColorProcessedImage == null)
            {
                MessageBox.Show("If you want to save your processed image, " +
                    "please load and process an image first!");
                return;
            }

            string imagePath = SaveFileDialog("image.jpg");
            if (imagePath != null)
            {
                GrayProcessedImage?.Bitmap.Save(imagePath, GetJpegCodec("image/jpeg"), GetEncoderParameter(Encoder.Quality, 100));
                ColorProcessedImage?.Bitmap.Save(imagePath, GetJpegCodec("image/jpeg"), GetEncoderParameter(Encoder.Quality, 100));
                OpenImage(imagePath);
            }
        }
        #endregion

        #region Save both images
        private ICommand _saveImagesCommand;
        public ICommand SaveImagesCommand
        {
            get
            {
                if (_saveImagesCommand == null)
                    _saveImagesCommand = new RelayCommand(SaveImages);
                return _saveImagesCommand;
            }
        }

        private void SaveImages(object parameter)
        {
            if (GrayInitialImage == null && ColorInitialImage == null)
            {
                MessageBox.Show("If you want to save both images, " +
                    "please load and process an image first!");
                return;
            }

            if (GrayProcessedImage == null && ColorProcessedImage == null)
            {
                MessageBox.Show("If you want to save both images, " +
                    "please process your image first!");
                return;
            }

            string imagePath = SaveFileDialog("image.jpg");
            if (imagePath != null)
            {
                IImage processedImage = null;
                if (GrayInitialImage != null && GrayProcessedImage != null)
                    processedImage = Utils.Combine(GrayInitialImage, GrayProcessedImage);

                if (GrayInitialImage != null && ColorProcessedImage != null)
                    processedImage = Utils.Combine(GrayInitialImage, ColorProcessedImage);

                if (ColorInitialImage != null && GrayProcessedImage != null)
                    processedImage = Utils.Combine(ColorInitialImage, GrayProcessedImage);

                if (ColorInitialImage != null && ColorProcessedImage != null)
                    processedImage = Utils.Combine(ColorInitialImage, ColorProcessedImage);

                processedImage?.Bitmap.Save(imagePath, GetJpegCodec("image/jpeg"), GetEncoderParameter(Encoder.Quality, 100));
                OpenImage(imagePath);
            }
        }
        #endregion

        #region Exit
        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                    _exitCommand = new RelayCommand(Exit);
                return _exitCommand;
            }
        }

        private void Exit(object parameter)
        {
            CloseWindows();
            System.Environment.Exit(0);
        }
        #endregion

        #endregion

        #region Edit

        #region Remove drawn shapes from initial canvas
        private ICommand _removeInitialDrawnShapesCommand;
        public ICommand RemoveInitialDrawnShapesCommand
        {
            get
            {
                if (_removeInitialDrawnShapesCommand == null)
                    _removeInitialDrawnShapesCommand = new RelayCommand(RemoveInitialDrawnShapes);
                return _removeInitialDrawnShapesCommand;
            }
        }

        private void RemoveInitialDrawnShapes(object parameter)
        {
            RemoveUiElements(parameter as Canvas);
        }
        #endregion

        #region Remove drawn shapes from processed canvas
        private ICommand _removeProcessedDrawnShapesCommand;
        public ICommand RemoveProcessedDrawnShapesCommand
        {
            get
            {
                if (_removeProcessedDrawnShapesCommand == null)
                    _removeProcessedDrawnShapesCommand = new RelayCommand(RemoveProcessedDrawnShapes);
                return _removeProcessedDrawnShapesCommand;
            }
        }

        private void RemoveProcessedDrawnShapes(object parameter)
        {
            RemoveUiElements(parameter as Canvas);
        }
        #endregion

        #region Remove drawn shapes from both canvases
        private ICommand _removeDrawnShapesCommand;
        public ICommand RemoveDrawnShapesCommand
        {
            get
            {
                if (_removeDrawnShapesCommand == null)
                    _removeDrawnShapesCommand = new RelayCommand(RemoveDrawnShapes);
                return _removeDrawnShapesCommand;
            }
        }

        private void RemoveDrawnShapes(object parameter)
        {
            var canvases = (object[])parameter;
            RemoveUiElements(canvases[0] as Canvas);
            RemoveUiElements(canvases[1] as Canvas);
        }
        #endregion

        #region Clear initial canvas
        private ICommand _clearInitialCanvasCommand;
        public ICommand ClearInitialCanvasCommand
        {
            get
            {
                if (_clearInitialCanvasCommand == null)
                    _clearInitialCanvasCommand = new RelayCommand(ClearInitialCanvas);
                return _clearInitialCanvasCommand;
            }
        }

        private void ClearInitialCanvas(object parameter)
        {
            RemoveUiElements(parameter as Canvas);

            GrayInitialImage = null;
            ColorInitialImage = null;
            InitialImage = null;
        }
        #endregion

        #region Clear processed canvas
        private ICommand _clearProcessedCanvasCommand;
        public ICommand ClearProcessedCanvasCommand
        {
            get
            {
                if (_clearProcessedCanvasCommand == null)
                    _clearProcessedCanvasCommand = new RelayCommand(ClearProcessedCanvas);
                return _clearProcessedCanvasCommand;
            }
        }

        private void ClearProcessedCanvas(object parameter)
        {
            RemoveUiElements(parameter as Canvas);

            GrayProcessedImage = null;
            ColorProcessedImage = null;
            ProcessedImage = null;
        }
        #endregion

        #region Closing all open windows and clear both canvases
        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                    _clearCommand = new RelayCommand(Clear);
                return _clearCommand;
            }
        }

        private void Clear(object parameter)
        {
            CloseWindows();

            ScaleValue = 1;

            var canvases = (object[])parameter;
            ClearInitialCanvas(canvases[0] as Canvas);
            ClearProcessedCanvas(canvases[1] as Canvas);
        }
        #endregion

        #endregion

        #region Tools

        #region Magnifier
        private ICommand _magnifierCommand;
        public ICommand MagnifierCommand
        {
            get
            {
                if (_magnifierCommand == null)
                    _magnifierCommand = new RelayCommand(Magnifier);
                return _magnifierCommand;
            }
        }

        private void Magnifier(object parameter)
        {
            if (MagnifierOn == true) return;
            if (VectorOfMousePosition.Count == 0)
            {
                MessageBox.Show("Please select an area first.");
                return;
            }

            MagnifierWindow magnifierWindow = new MagnifierWindow();
            magnifierWindow.Show();
        }
        #endregion

        #region Display Gray/Color levels

        #region On row
        private ICommand _displayLevelsOnRowCommand;
        public ICommand DisplayLevelsOnRowCommand
        {
            get
            {
                if (_displayLevelsOnRowCommand == null)
                    _displayLevelsOnRowCommand = new RelayCommand(DisplayLevelsOnRow);
                return _displayLevelsOnRowCommand;
            }
        }

        private void DisplayLevelsOnRow(object parameter)
        {
            if (RowColorLevelsOn == true) return;
            if (VectorOfMousePosition.Count == 0)
            {
                MessageBox.Show("Please select an area first.");
                return;
            }

            ColorLevelsWindow window = new ColorLevelsWindow(_mainVM, CLevelsType.Row);
            window.Show();
        }
        #endregion

        #region On column
        private ICommand _displayLevelsOnColumnCommand;
        public ICommand DisplayLevelsOnColumnCommand
        {
            get
            {
                if (_displayLevelsOnColumnCommand == null)
                    _displayLevelsOnColumnCommand = new RelayCommand(DisplayLevelsOnColumn);
                return _displayLevelsOnColumnCommand;
            }
        }

        private void DisplayLevelsOnColumn(object parameter)
        {
            if (ColumnColorLevelsOn == true) return;
            if (VectorOfMousePosition.Count == 0)
            {
                MessageBox.Show("Please select an area first.");
                return;
            }

            ColorLevelsWindow window = new ColorLevelsWindow(_mainVM, CLevelsType.Column);
            window.Show();
        }
        #endregion

        #endregion

        #region Visualize image histogram

        #region Initial image histogram
        private ICommand _histogramInitialImageCommand;
        public ICommand HistogramInitialImageCommand
        {
            get
            {
                if (_histogramInitialImageCommand == null)
                    _histogramInitialImageCommand = new RelayCommand(HistogramInitialImage);
                return _histogramInitialImageCommand;
            }
        }

        private void HistogramInitialImage(object parameter)
        {
            if (InitialHistogramOn == true) return;
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image !");
                return;
            }

            HistogramWindow window = null;

            if (ColorInitialImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.InitialColor);
            }
            else if (GrayInitialImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.InitialGray);
            }

            window.Show();
        }
        #endregion

        #region Processed image histogram
        private ICommand _histogramProcessedImageCommand;
        public ICommand HistogramProcessedImageCommand
        {
            get
            {
                if (_histogramProcessedImageCommand == null)
                    _histogramProcessedImageCommand = new RelayCommand(HistogramProcessedImage);
                return _histogramProcessedImageCommand;
            }
        }

        private void HistogramProcessedImage(object parameter)
        {
            if (ProcessedHistogramOn == true) return;
            if (ProcessedImage == null)
            {
                MessageBox.Show("Please process an image !");
                return;
            }

            HistogramWindow window = null;

            if (ColorProcessedImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.ProcessedColor);
            }
            else if (GrayProcessedImage != null)
            {
                window = new HistogramWindow(_mainVM, ImageType.ProcessedGray);
            }

            window.Show();
        }
        #endregion

        #endregion


        #region Copy image
        private ICommand _copyImageCommand;
        public ICommand CopyImageCommand
        {
            get
            {
                if (_copyImageCommand == null)
                    _copyImageCommand = new RelayCommand(CopyImage);
                return _copyImageCommand;
            }
        }

        private void CopyImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image !");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.Copy(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
            else if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.Copy(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
        }
        #endregion

        #region Invert image
        private ICommand _invertImageCommand;
        public ICommand InvertImageCommand
        {
            get
            {
                if (_invertImageCommand == null)
                    _invertImageCommand = new RelayCommand(InvertImage);
                return _invertImageCommand;
            }
        }

        private void InvertImage(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image !");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.Invert(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            else if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.Invert(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }
        #endregion

        #region Convert color image to grayscale image
        private ICommand _convertImageToGrayscaleCommand;
        public ICommand ConvertImageToGrayscaleCommand
        {
            get
            {
                if (_convertImageToGrayscaleCommand == null)
                    _convertImageToGrayscaleCommand = new RelayCommand(ConvertImageToGrayscale);
                return _convertImageToGrayscaleCommand;
            }
        }

        private void ConvertImageToGrayscale(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image !");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (ColorInitialImage != null)
            {
                GrayProcessedImage = Tools.Convert(ColorInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            else
            {
                MessageBox.Show("It is possible to convert only color images !");
            }
        }
        #endregion

        #region Rotate image

        #region Rotate clockwise
        private ICommand _rotateImageClockwiseCommand;
        public ICommand RotateImageClockwiseCommand
        {
            get
            {
                if (_rotateImageClockwiseCommand == null)
                    _rotateImageClockwiseCommand = new RelayCommand(RotateImageClockwise);
                return _rotateImageClockwiseCommand;
            }
        }

        private void RotateImageClockwise(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image !");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.RotateClockwise(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            else if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.RotateClockwise(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }
        #endregion

        #region Rotate anti-clockwise
        private ICommand _rotateImageAntiClockwiseCommand;
        public ICommand RotateImageAntiClockwiseCommand
        {
            get
            {
                if (_rotateImageAntiClockwiseCommand == null)
                    _rotateImageAntiClockwiseCommand = new RelayCommand(RotateImageAntiClockwise);
                return _rotateImageAntiClockwiseCommand;
            }
        }

        private void RotateImageAntiClockwise(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please add an image !");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.RotateAntiClockwise(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            else if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.RotateAntiClockwise(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }
        #endregion

        #endregion

        #region Mirror image vertically

        private ICommand _mirrorImageVertically;

        public ICommand MirrorImageVeritcally

        {
            get
            {
                if (_mirrorImageVertically == null)
                    _mirrorImageVertically = new RelayCommand(MirrorImageVeritcallyMethod);

                return _mirrorImageVertically;
            }
        }

        private void MirrorImageVeritcallyMethod(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please load an image!");
                return;
            }

            ClearProcessedCanvas(parameter);

            if (GrayInitialImage != null)
            {
                GrayProcessedImage = Tools.MirorImage(GrayInitialImage);
                ProcessedImage = Convert(GrayProcessedImage);
            }
            else if (ColorInitialImage != null)
            {
                ColorProcessedImage = Tools.MirorImage(ColorInitialImage);
                ProcessedImage = Convert(ColorProcessedImage);
            }
        }

        #endregion

        #region Binarizare

        private ICommand _binarizareImage;

        public ICommand BinarizareImage
        {
            get
            {
                if (_binarizareImage == null)
                    _binarizareImage = new RelayCommand(BinarizareImageMethod);

                return _binarizareImage;
            }
        }

        private void BinarizareImageMethod(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please load an image!");
                return;
            }

            ClearProcessedCanvas(parameter);

            List<string> paramass = new List<string>();
            paramass.Add("Threshold: ");

            DialogBox db = new DialogBox(_mainVM, paramass);
            db.ShowDialog();

            List<double> results = db.GetValues();

            if(results != null)
            {
                int threshold = (int)results[0];

                if (GrayInitialImage != null)
                {
                    GrayProcessedImage = Tools.BinarizareImagine(GrayInitialImage,threshold);
                    ProcessedImage = Convert(GrayProcessedImage);
                }
                else if (ColorInitialImage != null)
                {
                    ColorProcessedImage = Tools.BinarizareImagine(ColorInitialImage,threshold);
                    ProcessedImage = Convert(ColorProcessedImage);
                }
            }

        }

        #endregion

        #region Crop

        private ICommand _crop;

        public ICommand Crop
        {
            get
            {
                if (_crop == null)
                    _crop = new RelayCommand(CropMethod);

                return _crop;
            }
        }

        private void CropMethod(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please load an image!");
                return;
            }

            object[] canvases = (object[])parameter;

            ClearProcessedCanvas(canvases[1]);

            int nrOfMousePosition = VectorOfMousePosition.Count;

            if (nrOfMousePosition >= 2)
            {
                Point p1 = VectorOfMousePosition[nrOfMousePosition - 2];
                Point p2 = VectorOfMousePosition[nrOfMousePosition - 1];

                int topX = (int)System.Math.Min(p1.X, p2.X);
                int topY = (int)System.Math.Min(p1.Y, p2.Y);

                Tuple<int, int> top = new Tuple<int, int>(topX, topY);

                int bottomX = (int)System.Math.Max(p1.X, p2.X);
                int bottomY = (int)System.Math.Max(p1.Y, p2.Y);

                Tuple<int, int> bottom = new Tuple<int, int>(bottomX, bottomY);

                DrawRectangle((Canvas)canvases[0],
                    new Point(topX, topY),
                    new Point(bottomX, bottomY),
                    2,
                    Brushes.Blue,
                    ScaleValue);


                if (GrayInitialImage != null)
                {
                    GrayProcessedImage = Tools.Crop(GrayInitialImage,top,bottom);
                    ProcessedImage = Convert(GrayProcessedImage);
                }
                else if (ColorInitialImage != null)
                {
                    ColorProcessedImage = Tools.Crop(ColorInitialImage, top, bottom);
                    ProcessedImage = Convert(ColorProcessedImage);
                }
            }

        }

        #endregion


        #endregion



        #region Pointwise operations

        #region Binarizare

        private ICommand _colorContrastStretching;

        public ICommand ColorContrastStretching
        {
            get
            {
                if (_colorContrastStretching == null)
                    _colorContrastStretching = new RelayCommand(ColorContrastStretchingMethod);

                return _colorContrastStretching;
            }
        }

        private void ColorContrastStretchingMethod(object parameter)
        {
            if (InitialImage == null)
            {
                MessageBox.Show("Please load an imageT!");
                return;
            }

            ClearProcessedCanvas(parameter);

            List<string> paramass = new List<string>();
            paramass.Add("Threshold: ");

            DialogBox db = new DialogBox(_mainVM, paramass);
            db.ShowDialog();

            List<double> results = db.GetValues();

            if (results != null)
            {
                int threshold = (int)results[0];

            if (ColorInitialImage != null)
                {
                    ColorProcessedImage = PointwiseOperations.ColorContrastStretchingImg(ColorInitialImage);
                    ProcessedImage = Convert(ColorProcessedImage);
                }
            }

        }

        #endregion

        #endregion

        #region Thresholding
        #endregion

        #region Filters
        #endregion

        #region Morphological operations
        #endregion

        #region Geometric transformations
        #endregion

        #region Segmentation
        #endregion

        #region Save processed image as original image
        private ICommand _saveAsOriginalImageCommand;
        public ICommand SaveAsOriginalImageCommand
        {
            get
            {
                if (_saveAsOriginalImageCommand == null)
                    _saveAsOriginalImageCommand = new RelayCommand(SaveAsOriginalImage);
                return _saveAsOriginalImageCommand;
            }
        }

        private void SaveAsOriginalImage(object parameter)
        {
            if (ProcessedImage == null)
            {
                MessageBox.Show("Please process an image first.");
                return;
            }

            var canvases = (object[])parameter;

            ClearInitialCanvas(canvases[0] as Canvas);

            if (GrayProcessedImage != null)
            {
                GrayInitialImage = GrayProcessedImage;
                InitialImage = Convert(GrayInitialImage);
            }
            else if (ColorProcessedImage != null)
            {
                ColorInitialImage = ColorProcessedImage;
                InitialImage = Convert(ColorInitialImage);
            }

            ClearProcessedCanvas(canvases[1] as Canvas);
        }
        #endregion
    }
}