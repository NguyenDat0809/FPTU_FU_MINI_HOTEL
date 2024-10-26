using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelManagement_BusinessObject.Context;
using HotelManagement_BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using HotelManagement_Services.Interfaces;

namespace MiniHotelManagement_Razor.Pages.RolePage
{
    public class CreateModel : PageModel
    {
        private readonly IRoleService _roleService;
        public CreateModel(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Role Role { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _roleService == null || Role == null)
            {
                return Page();
            }

            var addResult = await _roleService.CreateRole(Role);
            if (!addResult)
                TempData["ErrorMessage"] = "Add fail";
            else
                TempData["SuccessMessage"] = "Add success";
            return RedirectToPage("./Index");
        }
    }
}
