using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductDTO
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

        public virtual CategoryDTO? Category { get; set; } = null!;
    }
}
