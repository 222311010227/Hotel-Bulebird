using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HotelBluebird.Models;
using Hotel_Bulebird.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace HotelBluebird.Controllers
{
    [Authorize]
    public class ChangePasswordController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ChangePasswordController(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            // Get currently logged-in username
            string? username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "User session expired. Please login again.";
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Login", "Account");
            }

            // Verify current password
            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.CurrentPassword);
            if (passwordVerification == PasswordVerificationResult.Failed)
            {
                TempData["Error"] = "Incorrect current password!";
                return RedirectToAction("Index");
            }

            // Check if new password and confirm password match
            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["Error"] = "New passwords do not match!";
                return RedirectToAction("Index");
            }

            // Hash new password and update
            user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Password changed successfully!";
            return RedirectToAction("Index");
        }
    }
}
