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
        [Required(ErrorMessage = "Account id is required")]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email syntax")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "The phone number is invalid")]
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? AvatarLink { get; set; }
        public string? Description { get; set; }
    }
}
