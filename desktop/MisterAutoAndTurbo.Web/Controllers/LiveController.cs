using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Lightweight endpoints used for live dashboard updates / polling.
    /// </summary>
    [Authorize]
    public class LiveController : Controller
    {
        [HttpGet]
        public IActionResult Stats()
        {
            var stats = new
            {
                openJobs = AppDataStore.JobCards.Count(j => j.Status != "Completed" && j.Status != "Cancelled"),
                pendingAppointments = AppDataStore.Appointments.Count(a => a.Status == "Pending" || a.Status == "Scheduled"),
                openComplaints = AppDataStore.Complaints.Count(c => c.Status == "Open"),
                readyForCollection = AppDataStore.VehicleCollections.Count(v => v.CollectionStatus == "Ready")
            };
            return Json(stats);
        }

        [HttpGet]
        public IActionResult RecentActivity()
        {
            var recent = AppDataStore.Notifications
                .OrderByDescending(n => n.CreatedAt)
                .Take(10)
                .Select(n => new { n.Title, n.Message, n.CreatedAt, n.IsRead })
                .ToList();
            return Json(recent);
        }
    }
}
