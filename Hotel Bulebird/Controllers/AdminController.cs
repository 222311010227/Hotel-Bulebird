using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hotel_Bulebird.Data;
using System.Linq;

namespace Hotel_Bulebird.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalRooms = _context.Rooms.Count();
            ViewBag.BookedRooms = _context.ConfirmedBookings.Count();
            ViewBag.TotalPayment = _context.Payments.Sum(p => p.TotalAmount);
            // Replace 'Amount' with correct column

            return View();
        }
    }
}
