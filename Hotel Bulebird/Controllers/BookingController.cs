using Hotel_Bulebird.Data;
using Hotel_Bulebird.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hotel_Bulebird.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SaveBooking([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "❌ Invalid booking data!" });
            }

            try
            {
                booking.BookingDate = DateTime.Now;

                // Set price by room type
                booking.PricePerNight = booking.RoomType switch
                {
                    "Single Room" => 500,
                    "Double Room" => 1000,
                    "Economy Room" => 1500,
                    "Deluxe Room" => 2000,
                    "Suite Room" => 2500,
                    _ => 0
                };

                int nights = (booking.CheckOutDate - booking.CheckInDate).Days;
                booking.TotalAmount = booking.PricePerNight * nights;

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "✅ Booking saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"❌ Booking failed: {ex.Message}" });
            }
        }
    }
}
