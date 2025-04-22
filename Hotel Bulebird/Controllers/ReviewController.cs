using Microsoft.AspNetCore.Mvc;
using HotelBluebird.Models;
using Hotel_Bulebird.Data;

namespace HotelBluebird.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageReview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitReview(IFormCollection form)
        {
            try
            {
                var review = new Review
                {
                    CleanlinessRating = int.Parse(form["cleanliness"]),
                    StaffRating = int.Parse(form["staff"]),
                    FoodRating = int.Parse(form["food"]),
                    ComfortRating = int.Parse(form["comfort"]),
                    RecommendationRating = int.Parse(form["recommendation"]),
                    Comments = form["comments"]
                };

                _context.Reviews.Add(review);
                _context.SaveChanges();

                TempData["Success"] = "✅ Thank you for your feedback!";
            }
            catch (Exception)
            {
                TempData["Error"] = "❌ Failed to submit review. Please try again.";
            }

            return RedirectToAction("Index");
        }
    }
}
