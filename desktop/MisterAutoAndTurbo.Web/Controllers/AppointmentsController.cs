using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Appointment booking and management.
    /// </summary>
    [Authorize(Roles = "Manager,Receptionist")]
    public class AppointmentsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string? status = null)
        {
            var query = AppDataStore.Appointments.AsEnumerable();
            if (!string.IsNullOrEmpty(status))
                query = query.Where(a => a.Status == status);

            return View(query.OrderByDescending(a => a.ScheduledAt).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            ViewBag.Vehicles = AppDataStore.Vehicles.ToList();
            return View(new Appointment { ScheduledAt = DateTime.Now.AddDays(1) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment model)
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            ViewBag.Vehicles = AppDataStore.Vehicles.ToList();
            if (!ModelState.IsValid) return View(model);

            model.Id = Guid.NewGuid().ToString();
            model.CreatedById = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
            model.Status = "Scheduled";
            AppDataStore.Appointments.Add(model);
            TempData["Success"] = "Appointment created.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var appt = AppDataStore.Appointments.FirstOrDefault(a => a.Id == id);
            if (appt == null) return NotFound();
            ViewBag.Customer = AppDataStore.Customers.FirstOrDefault(c => c.Id == appt.CustomerId);
            ViewBag.Vehicle = AppDataStore.Vehicles.FirstOrDefault(v => v.Id == appt.VehicleId);
            return View(appt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(string id, string status)
        {
            var appt = AppDataStore.Appointments.FirstOrDefault(a => a.Id == id);
            if (appt == null) return NotFound();
            appt.Status = status;
            TempData["Success"] = "Appointment status updated.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
