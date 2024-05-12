using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ItemDTO
    {
        public string ItemId { get; set; } = null!;
        public string ItemType { get; set; } = null!;

        public virtual OrderDetailDTO? OrderDetail { get; set; }
        public virtual ProductDTO? Product { get; set; }
        public virtual ServiceSchedulerDTO? ServiceScheduler { get; set; }
    }
}
