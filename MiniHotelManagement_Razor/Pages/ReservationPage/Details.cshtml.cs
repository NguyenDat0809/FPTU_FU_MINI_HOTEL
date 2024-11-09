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

namespace MiniHotelManagement_Razor.Pages.Reservation
{
    public class DetailsModel : PageModel
    {
        private readonly IReservationService _reservationService;
        public Dictionary<byte?, string> status = new Dictionary<byte?, string>
        {
            { 0, "New" },
            { 1, "Done" },
            { 2, "Cancled" },

        };
        public DetailsModel(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

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
            else 
            {
                BookingReservation = bookingreservation;
            }
            return Page();
        }
    }
}
