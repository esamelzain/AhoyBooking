using AhoyBooking.Data;
using AhoyBooking.Models;
using AhoyBooking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhoyBooking.Services
{
    public interface IHotelsService
    {
        Hotel AddHotel(Hotel hotel);
        List<Hotel> GetHotels(int page, int count);
        List<Hotel> GetHotelsByName(string name);
        Hotel GetHotel(int Id);
        object SearchHotel(DateTime checkIn, DateTime checkOut, int persons, int page, int count, string key = "");
    }
    public class HotelsService : IHotelsService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelImageRepository _imageRepository;
        private readonly IRoomsPriceRepository _roomsPriceRepository;
        public HotelsService(IHotelRepository hotelRepository, IHotelImageRepository imageRepository, IRoomsPriceRepository roomsPriceRepository)
        {
            _hotelRepository = hotelRepository;
            _imageRepository = imageRepository;
            _roomsPriceRepository = roomsPriceRepository;
        }
        /// <summary>
        /// return the new hotel Id
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns>Hotel hotel</returns>
        public Hotel AddHotel(Hotel hotel) => _hotelRepository.AddWithReturned(hotel);
        /// <summary>
        /// get hotels list
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns>List<Hotel> hotels</returns>
        public List<Hotel> GetHotels(int page, int count) => _hotelRepository.GetAll(page, count).ToList();
        /// <summary>
        /// Get Hotel Details By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Hotel hotel</returns>
        public Hotel GetHotel(int Id)
        {
            var hotel = _hotelRepository.Get(Id);
            hotel.HotelImages = _imageRepository.GetByHotelId(Id);
            hotel.RoomsPrices = _roomsPriceRepository.GetByHotelId(Id);
            return hotel;
        }
        /// <summary>
        /// Get List of hotels by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns> List<Hotel>  hotels</returns>
        public List<Hotel> GetHotelsByName(string name) => _hotelRepository.GetByName(name).ToList();
        /// <summary>
        /// Search Hotels
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public object SearchHotel(DateTime checkIn, DateTime checkOut, int persons, int page, int count, string key = "")
        {
            List<Hotel> hotels = _hotelRepository.SearchHotel(page, count, key).ToList();
            int days = (int)(checkOut - checkIn).TotalDays;
            days = days == 0 ? 1 : days;
            if (days < 0)
            {
                return new ResponseMessage { Message = "Please choose correct date (check In/check Out)", Code = 400 };
            }
            foreach (var hotel in hotels)
            {
                hotel.RoomsPrices = _roomsPriceRepository.GetByHotelId(hotel.Id).OrderByDescending(r => r.Persons).ToList();
                List<RoomsPrice> calculatedPrices = new();
                foreach (var price in hotel.RoomsPrices)
                {
                    if (persons >= price.Persons)
                    {
                        decimal personPrice = price.ActualPrice / price.Persons;
                        price.Persons = persons;
                        price.ActualPrice = personPrice * persons * days;
                        calculatedPrices.Add(price);
                    }
                }
                hotel.RoomsPrices = calculatedPrices;
            }
            return hotels;
        }
    }
}
