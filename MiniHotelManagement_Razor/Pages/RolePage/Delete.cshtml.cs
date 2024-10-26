using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement_BusinessObject.Context;
using HotelManagement_BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using HotelManagement_Services.Interfaces;

namespace MiniHotelManagement_Razor.Pages.RolePage
{
    public class DeleteModel : PageModel
    {
        private readonly IRoleService _roleService;

        public DeleteModel(IRoleService roleService)
        {
           _roleService = roleService;
        }

        [BindProperty]
      public Role Role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _roleService == null)
            {
                return NotFound();
            }

            var role = await _roleService.GetRoleById((int)id);

            if (role == null)
            {
                return NotFound();
            }
            else 
            {
                Role = role;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _roleService == null)
            {
                return NotFound();
            }
            var role = await _roleService.GetRoleById((int)id);

            if (role != null)
            {
                Role = role;
                var deleteRs = await _roleService.DeleteRole(role);
                if (!deleteRs)
                    TempData["ErrorMessage"] = "Delete fail";
                else
                    TempData["SuccessMessage"] = "Delete success";
            }

            return RedirectToPage("./Index");
        }
    }
}
