using AhoyBooking.Data;
using AhoyBooking.Models;
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
    }
    public class HotelsService : IHotelsService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelImageRepository _imageRepository;
        public HotelsService(IHotelRepository hotelRepository, IHotelImageRepository imageRepository)
        {
            _hotelRepository = hotelRepository;
            _imageRepository = imageRepository;
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
            return hotel;
        }
        /// <summary>
        /// Get List of hotels by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns> List<Hotel>  hotels</returns>
        public List<Hotel> GetHotelsByName(string name) => _hotelRepository.GetByName(name).ToList();

    }
}
