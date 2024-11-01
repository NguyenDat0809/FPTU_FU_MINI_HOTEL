using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelManagement_BusinessObject.Models;
using HotelManagement_Services.Interfaces;

namespace MiniHotelManagement_Razor.Pages.ReservationPage
{
    public class CreateModel : PageModel
    {
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;
        public CreateModel(IReservationService reservationService, IRoomService roomService)
        {
            _reservationService = reservationService;
            _roomService = roomService;
        }

        public async Task<IActionResult> OnGet()
        {
            var rooms = await _roomService.GetRooms();
            ViewData["RoomId"] = new SelectList(rooms, "RoomId", "RoomName");
            return Page();
        }

        [BindProperty]
        public BookingReservation BookingReservation { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _reservationService == null || BookingReservation == null)
            {
                return Page();
            }
            if (DateTime.Parse(BookingReservation.BookingDateFormat).Day > DateTime.Now.Day)
            {
                TempData["ErrorMessage"] = "Only book for tomorow";
                return Page(); ;
            }
            var duplicatedReservation = await _reservationService.GetReservationById(BookingReservation.BookingReservationId);
            if (duplicatedReservation != null)
            {
                TempData["ErrorMessage"] = "Duplicated Reservation id";
                return Page(); ;
            }
            var duplicatedDayReservation = await _reservationService.GetReservationsByDay(BookingReservation.BookingDate.Value);
            if (duplicatedDayReservation != null)
            {
                var isSameRoomInDay = false;
                foreach (var roomInDay in duplicatedDayReservation)
                {
                    if (roomInDay.RoomId == BookingReservation.RoomId)
                    {
                        isSameRoomInDay = true;
                        break;
                    }
                }
                if (isSameRoomInDay)
                {
                    TempData["ErrorMessage"] = $"This room is ordered in {BookingReservation.BookingDate}";
                    return Page();
                }

            }
            BookingReservation.BookingDate = DateTime.Parse(BookingReservation.BookingDateFormat);
            var addRs = await _reservationService.CreateReservation(BookingReservation);
            if (!addRs)
                TempData["ErrorMessage"] = "Add fail";
            else
                TempData["SuccessMessage"] = "Add success";

            return RedirectToPage("./Index");
        }
    }
}
