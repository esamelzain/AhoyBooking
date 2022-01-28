using AhoyBooking.Models;
using AhoyBooking.Services;
using AhoyBooking.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public RoomsPrice AddHotel(PriceVM priceVM) => _roomsPriceService.AddRoomsPrice(_mapper.Map<RoomsPrice>(priceVM));

    }
}
