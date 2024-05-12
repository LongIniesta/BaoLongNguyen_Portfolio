using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class Item
    {
        public Item()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string ItemId { get; set; } = null!;
        public string ItemType { get; set; } = null!;

        public virtual Product? Product { get; set; }
        public virtual ServiceScheduler? ServiceScheduler { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
