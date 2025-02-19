using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickResume.Data;
using QuickResume.Models;

namespace QuickResume.Controllers
{
    [Authorize]
    public class LanguageController(QuickResumeDbContext context) : Controller
    {
        private readonly QuickResumeDbContext context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var languageList = await context.Languages
                .Where(e => e.UserId == userId)
                .ToListAsync();

            if (languageList == null)
            {
                return RedirectToAction("Create", "Language");
            }

            return View(languageList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Language language)
        {
            if (ModelState.IsValid)
            {
                return View(language);
            }

            language.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Add(language);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Language");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var language = await context.Languages
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (language == null)
            {
                return RedirectToAction("Create", "Language");
            }

            return View(language);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Language language)
        {
            if (id != language.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingLanguage = await context.Languages.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (existingLanguage == null)
            {
                return NotFound();
            }

            existingLanguage.Name = language.Name;
            existingLanguage.Proficiency = language.Proficiency;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while saving changes.");
            }

            return RedirectToAction("Index", "Language");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var language = await context.Languages
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var language = await context.Languages
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (language == null)
            {
                return NotFound();
            }

            context.Languages.Remove(language);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Language");
        }
    }
}