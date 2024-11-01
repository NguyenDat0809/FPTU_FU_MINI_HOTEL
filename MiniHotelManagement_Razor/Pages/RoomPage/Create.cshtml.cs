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
using MiniHotelManagement_Razor.Extensions;
using System.ComponentModel.DataAnnotations;
using HotelManagement_Services.Implements;

namespace MiniHotelManagement_Razor.Pages.RoomPage
{
    public class CreateModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IRazorPictureService _pictureService;
       
        public CreateModel(IRoomService roomService, IRoomTypeService roomTypeService, IRazorPictureService pictureService)
        {
            _roomTypeService = roomTypeService;
            _roomService = roomService;
            _pictureService = pictureService;
        }

        public async Task<IActionResult> OnGet()
        {
            var types = await _roomTypeService.GetRoomTypes();
            ViewData["RoomTypeId"] = new SelectList(types, "RoomTypeId", "RoomTypeName");
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; } = default!;
        [Required(ErrorMessage = "Please choose at least 1 file")]
        [DataType(DataType.Upload)]
        //[AllowedExtensions(errorMessage: "Only png, jpg, jpeg, gif file are allowed", ".png", ".jpg", ".jpeg", ".gif")]
        //[FileExtensions(Extensions = "png,jpg,jpeg,gif", ErrorMessage = "Only png, jpg, jpeg, gif files are allowed.")]
        [Display(Name = " Choose file(s) to upload")]
        [BindProperty]
        public IFormFile[] FileUpload { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _roomService == null || Room == null)
            {
                var types = await _roomTypeService.GetRoomTypes();
                ViewData["RoomTypeId"] = new SelectList(types, "RoomTypeId", "RoomTypeName");

                return Page();
            }
            var roomCheck = await _roomService.GetRoomById(Room.RoomId);
            if (roomCheck != null)
            {
                TempData["ErrorMessage"] = "Account Id Duplicated";
                return RedirectToPage("./Index");
            }
            
            //upload image
            if (FileUpload != null)
            {
                var imagePath = await _pictureService.SaveImageToEnv(FileUpload[0], ".png", ".jpg", ".jpeg", ".gif");
                if (string.IsNullOrEmpty(imagePath))
                {
                    TempData["ErrorMessage"] = "Upload Image(.png, .jpg, .jpeg, .gif) failed";
                    return RedirectToPage("./Index");
                }
                Room.ImageUrl = imagePath;
                
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
