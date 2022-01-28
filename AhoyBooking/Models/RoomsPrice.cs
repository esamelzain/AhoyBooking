using Newtonsoft.Json;
using static AhoyBooking.Models.Enums;

namespace AhoyBooking.Models
{
    public class RoomsPrice : BaseModel
    {
        public int HotelId { get; set; }
        public int RoomsCount { get; set; }
        public int Persons { get; set; }
        public RoomType RoomType { get; set; }
        public decimal ActualPrice { get; set; }
        [JsonIgnore]
        public virtual Hotel Hotel { get; set; }
    }
}
