using IAcademyOfDoom.Logic.Mobiles;
using IAcademyOfDoom.Logic.Places;
using System.Drawing;

namespace IAcademyOfDoom.View
{
    /// <summary>
    /// This non-instanciated class contains the settings for the graphics display.
    /// </summary>
    public static class Settings
    {
        public static int Cols { get; } = 6;
        public static int Rows { get; } = 5;
        public static int Width { get; } = 80;
        public static int Height { get; } = 70;
        public static int Left { get; } = 40;
        public static int Top { get; } = 100;
        public static Pen Pen { get; } = Pens.Black;
        public static Size BotlingSize { get; } = new Size(10, 10);
        public static Size TextOffset { get; } = new Size(5, Height/2);
        public static Brush TextBrush { get; } = Brushes.Black;
        public static Font RoomFont { get; } = SystemFonts.IconTitleFont;
        public static int PlaceableLeft = Left + Width * Cols + 50;
        public static int PlaceableTop = Top + 50;
        public static Size PlaceableSquare { get; } = new Size(7, 7);
        public static Brush PlaceableSquareBrush { get; } = Brushes.Firebrick;
        public static int PlaceableOffset { get; } = 20;
        public static Color GetRoomColourFor(RoomType type)
        {
            switch (type)
            {
                case RoomType.Cycle:
                    return Color.SlateGray;
                case RoomType.Prof:
                    return Color.Aquamarine;
                case RoomType.Facility:
                    return Color.LightYellow;
                default:
                    return Color.LightGray;
            }
        }
        public static Color GetBotColourFor(BotType type)
        {
            switch (type)
            {
                case BotType.None:
                    return Color.CadetBlue;
                default:
                    return Color.Black;
            }
        }
    }
}
