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
    public class IndexModel : PageModel
    {
        private readonly IReservationService _reservationService;
        public Dictionary<byte?, string> status = new Dictionary<byte?, string>
        {
            { 0, "New" },
            { 1, "Done" },
            { 2, "Cancled" },

        };
        public IndexModel(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public IList<BookingReservation> BookingReservation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_reservationService != null)
            {
                BookingReservation = (IList<BookingReservation>)await _reservationService.GetReservations();
                
            }
        }
    }
}
