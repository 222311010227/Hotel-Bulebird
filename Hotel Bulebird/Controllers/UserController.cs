using Hotel_Bulebird.Data;
using HotelBluebird.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Bluebird.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account"); // Or show error
            }

            int userId = int.Parse(userIdClaim.Value);

            var userProfile = _context.UserProfiles.FirstOrDefault(u => u.AppUserId == userId);



            var viewModel = new UserProfileViewModel
            {
                Name = userProfile?.Name,
                Email = userProfile?.Email,
                Mobile = userProfile?.Mobile,
                Address = userProfile?.Address,
                ProfilePicture = userProfile?.ProfilePicture
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserProfileViewModel model, IFormFile ProfileImageFile)
        {
            string imagePath = null;
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (ProfileImageFile != null && ProfileImageFile.Length > 0)
            {
                string uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles");
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImageFile.FileName);
                string filePath = Path.Combine(uploadsDir, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImageFile.CopyToAsync(stream);
                }

                imagePath = "/images/profiles/" + uniqueFileName;
            }

            var existing = _context.UserProfiles.FirstOrDefault(p => p.AppUserId == userId);

            if (existing != null)
            {
                existing.Name = model.Name;
                existing.Email = model.Email;
                existing.Mobile = model.Mobile;
                existing.Address = model.Address;
                if (imagePath != null) existing.ProfilePicture = imagePath;
            }
            else
            {
                _context.UserProfiles.Add(new UserProfile
                {
                    Name = model.Name,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    Address = model.Address,
                    ProfilePicture = imagePath ?? "/images/default.jpg",
                    AppUserId = userId
                });
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Index");
        }
    }
}
