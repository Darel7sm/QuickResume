using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickResume.Data;
using QuickResume.Models;

namespace QuickResume.Controllers
{
    [Authorize]
    public class ExperienceController(QuickResumeDbContext context) : Controller
    {
        private readonly QuickResumeDbContext context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var experienceList = await context.Experiences.Where(e => e.UserId == userId).ToListAsync();

            if (experienceList == null)
            {
                return RedirectToAction("Create", "Experience");
            }

            return View(experienceList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Experience experience)
        {
            if (ModelState.IsValid) return View(experience);

            experience.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Experiences.Add(experience);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Experience");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var experience = await context.Experiences.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (experience == null) return RedirectToAction("Create", "Experience"); ;

            return View(experience);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Experience experience)
        {
            if (id != experience.Id) return BadRequest();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingExperience = await context.Experiences.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (existingExperience == null) return NotFound();

            existingExperience.CompanyName = experience.CompanyName;
            existingExperience.JobTitle = experience.JobTitle;
            existingExperience.StartDate = experience.StartDate;
            existingExperience.EndDate = experience.EndDate;
            existingExperience.State = experience.State;
            existingExperience.City = experience.City;
            existingExperience.IsCurrentlyWorking = experience.IsCurrentlyWorking;
            existingExperience.JobDuty = experience.JobDuty;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while saving changes.");
            }

            return RedirectToAction("Index", "Experience");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var experience = await context.Experiences.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (experience == null) return NotFound();

            return View(experience);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var experience = await context.Experiences.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (experience == null) return NotFound();

            context.Experiences.Remove(experience);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Experience");
        }
    }
}
