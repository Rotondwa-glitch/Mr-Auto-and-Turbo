using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisterAutoAndTurbo.Web.Data;
using MisterAutoAndTurbo.Web.Models;
using MisterAutoAndTurbo.Web.Services;

namespace MisterAutoAndTurbo.Web.Controllers
{
    /// <summary>
    /// Login / Register / Logout using cookie authentication
    /// (Microsoft.AspNetCore.Authentication.Core + Cookies).
    /// Also handles forgot-password flow via email recovery codes.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly PasswordResetService _resetService;
        private readonly IEmailService _emailService;

        public AccountController(PasswordResetService resetService, IEmailService emailService)
        {
            _resetService = resetService;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var user = AppDataStore.Users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                });

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string fullName, string email, string password, string role)
        {
            if (AppDataStore.Users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(string.Empty, "Email already registered.");
                return View();
            }

            var user = new UserAccount
            {
                Id = Guid.NewGuid().ToString(),
                FullName = fullName.Trim(),
                Email = email.Trim().ToLowerInvariant(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = role,
                CreatedAt = DateTime.UtcNow
            };

            AppDataStore.Users.Add(user);
            TempData["Success"] = "Account created. Please log in.";
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = AppDataStore.Users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                var code = _resetService.GenerateCode(user.Email);
                await _emailService.SendPasswordResetAsync(user.Email, code);
            }

            // Always show success to avoid revealing whether the email exists
            TempData["Success"] = "If that email is registered, a recovery code has been sent.";
            return RedirectToAction(nameof(ResetPassword));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(string email, string code, string newPassword)
        {
            if (!_resetService.ValidateCode(email, code))
            {
                ModelState.AddModelError(string.Empty, "Invalid or expired recovery code.");
                return View();
            }

            var user = AppDataStore.Users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return View();
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _resetService.InvalidateCode(email);

            TempData["Success"] = "Password updated. Please log in.";
            return RedirectToAction(nameof(Login));
        }
    }
}
