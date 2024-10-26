﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelManagement_BusinessObject.Context;
using HotelManagement_BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using HotelManagement_Services.Interfaces;

namespace MiniHotelManagement_Razor.Pages.RoomTypePage
{
    public class CreateModel : PageModel
    {
        private readonly IRoomTypeService _roomTypeService;

        public CreateModel(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RoomType RoomType { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _roomTypeService == null || RoomType == null)
            {
                return Page();
            }

           var addRs = await _roomTypeService.CreateRoomType(RoomType);
            if (!addRs)
                TempData["ErrorMessage"] = "Add fail";
            else
                TempData["SuccessMessage"] = "Add success";

            return RedirectToPage("./Index");
        }
    }
}
