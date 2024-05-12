using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class VoiceProperty
    {
        public int VoiceSellerId { get; set; }
        public string VoicePropertyName { get; set; } = null!;

        public virtual VoiceSeller VoiceSeller { get; set; } = null!;
    }
}
