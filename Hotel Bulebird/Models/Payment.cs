using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Bulebird.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string RoomType { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}
