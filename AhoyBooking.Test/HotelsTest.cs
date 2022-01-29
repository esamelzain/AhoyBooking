using AhoyBooking.Controllers;
using AhoyBooking.Data;
using AhoyBooking.Models;
using AhoyBooking.Services;
using AhoyBooking.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AhoyBooking.Test
{
    public class HotelsTest
    {
        private static IMapper _mapper;
        private static Mock<IHotelsService> _hotelMock;
        private static Mock<IHotelImageRepository> _imagesMock;
        public HotelsTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _hotelMock = new Mock<IHotelsService>();
            _hotelMock.Setup(repo => repo.AddHotel(It.IsAny<Hotel>()))
                .Returns(new Hotel {Id=1 })
                .Verifiable();
            _imagesMock = new Mock<IHotelImageRepository>();
        }
        [Fact]
        public void AddHotel()
        {
            HotelsController hotelsController = new HotelsController(_hotelMock.Object, _mapper, _imagesMock.Object);
            var result = hotelsController.AddHotel(GetHotelVM());
            Assert.IsType<ActionResult<Hotel>>(result.Result);
        }
        //helper
        private HotelViewModel GetHotelVM() => new HotelViewModel
        {
            Address = "Dubai",
            ContactEmail = "info@Khartoum.com",
            ContactNumber = "0561127043",
            Description = "Feel like you are sudan in dubai",
            HotelName = "Khartoum",
            HotelType = Enums.HotelType.Luxury,
            Id = 1,
            Location = "15.3636,13.589"
        };
    }
}
