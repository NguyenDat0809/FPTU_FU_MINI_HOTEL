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

namespace MiniHotelManagement_Razor.Pages.ReservationPage
{
    public class EditModel : PageModel
    {
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;

        public EditModel(IReservationService reservationService, IRoomService roomService)
        {
            _reservationService = reservationService;
            _roomService = roomService;
        }

        [BindProperty]
        public BookingReservation BookingReservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _reservationService == null)
            {
                return NotFound();
            }

            var bookingreservation = await _reservationService.GetReservationById(id);
            if (bookingreservation == null)
            {
                return NotFound();
            }
            BookingReservation = bookingreservation;
            var rooms = await _roomService.GetRooms();
            ViewData["RoomId"] = new SelectList(rooms, "RoomId", "RoomName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var rooms = await _roomService.GetRooms();
                ViewData["RoomId"] = new SelectList(rooms, "RoomId", "RoomName");
                return Page();
            }

            try
            {
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
                var updateRs = await _reservationService.UpdateReservation(BookingReservation);
                if (!updateRs)
                    TempData["ErrorMessage"] = "Update fail";
                else
                    TempData["SuccessMessage"] = "Update success";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await BookingReservationExists(BookingReservation.BookingReservationId))
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

        private async Task<bool> BookingReservationExists(string id)
        {
            return await _reservationService.GetReservationById(id) != null;
        }
    }
}
