using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public virtual CategoryDetail CategoryNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
