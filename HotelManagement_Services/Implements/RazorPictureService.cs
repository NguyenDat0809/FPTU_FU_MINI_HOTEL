using HotelManagement_Services.Interfaces;
using System;
using System.Collections.Generic;
//using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace HotelManagement_Services.Implements
{
    public class RazorPictureService : IRazorPictureService
    {
        //private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHostingEnvironment _env;
        public RazorPictureService(IHostingEnvironment env)
        {
            _env = env;
        }
        public async Task<string> SaveImageToEnv(IFormFile file, params string[] allowExtensions)
        {
            try
            {
                var wwwPath = _env.WebRootPath;
                var imagesPath = Path.Combine(wwwPath, "images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }
                var desPath = Path.Combine(imagesPath, file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if(!allowExtensions.Contains(extension))
                {
                    return string.Empty;
                }
                using (var fileStream = new FileStream(desPath, FileMode.OpenOrCreate))
                {
                    await file.CopyToAsync(fileStream);

                };
                return file.FileName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


    }
}
