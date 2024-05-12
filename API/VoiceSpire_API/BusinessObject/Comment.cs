using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int VoiceSeller { get; set; }
        public string Content { get; set; } = null!;
        public int? Likes { get; set; }
        public DateTime? CreateDate { get; set; }
        public int VoicePostId { get; set; }
        public bool Status { get; set; }

        public virtual VoicePost VoicePost { get; set; } = null!;
        public virtual VoiceSeller VoiceSellerNavigation { get; set; } = null!;
    }
}
