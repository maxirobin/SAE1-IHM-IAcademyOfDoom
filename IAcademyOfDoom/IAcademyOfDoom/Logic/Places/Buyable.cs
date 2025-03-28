using IAcademyOfDoom.Logic.Skills;

namespace IAcademyOfDoom.Logic.Places
{
    /// <summary>
    /// Class stub for a possible buyable item that could be placed.
    /// </summary>
    public class Buyable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="roomType"></param>
        /// <param name="price"></param>
        /// <param name="name"></param>
        /// <param name="skill"></param>
        public Buyable(RoomType roomType, int price, string name=null, SkillType? skill = null)
        {

        }
        /// <summary>
        /// Turns this in a placeable item.
        /// </summary>
        /// <returns>null</returns>
        public Placeable MakePlaceable() { return null; }
    }
}