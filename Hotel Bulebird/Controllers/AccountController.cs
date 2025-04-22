using Hotel_Bulebird.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hotel_Bulebird.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using HotelBluebird.Models;  // Ensure this line is present


public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AccountController(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    // ---------- REGISTER ----------
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
        if (existingUser != null)
        {
            ModelState.AddModelError("Username", "Username is already taken.");
            return View(model);
        }

        var newUser = new User
        {
            Username = model.Username,
            PasswordHash = _passwordHasher.HashPassword(new User(), model.Password),
            IsAdmin = false
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Registration successful! You can now log in.";
        return RedirectToAction("Login");
    }

    // ---------- LOGIN ----------
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
        if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // ✅ Required
            new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        if (user.IsAdmin)
        {
            TempData["SuccessMessage"] = "Admin login successful!";
            return RedirectToAction("Index", "Admin");
        }

        TempData["SuccessMessage"] = "Login successful!";
        return RedirectToAction("Index", "Home");
    }

    // ---------- ADMIN LOGIN ----------
    [HttpGet]
    [AllowAnonymous]
    public IActionResult AdminLogin()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> AdminLogin(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Static admin credentials (for testing only)
        if (model.Username == "Admin" && model.Password == "123456")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            TempData["SuccessMessage"] = "Admin login successful!";
            return RedirectToAction("Index", "Admin");
        }

        // Database admin validation (existing logic)
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.IsAdmin);
        if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Invalid admin credentials.");
            return View(model);
        }

        var dbClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var dbClaimsIdentity = new ClaimsIdentity(dbClaims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(dbClaimsIdentity));

        TempData["SuccessMessage"] = "Admin login successful!";
        return RedirectToAction("Index", "Admin");
    }

    // ---------- LOGOUT ----------
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    // ---------- FORGOT PASSWORD ----------
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // ---------- ACCESS DENIED ----------
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}