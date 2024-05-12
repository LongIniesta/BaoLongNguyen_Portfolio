using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ComboDetailDTO
    {
        public ComboDetailDTO() { }
        public int ComboId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageLink { get; set; }
        public double ProductDiscount { get; set; }
        public int Quantity { get; set; }
    }
}
