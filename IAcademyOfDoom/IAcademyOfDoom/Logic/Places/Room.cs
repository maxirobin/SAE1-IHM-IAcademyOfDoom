using IAcademyOfDoom.Logic.Mobiles;

namespace IAcademyOfDoom.Logic.Places
{
    /// <summary>
    /// A class modelling a room placed in a cell of the grid.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// The column.
        /// </summary>
        public int X {  get; private set; }
        /// <summary>
        /// The row.
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// The type of the room.
        /// </summary>
        public RoomType Type { get; internal set; }
        public string Name { get; internal set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">the column</param>
        /// <param name="y">the row</param>
        public Room(int x, int y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Factory method for the spawn area (top-left corner).
        /// </summary>
        /// <returns>a new spawn area room</returns>
        public static Room SpawnArea()
        {
            Room r = new Room(0, 0)
            {
                Name = "Spawn area",
                Type = RoomType.Cycle
            };
            return r;
        }
        /// <summary>
        /// Factory method for the exam room (bottom-right corner).
        /// </summary>
        /// <returns>a new exam room</returns>
        public static Room ExamRoom()
        {
            Room r = new Room(Game.MaxX, Game.MaxY)
            {
                Name = "Examination",
                Type = RoomType.Cycle
            };
            return r;
        }
        /// <summary>
        /// What happens when a botling enters this room.
        /// Virtual: can be overriden by subclasses; this code
        /// is only suitable for the exam room...
        /// </summary>
        /// <param name="botling">the botling entering the room</param>
        /// <returns>the result of the exam - actual type: ExamResult</returns>
        public virtual object ActOnEntry(Botling botling)
        {
            return botling.Exam();
        }
    }
}
