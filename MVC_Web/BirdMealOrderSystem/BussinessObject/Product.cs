using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BussinessObject
{
    public partial class Product
    {
        public Product()
        {
            Categories = new HashSet<Category>();
            ComboDetails = new HashSet<ComboDetail>();
            OrderProductDetails = new HashSet<OrderProductDetail>();
        }

        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name id is required!")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The length of email is from 1 to 200 charater")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Unit in stock is required!")]
        public int UnitInStock { get; set; }
        [Required(ErrorMessage = "Unit price is required!")]
        public decimal UnitPrice { get; set; }

        public string ImageLink { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "The length of description is from 1 to 1000 charater")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Nutritional Ingredient is required!")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "The length of Nutritional Ingredient is from 1 to 1000 charater")]
        public string NutritionalIngredients { get; set; }

        [Required(ErrorMessage = "User manual is required!")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "The length of user manual is from 1 to 500 charater")]
        public string UserManual { get; set; }

        [Required(ErrorMessage = "Unit is required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "The length of unit is from 1 to 50 charater")]
        public string Unit { get; set; }
        [Required(ErrorMessage = "Status is required!")]
        public bool ProductSattus { get; set; }

        [Required(ErrorMessage = "Discount is required!")]
        [DiscountValid]
        public double ProductDiscount { get; set; }

        [Required(ErrorMessage = "Supplier is required!")]
        public int SupplierId { get; set; }
        public int? Ratingavg { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ComboDetail> ComboDetails { get; set; }
        public virtual ICollection<OrderProductDetail> OrderProductDetails { get; set; }
    }
}
