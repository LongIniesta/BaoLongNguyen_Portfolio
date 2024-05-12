using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class VoiceJob
    {
        public int VoiceJobId { get; set; }
        public int VoiceProjectId { get; set; }
        public int VoiceSellerId { get; set; }
        public string VoiceJobStatus { get; set; } = null!;
        public string? LinkDemo { get; set; }
        public bool Status { get; set; }

        public virtual VoiceProject VoiceProject { get; set; } = null!;
        public virtual VoiceSeller VoiceSeller { get; set; } = null!;
    }
}
