using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Vehicle inspections linked to job cards.
    /// </summary>
    [Authorize(Roles = "Manager,Mechanic")]
    public class InspectionsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var list = AppDataStore.Inspections.OrderByDescending(i => i.InspectedAt).ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create(string? jobCardId = null)
        {
            ViewBag.JobCards = AppDataStore.JobCards
                .Where(j => j.Status != "Completed" && j.Status != "Cancelled")
                .ToList();
            return View(new Inspection { JobCardId = jobCardId ?? string.Empty, InspectedAt = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inspection model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.JobCards = AppDataStore.JobCards.ToList();
                return View(model);
            }

            model.Id = Guid.NewGuid().ToString();
            model.InspectedById = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
            AppDataStore.Inspections.Add(model);
            TempData["Success"] = "Inspection recorded.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var item = AppDataStore.Inspections.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}
