using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BussinessObject
{
    public partial class Combo
    {
        public Combo()
        {
            ComboDetails = new HashSet<ComboDetail>();
            OrderComboDetails = new HashSet<OrderComboDetail>();
        }


        public int ComboId { get; set; }
        [Required(ErrorMessage = "Combo name is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length of combo name is from 1 to 100 charater")]
        public string ComboName { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        public decimal Price { get; set; }
        public int Ratingavg { get; set; }
        [DiscountValid]
        public double Discount { get; set; }
        [Required(ErrorMessage = "Combo description is required!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The length of description is from 1 to 100 charater")]
        public string ComboDescription { get; set; }

        public bool ComboStatus { get; set; }

        public virtual ICollection<ComboDetail> ComboDetails { get; set; }
        public virtual ICollection<OrderComboDetail> OrderComboDetails { get; set; }
    }
}
