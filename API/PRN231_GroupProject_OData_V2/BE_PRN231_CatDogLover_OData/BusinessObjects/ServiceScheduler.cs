using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class ServiceScheduler
    {
        public string ItemId { get; set; } = null!;
        public string ServiceId { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
