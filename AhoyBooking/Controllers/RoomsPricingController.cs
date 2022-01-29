using AhoyBooking.Models;
using AhoyBooking.Services;
using AhoyBooking.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AhoyBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsPricingController : ControllerBase
    {
        private readonly IRoomsPriceService _roomsPriceService;
        private readonly IMapper _mapper;
        public RoomsPricingController(IRoomsPriceService roomsPriceService, IMapper mapper)
        {
            _roomsPriceService = roomsPriceService;
            _mapper = mapper;
        }
        [HttpPost("AddRoomPrice")]
        public ActionResult<RoomsPrice> AddRoomPrice(PriceVM priceVM) => Ok(_roomsPriceService.AddRoomsPrice(_mapper.Map<RoomsPrice>(priceVM)));
        [HttpGet("CalculateActualPrice")]
        public ActionResult<RoomsPrice> CalculateActualPrice(int pricingId, int persons, DateTime checkIn, DateTime checkOut)
        {
            var result = _roomsPriceService.CalculatePrice(pricingId, persons, checkIn, checkOut);
            if (result is RoomsPrice)
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
