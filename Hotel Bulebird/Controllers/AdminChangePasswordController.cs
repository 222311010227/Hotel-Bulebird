using Microsoft.AspNetCore.Mvc;
using HotelBluebird.Models;

namespace HotelBluebird.Controllers
{
    public class AdminChangePasswordController : Controller
    {
        private static string adminPassword = "Admin@123"; // Temporary password storage

        public IActionResult Index()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        public IActionResult Index(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.CurrentPassword != adminPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Current password is incorrect.");
                return View(model);
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(model);
            }

            adminPassword = model.NewPassword;
            ViewBag.Message = "Password changed successfully!";
            ModelState.Clear();
            return View(new ChangePasswordModel());
        }
    }
}
