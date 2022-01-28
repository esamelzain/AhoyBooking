using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static AhoyBooking.Models.Enums;

namespace AhoyBooking.Models
{
    public class Book: BaseModel
    {
        public int Rooms { get; set; }
        public RoomType RoomType { get; set; }
        public int Persons { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsConfirmed { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal PaidAmount { get; set; }
        public string IdentityId { get; set; }
        [ForeignKey("IdentityId")]
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
