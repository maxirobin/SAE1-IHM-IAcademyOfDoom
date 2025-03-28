namespace IAcademyOfDoom.Logic
{
    /// <summary>
    /// A class for the production of up to 26000 different bot names.
    /// </summary>
    public static class Names
    {
        private readonly static string[] names = { "Alice", "Bob", "Carol", "Dave", "Eve", "Fabrice", "Gertrude",
         "Horace", "Isabel", "Jack",
        "Karin", "Lars", "Morgana", "Neville", "Ophelia", "Patrick", "Querre", "Rob", "Sally", "Terrence", "Ursula", "Vic",
        "Wisteria", "Xiaomei", "Ysengrin", "Zephyra" };
        private static int index = 0;
        private static int suffix = 0;
        /// <summary>
        /// Method that provides a new bot name.
        /// </summary>
        /// <returns>the next name (% 26000)</returns>
        public static string Next()
        { 
            if (index==names.Length)
            {
                index = 0;
                suffix++;
            }
            return names[index++]+suffix.ToString("000");
        }
    }
}
