using System.Collections.Generic;

namespace IAcademyOfDoom.Logic.Skills
{
    /// <summary>
    /// An helper class for skill types
    /// </summary>
    public static class SkillTypeUtils
    {
        /// <summary>
        /// Gets the pair of basic skills for a combo skill
        /// </summary>
        /// <param name="skillType"></param>
        /// <returns>a pair of basic skills, or null for non-combo skills</returns>
        public static (SkillType, SkillType)? BasePair(this SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.Classify:
                    return (SkillType.Analyse, SkillType.Recognise);
                case SkillType.Produce:
                    return (SkillType.Analyse, SkillType.Generate);
                case SkillType.Dialogue:
                    return (SkillType.Generate, SkillType.Communicate);
                case SkillType.Interpret:
                    return (SkillType.Recognise, SkillType.Communicate);
                case SkillType.Synthetise:
                    return (SkillType.Recognise, SkillType.Generate);
                case SkillType.Present:
                    return (SkillType.Analyse, SkillType.Communicate);
                default:
                    return null;
            }
        }
        public static bool IsBaseSkill(this SkillType skillType)
        {
            return AllBaseSkills().Contains(skillType);
        }
        /// <summary>
        /// Determines whether a skill type is basic or combinated (combo)
        /// </summary>
        /// <param name="skillType"the skill type to call the method on</param>
        /// <returns>true iff the skill is combinated</returns>

        public static bool IsCombinatedSkill(this SkillType skillType)
        {
            return !skillType.IsBaseSkill();
        }
        /// <summary>
        /// For reference, gets the list of base skills.
        /// </summary>
        /// <returns>the list</returns>
        public static List<SkillType> AllBaseSkills() => new List<SkillType>() { SkillType.Analyse, SkillType.Recognise, SkillType.Generate, SkillType.Communicate };
        /// <summary>
        /// For reference, gets the list of combo skills.
        /// </summary>
        /// <returns>the list</returns>
        public static List<SkillType> AllCombinatedSkills() => new List<SkillType>() { SkillType.Classify, SkillType.Produce, SkillType.Dialogue, SkillType.Interpret,
            SkillType.Synthetise, SkillType.Present };
    }
}