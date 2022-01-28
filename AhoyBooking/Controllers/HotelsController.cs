using AhoyBooking.Data;
using AhoyBooking.Models;
using AhoyBooking.Services;
using AhoyBooking.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AhoyBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsService _hotelsService;
        private readonly IHotelImageRepository _hotelImageRepository;
        private readonly IMapper _mapper;
        public HotelsController(IHotelsService hotelsService, IMapper mapper, IHotelImageRepository hotelImageRepository)
        {
            _hotelsService = hotelsService;
            _mapper = mapper;
            _hotelImageRepository = hotelImageRepository;
        }

        [HttpPost]
        public Hotel AddHotel(HotelViewModel hotelVM)
        {
            Hotel hotel = _mapper.Map<Hotel>(hotelVM);
            return _hotelsService.AddHotel(hotel);
        }
        [HttpGet("GetAllHotels")]
        public List<Hotel> GetHotels(int page = 0, int count = 10) => _hotelsService.GetHotels(page, count);
        [HttpGet("GetHotelById")]
        public Hotel GetHotel(int Id) => _hotelsService.GetHotel(Id);
        [HttpGet("GetHotelsByName")]
        public List<Hotel> GetHotelsByName(string name) => _hotelsService.GetHotelsByName(name);
        [HttpPost("AddToGallery")]
        public ActionResult<int> AddToGallery(HotelGallery gallery)
        {
            if (gallery.HotelId > 0)
            {
                List<HotelImage> hotelImages = new();
                foreach (var item in gallery.URLs)
                {
                    hotelImages.Add(new HotelImage
                    {
                        HotelId = gallery.HotelId,
                        Url = item
                    });
                }
                _hotelImageRepository.AddRange(hotelImages);
                if (_hotelImageRepository.SaveChanges() > 0)
                {
                    return Ok(gallery);
                }
                else
                {
                    return BadRequest(gallery);
                }
            }
            else
            {
                return BadRequest(gallery);
            }
        }
    }
}
