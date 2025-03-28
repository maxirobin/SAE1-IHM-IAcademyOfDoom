namespace IAcademyOfDoom.Logic.Mobiles
{
    /// <summary>
    /// BotType enum.
    /// None: a regular bot
    /// Speedster: goes directly to the next room or edge in the current direction
    /// Perfectionnist: keep having lessons when failing
    /// Introvert: avoids to go in a space with other bots
    /// Lucky: gets better odds
    /// Aimless: can occasionnally go backwards
    /// Persistent: returns to the start if the exam is failed
    /// </summary>
    public enum BotType
    {
        None, Speedster, Perfectionnist, Introvert, Lucky, Aimless, Persistent
    }
}