using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BankInfomationDTO
    {
        public int ProjectId { get; set; }
        public string BankNameBuyer { get; set; }
        public string BankNumberBuyer { get; set; }
        public string BankAccountNameBuyer { get; set; }
        public string BankNameSeller { get; set; }
        public string BankNumberSeller { get; set; }
        public string BankAccountNameSeller { get; set; }
    }
}
