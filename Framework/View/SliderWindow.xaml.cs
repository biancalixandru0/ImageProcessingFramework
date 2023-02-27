using Emgu.CV;
using System;
using System.Windows;

using Framework.ViewModel;
using static Framework.Utilities.DataProvider;

namespace Framework.View
{
    public partial class SliderWindow : Window
    {
        private readonly SliderVM _sliderVM;

        public SliderWindow(MainVM mainVM, string description)
        {
            InitializeComponent();

            SliderOn = true;

            _sliderVM = new SliderVM(mainVM);

            _sliderVM.Description = description;

            DataContext = _sliderVM;
        }

        public void SetWindowData<InColor, OutColor>(Image<InColor, byte> image,
            Func<Image<InColor, byte>, double, Image<OutColor, byte>> algorithm)
            where InColor : struct, IColor
            where OutColor : struct, IColor
        {
            _sliderVM.Image = image;
            _sliderVM.Algorithm = algorithm;
        }

        public void ConfigureSlider(double minimumValue = 0, double maximumValue = 255, double value = 0, double frequency = 5)
        {
            _sliderVM.MinimumValue = minimumValue;
            _sliderVM.MaximumValue = maximumValue;
            _sliderVM.Value = value;
            _sliderVM.Frequency = frequency;
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SliderOn = false;
        }
    }
}