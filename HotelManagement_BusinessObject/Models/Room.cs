using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HotelManagement_BusinessObject.Models
{
    public partial class Room
    {
        
        public Room()
        {
        }

        public string RoomId { get; set; } = null!;
        public string RoomName { get; set; } = null!;
        public string? RoomTypeId { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        [DisplayName("Image")]
        public string? ImageUrl { get; set; }
        [XmlIgnore]
        public virtual RoomType? RoomType { get; set; }
        [XmlIgnore]
        public virtual ICollection<BookingReservation> BookingReservations { get; set; } =  new HashSet<BookingReservation>();
    }
}
