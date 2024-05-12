using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ReactDTO
    {
        public int AccountId { get; set; }
        public int PostId { get; set; }
        public int ReactTypeId { get; set; }
        public virtual ReactTypeDTO ReactType { get; set; } = null!;
    }
}
