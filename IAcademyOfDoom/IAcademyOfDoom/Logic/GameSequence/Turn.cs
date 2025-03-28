using IAcademyOfDoom.Logic.Mobiles;
using System.Collections.Generic;

namespace IAcademyOfDoom.Logic.GameSequence
{
    /// <summary>
    /// A class containing the data for one wave.
    /// </summary>
    public class Turn
    {
        /// <summary>
        /// The number of repetitions (spawns).
        /// </summary>
        public int Reps { get; private set; }
        /// <summary>
        /// True iff reps are exhausted.
        /// </summary>
        public bool Finished { get { return Reps <= 0; } }
        /// <summary>
        /// For each spawn event, how many bots of each type to generate.
        /// </summary>
        public Dictionary<BotType, int> Bots { get; private set; }
        /// <summary>
        /// Parametered constructor
        /// </summary>
        /// <param name="reps">the repetitions</param>
        /// <param name="bots">botType to number of instances dictionary</param>
        public Turn(int reps, Dictionary<BotType, int> bots) {
            Reps = reps;
            Bots = bots;
        }
        /// <summary>
        /// Produces a new list of botlings or null.
        /// </summary>
        /// <returns>the list of botlings spawned, null if finished</returns>
        public List<Botling> SpawnOrNull()
        {
            List<Botling> res = null;
            if (Reps > 0)
            {
                res = new List<Botling>();
                foreach (BotType bot in Bots.Keys)
                {
                    for (int i = 0; i < Bots[bot]; i++)
                    {
                        if (bot is BotType.None)
                        {
                            res.Add(new Botling(bot));
                        }
                    }
                }
                Reps--;
            }
            return res;
        }
    }
}