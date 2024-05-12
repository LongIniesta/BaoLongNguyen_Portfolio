using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class VoiceDetail
    {
        public int VoiceDetailId { get; set; }
        public int VoiceSellerId { get; set; }
        public DateTime CreateDate { get; set; }
        public string MainVoiceLink { get; set; } = null!;
        public int? Price { get; set; }
        public string? VoiceGender { get; set; }
        public string? VoiceRegion { get; set; }
        public int? VoiceTone { get; set; }
        public string? VoiceLocal { get; set; }
        public int? VoiceInspirational { get; set; }
        public int? VoiceStress { get; set; }
        public int? VoiceSpeed { get; set; }
        public int? VoicePronouce { get; set; }
        public int? NumberOfEdit { get; set; }
        public bool IsApprove { get; set; }
        public bool Status { get; set; }

        public virtual VoiceSeller VoiceSeller { get; set; } = null!;
    }
}
