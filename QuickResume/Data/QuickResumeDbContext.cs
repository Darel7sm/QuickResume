using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickResume.Models;

namespace QuickResume.Data
{
    public class QuickResumeDbContext : IdentityDbContext<QuickResumeUser>
    {
        public QuickResumeDbContext(DbContextOptions options) : base(options)
        {
        }

        public required DbSet<Profile> Profiles { get; set; }
        public required DbSet<Summary> Summaries { get; set; }
        public required DbSet<Education> Educations { get; set; }
        public required DbSet<Experience> Experiences { get; set; }
        public required DbSet<Skill> Skills { get; set; }
        public required DbSet<Language> Languages { get; set; }
        public required DbSet<Social> Socials { get; set; }
    }
}
