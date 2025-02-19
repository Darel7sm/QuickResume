using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickResume.Data;
using QuickResume.Models;

namespace QuickResume.Controllers
{
    [Authorize]
    public class SocialController(QuickResumeDbContext context) : Controller
    {
        private readonly QuickResumeDbContext context = context;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var socialList = await context.Socials.FirstOrDefaultAsync(e => e.UserId == userId);

            if (socialList == null)
            {
                return RedirectToAction("Create", "Social");
            }

            return View(socialList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Social social)
        {
            if (ModelState.IsValid)
            {
                return View(social);
            }

            social.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Add(social);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Social");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var socialList = await context.Socials.FirstOrDefaultAsync(e => e.UserId == userId);

            if (socialList == null)
            {
                return RedirectToAction("Create", "Social");
            }

            return View(socialList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Social Social)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingSocial = await context.Socials.FirstOrDefaultAsync(e => e.UserId == userId);

            if (Social == null)
            {
                return RedirectToAction("Create", "Social");
            }

            existingSocial.LinkedIn = Social.LinkedIn;
            existingSocial.GitHub = Social.GitHub;
            existingSocial.PersonalWebsite = Social.PersonalWebsite;

            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Social");
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var fetchSocial = await context.Socials.FirstOrDefaultAsync(e => e.UserId == userId);

            if (fetchSocial == null)
            {
                return RedirectToAction("Create", "Social");
            }

            return View(fetchSocial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Social social)
        {
            context.Socials.Remove(social);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Social");
        }
    }
}
