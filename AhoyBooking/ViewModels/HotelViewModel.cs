using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static AhoyBooking.Models.Enums;

namespace AhoyBooking.ViewModels
{
    public class HotelViewModel
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string Description { get; set; }
        [EmailAddress]
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        //comma seperated string
        public string Location { get; set; }
        public HotelType HotelType { get; set; }
    }
    public class HotelGallery
    {
        public int HotelId { get; set; }
        public List<string> URLs { get; set; }
    }
}
