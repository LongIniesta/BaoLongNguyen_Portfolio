using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ServiceDTO
    {
        public string ServiceId { get; set; } = null!;
        public string? ServiceName { get; set; }
        public int PostId { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public string? ImageLink { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<ServiceSchedulerDTO> ServiceSchedulers { get; set; }
    }
}
