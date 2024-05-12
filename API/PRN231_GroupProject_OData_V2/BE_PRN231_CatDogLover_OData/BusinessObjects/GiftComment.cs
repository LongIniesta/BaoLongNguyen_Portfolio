using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class GiftComment
    {
        public int GiftCommentId { get; set; }
        public string GiftId { get; set; } = null!;
        public int AccountId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public string ApproveStatus { get; set; } = null!;

        public virtual Account Account { get; set; } = null!;
        public virtual Gift Gift { get; set; } = null!;
    }
}
