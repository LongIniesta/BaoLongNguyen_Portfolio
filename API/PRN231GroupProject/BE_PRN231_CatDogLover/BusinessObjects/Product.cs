using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class Product
    {
        public string ItemId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public int PostId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageLink { get; set; }
        public int CategoryId { get; set; }
        public bool Status { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
