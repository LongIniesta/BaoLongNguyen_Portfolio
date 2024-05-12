using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public string? Phone { get; set; }
        public string? BanReason { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Address { get; set; }
        public string? AvatarLink { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }

        public virtual RoleDTO Role { get; set; } = null!;
    }
}
