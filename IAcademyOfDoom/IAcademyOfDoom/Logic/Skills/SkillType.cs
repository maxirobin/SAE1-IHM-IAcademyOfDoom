namespace IAcademyOfDoom.Logic.Skills
{
    /// <summary>
    /// Skill type enum.
    /// Basic skills: ARGComm
    /// Combo skills: ClassProdDISPres
    /// </summary>
    public enum SkillType
    {
        Analyse, Recognise, Generate, Communicate,
        Classify, // Analyse+Recognise
        Produce, // Analyse+Generate
        Dialogue, // Generate+Communicate
        Interpret, // Recognise+Communicate
        Synthetise, // Recognise+Generate
        Present // Analyse+Communicate
    }
}