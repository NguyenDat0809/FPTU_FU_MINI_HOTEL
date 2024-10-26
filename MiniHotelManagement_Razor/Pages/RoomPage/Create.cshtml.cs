using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelManagement_BusinessObject.Context;
using HotelManagement_BusinessObject.Models;
using HotelManagement_Services.Interfaces;
using static MessagePack.MessagePackSerializer;

namespace MiniHotelManagement_Razor.Pages.RoomPage
{
    public class CreateModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;


        public CreateModel(IRoomService roomService, IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
            _roomService = roomService;
        }

        public async Task<IActionResult> OnGet()
        {
            var types = await _roomTypeService.GetRoomTypes();
            ViewData["RoomTypeId"] = new SelectList(types, "RoomTypeId", "RoomTypeId");
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _roomService == null || Room == null)
            {
                return Page();
            }

            var addResult = await _roomService.CreateRoom(Room);
            if (!addResult)
                TempData["ErrorMessage"] = "Add fail";
            else
                TempData["SuccessMessage"] = "Add success";
            return RedirectToPage("./Index");
        }
    }
}
