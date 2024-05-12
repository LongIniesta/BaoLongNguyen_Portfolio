using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class VoiceTransaction
    {
        public int VoiceTransactionId { get; set; }
        public int VoiceProjectId { get; set; }
        public int VoiceSellerId { get; set; }
        public string? VoiceTransactionStatus { get; set; }
        public string? Feedback { get; set; }
        public string? LinkVoice { get; set; }
        public bool Status { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual VoiceProject VoiceProject { get; set; } = null!;
        public virtual VoiceSeller VoiceSeller { get; set; } = null!;
    }
}
