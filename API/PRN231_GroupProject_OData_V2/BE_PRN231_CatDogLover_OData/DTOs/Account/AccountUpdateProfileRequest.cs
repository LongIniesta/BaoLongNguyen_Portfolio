using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Account
{
    public class AccountUpdateProfileRequest
    {
        [Required(ErrorMessage = "Book id is required")]
        public int AccountId { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email syntax")]
        public string? Email { get; set; } = null!;
        public string? CurrentPassword { get; set; }
        [Range(8, 50, ErrorMessage = "The length of new password must be between 8 and 50 characters")]
        public string? NewPassword { get; set; }
        public string? NewPasswordConfirm { get; set; }
        public string? FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "The phone number is invalid")]
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? AvatarLink { get; set; }
        public string? Description { get; set; }
    }
}
