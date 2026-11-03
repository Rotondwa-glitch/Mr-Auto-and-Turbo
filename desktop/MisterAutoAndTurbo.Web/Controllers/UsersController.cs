using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;
using MisterAutoAndTurbo.Web.ViewModels;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Staff user management (Manager only).
    /// </summary>
    [Authorize(Roles = "Manager")]
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(AppDataStore.Users.OrderBy(u => u.FullName).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = UserRoles.All;
            return View(new UserCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserCreateViewModel model)
        {
            ViewBag.Roles = UserRoles.All;
            if (!ModelState.IsValid) return View(model);

            if (AppDataStore.Users.Any(u => u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(nameof(model.Email), "Email already in use.");
                return View(model);
            }

            var user = new UserAccount
            {
                Id = Guid.NewGuid().ToString(),
                FullName = model.FullName.Trim(),
                Email = model.Email.Trim().ToLowerInvariant(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = model.Role,
                CreatedAt = DateTime.UtcNow
            };
            AppDataStore.Users.Add(user);
            TempData["Success"] = "User created.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user = AppDataStore.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            ViewBag.Roles = UserRoles.All;
            return View(new UserEditViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditViewModel model)
        {
            ViewBag.Roles = UserRoles.All;
            if (!ModelState.IsValid) return View(model);

            var user = AppDataStore.Users.FirstOrDefault(u => u.Id == model.Id);
            if (user == null) return NotFound();

            user.FullName = model.FullName.Trim();
            user.Email = model.Email.Trim().ToLowerInvariant();
            user.Role = model.Role;
            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            }
            TempData["Success"] = "User updated.";
            return RedirectToAction(nameof(Index));
        }
    }
}
