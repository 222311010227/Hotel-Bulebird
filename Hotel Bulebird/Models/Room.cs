using System.ComponentModel.DataAnnotations;

namespace HotelBluebird.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
