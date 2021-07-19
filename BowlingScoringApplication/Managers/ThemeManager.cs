using System.Drawing;

namespace BowlingScoringApplication
{
    /// <summary>
    /// ThemeManager stores the Color theming used for the application.
    /// </summary>
    public static class ThemeManager
    {
        public static Color HeaderColor = ColorTranslator.FromHtml("#F1D1B5");
        public static Color BodyColor = ColorTranslator.FromHtml("#568EA6");
        public static Color[] RowColors = { ColorTranslator.FromHtml("#F1D1B5"), ColorTranslator.FromHtml("#F0B7A4") };
        public static Color ColumnHeadColor = ColorTranslator.FromHtml("#F18C8E");
        //RGB 48, 95, 114
    }
}
