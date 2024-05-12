using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class Gift
    {
        public Gift()
        {
            GiftComments = new HashSet<GiftComment>();
        }

        public string GiftId { get; set; } = null!;
        public string GiftName { get; set; } = null!;
        public string? Description { get; set; }
        public int PostId { get; set; }
        public string? ImageLink { get; set; }
        public bool Status { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual ICollection<GiftComment> GiftComments { get; set; }
    }
}
