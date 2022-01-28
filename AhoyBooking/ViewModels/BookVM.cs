using System;
using static AhoyBooking.Models.Enums;

namespace AhoyBooking.ViewModels
{
    public class BookVM
    {
        public int Rooms { get; set; }
        public RoomType RoomType { get; set; }
        public int Persons { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
