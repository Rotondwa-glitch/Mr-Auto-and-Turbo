using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Payment recording and history.
    /// </summary>
    [Authorize(Roles = "Manager,Receptionist")]
    public class PaymentsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(AppDataStore.Payments.OrderByDescending(p => p.PaidAt).ToList());
        }

        [HttpGet]
        public IActionResult Create(string? jobCardId = null, string? quotationId = null)
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            ViewBag.JobCards = AppDataStore.JobCards.ToList();
            return View(new Payment
            {
                JobCardId = jobCardId,
                QuotationId = quotationId,
                PaidAt = DateTime.Now,
                Status = "Completed"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Payment model)
        {
            ViewBag.Customers = AppDataStore.Customers.OrderBy(c => c.FullName).ToList();
            ViewBag.JobCards = AppDataStore.JobCards.ToList();
            if (!ModelState.IsValid) return View(model);

            model.Id = Guid.NewGuid().ToString();
            model.RecordedById = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
            AppDataStore.Payments.Add(model);
            TempData["Success"] = "Payment recorded.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var payment = AppDataStore.Payments.FirstOrDefault(p => p.Id == id);
            if (payment == null) return NotFound();
            ViewBag.Customer = AppDataStore.Customers.FirstOrDefault(c => c.Id == payment.CustomerId);
            return View(payment);
        }
    }
}
