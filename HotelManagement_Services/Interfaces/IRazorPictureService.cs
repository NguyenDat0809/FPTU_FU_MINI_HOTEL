using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Interfaces
{
    public interface IRazorPictureService
    {
        Task<string> SaveImageToEnv(IFormFile file, params string[] allowExtensions);
    }
}
