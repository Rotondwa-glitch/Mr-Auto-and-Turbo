using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Customer management (list, create, edit, details).
    /// </summary>
    [Authorize(Roles = "Manager,Receptionist")]
    public class CustomersController : Controller
    {
        [HttpGet]
        public IActionResult Index(string? search = null)
        {
            var query = AppDataStore.Customers.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLowerInvariant();
                query = query.Where(c =>
                    c.FullName.ToLower().Contains(search) ||
                    c.Email.ToLower().Contains(search) ||
                    c.Phone.Contains(search));
            }
            return View(query.OrderBy(c => c.FullName).ToList());
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var customer = AppDataStore.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            ViewBag.Vehicles = AppDataStore.Vehicles.Where(v => v.CustomerId == id).ToList();
            ViewBag.JobCards = AppDataStore.JobCards.Where(j => j.CustomerId == id).OrderByDescending(j => j.CreatedAt).ToList();
            return View(customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Customer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Id = Guid.NewGuid().ToString();
            model.CreatedAt = DateTime.UtcNow;
            AppDataStore.Customers.Add(model);
            TempData["Success"] = "Customer created.";
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var customer = AppDataStore.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer model)
        {
            if (!ModelState.IsValid) return View(model);

            var existing = AppDataStore.Customers.FirstOrDefault(c => c.Id == model.Id);
            if (existing == null) return NotFound();

            existing.FullName = model.FullName;
            existing.Email = model.Email;
            existing.Phone = model.Phone;
            existing.Address = model.Address;
            TempData["Success"] = "Customer updated.";
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }
    }
}
