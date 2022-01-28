using static AhoyBooking.Models.Enums;

namespace AhoyBooking.ViewModels
{
    public class PriceVM
    {
        public int HotelId { get; set; }
        public int RoomsCount { get; set; }
        public int Persons { get; set; }
        public RoomType RoomType { get; set; }
        public decimal ActualPrice { get; set; }
    }
}
