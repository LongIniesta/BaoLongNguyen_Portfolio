using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ShoppingItem
    {
        public ShoppingItem()
        {
        }

        public string type { get; set; }
        public int ItemId { get; set; }

        public  int quantity { get; set; }

        public decimal unitPirce { get; set; }
        public double discount { get; set; }
        public string imgLink { get; set; }
        public string name { get; set; }
        
    }
}
