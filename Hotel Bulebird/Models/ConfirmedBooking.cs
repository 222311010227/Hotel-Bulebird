using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Bulebird.Models
{
    public class ConfirmedBooking
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        public decimal PricePerNight { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Confirmed";
    }
}
