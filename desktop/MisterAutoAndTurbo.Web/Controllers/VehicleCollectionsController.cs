using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Vehicle collection scheduling and tracking.
    /// </summary>
    [Authorize(Roles = "Manager,Receptionist")]
    public class VehicleCollectionsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string? status = null)
        {
            var query = AppDataStore.VehicleCollections.AsEnumerable();
            if (!string.IsNullOrEmpty(status))
                query = query.Where(v => v.CollectionStatus == status);

            return View(query.OrderByDescending(v => v.ScheduledAt).ToList());
        }

        [HttpGet]
        public IActionResult Create(string? jobCardId = null)
        {
            ViewBag.JobCards = AppDataStore.JobCards
                .Where(j => j.Status == "Completed" || j.Status == "In Progress")
                .ToList();
            return View(new VehicleCollection
            {
                JobCardId = jobCardId ?? string.Empty,
                CollectionStatus = "Pending",
                ScheduledAt = DateTime.Now.AddHours(2)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleCollection model)
        {
            ViewBag.JobCards = AppDataStore.JobCards.ToList();
            if (!ModelState.IsValid) return View(model);

            model.Id = Guid.NewGuid().ToString();
            model.ManagedById = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";

            var job = AppDataStore.JobCards.FirstOrDefault(j => j.Id == model.JobCardId);
            if (job != null)
            {
                model.CustomerId = job.CustomerId;
                model.VehicleId = job.VehicleId;
            }

            AppDataStore.VehicleCollections.Add(model);
            TempData["Success"] = "Collection scheduled.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkCollected(string id)
        {
            var item = AppDataStore.VehicleCollections.FirstOrDefault(v => v.Id == id);
            if (item == null) return NotFound();

            item.CollectionStatus = "Collected";
            item.CollectedAt = DateTime.UtcNow;
            TempData["Success"] = "Vehicle marked as collected.";
            return RedirectToAction(nameof(Index));
        }
    }
}
