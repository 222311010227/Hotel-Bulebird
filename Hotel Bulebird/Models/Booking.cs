using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Bulebird.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
