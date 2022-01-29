
using AhoyBooking.Services;
using AhoyBooking.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AhoyBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploaderController : ControllerBase
    {
        private readonly IFileUpload _fileUpload;

        public FileUploaderController(IFileUpload fileUpload)
        {
            _fileUpload = fileUpload;
        }
        [HttpPost]
        [Route("upload")]
        public ActionResult<FileURL> Upload([FromForm] FileUploadVM fileUploadVM)
        {
            return Ok(_fileUpload.UploadFile(fileUploadVM.folder, fileUploadVM.FormFile));
        }
    }
}
