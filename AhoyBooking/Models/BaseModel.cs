using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AhoyBooking.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        [JsonIgnore]
        public DateTime? DateModified { get; set; }

        public BaseModel()
        {
            IsDeleted = false;
            DateCreated = DateTime.Now;
        }
    }
}
