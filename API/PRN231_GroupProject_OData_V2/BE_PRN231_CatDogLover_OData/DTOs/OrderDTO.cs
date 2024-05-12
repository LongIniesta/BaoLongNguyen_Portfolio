using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class OrderDTO
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int AccountId { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool Status { get; set; }

        public virtual AccountDTO? Account { get; set; } = null!;
        public virtual ICollection<OrderDetailDTO>? OrderDetails { get; set; }
    }
}
