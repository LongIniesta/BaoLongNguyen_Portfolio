using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Page<T>
    {
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> results { get; set; } 
    }
}
