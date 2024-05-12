using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VoiceSellerDTO
    {
        public int VoiceSellerId { get; set; }
        public string Fullname { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Introduce { get; set; }
        public string? Address { get; set; }
        public string Gender { get; set; }
        public string? AvatarLink { get; set; }
        public double? RateAvg { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }
        public string? BankAccountName { get; set; }
        public string? GoogleId { get; set; }
        public bool Status { get; set; }


    }
}
