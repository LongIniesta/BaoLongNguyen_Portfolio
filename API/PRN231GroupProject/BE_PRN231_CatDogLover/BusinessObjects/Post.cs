using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class Post
    {
        public Post()
        {
            Gifts = new HashSet<Gift>();
            Products = new HashSet<Product>();
            Reacts = new HashSet<React>();
            Services = new HashSet<Service>();
        }

        public int PostId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int OwnerId { get; set; }
        public string Type { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public int? NumberOfReact { get; set; }

        public virtual Account Owner { get; set; } = null!;
        public virtual ICollection<Gift> Gifts { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<React> Reacts { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
