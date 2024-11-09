using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagement_BusinessObject.Context;
using HotelManagement_BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using HotelManagement_Services.Interfaces;
using HotelManagement_Services.Implements;

namespace MiniHotelManagement_Razor.Pages.RoomTypePage
{
 
    public class EditModel : PageModel
    {
        private readonly IRoomTypeService _roomTypeService;
        public EditModel(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        [BindProperty]
        public RoomType RoomType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _roomTypeService == null)
            {
                TempData["ErrorMessage"] = "Not found room type in database";
                return RedirectToPage("./Index");
            }

            var roomtype =  await _roomTypeService.GetRoomTypeById(id);
            if (roomtype == null)
            {TempData["ErrorMessage"] = "Not found room type in database";
                return RedirectToPage("./Index");
            }
            RoomType = roomtype;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
               var updateRs =  await _roomTypeService.UpdateRoomType(RoomType);
                if (!updateRs)
                    TempData["ErrorMessage"] = "update fail";
                else
                    TempData["SuccessMessage"] = "Update success";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoomTypeExists(RoomType.RoomTypeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> RoomTypeExists(string id)
        {
            return await _roomTypeService.GetRoomTypeById(id) != null;
        }
    }
}
