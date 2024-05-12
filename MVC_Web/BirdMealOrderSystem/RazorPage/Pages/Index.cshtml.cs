using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        public class LoginDTO
        {

            [Required(ErrorMessage = "PLease enter your phone")]
            [Display(Name = "Phone number")]
            [Phone]
            public string phone { get; set; }
            [Required(ErrorMessage = "Please fill your password")]
            [Display(Name = "Password")]
            public string password { get; set; }
        }

        [BindProperty]
        public LoginDTO LoginDTOInfor { get; set; }
        public string Message { get; set; }
        private readonly IConfiguration Configuration;

        IUserRepository UserRepository = new UserRepository();


        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult OnGet()
        {
            LoginDTOInfor = new LoginDTO();
            if (SessionHelper.checkPermission(HttpContext.Session, "admin"))
            {
                return RedirectToPage("Admin/Users/Index");
            }
            if (SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return RedirectToPage("Staff/Products/Index");
            }
            if (SessionHelper.checkPermission(HttpContext.Session, "customer"))
            {
                return RedirectToPage("Customer/MyOrder");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (LoginDTOInfor == null) return Page();

            if (LoginDTOInfor.phone.Equals(Configuration["AdminAccount:Phone"]) &&
                LoginDTOInfor.password.Equals(Configuration["AdminAccount:Password"]))
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "role", "admin");
                return RedirectToPage("Admin/Users/Index");
            }
            else
            {
                List<User> list = UserRepository.GetAll().ToList();
                User user = null;
                user = list.SingleOrDefault(p => p.PhoneNumber.Equals(LoginDTOInfor.phone) && p.Password.Equals(LoginDTOInfor.password));
                if (user != null && user.Status == true)
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "role", user.Role);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cusId", user.UserId);
                    if (user.Role.Equals("staff"))
                    {
                        return RedirectToPage("Staff/Products/Index");
                    }
                    if (user.Role.Equals("customer"))
                    {
                        return RedirectToPage("ShoppingProduct");
                    }
                    return Page();
                }
                else
                {
                    Message = "Password or account is incorrect!";
                    return Page();
                }
            }

        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("Index");
        }
    }
}
