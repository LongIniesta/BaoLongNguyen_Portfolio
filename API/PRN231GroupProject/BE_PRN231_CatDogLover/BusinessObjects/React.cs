using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class React
    {
        public int AccountId { get; set; }
        public int PostId { get; set; }
        public int ReactTypeId { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual ReactType ReactType { get; set; } = null!;
    }
}
