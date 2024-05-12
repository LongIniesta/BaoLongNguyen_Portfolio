using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class Report
    {
        public int ReporterId { get; set; }
        public int ReportedPersonId { get; set; }
        public string? Content { get; set; }

        public virtual Account ReportedPerson { get; set; } = null!;
        public virtual Account Reporter { get; set; } = null!;
    }
}
