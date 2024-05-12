using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class GiftCommentDTO
    {
        [Key]
        public int GiftCommentId { get; set; }
        public string GiftId { get; set; } = null!;
        public int AccountId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public bool Status { get; set; }
        public string ApproveStatus { get; set; } = null!;

        public virtual AccountDTO Account { get; set; } = null!;
    }
}
