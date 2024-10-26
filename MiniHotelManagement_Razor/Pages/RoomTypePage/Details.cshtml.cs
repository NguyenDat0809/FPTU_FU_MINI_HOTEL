using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement_BusinessObject.Context;
using HotelManagement_BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using HotelManagement_Services.Interfaces;

namespace MiniHotelManagement_Razor.Pages.RoomTypePage
{
    public class DetailsModel : PageModel
    {
        private readonly IRoomTypeService _roomTypeService;

        public DetailsModel(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

      public RoomType RoomType { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _roomTypeService == null)
            {
                return NotFound();
            }

            var roomtype = await _roomTypeService.GetRoomTypeById(id);
            if (roomtype == null)
            {
                return NotFound();
            }
            else 
            {
                RoomType = roomtype;
            }
            return Page();
        }
    }
}
