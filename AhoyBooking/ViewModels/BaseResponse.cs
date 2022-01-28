using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoyBooking.ViewModels
{
    public class BaseResponse
    {
        public ResponseMessage Message { get; set; }
    }
    public class ResponseMessage
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
