using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HotelManagement_BusinessObject.Models
{
    public partial class RoomType
    {
        public RoomType()
        {
        }

        public string RoomTypeId { get; set; } = null!;
        public string RoomTypeName { get; set; } = null!;
        public string? Description { get; set; }
        [XmlIgnore]
        public virtual ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
    }
}
