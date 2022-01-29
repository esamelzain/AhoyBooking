using AhoyBooking.Models;
using AhoyBooking.Services;
using AhoyBooking.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace AhoyBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookingService _bookingService;
        private readonly ClaimsPrincipal _caller;
        public readonly string profileId;
        public BookingController(IHttpContextAccessor httpContextAccessor, IBookingService bookingService, IMapper mapper)
        {
            _caller = httpContextAccessor.HttpContext.User;
            Claim profileIdClaim = _caller.Claims.FirstOrDefault(c => c.Type == "userId");
            profileId = profileIdClaim == null ? "" : profileIdClaim.Value;
            _mapper = mapper;
            _bookingService = bookingService;
        }
        [HttpPost("AddBook")]
        [Authorize]
        public ActionResult<Book> AddBook(BookVM bookVM)
        {
            Book book = _mapper.Map<Book>(bookVM);
            book.IdentityId = profileId;
            var result = _bookingService.AddBook(book, bookVM.PricingId);
            if (result is Book)
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
