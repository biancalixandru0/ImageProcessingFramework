namespace Framework.Model
{
    public interface IThemeMode
    {
        string MenuBackgroundColor { get; }
        string MenuItemBackgroundColor { get; }
        string MenuItemForegroundColor { get; }

        string BackgroundColor { get; }
        string CanvasBackgroundColor { get; }
        string CanvasBorderBrush { get; }
        string TextForeground { get; }

        string ButtonBackgroundColor { get; }
        string ButtonForegroundColor { get; }
        string ButtonBorderBrush { get; }
    }
}
