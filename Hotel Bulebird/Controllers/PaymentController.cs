using Hotel_Bulebird.Models;
using Hotel_Bulebird.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HotelBluebird.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ManagePayment()
        {
            var payments = _context.Payments.ToList();
            return View(payments);
        }

        public IActionResult Delete(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.PaymentId == id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
            return RedirectToAction("ManagePayment");
        }

        public IActionResult Print(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View("Print", payment);
        }
    }
}
