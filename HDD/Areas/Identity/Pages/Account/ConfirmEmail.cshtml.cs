// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using HDD.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using HDD.Data;
using HDD.Models;

namespace HDD.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHDDRepository _hddDataService;
        public ConfirmEmailModel(UserManager<ApplicationUser> userManager,
            IHDDRepository hddDataService)
        {
            _userManager = userManager;
            _hddDataService = hddDataService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            var isConfirmedPreviously = await _userManager.IsEmailConfirmedAsync(user);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded && !isConfirmedPreviously)
            {
                var ownersVin = new OwnersVin();
                ownersVin.OwnerId = user.Id;
                ownersVin.Vin = user.VIN;
                ownersVin.UpdateDateTime = DateTime.Now;
                ownersVin.PrimaryOwner = user.VIN is not null ? "Y" : "N";
                ownersVin.OwnerStatus = true;
                ownersVin.UpdateDateTime = DateTime.Now;
                await _hddDataService.InsertOwnersVin(ownersVin);
            }

            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }
    }
}
