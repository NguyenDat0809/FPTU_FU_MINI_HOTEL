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
using HotelManagement_Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using HotelManagement_Services.Implements;
using MiniHotelManagement_Razor.Extensions;

namespace MiniHotelManagement_Razor.Pages.RoomPage
{
    public class EditModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IRazorPictureService _razorPictureService;

        public EditModel(IRoomService roomService, IRoomTypeService roomTypeService, IRazorPictureService razorPictureService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _razorPictureService = razorPictureService;
        }

        [BindProperty]
        public Room Room { get; set; } = default!;
        [Required(ErrorMessage = "Please choose at least 1 file")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(errorMessage: "Only png, jpg, jpeg, gif file are allowed", ".png",".jpg", ".jpeg", ".gif")]
        //[FileExtensions(Extensions = "png,jpg,jpeg,gif", ErrorMessage = "Only png, jpg, jpeg, gif files are allowed.")]
        [Display(Name = " Choose file(s) to upload")]
        [BindProperty]
        public IFormFile[] FileUpload { get; set; }

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
            Room = room;
            var types = await _roomTypeService.GetRoomTypes();
            ViewData["RoomTypeId"] = new SelectList(types, "RoomTypeId", "RoomTypeName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var types = await _roomTypeService.GetRoomTypes();
                ViewData["RoomTypeId"] = new SelectList(types, "RoomTypeId", "RoomTypeName");

                return Page();
            }



            try
            {
                //upload image
                if (FileUpload != null)
                {
                   var imagePath = await _razorPictureService.SaveImageToEnv(FileUpload[0], ".png", ".jpg", ".jpeg", ".gif");
                    if(string.IsNullOrEmpty(imagePath))
                    {
                        TempData["ErrorMessage"] = "Upload Image(.png, .jpg, .jpeg, .gif) failed";
                        return RedirectToPage("./Index");
                    }
                    Room.ImageUrl = imagePath;
                    var updateRs = await _roomService.UpdateRoom(Room);
                    if (!updateRs)
                        TempData["ErrorMessage"] = "update fail";
                    else
                        TempData["SuccessMessage"] = "Update success";
                }
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoomExists(Room.RoomId))
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

        private async Task<bool> RoomExists(string id)
        {
            return await _roomService.GetRoomById(id) != null;
        }
    }
}
