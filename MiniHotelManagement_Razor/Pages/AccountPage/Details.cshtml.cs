﻿using System;
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

namespace MiniHotelManagement_Razor.Pages.AccountPage
{
    public class DetailsModel : PageModel
    {
        private readonly IAccountService _accountService;
        public DetailsModel( IAccountService accountService)
        {
            _accountService = accountService;
        }

      public Account Account { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _accountService == null)
            {
                return NotFound();
            }
            var account = await _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            else 
            {
                Account = account;
            }
            return Page();
        }
    }
}
