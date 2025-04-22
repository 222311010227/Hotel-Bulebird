using Microsoft.AspNetCore.Mvc;
using Hotel_Bulebird.Data;
using HotelBluebird.Models;
using System.Linq;

namespace HotelBluebird.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch all users from the database
            var users = _context.Users.ToList();  // Get all users from the Users table

            // Ensure data is passed to the view
            return View(users);  // Pass the list of users to the view
        }


        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                TempData["Success"] = "✅ User deleted successfully!";
            }
            else
            {
                TempData["Error"] = "❌ User not found!";
            }

            return RedirectToAction("Index");
        }
    }
}
