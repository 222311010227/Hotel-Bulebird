using System.ComponentModel.DataAnnotations;

namespace HotelBluebird.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5)]
        public int CleanlinessRating { get; set; }

        [Range(1, 5)]
        public int StaffRating { get; set; }

        [Range(1, 5)]
        public int FoodRating { get; set; }

        [Range(1, 5)]
        public int ComfortRating { get; set; }

        [Range(1, 5)]
        public int RecommendationRating { get; set; }

        public string Comments { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
