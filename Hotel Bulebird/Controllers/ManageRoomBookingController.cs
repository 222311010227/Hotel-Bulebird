using Microsoft.AspNetCore.Mvc;
using HotelBluebird.Models;
using Hotel_Bulebird.Data;
using Hotel_Bulebird.Models;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBluebird.Controllers
{
    public class ManageRoomBookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageRoomBookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bookings = _context.Bookings
                .Select(b => new AdminBookingViewModel
                {
                    Id = b.BookingId,
                    Name = b.Username,
                    Email = b.Email,
                    RoomType = b.RoomType,
                    BedType = "N/A",
                    NumberOfRooms = 1,
                    Meal = "N/A",
                    CheckIn = b.CheckInDate,
                    CheckOut = b.CheckOutDate,
                    NumberOfDays = (b.CheckOutDate - b.CheckInDate).Days,
                    Country = "India",
                    Phone = "N/A",
                    Status = "Pending"
                }).ToList();

            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            // Insert into ConfirmedBookings
            var confirmed = new ConfirmedBooking
            {
                Username = booking.Username,
                Email = booking.Email,
                RoomType = booking.RoomType,
                PricePerNight = booking.PricePerNight,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalAmount = booking.TotalAmount,
                BookingDate = booking.BookingDate
            };
            _context.ConfirmedBookings.Add(confirmed);

            // Insert into Payments
            var payment = new Payment
            {
                Username = booking.Username,
                Email = booking.Email,
                RoomType = booking.RoomType,
                TotalAmount = booking.TotalAmount,
                PaymentDate = DateTime.Now
            };
            _context.Payments.Add(payment);

            // Remove from original bookings table
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id) => View(); // Optional

        public IActionResult Delete(int id) => View(); // Optional
    }
}
