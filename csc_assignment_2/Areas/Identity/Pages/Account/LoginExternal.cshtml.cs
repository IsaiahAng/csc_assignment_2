using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace csc_assignment_2.Areas.Identity.Pages.Account
{
    [Authorize]
    public class LoginExternalModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}