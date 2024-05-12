using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class VoicePost
    {
        public VoicePost()
        {
            Comments = new HashSet<Comment>();
            VoiceSellers = new HashSet<VoiceSeller>();
            VoiceSellersNavigation = new HashSet<VoiceSeller>();
        }

        public int VoicePostId { get; set; }
        public int? VoiceSellerId { get; set; }
        public string? Title { get; set; }
        public string LinkAudio { get; set; } = null!;
        public int NumberOfListen { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? Likes { get; set; }
        public bool Status { get; set; }

        public virtual VoiceSeller? VoiceSeller { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<VoiceSeller> VoiceSellers { get; set; }
        public virtual ICollection<VoiceSeller> VoiceSellersNavigation { get; set; }
    }
}
