using IAcademyOfDoom.App;
using IAcademyOfDoom.Logic.GameSettings;
using IAcademyOfDoom.Logic.Mobiles;
using IAcademyOfDoom.Logic.Places;
using IAcademyOfDoom.Logic.Skills;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IAcademyOfDoom.View
{
    /// <summary>
    /// The main window.
    /// </summary>
    public partial class MainWindow : Form
    {
        #region private attributes
        private Controller c = Controller.Instance;
        private readonly List<BotlingView> bots = new List<BotlingView>();
        private readonly List<RoomView> rooms = new List<RoomView>();
        private readonly List<PlaceableView> placeables = new List<PlaceableView>();
        #endregion
        #region constructor
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public MainWindow()
        {
            Difficulty? difficulty = null;
            string name = null;
            /*DifficultySelect select = new DifficultySelect();
            if (select.ShowDialog()==DialogResult.OK)
            {
                name = select.InputName;
                difficulty = select.Difficulty;
            }*/
            InitializeComponent();
            c.Associate(this, name, difficulty);
            if (c.Name!=null)
            {
                playerNameLabel.Text = c.Name+"'s game";
                playerNameLabel.Visible = true;
            } else
            {
                playerNameLabel.Visible=false;
            }
        }
        #endregion
        #region event handling methods
        /// <summary>
        /// Event handling: Paint
        /// </summary>
        /// <param name="sender">ignored</param>
        /// <param name="e">used to get the graphic context</param>
        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            numberOfBotlingsContentLabel.Text = bots.Count.ToString();
            foreach (PlaceableView placeable in placeables)
            {
                placeable.Draw(e.Graphics);
            }
            BackgroundGrid(e.Graphics);
            SyncRooms();
            foreach (RoomView room in rooms)
            {
                room.Draw(e.Graphics);
            }
            foreach (BotlingView bot in bots)
            {
                bot.Draw(e.Graphics);
            }
        }
        private void EndPrepButton_Click(object sender, EventArgs e)
        {
            if (c.CanEndPreparations()) { c.EndPreparations(); }
            else { WriteLine("Preparations are not complete yet."); }
        }
        /// <summary>
        /// Event handling: click on next in assault button.
        /// </summary>
        /// <param name="sender">ignored</param>
        /// <param name="e">ignored</param>
        private void NextInAssaultButton_Click(object sender, EventArgs e)
        {
            c.NextInAssault();
        }
        /// <summary>
        /// Event handling: click on quit button.
        /// </summary>
        /// <param name="sender">ignored</param>
        /// <param name="e">ignored</param>
        private void QuitButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        /// <summary>
        /// Event handling: Mouse button down
        /// </summary>
        /// <param name="sender">ignore</param>
        /// <param name="e">used for pointer location and mouse button id</param>
        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            (int x, int y) = PointCoordinates(e.Location);
            if (e.Button == MouseButtons.Left && endPrepButton.Enabled)
            {
                if (!c.CanEndPreparations() && c.Placeables().Count > 0 && RoomHere(e.Location) == null &&
                    !(x, y).Equals((-1, -1)))
                {
                    Placeable placeable = c.Placeables()[0];
                    c.PlaceHere(x, y, placeable);
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                Botling target = BotlingHere(e.Location);
                if (target != null)
                {
                    DisplayStateOf(target);
                }
            }
        }
        #endregion
        #region public methods
        /// <summary>
        /// Method called by the controller to set the window to assault mode.
        /// </summary>
        public void SetToAssault()
        {
            endPrepButton.Enabled = false;
            nextInAssaultButton.Enabled = true;
            WriteLine("Assault!");
        }
        /// <summary>
        /// Method called by the controller to update the display in asault mode.
        /// </summary>
        public void AssaultUpdate()
        {
            if (nextInAssaultButton.Enabled)
            {
                WriteLine("Assault continuation!");
            }
        }
        /// <summary>
        /// Method called by the controller to set the window to results mode.
        /// </summary>
        /// <param name="results">the results of the previous wave, as a pair</param>        
        public void DisplayResults((int successes, int failures) results)
        {
            WriteLine($"Assault ended! {results.successes} successes, {results.failures} failures.");
            endPrepButton.Enabled = true;
            nextInAssaultButton.Enabled = false ;
            Refresh();
            c.NextWave();
        }
        /// <summary>
        /// Method called by the controller to update the botling mobiles.
        /// </summary>
        /// <param name="botlings"></param>
        public void UpdateBots(List<Botling> botlings)
        {
            Dictionary<(int x, int y), List<Botling>> newBotlingsByRoom = new Dictionary<(int x, int y), List<Botling>>();
            Dictionary<(int x, int y), List<Botling>> oldBotlingsByRoom = new Dictionary<(int x, int y), List<Botling>>();
            foreach (Botling botling in botlings)
            {
                bool add = true;
                int i = 0;
                while (i < bots.Count && add)
                {
                    if (bots[i].Botling.Equals(botling))
                    {
                        add = false;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (add)
                {
                    PutBotlingInListByRoom(newBotlingsByRoom, botling);
                    WriteLine(botling.Name + ": New botling at:" + (botling.X, botling.Y));
                }
                else
                {
                    PutBotlingInListByRoom(oldBotlingsByRoom, botling);
                    WriteLine(botling.Name + ": Botling move to:" + (botling.X, botling.Y));
                }
            }
            foreach((int x, int y) in newBotlingsByRoom.Keys)
            {
                int deltaX = 0, deltaY = 0;
                foreach (Botling botling in newBotlingsByRoom[(x, y)])
                {
                    Point point = ConvertCoordinates(x, y);
                    bots.Add(new BotlingView(new Point(point.X+deltaX, point.Y+deltaY), botling));
                    deltaX += Settings.BotlingSize.Width*2;
                    if (deltaX > Settings.Width-Settings.BotlingSize.Width)
                    {
                        deltaX = 0;
                        deltaY += Settings.BotlingSize.Height * 2;
                    }
                }
            }
            foreach ((int x, int y) in oldBotlingsByRoom.Keys)
            {
                int deltaX = 0, deltaY = 0;
                foreach (Botling botling in oldBotlingsByRoom[(x, y)])
                {
                    BotlingView view = null;
                    int i = 0;
                    while (i < bots.Count && view==null)
                    {
                        if (bots[i].Botling.Equals(botling))
                        {
                            view = bots[i];
                        }
                        else
                        {
                            i++;
                        }
                    }
                    if (view != null)
                    {
                        (int baseX, int baseY) = ConvertCoordinates(view.Location);
                        Point newLoc = ConvertCoordinates(x, y);
                        Point arrival = new Point(newLoc.X+(baseX+deltaX)%Settings.Width, newLoc.Y + (baseY + deltaY) % Settings.Width);
                        view.Location = arrival;
                        deltaX += Settings.BotlingSize.Width * 2;
                        if (baseX + deltaX > Settings.Width-Settings.BotlingSize.Width)
                        {
                            deltaX = 0;
                            deltaY += Settings.BotlingSize.Height * 2;
                        }
                    }
                }
            }
            Refresh();
        }
        /// <summary>
        /// Writes to the output list box.
        /// </summary>
        /// <param name="s">the string to be written</param>
        public void WriteLine(string s)
        {
            List<string> strs = s.Split('\n').ToList();
            foreach (string str in strs)
            {
                outputListBox.Items.Add(str);
            }
            if (outputListBox.Items.Count > 0)
            {
                outputListBox.SelectedIndex = outputListBox.Items.Count - 1;
            }
            outputListBox.Refresh();
        }
        /// <summary>
        /// Converts a logical pair of coordinates to a graphic point.
        /// </summary>
        /// <param name="x">the column id</param>
        /// <param name="y">the row id</param>
        /// <returns>the top left corner of the graphical cell or a problematic point if outside of the range</returns>
        public static Point ConvertCoordinates(int x, int y)
        {
            return new Point(Settings.Left + x * Settings.Width, Settings.Top + y * Settings.Height);
        }
        /// <summary>
        /// Converts a point to the offset from the top-left corner of its cell.
        /// </summary>
        /// <param name="point">the point</param>
        /// <returns>a pair of offset coordinates, in pixels</returns>
        public static (int x, int y) ConvertCoordinates(Point point)
        {
            return ((point.X - Settings.Left) % Settings.Width, (point.Y - Settings.Top) % Settings.Height);
        }
        /// <summary>
        /// Method called by the controller to remove some botlings.
        /// </summary>
        /// <param name="removed">a list of logical botlings to remove</param>
        public void RemoveBots(List<Botling> removed)
        {
            List<BotlingView> views = new List<BotlingView>();
            foreach (Botling bot in removed)
            {
                foreach (BotlingView view in bots)
                {
                    if (view.Botling.Equals(bot))
                    {
                        views.Add(view);
                    }
                }
            }
            foreach (BotlingView view in views)
            {
                bots.Remove(view);
            }
        }
        /// <summary>
        /// Method called by the controller to update the placeable items.
        /// </summary>
        /// <param name="placeables">the current list of placeables</param>
        public void PreviewPlaceableItems(List<Placeable> placeables)
        {
            this.placeables.Clear();
            if (placeables.Count == 0)
            {
                WriteLine("All items placed !");
                return;
            }

            string items = "";
            int x = Settings.PlaceableLeft;
            int y = Settings.PlaceableTop;

            foreach (Placeable placeable in placeables)
            {
                items += " " + placeable.ToString();
                this.placeables.Add(new PlaceableView(placeable, new Point(x, y)));
                y += Settings.PlaceableOffset;
            }
            WriteLine("Preparations: please place the following...");
            WriteLine("Items:"+items);
            Refresh();
        }
        /// <summary>
        /// Method displaying the current status of a logical botling mobile.
        /// </summary>
        /// <param name="botling">the logical botling</param>
        public void DisplayStateOf(Botling botling)
        {
            string name = botling.Name;
            string hp = botling.HP.ToString() + " HP";
            string skills = "Skills: ";
            foreach (SkillType skill in botling.Skills.Keys)
            {
                skills += "[" + skill.ToString() + "]" + "=" + botling.Skills[skill] + " ";
            }
            string badges = "Badges:";
            foreach (SkillType skill in botling.Badges.Keys)
            {
                if (botling.Badges[skill])
                {
                    badges += "[" + skill.ToString() + "] ";
                }
            }
            if (badges.EndsWith(":"))
            {
                badges += " none";
            }
            WriteLine("Botling " + name + ": " + hp);
            WriteLine("  " + skills);
            WriteLine("  " + badges);
        }
        /// <summary>
        /// Method called by the controller when the game is over.
        /// </summary>
        public void GameOver()
        {
            WriteLine("Game over.");
            nextInAssaultButton.Enabled = false;
            endPrepButton.Enabled = false;
            quitButton.Enabled = true;
            quitButton.Visible = true;
        }
        #endregion
        #region private mehods
        private Botling BotlingHere(Point location)
        {
            int i = 0;
            int index = -1;
            while (index == -1 && i < bots.Count)
            {
                if (bots[i].Contains(location))
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
                return bots[index].Botling;
            }
        }
        private (int x,  int y) PointCoordinates(Point point)
        {
            int posX = point.X;
            int posY = point.Y;
            if (posX >= Settings.Left && posY >= Settings.Top &&
                posX < Settings.Width * Settings.Cols + Settings.Left &&
                posY < Settings.Height * Settings.Rows + Settings.Top)
            {
                return ((posX - Settings.Left) / Settings.Width, (posY - Settings.Top) / Settings.Height);
            }
            else
            {
                return (-1, -1);
            }
        }
        private void SyncRooms()
        {
            foreach (Room r in c.Rooms())
            {
                bool add = true;
                int i = 0;
                while (i < rooms.Count && !add)
                {
                    if (rooms[i].Room.Equals(r))
                    {
                        add = false;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (add)
                {
                    rooms.Add(RoomView.CreateFromRoom(r));
                }
            }
            List<RoomView> checkList = new List<RoomView>(rooms);
            foreach (RoomView view in checkList)
            {
                if (!c.Rooms().Contains(view.Room))
                {
                    rooms.Remove(view);
                }
            }
        }
        private static void BackgroundGrid(Graphics graphics)
        {
            for (int i = 0; i < Settings.Cols; i++)
            {
                for (int j = 0; j < Settings.Rows; j++)
                {
                    Rectangle r = new Rectangle(ConvertCoordinates(i, j), new Size(Settings.Width, Settings.Height));
                    graphics.DrawRectangle(Settings.Pen, r);
                }
            }
        }
        private RoomView RoomHere(Point location)
        {
            int i = 0;
            int index = -1;
            while (index == -1 && i < rooms.Count)
            {
                if (rooms[i].Contains(location))
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
        private void PutBotlingInListByRoom(Dictionary<(int x, int y), List<Botling>> list, Botling botling)
        {
            if (!list.ContainsKey((botling.X, botling.Y)))
            {
                list.Add((botling.X, botling.Y), new List<Botling>());
            }
            list[(botling.X, botling.Y)].Add(botling);
        }
        #endregion
    }
}