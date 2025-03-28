using IAcademyOfDoom.App;
using IAcademyOfDoom.Logic.GameSettings;
using IAcademyOfDoom.Logic.Mobiles;
using IAcademyOfDoom.Logic.Skills;

namespace IAcademyOfDoom.Logic.Places
{
    /// <summary>
    /// A room for professors or tutors in a given skill.
    /// </summary>
    public class ProfRoom : Room
    {
        /// <summary>
        /// Specific: the hit points of the teacher in the room (0 or less: exhausted).
        /// </summary>
        public int HP {  get; set; }
        /// <summary>
        /// The skill taught in this room.
        /// </summary>
        public SkillType SkillType {  get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">the column</param>
        /// <param name="y">the row</param>
        public ProfRoom(int x, int y) : base(x, y)
        {
            Type = RoomType.Prof;
            HP = Default.BaseProfHitPoints;
        }
        /// <summary>
        /// Override method: a botling entering this room is lectured.
        /// The room may also be destroyed
        /// </summary>
        /// <param name="botling">the botling entering this room</param>
        /// <returns>the result of the lesson - actual type: bool</returns>
        public override object ActOnEntry(Botling botling)
        {
            HP--;
            if (HP <=0)
            {
                Controller.Instance.DestroyRoom(this);
            }
            return botling.GetLessonIn(SkillType);
        }
    }
}
