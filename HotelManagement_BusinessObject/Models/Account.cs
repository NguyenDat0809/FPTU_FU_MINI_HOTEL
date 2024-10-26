using System;
using System.Collections.Generic;

namespace HotelManagement_BusinessObject.Models
{
    public partial class Account
    {
        public string AccountId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
    }
}
