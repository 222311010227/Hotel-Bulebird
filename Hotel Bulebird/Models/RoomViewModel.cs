namespace HotelBluebird.Controllers
{
    internal class RoomViewModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string BedType { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}