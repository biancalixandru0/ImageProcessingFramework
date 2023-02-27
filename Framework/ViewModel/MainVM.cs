using System.Windows.Input;
using System.Windows.Media;

using Framework.Model;

namespace Framework.ViewModel
{
    public class MainVM : BaseVM
    {
        public MenuCommands MenuCommands { get; set; }

        public MainVM()
        {
            MenuCommands = new MenuCommands(this);
            ScaleValue = 1;

            string theme = Properties.Settings.Default.Theme;
            SetThemeMode(theme);
        }

        public void SetThemeMode(string theme)
        {
            switch (theme)
            {
                case "Lime Green":
                    Theme = LimeGreenTheme.Instance;
                    break;

                case "Forest Green":
                    Theme = ForestGreenTheme.Instance;
                    break;

                case "Pale Pink":
                    Theme = PalePinkTheme.Instance;
                    break;

                case "Lavender Violet":
                    Theme = LavenderVioletTheme.Instance;
                    break;

                case "Cobalt Blue":
                    Theme = CobaltBlueTheme.Instance;
                    break;
            }
        }

        private IThemeMode _theme;
        public IThemeMode Theme
        {
            get
            {
                return _theme;
            }
            set
            {
                _theme = value;
                NotifyPropertyChanged(nameof(Theme));
            }
        }

        private ImageSource _initialImage;
        public ImageSource InitialImage
        {
            get
            {
                return _initialImage;
            }
            set
            {
                _initialImage = value;

                if (_initialImage != null)
                {
                    OriginalCanvasWidth = InitialImage.Width * ScaleValue;
                    OriginalCanvasHeight = InitialImage.Height * ScaleValue;
                }
                else
                {
                    OriginalCanvasWidth = 0;
                    OriginalCanvasHeight = 0;
                }

                NotifyPropertyChanged(nameof(InitialImage));
            }
        }

        private ImageSource _processedImage;
        public ImageSource ProcessedImage
        {
            get
            {
                return _processedImage;
            }
            set
            {
                _processedImage = value;

                if (_processedImage != null)
                {
                    ProcessedCanvasWidth = ProcessedImage.Width * ScaleValue;
                    ProcessedCanvasHeight = ProcessedImage.Height * ScaleValue;
                }
                else
                {
                    ProcessedCanvasWidth = 0;
                    ProcessedCanvasHeight = 0;
                }

                NotifyPropertyChanged(nameof(ProcessedImage));
            }
        }

        private double _scaleValue;
        public double ScaleValue
        {
            get => _scaleValue;
            set
            {
                _scaleValue = value;
                NotifyPropertyChanged(nameof(ScaleValue));
            }
        }

        private string _xPos;
        public string XPos
        {
            get => _xPos;
            set
            {
                _xPos = value;
                NotifyPropertyChanged(nameof(XPos));
            }
        }

        private string _yPos;
        public string YPos
        {
            get => _yPos;
            set
            {
                _yPos = value;
                NotifyPropertyChanged(nameof(YPos));
            }
        }

        private string _grayValue;
        public string GrayValue
        {
            get => _grayValue;
            set
            {
                _grayValue = value;
                NotifyPropertyChanged(nameof(GrayValue));
            }
        }

        private string _redValue;
        public string RedValue
        {
            get => _redValue;
            set
            {
                _redValue = value;
                NotifyPropertyChanged(nameof(RedValue));
            }
        }

        private string _greenValue;
        public string GreenValue
        {
            get => _greenValue;
            set
            {
                _greenValue = value;
                NotifyPropertyChanged(nameof(GreenValue));
            }
        }

        private string _blueValue;
        public string BlueValue
        {
            get => _blueValue;
            set
            {
                _blueValue = value;
                NotifyPropertyChanged(nameof(BlueValue));
            }
        }

        #region Reset zoom
        private ICommand _resetZoomCommand;
        public ICommand ResetZoomCommand
        {
            get
            {
                if (_resetZoomCommand == null)
                    _resetZoomCommand = new RelayCommand(ResetZoom);

                return _resetZoomCommand;
            }
        }

        public void ResetZoom(object parameter)
        {
            ScaleValue = 1;
        }
        #endregion

        #region Canvases properties
        public double _originalCanvasWidth;
        public double OriginalCanvasWidth
        {
            get => _originalCanvasWidth;
            set
            {
                _originalCanvasWidth = value;
                NotifyPropertyChanged(nameof(OriginalCanvasWidth));
            }
        }

        public double _originalCanvasHeight;
        public double OriginalCanvasHeight
        {
            get => _originalCanvasHeight;
            set
            {
                _originalCanvasHeight = value;
                NotifyPropertyChanged(nameof(OriginalCanvasHeight));
            }
        }

        public double _processedCanvasWidth;
        public double ProcessedCanvasWidth
        {
            get => _processedCanvasWidth;
            set
            {
                _processedCanvasWidth = value;
                NotifyPropertyChanged(nameof(ProcessedCanvasWidth));
            }
        }

        public double _processedCanvasHeight;
        public double ProcessedCanvasHeight
        {
            get => _processedCanvasHeight;
            set
            {
                _processedCanvasHeight = value;
                NotifyPropertyChanged(nameof(ProcessedCanvasHeight));
            }
        }
        #endregion
    }
}