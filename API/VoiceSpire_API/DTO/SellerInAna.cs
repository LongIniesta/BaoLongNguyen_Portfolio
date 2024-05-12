using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SellerInAna
    {
        public int? Price { get; set; }
        public string? VoiceGender { get; set; }
        public string? VoiceRegion { get; set; }
        public int? VoiceTone { get; set; }
        public string? VoiceLocal { get; set; }
        public int? VoiceInspirational { get; set; }
        public int? VoiceStress { get; set; }
        public int? VoiceSpeed { get; set; }
        public int? VoicePronouce { get; set; }
        public List<VoicePropertyDTO>? voiceProperties { get; set; }

        public VoiceSellerDTO? VoiceSeller { get; set; }
    }
}
