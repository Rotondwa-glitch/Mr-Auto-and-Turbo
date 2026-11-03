using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Vehicle management linked to customers.
    /// </summary>
    [Authorize(Roles = "Manager,Receptionist")]
    public class VehiclesController : Controller
    {
        [HttpGet]
        public IActionResult Index(string? customerId = null)
        {
            var query = AppDataStore.Vehicles.AsEnumerable();
            if (!string.IsNullOrEmpty(customerId))
                query = query.Where(v => v.CustomerId == customerId);

            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            return View(query.OrderBy(v => v.RegistrationNumber).ToList());
        }

        [HttpGet]
        public IActionResult Create(string? customerId = null)
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            return View(new Vehicle { CustomerId = customerId ?? string.Empty });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vehicle model)
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            if (!ModelState.IsValid) return View(model);

            model.Id = Guid.NewGuid().ToString();
            AppDataStore.Vehicles.Add(model);
            TempData["Success"] = "Vehicle registered.";
            return RedirectToAction(nameof(Index), new { customerId = model.CustomerId });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var vehicle = AppDataStore.Vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle == null) return NotFound();
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Vehicle model)
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            if (!ModelState.IsValid) return View(model);

            var existing = AppDataStore.Vehicles.FirstOrDefault(v => v.Id == model.Id);
            if (existing == null) return NotFound();

            existing.CustomerId = model.CustomerId;
            existing.RegistrationNumber = model.RegistrationNumber;
            existing.Make = model.Make;
            existing.Model = model.Model;
            existing.Year = model.Year;
            existing.Mileage = model.Mileage;
            TempData["Success"] = "Vehicle updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var vehicle = AppDataStore.Vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle == null) return NotFound();
            ViewBag.Customer = AppDataStore.Customers.FirstOrDefault(c => c.Id == vehicle.CustomerId);
            ViewBag.JobCards = AppDataStore.JobCards.Where(j => j.VehicleId == id).OrderByDescending(j => j.CreatedAt).ToList();
            return View(vehicle);
        }
    }
}
