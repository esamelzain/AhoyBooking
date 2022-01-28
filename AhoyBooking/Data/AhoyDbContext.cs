using AhoyBooking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace AhoyBooking.Data
{
    public class AhoyDbContext : IdentityDbContext<ApplicationUser>
    {
        public AhoyDbContext(DbContextOptions<AhoyDbContext> options) : base(options)
        {
          
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<RoomsPrice> RoomsPrices { get; set; }
    }
}