namespace Framework.Model
{
    internal class LimeGreenTheme : IThemeMode
    {
        private LimeGreenTheme() { }

        private static LimeGreenTheme _instance;
        public static LimeGreenTheme Instance
        {
            get
            {
                return _instance ?? (_instance = new LimeGreenTheme());
            }
        }

        public string MenuBackgroundColor => "#2F9C95";

        public string MenuItemBackgroundColor => "#A3F7B5";

        public string MenuItemForegroundColor => "#1E1E1E";

        public string BackgroundColor => "#E5F9E0";

        public string CanvasBackgroundColor => "#A3F7B5";

        public string CanvasBorderBrush => "#664147";

        public string TextForeground => "#1E1E1E";

        public string ButtonBackgroundColor => "#A3F7B5";

        public string ButtonForegroundColor => "#1E1E1E";

        public string ButtonBorderBrush => "#40C9A2";
    }
}