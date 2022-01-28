using AhoyBooking.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhoyBooking.Services
{
    public interface IFileUpload
    {
        FileURL UploadFile(string subPath, IFormFile formFile);
    }

    public class FileUpload : IFileUpload
    {
        public FileURL UploadFile(string subPath, IFormFile formFile)
        {
            Handler handler = new();
            FileURL path;
            //Save File Here
            if (handler.CheckIfExcelFile(formFile))
            {
                path = handler.WriteFile(subPath, formFile);
            }
            else
            {
                path = new FileURL() { FilePath = "Error", WebUrl = "Not correct file extention" };
            }
            return path;
        }
    }
}
