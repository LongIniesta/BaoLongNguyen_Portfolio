using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class GiftDTO
    {
        public string GiftId { get; set; } = null!;
        public string GiftName { get; set; } = null!;
        public string? Description { get; set; }
        public int PostId { get; set; }
        public string? ImageLink { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<GiftCommentDTO>? GiftComments { get; set; }
    }
}
