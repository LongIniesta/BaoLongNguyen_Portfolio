using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class LoginRespone
    {
        public string Role { get; set; }
        public string AccessToken { get;set; }
        public string RefreshToken { get; set; }
        public AccountDTO Account { get; set; }
    }
}
