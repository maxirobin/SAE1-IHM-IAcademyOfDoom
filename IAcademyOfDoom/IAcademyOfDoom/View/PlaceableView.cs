using IAcademyOfDoom.Logic.Places;
using System.Drawing;

namespace IAcademyOfDoom.View
{
    /// <summary>
    /// A class giving the graphic representation of a placeable item.
    /// </summary>
    public class PlaceableView
    {
        /// <summary>
        /// The logical placeable item refered by this graphic object.
        /// </summary>
        public Placeable Placeable { get; private set; }
        /// <summary>
        /// The location - top left corner.
        /// </summary>
        public Point Location { get; private set; }
        /// <summary>
        /// Parametered constructor.
        /// </summary>
        /// <param name="placeable">the logical placeable item</param>
        /// <param name="location">the location (top left corner) of the display</param>
        public PlaceableView(Placeable placeable, Point location)
        {
            Placeable = placeable;
            Location = location;
        }
        /// <summary>
        /// Method displaying the graphics.
        /// </summary>
        /// <param name="graphics">a reference to the graphic context to be used</param>
        public void Draw(Graphics graphics)
        {
            Point p = new Point(Location.X + Settings.TextOffset.Width, Location.Y);
            Rectangle rectangle = new Rectangle(Location, Settings.PlaceableSquare);
            graphics.FillRectangle(Settings.PlaceableSquareBrush, rectangle);
            graphics.DrawString("Placeable: " + Placeable.ToString(), Settings.RoomFont, Settings.TextBrush, p);
        }
        /// <summary>
        /// Method checking whether a point is on the square next to the placeable item's display.
        /// </summary>
        /// <param name="point">the point</param>
        /// <returns>true iff the point is inside the square</returns>
        public bool OnSquare(Point point)
        {
            Rectangle rectangle = new Rectangle(Location, Settings.PlaceableSquare);
            return rectangle.Contains(point);
        }
    }
}
