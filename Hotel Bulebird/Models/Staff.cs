using System.ComponentModel.DataAnnotations;

namespace HotelBluebird.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Work { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
