using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public string Type { get; set; } = null!;
        public decimal? Price { get; set; }
        public string ItemId { get; set; } = null!;
        public string? ShipAddress { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Order? Order { get; set; }
    }
}
