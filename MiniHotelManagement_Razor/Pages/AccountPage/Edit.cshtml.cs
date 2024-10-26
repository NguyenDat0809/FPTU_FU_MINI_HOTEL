using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagement_BusinessObject.Context;
using HotelManagement_BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using HotelManagement_Services.Interfaces;

namespace MiniHotelManagement_Razor.Pages.AccountPage
{
  
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        public EditModel(IAccountService accountService, IRoleService roleService)
        {
           _accountService = accountService;
            _roleService = roleService;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _accountService == null)
            {
                return NotFound();
            }

            var account =  await _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
            var roles = await _roleService.GetRoles();
           ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           

            try
            {
                var updateRs = await _accountService.UpdateAccount(Account);
                if (!updateRs)
                    TempData["ErrorMessage"] = "Update fail";
                else
                    TempData["SuccessMessage"] = "Update success";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AccountExists(Account.AccountId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> AccountExists(string id)
        {
          return await _accountService.GetAccountById(id) != null;
        }
    }
}
