using AhoyBooking.Models;
using System.Collections.Generic;
using System.Linq;

namespace AhoyBooking.Data
{
    public interface IRoomsPriceRepository : IRepository<RoomsPrice>
    {
        List<RoomsPrice> GetByHotelId(int hotelId);
    }
    public class RoomsPriceRepository : Repository<RoomsPrice>, IRoomsPriceRepository
    {
        public RoomsPriceRepository(AhoyDbContext context) : base(context)
        {
        }
        public List<RoomsPrice> GetByHotelId(int hotelId) => AhoyDbContext.RoomsPrices.Where(img => img.HotelId == hotelId).ToList();

        public AhoyDbContext AhoyDbContext => Context as AhoyDbContext;

    }
}