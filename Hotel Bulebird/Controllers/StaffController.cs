using HotelBluebird.Models;
using Microsoft.AspNetCore.Mvc;
using Hotel_Bulebird.Data;
using System.Linq;

namespace HotelBluebird.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display staff page
        public IActionResult Index()
        {
            var staffList = _context.Staffs.ToList();
            return View(staffList);
        }

        // Add new staff
        [HttpPost]
        public IActionResult AddStaff(string name, string work, string email, string phone)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(work))
            {
                var staff = new Staff
                {
                    Name = name,
                    Work = work,
                    Email = email,
                    Phone = phone
                };
                _context.Staffs.Add(staff);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Delete staff
        public IActionResult Delete(int id)
        {
            var staff = _context.Staffs.FirstOrDefault(s => s.Id == id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
