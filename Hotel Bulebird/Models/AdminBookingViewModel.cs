using System;

namespace HotelBluebird.Models
{
    public class AdminBookingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoomType { get; set; }
        public string BedType { get; set; }
        public int NumberOfRooms { get; set; }
        public string Meal { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfDays { get; set; }
        public string Status { get; set; }
    }
}
