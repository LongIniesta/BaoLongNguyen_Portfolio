using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject;
using Repository;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.Extensions.Configuration;

namespace RazorPage.Pages.Admin.Users
{
    public class CreateModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();


        private readonly IConfiguration Configuration;
        public CreateModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Message { get; set; }
        public IActionResult OnGet()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "admin"))
            {
                return Redirect("~/ErrorRole");
            }
            loadData();
            return Page();
        }

        private void loadData()
        {
            List<string> listStatus = new List<string>();
            listStatus.Add("customer");
            listStatus.Add("staff");
            ViewData["role"] = new SelectList(listStatus);
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "admin"))
            {
                return Redirect("~/ErrorRole");
            }
            if (!ModelState.IsValid)
            {
                loadData();
                return Page();
            }

            if (!userRepository.GetAll().Any(u => u.PhoneNumber == User.PhoneNumber))
            {
                if (User.PhoneNumber.Equals(Configuration["AdminAccount:Phone"]))
                {
                    Message = "Phone is already exist!!!";
                    loadData();
                    return Page();
                }
                else
                {
                    User.UserId = 0;
                    userRepository.Create(User);
                    return RedirectToPage("./Index");
                }
            }
            else
            {
                Message = "Phone is already exist!!!";
                loadData();
                return Page();
            }

        }
    }
}
