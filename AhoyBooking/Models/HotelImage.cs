using System.ComponentModel.DataAnnotations.Schema;

namespace AhoyBooking.Models
{
    public class HotelImage : BaseModel
    {
        public string Url { get; set; }
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
    }
}
