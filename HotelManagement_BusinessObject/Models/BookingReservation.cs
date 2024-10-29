using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public string BookingDateFormat
        {
            get => BookingDate?.ToString("yyyy/MM/dd") ?? "N/A";
            set => BookingDate = DateTime.Parse(value);
        }
    }
}
