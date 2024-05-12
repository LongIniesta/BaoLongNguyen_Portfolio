 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VoiceJobDTO
    {
        public int VoiceJobId { get; set; }
        public int VoiceProjectId { get; set; }
        public int VoiceSellerId { get; set; }
        public string VoiceJobStatus { get; set; } = null!;
        public string? LinkDemo { get; set; }
        public bool Status { get; set; }

        public virtual VoiceProjectDTO VoiceProject { get; set; } = null!;
    }
}
