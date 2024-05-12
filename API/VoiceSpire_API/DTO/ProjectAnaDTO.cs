using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProjectAnaDTO
    {
        public int Suitability { get; set; }
        public int voiceProjectId { get; set; }
        public int voiceDetailId { get; set; }
        public ProjectInAna projectInAna { get; set; }
        public SellerInAna SellerInAna { get; set; }
    }
}
