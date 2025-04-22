using Microsoft.EntityFrameworkCore;
using Hotel_Bulebird.Models;
using Hotel_Bulebird.Controllers;
using HotelBluebird.Models;

namespace Hotel_Bulebird.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // ✅ Keep only the Users table

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Staff> Staffs { get; set; }


        // public DbSet<Hotel_Bulebird.Models.ConfirmedBooking> ConfirmedBookings { get; set; }

        public DbSet<ConfirmedBooking> ConfirmedBookings { get; set; }




        public DbSet<Payment> Payments { get; set; }


    }
}
