using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Framework.View;
using Framework.Model;
using Framework.Utilities;

namespace Framework.ViewModel
{
    class DialogBoxVM : BaseVM
    {
        public void CreateParameters(List<string> texts)
        {
            Height = (texts.Count + 3) * 40;

            foreach (var text in texts)
            {
                Parameters.Add(new DialogBoxParameter()
                {
                    ParamText = text,
                    Foreground = Theme.TextForeground,
                    Height = 20,
                });
            }
        }

        public List<double> GetValues()
        {
            var values = new List<double>();

            foreach (var parameter in Parameters)
            {
                string text = parameter.InputText;
                if (text == null || text.Trim().Length == 0 || IsNumeric(text) == false)
                    values.Add(0);
                else
                    values.Add(double.Parse(text));
            }

            return values;
        }

        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        #region Properties and commands
        public double Height { get; set; }

        public IThemeMode Theme { get; set; } =
            LimeGreenTheme.Instance;

        public ObservableCollection<DialogBoxParameter> Parameters { get; } =
            new ObservableCollection<DialogBoxParameter>();

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(p =>
                    {
                        DataProvider.CloseWindow<DialogBox>();
                    });

                return _closeCommand;
            }
        }
        #endregion
    }
}