using BusinessObjects;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ReportDTO
    {
        [Required]
        public int ReporterId { get; set; }
        [Required]
        public int ReportedPersonId { get; set; }
        [Required]
        public string? Content { get; set; }

        public virtual AccountDTO? ReportedPerson { get; set; } = null!;
        public virtual AccountDTO? Reporter { get; set; } = null!;
    }
}
