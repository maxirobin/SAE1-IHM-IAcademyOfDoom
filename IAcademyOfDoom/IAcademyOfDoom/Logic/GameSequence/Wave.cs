using IAcademyOfDoom.Logic.Mobiles;
using System.Collections.Generic;

namespace IAcademyOfDoom.Logic.GameSequence
{
    /// <summary>
    /// A class containing the initialisation for turns.
    /// </summary>
    public class Wave
    {
        /// <summary>
        /// The current turn, null if the game is over/not started.
        /// </summary>
        public Turn Turn { get; private set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="waveNumber">the number of the current wave.</param>
        public Wave(int waveNumber) {
            Turn = null;
            switch (waveNumber)
            {
                case 1:
                    Turn = new Turn(8, new Dictionary<BotType, int>() { { BotType.None, 4 } });
                    break;
                case 2:
                    Turn = new Turn(8, new Dictionary<BotType, int>() { { BotType.None, 5} });
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                    Turn = new Turn(10, new Dictionary<BotType, int>() { { BotType.None, 5 } });
                    break;
                default:
                    break;
            }
        }
    }
}