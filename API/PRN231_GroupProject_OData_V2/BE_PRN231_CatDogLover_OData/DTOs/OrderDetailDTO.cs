using BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class OrderDetailDTO
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        [Required]
        public string Type { get; set; } = null!;
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public string ItemId { get; set; } = null!;

        public virtual ItemDTO? Item { get; set; } = null!;
        public string? ShipAddress { get; set; }
    }
}
