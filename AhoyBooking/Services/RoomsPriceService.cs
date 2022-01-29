using AhoyBooking.Data;
using AhoyBooking.Models;
using AhoyBooking.ViewModels;
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
        object CalculatePrice(int pricingId, int persons, DateTime checkIn, DateTime checkOut);
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
        /// <summary>
        /// claculating actual price
        /// </summary>
        /// <param name="pricingId,persons,checkIn checkOut"></param>
        /// <returns></returns>
        public object CalculatePrice(int pricingId, int persons, DateTime checkIn, DateTime checkOut)
        {
            int days = (int)(checkOut - checkIn).TotalDays;
            if (pricingId<=0)
            {
                return new ResponseMessage { Message = "Please choose correct package", Code = 400 };
            }
            days = days == 0 ? 1 : days;
            if (days < 0 )
            {
                return new ResponseMessage { Message = "Please choose correct date (check In/check Out)", Code = 400 };
            }
            RoomsPrice roomsPrice = _roomsPriceRepository.Get(pricingId);
            if (persons >= roomsPrice.Persons)
            {
                decimal personPrice = roomsPrice.ActualPrice / roomsPrice.Persons;
                roomsPrice.Persons = persons;
                roomsPrice.ActualPrice = personPrice * persons * days;
            }
            return roomsPrice;
        }
    }
}
