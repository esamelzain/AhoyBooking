using AhoyBooking.Models;
using System.Collections.Generic;
using System.Linq;

namespace AhoyBooking.Data
{
    public interface IHotelImageRepository : IRepository<HotelImage>
    {
        List<HotelImage> GetByHotelId(int hotelId);
    }
    public class HotelImageRepository : Repository<HotelImage>, IHotelImageRepository
    {
        public HotelImageRepository(AhoyDbContext context) : base(context)
        {
        }
        public List<HotelImage> GetByHotelId(int hotelId) => AhoyDbContext.HotelImages.Where(img => img.HotelId == hotelId).ToList();

        public AhoyDbContext AhoyDbContext => Context as AhoyDbContext;

    }
}