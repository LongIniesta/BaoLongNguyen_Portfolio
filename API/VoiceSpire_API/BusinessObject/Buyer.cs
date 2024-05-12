using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class Buyer
    {
        public Buyer()
        {
            VoiceProjects = new HashSet<VoiceProject>();
        }

        public int BuyerId { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankNumber { get; set; }
        public bool IsPro { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<VoiceProject> VoiceProjects { get; set; }
    }
}
