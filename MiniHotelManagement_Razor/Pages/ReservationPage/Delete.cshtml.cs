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

namespace MiniHotelManagement_Razor.Pages.ReservationPage
{
    public class DeleteModel : PageModel
    {
        private readonly IReservationService _reservationService;

        public DeleteModel(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [BindProperty]
      public BookingReservation BookingReservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _reservationService  == null)
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _reservationService == null)
            {
                return NotFound();
            }
            var bookingreservation = await _reservationService.GetReservationById(id);

            if (bookingreservation != null)
            {
                BookingReservation = bookingreservation;
                _reservationService.DeleteReservation(BookingReservation);
            }

            return RedirectToPage("./Index");
        }
    }
}
