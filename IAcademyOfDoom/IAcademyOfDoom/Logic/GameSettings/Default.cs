namespace IAcademyOfDoom.Logic.GameSettings
{
    /// <summary>
    /// Logical defaults, change the values here.
    /// </summary>
    public static class Default
    {
        public static int Lines { get; } = 5;
        public static int Columns { get; } = 6;
        public static int SkillPoints { get; } = 20;
        public static int MaxSkillLevel { get; } = 10;
        public static int DieSize { get; } = 6;
        public static int TutorReward { get; } = 3;
        public static int TutorCost { get; } = 5;
        public static int ServiceCost { get; } = 5;
        public static int StudentCost { get; } = 5;
        public static int FacultyCost { get; } = 5;
        public static int BaseProfHitPoints { get; } = 50;
        public static int ExamDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 14;
                case Difficulty.Medium:
                    return 17;
                case Difficulty.Hard:
                    return 20;
                case Difficulty.Expert:
                    return 23;
                default:
                    return 0;
            }
        }
        public static int LessonDifficulty(Difficulty difficulty, int numberOfSkills)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 6 * numberOfSkills;
                case Difficulty.Medium:
                    return 9 * numberOfSkills;
                case Difficulty.Hard:
                    return 12 * numberOfSkills;
                case Difficulty.Expert:
                    return 15 * numberOfSkills;
                default:
                    return 0;
            }
        }
        public static int BaseHitPoints(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 10;
                case Difficulty.Medium:
                    return 8;
                case Difficulty.Hard:
                    return 6;
                case Difficulty.Expert:
                    return 4;
                default:
                    return 0;
            }
        }
        public static int BaseMoney(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 50;
                case Difficulty.Medium:
                    return 40;
                case Difficulty.Hard:
                    return 30;
                case Difficulty.Expert:
                    return 20;
                default:
                    return 0;
            }
        }
    }
}