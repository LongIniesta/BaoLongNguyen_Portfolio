using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PaymentDetail
    {
        public int VoiceProjectId { get; set; }
        public int Price { get; set; }
        public int ToalOutputPrice { get; set; }
        public int? Duration { get; set; }
        public string BankCode { get; set; }
    }
}
