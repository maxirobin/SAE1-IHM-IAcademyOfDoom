using IAcademyOfDoom.App;
using IAcademyOfDoom.Logic.GameSequence;
using IAcademyOfDoom.Logic.GameSettings;
using IAcademyOfDoom.Logic.Mobiles;
using IAcademyOfDoom.Logic.Places;
using IAcademyOfDoom.Logic.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace IAcademyOfDoom.Logic
{
    /// <summary>
    /// The class representing the game.
    /// Normally, only one instance at any time
    /// </summary>
    public class Game
    {
        #region public static (class-level) properties
        /// <summary>
        /// A random number generator (used everywhere).
        /// </summary>
        public static Random Random { get; } = new Random();
        /// <summary>
        /// The difficulty of the game.
        /// </summary>
        public static Difficulty Difficulty {  get; set; } = Difficulty.Easy;
        /// <summary>
        /// The maximum X (highest index for columns, starting at 0).
        /// </summary>
        public static int MaxX { get; private set; } = Default.Columns - 1;
        /// <summary>
        /// The maximum Y (highest index for rows, starting at 0).
        /// </summary>
        public static int MaxY { get; private set; } = Default.Lines- 1;
        #endregion
        #region public read-only instance properties
        /// <summary>
        /// A list of buyable items.
        /// </summary>
        public List<Buyable> Buyables { get; } = new List<Buyable>();
        /// <summary>
        /// The current money of the player.
        /// </summary>
        public int Money { get; private set; }
        #endregion
        #region private attributes
        private Phase currentPhase = Phase.Preparation;
        private int waveNumber = 1;
        private Wave wave = null;
        private int successes;
        private int failures;
        private readonly List<Room> rooms = new List<Room>();
        private readonly List<Placeable> placeables = new List<Placeable>();
        private readonly List<Botling> botlings = new List<Botling>();
        private Controller c = Controller.Instance;
        #endregion
        #region constructor
        /// <summary>
        /// Constructor.
        /// Sets up base values
        /// </summary>
        public Game() { 
            rooms.Add(Room.SpawnArea());
            rooms.Add(Room.ExamRoom());
            placeables.Add(new Placeable(RoomType.Prof, SkillType.Classify, "Classification Professor"));
            placeables.Add(new Placeable(RoomType.Prof, SkillType.Produce, "Production Professor"));
            placeables.Add(new Placeable(RoomType.Prof, SkillType.Dialogue, "Dialogue Professor"));
            placeables.Add(new Placeable(RoomType.Prof, SkillType.Interpret, "Interpretation Professor"));
            placeables.Add(new Placeable(RoomType.Prof, SkillType.Synthetise, "Synthesis Professor"));
            placeables.Add(new Placeable(RoomType.Prof, SkillType.Present, "Presentation Professor"));
            Buyables.Add(new Buyable(RoomType.Prof, Default.TutorCost, "Analysys Tutor", SkillType.Analyse));
            Buyables.Add(new Buyable(RoomType.Prof, Default.TutorCost, "Recognition Tutor", SkillType.Recognise));
            Buyables.Add(new Buyable(RoomType.Prof, Default.TutorCost, "Generation Tutor", SkillType.Generate));
            Buyables.Add(new Buyable(RoomType.Prof, Default.TutorCost, "Communication Tutor", SkillType.Communicate));
            Buyables.Add(new Buyable(RoomType.Facility, Default.ServiceCost, "Orientation"));
            Buyables.Add(new Buyable(RoomType.Facility, Default.StudentCost, "Rest room"));
            Buyables.Add(new Buyable(RoomType.Facility, Default.StudentCost, "Party room"));
            Buyables.Add(new Buyable(RoomType.Facility, Default.FacultyCost, "Faculty lounge"));
            Money = Default.BaseMoney(Difficulty);
        }
        #endregion
        #region public methods
        /// <summary>
        /// Adds a room at one position given a placeable item.
        /// </summary>
        /// <param name="x">the column</param>
        /// <param name="y">the row</param>
        /// <param name="placeable">the placeable item</param>
        public void AddRoomHere(int x, int y, Placeable placeable)
        {
            rooms.Add(placeable.MakeRoom(x, y));
        }
        /// <summary>
        /// Provides a copy of the list of placeable items.
        /// </summary>
        /// <returns>a new list</returns>
        public List<Placeable> Placeables() => new List<Placeable>(placeables);
        public List<Room> Rooms()
        {
            return new List<Room>(rooms);
        }
        /// <summary>
        /// Attempts to place a placeable item at some position.
        /// </summary>
        /// <param name="x">the column</param>
        /// <param name="y">the row</param>
        /// <param name="placeable">the candidate placeable item</param>
        /// <returns>true iff the placeable has been correctly placed</returns>
        public bool PlaceThisHere(int x, int y, Placeable placeable)
        {
            if (placeables.Contains(placeable))
            {
                AddRoomHere(x, y, placeable);
                placeables.Remove(placeable);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Ends the preparation phase and goes into the assault phase.
        /// </summary>
        public void EndPreparations()
        {
        
            
                currentPhase = Phase.Assault;
                successes = failures = 0;
                wave = new Wave(waveNumber);
            
                
            
            
        }
        /// <summary>
        /// Progresses in the assault phase.
        /// </summary>
        public void NextInAssault()
        {
            bool change = false;
            List<Botling> spawnedNow=null;
            List<Botling> terminatedNow=new List<Botling>();
            if (wave != null && wave.Turn != null)
            {
                spawnedNow=wave.Turn.SpawnOrNull();
            }
            if (botlings.Count > 0)
            {
                foreach (Botling botling in botlings)
                {
                    botling.Move();
                    (int x, int y) = (botling.X, botling.Y);
                    Room entered = FindRoomAt(x, y);
                    object result = entered?.ActOnEntry(botling);
                    if (result is ExamResult examResult)
                    {
                        StoreExamResult(examResult);
                        terminatedNow.Add(botling);
                    }
                    else if (result is bool b)
                    {
                        c.LessonResult(botling, b);
                        if (!b && botling.HP <= 0)
                        {
                            StoreExamResult(ExamResult.Failure);
                            terminatedNow.Add(botling);
                        }
                    }
                }
                change = true;
            }
            if (spawnedNow?.Count > 0)
            {
                botlings.AddRange(spawnedNow);
                change = true;
            }
            if (change)
            {
                foreach(Botling removable in terminatedNow)
                {
                    botlings.Remove(removable);
                }
                c.BotRemove(terminatedNow);
                c.BotChange(botlings);
            }
            else
            {
                currentPhase = Phase.Result;
                wave = null;
                waveNumber++;
                c.EndAssault();
            }
        }
        /// <summary>
        /// Accesses the current results of the assault.
        /// </summary>
        /// <returns>a success - failures pair</returns>
        public (int successes, int failures) GetResults()
        {
            return (successes, failures);
        }
        /// <summary>
        /// Removes a room.
        /// </summary>
        /// <param name="profRoom">the room to remove</param>
        public void DestroyRoom(ProfRoom profRoom)
        {
            rooms.Remove(profRoom);
        }
        /// <summary>
        /// Goes to the next wave.
        /// </summary>
        /// <returns>false: game over</returns>
        public bool NextWave()
        {
            return waveNumber <= 6;
        }
        #endregion
        #region private methods
        private void StoreExamResult(ExamResult examResult)
        {
            switch (examResult)
            {
                case ExamResult.Success:
                    successes++;
                    break;
                case ExamResult.Failure:
                    failures++;
                    break;
            }
        }
        private Room FindRoomAt(int x, int y)
        {
            int i = 0;
            int index = -1;
            while (index == -1 && i < rooms.Count)
            {
                if (rooms[i]?.X==x && rooms[i]?.Y==y)
                {
                    index = i;
                }
                else
                {
                    i++;
                }
            }
            if (index == -1)
            {
                return null;
            }
            else
            {
                return rooms[index];
            }
        }
        #endregion
    }
}