using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Account
{
    public class AccountResetPassword
    {
        [Required(ErrorMessage = "Account id is required")]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "CurrentPassword is required")]
        public string CurrentPassword { get; set; } = null!;
        [Required(ErrorMessage = "NewPassword id is required")]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,})", 
            ErrorMessage = "The new password must have all of these conditions: " +
            "\n-At least 8 characters" +
            "\n-Contains a lowercase, a uppercase and a special character")]
        public string NewPassword { get; set; } = null!;
    }
}
