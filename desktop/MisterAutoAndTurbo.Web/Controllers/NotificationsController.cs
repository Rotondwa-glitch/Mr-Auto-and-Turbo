using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;
using System.Security.Claims;

namespace MisterAutoAndTurbo.Web.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

        [HttpGet]
        public IActionResult Index()
        {
            var list = AppDataStore.Notifications
                .Where(n => string.IsNullOrEmpty((n.UserAccountId) || n.UserAccountId == CurrentUserId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkRead(string id)
        {
            var n = AppDataStore.Notifications.FirstOrDefault(x => x.Id == id);
            if (n != null) n.IsRead = true;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAllRead()
        {
            foreach (var n in AppDataStore.Notifications.Where(x => !x.IsRead))
                n.IsRead = true;
            TempData["Success"] = "All notifications marked as read.";
            return RedirectToAction(nameof(Index));
        }
    }
}
