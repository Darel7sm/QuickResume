using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickResume.Data;
using QuickResume.Models;

namespace QuickResume.Controllers
{
    [Authorize]
    public class ProfileController(QuickResumeDbContext context) : Controller
    {
        private readonly QuickResumeDbContext context = context;

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            return View(profile);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Profile profile)
        {
            if (ModelState.IsValid)
            {
                return View(profile);
            }

            profile.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Add(profile);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Profile");
        }

        public async Task<IActionResult> Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Profile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingProfile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

            if (existingProfile == null)
            { 
                return NotFound(); 
            }


            if (profile.FullName != existingProfile.FullName)
            {
                existingProfile.FullName = profile.FullName;
                context.Entry(existingProfile).Property(p => p.FullName).IsModified = true;
            }

            if (profile.DateofBirth != existingProfile.DateofBirth)
            {
                existingProfile.DateofBirth = profile.DateofBirth;
                context.Entry(existingProfile).Property(p => p.DateofBirth).IsModified = true;
            }

            if (profile.Gender != existingProfile.Gender)
            {
                existingProfile.Gender = profile.Gender;
                context.Entry(existingProfile).Property(p => p.Gender).IsModified = true;
            }

            if (profile.Email != existingProfile.Email)
            {
                existingProfile.Email = profile.Email;
                context.Entry(existingProfile).Property(p => p.Email).IsModified = true;
            }

            if (profile.PhoneNumber != existingProfile.PhoneNumber)
            {
                existingProfile.PhoneNumber = profile.PhoneNumber;
                context.Entry(existingProfile).Property(p => p.PhoneNumber).IsModified = true;
            }

            if (profile.Street != existingProfile.Street)
            {
                existingProfile.Street = profile.Street;
                context.Entry(existingProfile).Property(p => p.Street).IsModified = true;
            }

            if (profile.City != existingProfile.City)
            {
                existingProfile.City = profile.City;
                context.Entry(existingProfile).Property(p => p.City).IsModified = true;
            }

            if (profile.Province != existingProfile.Province)
            {
                existingProfile.Province = profile.Province;
                context.Entry(existingProfile).Property(p => p.Province).IsModified = true;
            }

            if (profile.ZipCode != existingProfile.ZipCode)
            {
                existingProfile.ZipCode = profile.ZipCode;
                context.Entry(existingProfile).Property(p => p.ZipCode).IsModified = true;
            }

            if (profile.Country != existingProfile.Country)
            {
                existingProfile.Country = profile.Country;
                context.Entry(existingProfile).Property(p => p.Country).IsModified = true;
            }


            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Profile");
        }

        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                return NotFound();

            return View(profile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            context.Profiles.Remove(profile);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Profile");
        }
    }
}
