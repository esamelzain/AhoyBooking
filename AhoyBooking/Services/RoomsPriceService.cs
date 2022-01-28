using AhoyBooking.Data;
using AhoyBooking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AhoyBooking.Services
{
    public interface IRoomsPriceService
    {
        RoomsPrice AddRoomsPrice(RoomsPrice RoomsPrice);
        RoomsPrice GetRoomsPrice(int Id);
    }
    public class RoomsPriceService : IRoomsPriceService
    {
        private readonly IRoomsPriceRepository _roomsPriceRepository;

        public RoomsPriceService(IRoomsPriceRepository roomsPriceRepository)
        {
            _roomsPriceRepository = roomsPriceRepository;
        }
        /// <summary>
        /// return the new RoomsPriceing Id
        /// </summary>
        /// <param name="RoomsPrice"></param>
        /// <returns>RoomsPrice RoomsPrice</returns>
        public RoomsPrice AddRoomsPrice(RoomsPrice RoomsPrice) => _roomsPriceRepository.AddWithReturned(RoomsPrice);
        /// <summary>
        /// Get RoomsPrice Details By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>RoomsPrice RoomsPrice</returns>
        public RoomsPrice GetRoomsPrice(int Id) => _roomsPriceRepository.Get(Id);
    }
}
