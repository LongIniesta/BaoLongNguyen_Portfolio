using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject;
using Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace RazorPage.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();

        private readonly IConfiguration Configuration;
        public EditModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Message { get; set; }

        [BindProperty]
        public User User { get; set; }

        private void loadData()
        {
            List<string> listStatus = new List<string>();
            listStatus.Add("customer");
            listStatus.Add("staff");
            ViewData["role"] = new SelectList(listStatus);
        }

       

        public IActionResult OnGet(int? id)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "admin"))
            {
                return Redirect("~/ErrorRole");
            }
            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetById((int)id);

            if (User == null)
            {
                return NotFound();
            }

            loadData();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

           

            try
            {

                if (userRepository.GetById(User.UserId).PhoneNumber == User.PhoneNumber || !userRepository.GetAll().Any(u => u.PhoneNumber == User.PhoneNumber))
                {
                    if (User.PhoneNumber.Equals(Configuration["AdminAccount:Phone"]))
                    {
                        Message = "Phone is already exist!!!";
                        loadData();
                        return Page();
                    }
                    else
                    {
                        userRepository.Update(User);
                        return RedirectToPage("./Index");
                    }
                }
                else {
                    Message = "Phone is already exist!!!";
                    loadData();
                    return Page();
                } 

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        private bool UserExists(int id)
        {
            return userRepository.GetById((int)id) != null;
        }
    }
}
