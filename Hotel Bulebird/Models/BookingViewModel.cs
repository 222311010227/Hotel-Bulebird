namespace HotelBluebird.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
    }
}
