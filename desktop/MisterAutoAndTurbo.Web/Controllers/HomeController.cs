using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Landing controller. Shows the staff dashboard (Index).
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.OpenJobs = AppDataStore.JobCards.Count(j => j.Status != "Completed" && j.Status != "Cancelled");
            ViewBag.PendingAppointments = AppDataStore.Appointments.Count(a => a.Status == "Pending" || a.Status == "Scheduled");
            ViewBag.OpenComplaints = AppDataStore.Complaints.Count(c => c.Status == "Open");
            ViewBag.ReadyForCollection = AppDataStore.VehicleCollections.Count(v => v.CollectionStatus == "Ready");
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
