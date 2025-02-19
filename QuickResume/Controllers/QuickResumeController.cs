using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickResume.Data;
using QuickResume.Models;
using QuickResume.Services;

namespace QuickResume.Controllers
{
    [Authorize]
    public class QuickResumeController(QuickResumeDbContext context, ResumeService pdfService) : Controller
    {
        private readonly QuickResumeDbContext context = context;
        private readonly ResumeService pdfService = pdfService;

        [Authorize]
        public async Task<IActionResult> ExportPdf()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            var summary = await context.Summaries.FirstOrDefaultAsync(s => s.UserId == userId);
            var education = await context.Educations.Where(e => e.UserId == userId).ToListAsync();
            var experience = await context.Experiences.Where(e => e.UserId == userId).ToListAsync();
            var skills = await context.Skills.Where(s => s.UserId == userId).ToListAsync();
            var languages = await context.Languages.Where(l => l.UserId == userId).ToListAsync();
            var socials = await context.Socials.FirstOrDefaultAsync(s => s.UserId == userId);


            if (profile == null)
                return NotFound("Profile not found");

            var pdfService = new ResumeService();

            var pdfBytes = pdfService.GenerateResume(profile, summary, education, languages, socials, experience, skills);

            return File(pdfBytes, "application/pdf", $"{profile.FullName}_Resume.pdf");
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var nullProfile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (nullProfile == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            var nullSummary = await context.Summaries.FirstOrDefaultAsync(p => p.UserId == userId);
            if (nullSummary == null)
            {
                return RedirectToAction("Create", "Summary");
            }

            var nullEducation = await context.Educations.FirstOrDefaultAsync(p => p.UserId == userId);
            if (nullEducation == null)
            {
                return RedirectToAction("Create", "Education");
            }

            var nullExperience = await context.Experiences.FirstOrDefaultAsync(p => p.UserId == userId);
            if (nullExperience == null)
            {
                return RedirectToAction("Create", "Experience");
            }

            var nullSkills = await context.Skills.FirstOrDefaultAsync(p => p.UserId == userId);
            if (nullSkills == null)
            {
                return RedirectToAction("Create", "Skill");
            }

            var nullLanguage = await context.Languages.FirstOrDefaultAsync(p => p.UserId == userId);
            if (nullLanguage == null)
            {
                return RedirectToAction("Create", "Language");
            }

            var nullSocials = await context.Socials.FirstOrDefaultAsync(p => p.UserId == userId);
            if (nullSocials == null)
            {
                return RedirectToAction("Create", "Social");
            }

            var profile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            var summary = await context.Summaries.FirstOrDefaultAsync(s => s.UserId == userId);
            var education = await context.Educations.Where(e => e.UserId == userId).ToListAsync();
            var experience = await context.Experiences.Where(e => e.UserId == userId).ToListAsync();
            var skills = await context.Skills.Where(s => s.UserId == userId).ToListAsync();
            var languages = await context.Languages.Where(l => l.UserId == userId).ToListAsync();
            var socials = await context.Socials.FirstOrDefaultAsync(s => s.UserId == userId);

            var viewModel = new ResumeViewModel
            {
                Profile = profile,
                Summary = summary,
                EducationList = education,
                ExperienceList = experience,
                Skills = skills,
                Languages = languages,
                Socials = socials
            };

            return View(viewModel);
        }
    }
}
