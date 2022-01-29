using AhoyBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AhoyBooking.Data
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        IEnumerable<Hotel> GetByName(string name);
        IEnumerable<Hotel> SearchHotel(int page, int count, string key);
    }
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(AhoyDbContext context) : base(context)
        {
        }
        public IEnumerable<Hotel> SearchHotel(int page, int count, string key)
        {
            count = count == 0 ? 10 : count;
            int skip = count * page;
            return AhoyDbContext.Hotels
                .Where(hotel => hotel.HotelName.Contains(key)
                || hotel.Description.Contains(key) || hotel.Address.Contains(key))
                .Skip(skip).Take(count).ToList();
        }
        public IEnumerable<Hotel> GetByName(string name) => AhoyDbContext.Hotels.Where(hotel => hotel.HotelName.Contains(name));
        public AhoyDbContext AhoyDbContext => Context as AhoyDbContext;
    }
}