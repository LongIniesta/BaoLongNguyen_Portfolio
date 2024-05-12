using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject;
using Repository;

namespace RazorPage.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();

        public IList<User> User { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "admin"))
            {
                return Redirect("~/ErrorRole");
            }
            User = userRepository.GetAll();
            return Page(); 
        }
        public IActionResult OnPost(string? strsearch)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "admin"))
            {
                return Redirect("~/ErrorRole");
            }
            if (strsearch == null)
            {
                strsearch = "";
            }
            else
            {
                strsearch = strsearch.ToLower();
            }
            User = userRepository.GetAll().Where(u => u.UserName.ToLower().Contains(strsearch) || u.Address.ToLower().Contains(strsearch)
            || u.PhoneNumber.ToLower().Contains(strsearch) || u.Email.ToLower().Contains(strsearch) || u.Role.ToLower().Contains(strsearch)
            ).ToList();
            return Page();
        }
    }
}
