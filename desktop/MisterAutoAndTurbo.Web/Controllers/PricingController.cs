using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Simple pricing list management (Manager).
    /// </summary>
    [Authorize(Roles = "Manager")]
    public class PricingController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(AppDataStore.Pricings.OrderBy(p => p.Name).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Pricing());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pricing model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Id = Guid.NewGuid().ToString();
            AppDataStore.Pricings.Add(model);
            TempData["Success"] = "Pricing entry created.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var item = AppDataStore.Pricings.FirstOrDefault(p => p.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pricing model)
        {
            if (!ModelState.IsValid) return View(model);

            var existing = AppDataStore.Pricings.FirstOrDefault(p => p.Id == model.Id);
            if (existing == null) return NotFound();

            existing.Name = model.Name;
            existing.Amount = model.Amount;
            TempData["Success"] = "Pricing updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var item = AppDataStore.Pricings.FirstOrDefault(p => p.Id == id);
            if (item != null)
            {
                AppDataStore.Pricings.Remove(item);
                TempData["Success"] = "Pricing deleted.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
