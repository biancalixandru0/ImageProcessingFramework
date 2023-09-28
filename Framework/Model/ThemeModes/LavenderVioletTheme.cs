namespace Framework.Model
{
    internal class LavenderVioletTheme : IThemeMode
    {
        private LavenderVioletTheme() { }

        private static LavenderVioletTheme _instance;
        public static LavenderVioletTheme Instance
        {
            get
            {
                return _instance ?? (_instance = new LavenderVioletTheme());
            }
        }

        public string MenuBackgroundColor => "#5E548E";

        public string MenuItemBackgroundColor => "#BE95C4";

        public string MenuItemForegroundColor => "#1E1E1E";

        public string BackgroundColor => "#E0B1CB";

        public string CanvasBackgroundColor => "#BE95C4";

        public string CanvasBorderBrush => "#231942";

        public string TextForeground => "#1E1E1E";

        public string ButtonBackgroundColor => "#BE95C4";

        public string ButtonForegroundColor => "#1E1E1E";

        public string ButtonBorderBrush => "#9F86C0";
    }
}