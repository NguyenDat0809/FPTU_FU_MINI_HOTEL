using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement_BusinessObject.Context;
using HotelManagement_BusinessObject.Models;
using HotelManagement_Services.Interfaces;

namespace MiniHotelManagement_Razor.Pages.RoomPage
{
    public class DeleteModel : PageModel
    {
        private readonly IRoomService _roomService;

        public DeleteModel(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [BindProperty]
        public Room Room { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _roomService == null)
            {
                return NotFound();
            }

            var room = await _roomService.GetRoomById(id);

            if (room == null)
            {
                return NotFound();
            }
            else
            {
                Room = room;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _roomService == null)
            {
                return NotFound();
            }
            var room = await _roomService.GetRoomById(id);

            if (room != null)
            {
                Room = room;
                var deleteRs = await _roomService.DeleteRoom(room);
                if (!deleteRs)
                    TempData["ErrorMessage"] = "Delete fail";
                else
                    TempData["SuccessMessage"] = "Delete success";
            }

            return RedirectToPage("./Index");
        }
    }
}
