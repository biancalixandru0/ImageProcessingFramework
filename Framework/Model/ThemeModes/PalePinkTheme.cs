namespace Framework.Model
{
    internal class PalePinkTheme : IThemeMode
    {
        private PalePinkTheme() { }

        private static PalePinkTheme _instance;
        public static PalePinkTheme Instance
        {
            get
            {
                return _instance ?? (_instance = new PalePinkTheme());
            }
        }

        public string MenuBackgroundColor => "#FB6376";

        public string MenuItemBackgroundColor => "#FFDCCC";

        public string MenuItemForegroundColor => "#1E1E1E";

        public string BackgroundColor => "#FFF9EC";

        public string CanvasBackgroundColor => "#FFDCCC";

        public string CanvasBorderBrush => "#5D2A42";

        public string TextForeground => "#1E1E1E";

        public string ButtonBackgroundColor => "#FFDCCC";

        public string ButtonForegroundColor => "#1E1E1E";

        public string ButtonBorderBrush => "#FCB1A6";
    }
}