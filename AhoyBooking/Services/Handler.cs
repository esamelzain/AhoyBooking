using AhoyBooking.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
namespace AhoyBooking.Services
{
    class Handler
    {
        public FileURL WriteFile(string subPath, IFormFile file)
        {
            bool isSaveSuccess = false;
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads\\{subPath}");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), pathBuilt,
                    fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
                return new FileURL()
                {
                    FilePath = $"Uploads{(subPath == "" ? $"\\{subPath}" : "")}\\{fileName}".Replace("\\", "/"),
                    WebUrl = $""
                };

            }
            catch (Exception e)
            {
                //log error
            }
            return new FileURL();
        }
        public bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1].ToLower();
            return (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif");
        }
    }
}

