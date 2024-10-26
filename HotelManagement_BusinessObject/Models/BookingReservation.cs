using System;
using System.Collections.Generic;

namespace HotelManagement_BusinessObject.Models
{
    public partial class BookingReservation
    {
        public string BookingReservationId { get; set; } = null!;
        public DateTime? BookingDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string CustomerName { get; set; } = null!;
        public string RoomId { get; set; } = null!;
        public byte? BookingStatus { get; set; }

        public virtual Room Room { get; set; } = null!;
    }
}
