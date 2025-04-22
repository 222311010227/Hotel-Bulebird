using Microsoft.AspNetCore.Mvc;
using Hotel_Bulebird.Data;
using Hotel_Bulebird.Models;
using System.Linq;

public class ViewBookingController : Controller
{
    private readonly ApplicationDbContext _context;

    public ViewBookingController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var username = User.Identity.Name;
        var bookings = _context.ConfirmedBookings
                               .Where(b => b.Username == username)
                               .ToList();

        return View(bookings);
    }
}
