using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickResume.Data;
using QuickResume.Models;

namespace QuickResume.Controllers
{
    [Authorize]
    public class SkillController(QuickResumeDbContext context) : Controller
    {
        private readonly QuickResumeDbContext context = context;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var skillList = await context.Skills.Where(e => e.UserId == userId).ToListAsync();

            if (skillList == null)
            {
                return RedirectToAction("Create", "Skill");
            }

            return View(skillList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                return View(skill);
            }

            skill.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Add(skill);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Skill");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var skill = await context.Skills.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (skill == null)
            {
                return RedirectToAction("Create", "Skill");
            }

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Skill skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingSkill = await context.Skills.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (existingSkill == null)
            {
                return NotFound();
            }

            existingSkill.Name = skill.Name;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while saving changes.");
            }

            return RedirectToAction("Index", "Skill");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var skill = await context.Skills.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        [HttpPost, ActionName("Delete")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var skill = await context.Skills.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (skill == null)
            {
                return NotFound();
            }

            context.Skills.Remove(skill);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Skill");
        }
    }
}
