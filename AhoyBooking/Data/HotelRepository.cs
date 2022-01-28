using AhoyBooking.Models;
using System.Collections.Generic;
using System.Linq;

namespace AhoyBooking.Data
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        IEnumerable<Hotel> GetByName(string name);
    }
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(AhoyDbContext context) : base(context)
        {
        }
        public IEnumerable<Hotel> GetByName(string name) => AhoyDbContext.Hotels.Where(user => user.HotelName.Contains(name));
        public AhoyDbContext AhoyDbContext => Context as AhoyDbContext;
    }
}