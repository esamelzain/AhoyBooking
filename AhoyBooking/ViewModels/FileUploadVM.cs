using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoyBooking.ViewModels
{
    public class FileUploadVM
    {
        public IFormFile FormFile { get; set; }
        public string folder { get; set; }
    }
    public class FileURL
    {
        public string FilePath { get; set; }
        public string WebUrl { get; set; }
    }
}
