using AhoyBooking.Data;
using AhoyBooking.Models;
using AhoyBooking.Services;
using AhoyBooking.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public ActionResult<Hotel> AddHotel(HotelViewModel hotelVM) => Ok(_hotelsService.AddHotel(_mapper.Map<Hotel>(hotelVM)));
        [HttpGet("GetAllHotels")]
        public ActionResult<List<Hotel>> GetHotels(int page = 0, int count = 10) => Ok(_hotelsService.GetHotels(page, count));
        [HttpGet("GetHotelById")]
        public ActionResult<Hotel> GetHotel(int Id) => Ok(_hotelsService.GetHotel(Id));
        [HttpGet("GetHotelsByName")]
        public ActionResult<List<Hotel>> GetHotelsByName(string name) => Ok(_hotelsService.GetHotelsByName(name));
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
        [HttpGet("Search")]
        public ActionResult<List<Hotel>> SearchHotel(DateTime checkIn, DateTime checkOut, int persons = 1, string key = "", int page = 0, int count = 10)
        {
            var result = _hotelsService.SearchHotel(checkIn, checkOut, persons, page, count);
            if (result is List<Hotel>)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

    }
}
