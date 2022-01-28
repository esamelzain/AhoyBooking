using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AhoyBooking.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Book> Books { get; set; }
    }
}