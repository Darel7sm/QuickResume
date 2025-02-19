using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickResume.Data;
using QuickResume.Models;

namespace QuickResume.Controllers
{
    [Authorize]
    public class SummaryController(QuickResumeDbContext context) : Controller
    {
        private readonly QuickResumeDbContext context = context;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var summaryList = await context.Summaries.FirstOrDefaultAsync(e => e.UserId == userId);

            if (summaryList == null)
            {
                return RedirectToAction("Create", "Summary");
            }

            return View(summaryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Summary summary)
        {
            if (ModelState.IsValid)
            {
                return View(summary);
            }

            summary.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Add(summary);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Summary");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var summaryList = await context.Summaries.FirstOrDefaultAsync(e => e.UserId == userId);

            if (summaryList == null)
            {
                return RedirectToAction("Create", "Summary");
            }

            return View(summaryList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Summary Summary)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingSummary = await context.Summaries.FirstOrDefaultAsync(e => e.UserId == userId);

            if (Summary == null)
            {
                return RedirectToAction("Create", "Summary");
            }

            existingSummary.Position = Summary.Position;
            existingSummary.Description = Summary.Description;

            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Summary");
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var fetchSummary = await context.Summaries.FirstOrDefaultAsync(e => e.UserId == userId);

            if (fetchSummary == null)
            {
                return RedirectToAction("Create", "Summary");
            }

            return View(fetchSummary);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Summary summary)
        {
            context.Summaries.Remove(summary);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Summary");
        }
    }
}
