using IAcademyOfDoom.Logic.Places;
using System.Drawing;

namespace IAcademyOfDoom.View
{
    public class RoomView
    {
        public int? Row { get; set; } = null;
        public int? Col { get; set; } = null;
        public Point Location { get; set; }
        public string Label { get; set; }
        public Color BackColour { get; set; }
        public Room Room { get; set; } = null;
        public RoomView(Point location, string label, Color backColour)
        {
            Location = location;
            label = label.Length > 12 ? label.Insert(12,"\n") : label;
            Label = label;
            BackColour = backColour;
        }
        public static RoomView CreateFromRoom(Room r)
        {
            Point p = MainWindow.ConvertCoordinates(r.X, r.Y);
            return new RoomView(p, r.Name, Settings.GetRoomColourFor(r.Type))
            {
                Room = r
            };
        }
        /// <summary>
        /// Method displaying the graphics.
        /// </summary>
        /// <param name="graphics">a reference to the graphic context to be used</param>
        public void Draw(Graphics graphics)
        {
            Rectangle r = new Rectangle(Location, new Size(Settings.Width, Settings.Height));
            graphics.FillRectangle(new SolidBrush(BackColour), r);
            graphics.DrawString(Label, Settings.RoomFont, Settings.TextBrush, new Point(r.X+Settings.TextOffset.Width, r.Y+Settings.TextOffset.Height));
        }
        /// <summary>
        /// Method checking whether a point is contained in the representation (bounding box).
        /// </summary>
        /// <param name="point">the point</param>
        /// <returns>true iff the point is within the graphic representation of the object</returns>
        public bool Contains(Point p)
        {
            return new Rectangle(Location, new Size(Settings.Width, Settings.Height)).Contains(p);
        }
    }
}