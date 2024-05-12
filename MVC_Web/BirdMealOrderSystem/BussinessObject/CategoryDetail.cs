using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject
{
    public partial class CategoryDetail
    {
        public CategoryDetail()
        {
            Categories = new HashSet<Category>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
