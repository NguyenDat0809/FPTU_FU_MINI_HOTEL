using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement_BusinessObject.Models
{
    public partial class Account
    {
        [Required]
        public string AccountId { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(6, ErrorMessage = " Min length of password is 6")]
        public string Password { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
    }
}
