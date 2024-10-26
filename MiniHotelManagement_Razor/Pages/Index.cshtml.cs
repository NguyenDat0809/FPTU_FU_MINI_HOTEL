using HotelManagement_BusinessObject.Models;
using HotelManagement_Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace MiniHotelManagement_Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public class LoginInputModel
        {
            [Required(ErrorMessage = "Email is required")]
            [EmailAddress]
            public string Email { get; set; }
            [Required(ErrorMessage = "Password is required")]
            [MinLength(6, ErrorMessage = "Password 's length is greater than 6")]
            public string Password { get; set; }
        }

        public IndexModel( IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
           
            var email = Input.Email;
            var password = Input.Password;  
            var account = await _accountService.GetAccountByEmail(email);
            if (account == null) {
                TempData["ErrorMessage"] = "Account not found.";
                return Page();
            }
            if(account.Password != password)
            {
                TempData["ErrorMessage"] = "Incorrect password.";
                return Page();
            }
            HttpContext.Session.SetString("Role", account.RoleId.ToString());
            if(account.RoleId != 1 && account.RoleId != 2)
                return RedirectToPage("/RoomPage/Index");
            return RedirectToPage("/AccountPage/Index");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("Role");
            return RedirectToPage("/Index"); 
        }


    }
}
