using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AhoyBooking.Models.Enums;

namespace AhoyBooking.Models
{
    public class Hotel : BaseModel
    {
        public string HotelName { get; set; }
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        //comma seperated string
        public string Location { get; set; }
        public HotelType HotelType { get; set; }
        public virtual List<HotelImage> HotelImages { get; set; }
        public virtual List<RoomsPrice> RoomsPrices { get; set; }
    }
}