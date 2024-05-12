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
    public class DeleteModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();
        IOrderRepository orderRepository = new OrderRepository();
        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "admin"))
            {
                return Redirect("~/ErrorRole");
            }
            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetById((int) id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
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

            if (User != null)
            {
                if (orderRepository.GetAll().ToList().Any(o => o.UserId == id))
                {
                    User.Status = false;
                    userRepository.Update(User);
                }
                else 
                userRepository.DeleteById(User.UserId);
            }

            return RedirectToPage("./Index");
        }
    }
}
