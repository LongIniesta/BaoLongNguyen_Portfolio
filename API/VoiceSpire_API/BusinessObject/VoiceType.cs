using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class VoiceType
    {
        public int VoiceSellerId { get; set; }
        public string VoiceTypeDetail { get; set; } = null!;

        public virtual VoiceSeller VoiceSeller { get; set; } = null!;
    }
}
