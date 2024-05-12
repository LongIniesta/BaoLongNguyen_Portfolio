using System.ComponentModel.DataAnnotations;

namespace DTOs.Account
{
    public class AccountCreateRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email syntax")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password id is required")]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,})",
    ErrorMessage = "The new password must have all of these conditions: " +
    "\n-At least 8 characters" +
    "\n-Contains a lowercase, a uppercase and a special character")]
        public string Password { get; set; } = null!;
        [Required]
        public string PasswordConfirm { get; set; } = null!;
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "The phone number is invalid")]
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? AvatarLink { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(0,2,ErrorMessage ="Unsupported role")]
        public int RoleId { get; set; } = 1;
    }
}
