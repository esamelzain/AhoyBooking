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
    public interface IBookingService
    {
        Book AddBook(Book book);
        List<Book> GetUserBooks();
        Book GetBook(int Id);
    }
    public class BookingService : IBookingService
    {
        private readonly IBookRepository _bookRepository;

        private readonly ClaimsPrincipal _caller;
        private readonly HttpContext _httpContext;
        public readonly string _language;
        public readonly string profileId;

        public BookingService(IHttpContextAccessor httpContextAccessor, IBookRepository bookRepository)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _httpContext = httpContextAccessor.HttpContext;
            Claim profileIdClaim = _caller.Claims.FirstOrDefault(c => c.Type == "userId");
            profileId = profileIdClaim == null ? "" : profileIdClaim.Value;
            _bookRepository = bookRepository;
        }
        /// <summary>
        /// return the new booking Id
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Book book</returns>
        public Book AddBook(Book book) => _bookRepository.AddWithReturned(book);
        /// <summary>
        /// get Books list
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns>List<Book> Books</returns>
        public List<Book> GetUserBooks() => _bookRepository.GetUserBooks(profileId).ToList();
        /// <summary>
        /// Get Book Details By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Book Book</returns>
        public Book GetBook(int Id) => _bookRepository.Get(Id);
    }
}
