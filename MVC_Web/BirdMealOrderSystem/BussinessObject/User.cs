using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BussinessObject
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        
        public int UserId { get; set; }
        [Required(ErrorMessage = "Username id is required!")]
        [StringLength(70, MinimumLength = 1, ErrorMessage = "The length of name is from 1 to 70 charater")]
        public string UserName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email id is required!")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The length of email is from 1 to 200 charater")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password id is required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "The length of password is from 1 to 50 charater")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Address id is required!")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The length of address is from 1 to 200 charater")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number id is required!")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "The length of phone is from 10 to 20 charater")]
        public string PhoneNumber { get; set; }


        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Role id is required!")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Status id is required!")]
        public bool Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
