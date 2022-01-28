using AhoyBooking.Models;
using System.Collections.Generic;
using System.Linq;

namespace AhoyBooking.Data
{
    public interface IBookRepository : IRepository<Book>
    {
        public List<Book> GetUserBooks(string userId);
    }
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AhoyDbContext context) : base(context)
        {
        }
        public List<Book> GetUserBooks(string userId) => AhoyDbContext.Books.Where(book => book.IdentityId == userId).ToList();
        public AhoyDbContext AhoyDbContext => Context as AhoyDbContext;
    }
}