using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VoiceDemoDTO
    {
        public int VoiceJobId { get; set; }
        public int VoiceProjectId { get; set; }
        public int VoiceSellerId { get; set; }
        public string? LinkDemo { get; set; }
        public VoiceSellerDTO VoiceSeller { get; set; }
        public VoiceDetailDTO VoiceDetail { get; set; }
        public int NumberOfBooking { get; set; } 
    }
}
