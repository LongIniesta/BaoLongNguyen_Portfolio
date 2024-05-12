using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class PostDTO
    {
        public int? PostId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int OwnerId { get; set; }
        public string Type { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public int? NumberOfReact { get; set; }
        public bool? Reacted { get; set; }

        public virtual AccountDTO? Owner { get; set; } = null!;
        public virtual ICollection<GiftDTO>? Gifts { get; set; }
        public virtual ICollection<ProductDTO>? Products { get; set; }
        public virtual ICollection<ServiceDTO>? Services { get; set; }
    }
}
