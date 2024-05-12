using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BuyerRegisterDTO
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string? Phone { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankNumber { get; set; }
        public bool IsPro { get; set; }
        public bool Status { get; set; }
    }
}
