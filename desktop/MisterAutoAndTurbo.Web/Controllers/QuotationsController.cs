using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Quotation creation, sending and approval tracking.
    /// </summary>
    [Authorize(Roles = "Manager,Receptionist")]
    public class QuotationsController : Controller
    {
        [HttpGet]
        public IActionResult Index(string? status = null)
        {
            var query = AppDataStore.Quotations.AsEnumerable();
            if (!string.IsNullOrEmpty(status))
                query = query.Where(q => q.Status == status);

            return View(query.OrderByDescending(q => q.CreatedAt).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            ViewBag.Vehicles = AppDataStore.Vehicles.ToList();
            ViewBag.Statuses = QuotationStatus.All;
            return View(new Quotation { Status = QuotationStatus.Draft });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Quotation model)
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            ViewBag.Vehicles = AppDataStore.Vehicles.ToList();
            ViewBag.Statuses = QuotationStatus.All;
            if (!ModelState.IsValid) return View(model);

            model.Id = Guid.NewGuid().ToString();
            model.CreatedAt = DateTime.UtcNow;
            model.CreatedById = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
            AppDataStore.Quotations.Add(model);
            TempData["Success"] = "Quotation created.";
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var quote = AppDataStore.Quotations.FirstOrDefault(q => q.Id == id);
            if (quote == null) return NotFound();
            ViewBag.Customer = AppDataStore.Customers.FirstOrDefault(c => c.Id == quote.CustomerId);
            ViewBag.Vehicle = AppDataStore.Vehicles.FirstOrDefault(v => v.Id == quote.VehicleId);
            return View(quote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(string id, string status)
        {
            var quote = AppDataStore.Quotations.FirstOrDefault(q => q.Id == id);
            if (quote == null) return NotFound();

            quote.Status = status;
            if (status == QuotationStatus.Approved)
                quote.ApprovedAt = DateTime.UtcNow;

            TempData["Success"] = "Quotation status updated.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
