using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using QuickResume.Data;
using QuickResume.Models;
using Microsoft.EntityFrameworkCore;

namespace QuickResume.Controllers
{
    [Authorize]
    public class EducationController(QuickResumeDbContext context) : Controller
    {
        private readonly QuickResumeDbContext context = context;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var educationList = await context.Educations.Where(e => e.UserId == userId).ToListAsync();

            if(educationList == null)
            {
                return RedirectToAction("Create", "Education");
            }

            return View(educationList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Education education)
        {
            if (ModelState.IsValid) return View(education);

            education.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Educations.Add(education);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Education");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var education = await context.Educations.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (education == null) return RedirectToAction("Create", "Education");

            return View(education);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Education education)
        {
            if (id != education.Id) return BadRequest();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingEducation = await context.Educations.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (existingEducation == null) return NotFound();

            existingEducation.Institution = education.Institution;
            existingEducation.Degree = education.Degree;
            existingEducation.FieldOfStudy = education.FieldOfStudy;
            existingEducation.StartDate = education.StartDate;
            existingEducation.EndDate = education.EndDate;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while saving changes.");
            }

            return RedirectToAction("Index", "Education");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var education = await context.Educations.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (education == null) return NotFound();

            return View(education);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var education = await context.Educations.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (education == null) return NotFound();

            context.Educations.Remove(education);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

