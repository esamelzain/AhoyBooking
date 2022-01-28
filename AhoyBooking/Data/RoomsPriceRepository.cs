using AhoyBooking.Models;
using System.Collections.Generic;

namespace AhoyBooking.Data
{
    public interface IRoomsPriceRepository : IRepository<RoomsPrice>
    {
    }
    public class RoomsPriceRepository : Repository<RoomsPrice>, IRoomsPriceRepository
    {
        public RoomsPriceRepository(AhoyDbContext context) : base(context)
        {
        }

        public AhoyDbContext AhoyDbContext => Context as AhoyDbContext;

    }
}