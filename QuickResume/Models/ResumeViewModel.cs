namespace QuickResume.Models
{
    public class ResumeViewModel
    {
        public Profile? Profile { get; set; }
        public Summary? Summary { get; set; }
        public List<Education>? EducationList { get; set; }
        public List<Experience>? ExperienceList { get; set; }
        public List<Skill>? Skills { get; set; }
        public List<Language>? Languages { get; set; }
        public Social? Socials { get; set; }
    }
}