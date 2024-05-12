using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject
{
    public partial class OrderComboDetail
    {
        public int OrderComboDetailId { get; set; }
        public int ComboId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public string Feedback { get; set; }
        public int? Rating { get; set; }

        public virtual Combo Combo { get; set; }
        public virtual Order Order { get; set; }
    }
}
