using IAcademyOfDoom.Logic;
using IAcademyOfDoom.Logic.GameSettings;
using IAcademyOfDoom.Logic.Mobiles;
using IAcademyOfDoom.Logic.Places;
using IAcademyOfDoom.View;
using System.Collections.Generic;

namespace IAcademyOfDoom.App
{
    /// <summary>
    /// The controller. One single instance serving as in-between for the logic and view parts.
    /// </summary>
    public class Controller
    {
        public string Name { get; set; }
        #region attributes, properties, constructor and method for the single instance
        private Game game;
        private MainWindow window;
        /// <summary>
        /// The single instance of the controller.
        /// </summary>
        public static Controller Instance { get; private set; } = new Controller();
        private Controller()
        {
        }
        /// <summary>
        /// Method used to set the game and the window.
        /// </summary>
        /// <param name="window">the window (from which this method is called)</param>
        /// <param name="name">optional name for the player</param>
        /// <param name="difficulty">optional difficulty for the game</param>
        public void Associate(MainWindow window, string name=null, Difficulty? difficulty=null)
        {
            this.window = window;
            game = new Game();
            if (name != null)
            {
                Name = name;
            }
            if (difficulty != null)
            {
                Game.Difficulty = difficulty.Value;
            }
            window.WriteLine("Init new game...");
            window.PreviewPlaceableItems(game.Placeables());
        }
        #endregion
        #region public methods
        /// <summary>
        /// Bridge to the same method in the game.
        /// </summary>
        /// <returns>the game's list of rooms</returns>
        public List<Room> Rooms() => game.Rooms();
        /// <summary>
        /// Bridge to the same method in the game.
        /// </summary>
        /// <returns>the game's list of placeables</returns>
        public List<Placeable> Placeables() => game.Placeables();
        /// <summary>
        /// Method called by the window when preparations are over.
        /// </summary>
        public void EndPreparations()
        {
            window.WriteLine("Preparations ended.");
            game.EndPreparations();
            window.SetToAssault();
        }
        /// <summary>
        /// Method called by the window when the assault continues.
        /// </summary>
        public void NextInAssault()
        {
            game.NextInAssault();
            window.AssaultUpdate();
        }
        /// <summary>
        /// Method called by the game at the end of the assault phase.
        /// </summary>
        public void EndAssault()
        {
            window.DisplayResults(game.GetResults());
        }
        /// <summary>
        /// Method called whenever botling mobiles should be updated.
        /// </summary>
        /// <param name="botlings">the current list of botlings</param>
        public void BotChange(List<Botling> botlings)
        {
            window.UpdateBots(botlings);
        }
        /// <summary>
        /// Method called whenever botling mobiles should be removed.
        /// </summary>
        /// <param name="botlings">the list of botlings to remove</param>
        public void BotRemove(List<Botling> removed)
        {
            window.RemoveBots(removed);
        }
        /// <summary>
        /// Method checking whether all mandatory placeable items are placed.
        /// </summary>
        /// <returns>true iff preparations can be ended</returns>
        public bool CanEndPreparations()
        {
            return game.Placeables().Count == 0;
        }
        /// <summary>
        /// Method called to place a placeable item.
        /// </summary>
        /// <param name="x">the logical column number</param>
        /// <param name="y">the logical row number</param>
        /// <param name="placeable">the placeable item</param>
        public void PlaceHere(int x, int y, Placeable placeable)
        {
            game.PlaceThisHere(x, y, placeable);
            window.WriteLine("Placed:" + placeable.ToString());
            window.PreviewPlaceableItems(game.Placeables());
            window.Refresh();
        }
        /// <summary>
        /// Method called from the game when a botling's lesson has ended.
        /// </summary>
        /// <param name="botling">the botling</param>
        /// <param name="b">true iff the lesson was successful</param>
        public void LessonResult(Botling botling, bool b)
        {
            window.WriteLine(botling.Name + " was lectured!" + (b ? " And succeeded!" : " ...And failed."));
            window.DisplayStateOf(botling);
        }
        /// <summary>
        /// Method called from the game when a prof room is removed.
        /// </summary>
        /// <param name="profRoom">the room being removed</param>
        public void DestroyRoom(ProfRoom profRoom)
        {
            game.DestroyRoom(profRoom);
            window.WriteLine(profRoom.Name + ", exhausted, retires after one final lesson.");
        }
        /// <summary>
        /// Method used to make the game advance to the next wave.
        /// </summary>
        public void NextWave()
        {
            bool b = game.NextWave();
            if (b)
            {
                window.WriteLine("Next wave !");
                window.PreviewPlaceableItems(game.Placeables());
            }
            else
            {
                window.GameOver();
            }
        }
        #endregion
    }
}