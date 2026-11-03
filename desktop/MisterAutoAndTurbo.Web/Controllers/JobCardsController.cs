using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Job card management – core of the workshop workflow.
    /// </summary>
    [Authorize]
    public class JobCardsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string? status = null)
        {
            var query = AppDataStore.JobCards.AsEnumerable();
            if (!string.IsNullOrEmpty(status))
                query = query.Where(j => j.Status == status);

            return View(query.OrderByDescending(j => j.CreatedAt).ToList());
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var job = AppDataStore.JobCards.FirstOrDefault(j => j.Id == id);
            if (job == null) return NotFound();

            ViewBag.Customer = AppDataStore.Customers.FirstOrDefault(c => c.Id == job.CustomerId);
            ViewBag.Vehicle = AppDataStore.Vehicles.FirstOrDefault(v => v.Id == job.VehicleId);
            ViewBag.Parts = AppDataStore.JobParts.Where(p => p.JobCardId == id).ToList();
            ViewBag.Inspections = AppDataStore.Inspections.Where(i => i.JobCardId == id).ToList();
            return View(job);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            ViewBag.Vehicles = AppDataStore.Vehicles.ToList();
            ViewBag.Statuses = JobStatus.All;
            return View(new JobCard { Status = JobStatus.Pending });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobCard model)
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            ViewBag.Vehicles = AppDataStore.Vehicles.ToList();
            ViewBag.Statuses = JobStatus.All;
            if (!ModelState.IsValid) return View(model);

            model.Id = Guid.NewGuid().ToString();
            model.CreatedAt = DateTime.UtcNow;
            AppDataStore.JobCards.Add(model);
            TempData["Success"] = "Job card created.";
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(string id, string status)
        {
            var job = AppDataStore.JobCards.FirstOrDefault(j => j.Id == id);
            if (job == null) return NotFound();

            job.Status = status;
            if (status == JobStatus.Completed)
                job.CompletedAt = DateTime.UtcNow;

            TempData["Success"] = "Job status updated.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
