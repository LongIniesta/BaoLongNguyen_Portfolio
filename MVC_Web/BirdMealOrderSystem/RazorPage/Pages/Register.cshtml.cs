using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RazorPage.Validation;
using Repository;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RazorPage.Pages
{
    public class RegisterModel : PageModel
    {
        public class RegisterInfo
        {
            [EmailAddress]
            [Required(ErrorMessage = "Email is required!")]
            [StringLength(100, MinimumLength = 11, ErrorMessage = "The length of email is from 11 to 100 charater")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Phone]
            [Required(ErrorMessage = "Phone is required!")]
            [StringLength(100, MinimumLength = 10, ErrorMessage = "The length of phone is from 10 to 100 charater")]
            [Display(Name = "Phone number")]
            [PhoneExist]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Name is required!")]
            [StringLength(180, MinimumLength = 5, ErrorMessage = "The length of name is from 5 to 180 charater")]
            [Display(Name = "Name")]
            public string Name { get; set; }


            [Required(ErrorMessage = "Address is required!")]
            [StringLength(100, MinimumLength = 1, ErrorMessage = "The length of address is from 1 to 100 charater")]
            [Display(Name = "Address")]
            public string Address { get; set; }


            [Required(ErrorMessage = "Password is required!")]
            [StringLength(30, MinimumLength = 8, ErrorMessage = "The length of password is from 8 to 30 charater")]
            [Display(Name = "Password")]
            [PasswordPropertyText]
            public string Password { get; set; }

            [Required(ErrorMessage = "Confirm is required!")]
            [Compare(nameof(Password))]
            [Display(Name = "Confirm password")]
            public string Confirmpassword { get; set; }


            [Display(Name = "Birthday")]
            [Required(ErrorMessage = "Birthday is required!")]
            public DateTime Birthday { get; set; }
        }

        [BindProperty]
        public RegisterInfo registerInfo { get; set; }
        public string Message { get; set; }
        private readonly IConfiguration Configuration;
        IUserRepository userRepository = new UserRepository();

        public void OnGet()
        {
        }
        public RegisterModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
                return Register();
            else return Page();
        }

        private IActionResult Register()
        {
            User user = new User();
            user.UserName = registerInfo.Name;
            user.Password = registerInfo.Password;
            user.Address = registerInfo.Address;
            user.Email = registerInfo.Email;
            user.Birthday = registerInfo.Birthday;
            user.PhoneNumber = registerInfo.Phone;
            user.Status = true;
            user.Role = "customer";
            if (registerInfo.Birthday.AddYears(16) > DateTime.Now)
            {
                Message = "You must be more 16 year old to use!";
                return Page();
            }
            try
            {
                userRepository.Create(user);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                Message = "System Error";
                return Page();
            }
        }
    }
}
