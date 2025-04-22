using Microsoft.AspNetCore.Mvc;
using HotelBluebird.Models;
using Hotel_Bulebird.Data;
using System.Linq;

namespace HotelBluebird.Controllers
{
    public class ManageReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ---------- Display All Reviews for Admin ----------
        public IActionResult Index()
        {
            var reviews = _context.Reviews.ToList();

            // DEBUG: Force printing to console
            Console.WriteLine("=== DEBUG REVIEW COUNT ===");
            Console.WriteLine("Total Reviews: " + reviews.Count);
            foreach (var r in reviews)
            {
                Console.WriteLine($"Review #{r.Id}: {r.CleanlinessRating} - {r.Comments}");
            }

            return View(reviews);
        }




        // ---------- Delete a Review ----------
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
                TempData["Success"] = "✅ Review deleted successfully!";
            }
            else
            {
                TempData["Error"] = "❌ Review not found!";
            }

            return RedirectToAction("Index");
        }

        // ---------- Edit a Review ----------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                TempData["Error"] = "❌ Review not found!";
                return RedirectToAction("Index");
            }

            return View(review);
        }

        [HttpPost]
        public IActionResult Edit(Review model)
        {
            if (ModelState.IsValid)
            {
                var review = _context.Reviews.FirstOrDefault(r => r.Id == model.Id);
                if (review != null)
                {
                    review.Comments = model.Comments;
                    review.CleanlinessRating = model.CleanlinessRating;
                    review.StaffRating = model.StaffRating;
                    review.FoodRating = model.FoodRating;
                    review.ComfortRating = model.ComfortRating;
                    review.RecommendationRating = model.RecommendationRating;

                    _context.SaveChanges();

                    TempData["Success"] = "✅ Review updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            TempData["Error"] = "❌ Failed to update review.";
            return View(model);
        }
    }
}
