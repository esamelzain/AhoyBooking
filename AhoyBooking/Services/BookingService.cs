using AhoyBooking.Data;
using AhoyBooking.Models;
using AhoyBooking.ViewModels;
using System.Collections.Generic;
using System.Linq;
namespace AhoyBooking.Services
{
    public interface IBookingService
    {
        object AddBook(Book book,int pricingId);
        List<Book> GetUserBooks(string profileId);
        Book GetBook(int Id);
    }
    public class BookingService : IBookingService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IRoomsPriceService _roomsPrice;
        public BookingService(IBookRepository bookRepository, IRoomsPriceService roomsPrice)
        {
            _bookRepository = bookRepository;
            _roomsPrice = roomsPrice;
        }
        /// <summary>
        /// return the new booking Id
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Book book</returns>
        public object AddBook(Book book, int pricingId) 
        {
            var result = _roomsPrice.CalculatePrice(pricingId,book.Persons,book.CheckIn,book.CheckOut);
            if (result is RoomsPrice)
            {
                RoomsPrice price = result as RoomsPrice;
                book.ActualPrice = price.ActualPrice;
                return _bookRepository.AddWithReturned(book);
            }
            else
            {
                return new ResponseMessage { Message = "Please choose correct date (check In/check Out)", Code = 400 };
            }
        }
        /// <summary>
        /// get Books list
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns>List<Book> Books</returns>
        public List<Book> GetUserBooks(string profileId) => _bookRepository.GetUserBooks(profileId).ToList();
        /// <summary>
        /// Get Book Details By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Book Book</returns>
        public Book GetBook(int Id) => _bookRepository.Get(Id);
    }
}
