using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Inventory / parts management.
    /// </summary>
    [Authorize(Roles = "Manager,Receptionist")]
    public class PartsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(AppDataStore.Parts.OrderBy(p => p.Name).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Part());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Part model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Id = Guid.NewGuid().ToString();
            AppDataStore.Parts.Add(model);
            TempData["Success"] = "Part created.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var item = AppDataStore.Parts.FirstOrDefault(p => p.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Part model)
        {
            if (!ModelState.IsValid) return View(model);

            var existing = AppDataStore.Parts.FirstOrDefault(p => p.Id == model.Id);
            if (existing == null) return NotFound();

            existing.Name = model.Name;
            existing.Sku = model.Sku;
            existing.UnitPrice = model.UnitPrice;
            existing.StockQuantity = model.StockQuantity;
            TempData["Success"] = "Part updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var item = AppDataStore.Parts.FirstOrDefault(p => p.Id == id);
            if (item != null)
            {
                AppDataStore.Parts.Remove(item);
                TempData["Success"] = "Part deleted.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
