using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Customer complaint handling.
    /// </summary>
    [Authorize(Roles = "Manager,Receptionist")]
    public class ComplaintsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string? status = null)
        {
            var query = AppDataStore.Complaints.AsEnumerable();
            if (!string.IsNullOrEmpty(status))
                query = query.Where(c => c.Status == status);

            return View(query.OrderByDescending(c => c.CreatedAt).ToList());
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var complaint = AppDataStore.Complaints.FirstOrDefault(c => c.Id == id);
            if (complaint == null) return NotFound();
            ViewBag.Customer = AppDataStore.Customers.FirstOrDefault(c => c.Id == complaint.CustomerId);
            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Resolve(string id, string resolutionNotes)
        {
            var complaint = AppDataStore.Complaints.FirstOrDefault(c => c.Id == id);
            if (complaint == null) return NotFound();

            complaint.Status = "Resolved";
            complaint.ResolvedAt = DateTime.UtcNow;
            complaint.ResolutionNotes = resolutionNotes;
            TempData["Success"] = "Complaint marked as resolved.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
