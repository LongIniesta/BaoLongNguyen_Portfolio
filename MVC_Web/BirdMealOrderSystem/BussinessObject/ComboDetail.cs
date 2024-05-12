using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject
{
    public partial class ComboDetail
    {
        public int ComboId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Combo Combo { get; set; }
        public virtual Product Product { get; set; }
    }
}
