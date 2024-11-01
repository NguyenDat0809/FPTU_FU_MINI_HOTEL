using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement_BusinessObject.Models
{
    public partial class BookingReservation
    {
        [Required]
        public string BookingReservationId { get; set; } = null!;
        public DateTime? BookingDate { get; set; }
        public decimal? TotalPrice { get; set; }
        [Required]
        public string CustomerName { get; set; } = null!;
        [Required]
        public string RoomId { get; set; } = null!;
        [Required]
        public byte? BookingStatus { get; set; }
        [ValidateNever]
        public virtual Room Room { get; set; } = null!;
        [NotMapped]
        public string BookingDateFormat
        {
            get => BookingDate?.ToString("yyyy/MM/dd") ?? "N/A";
            set => BookingDate = DateTime.Parse(value);
        }
    }
}
