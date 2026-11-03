using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Role-based dashboard with summary stats.
    /// </summary>
    [Authorize]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.OpenJobs = AppDataStore.JobCards.Count(j => j.Status != "Completed" && j.Status != "Cancelled");
            ViewBag.InProgressJobs = AppDataStore.JobCards.Count(j => j.Status == "In Progress");
            ViewBag.PendingAppointments = AppDataStore.Appointments.Count(a => a.Status == "Pending" || a.Status == "Scheduled");
            ViewBag.OpenComplaints = AppDataStore.Complaints.Count(c => c.Status == "Open");
            ViewBag.ReadyForCollection = AppDataStore.VehicleCollections.Count(v => v.CollectionStatus == "Ready");
            ViewBag.PendingQuotations = AppDataStore.Quotations.Count(q => q.Status == "Sent" || q.Status == "Draft");

            ViewBag.RecentJobs = AppDataStore.JobCards
                .OrderByDescending(j => j.CreatedAt)
                .Take(8)
                .ToList();

            return View();
        }
    }
}
