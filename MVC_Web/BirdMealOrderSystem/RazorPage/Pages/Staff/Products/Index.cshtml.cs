 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject;
using Repository;

namespace RazorPage.Pages.Staff.Products
{
    public class IndexModel : PageModel
    {
        IProductRepository productRepository = new ProductRepository();
        ICategoryDetail categoryDetailRepository = new CategoryDetailRepository();
        public IList<Product> Product { get; set; }
        public List<CategoryDetail> listCategory { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            listCategory = categoryDetailRepository.GetAll();
            Product = productRepository.GetAll();
            return Page();
        }
        public IActionResult OnPost(string? strsearch, List<int> selectedItems)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            listCategory = categoryDetailRepository.GetAll();
            if (strsearch == null)
            {
                strsearch = "";
            }
            else
            {
                strsearch = strsearch.ToLower();
            }
            IList<Product> productsTmp = productRepository.GetAll().Where(u => u.ProductName.ToLower().Contains(strsearch)
            || u.Supplier.SupplierName.ToLower().Contains(strsearch)
            ).ToList();

            Product = new List<Product>();
            if (selectedItems != null && selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    foreach (Product product in productsTmp)
                    {
                        foreach (Category cad in product.Categories)
                        {
                            if (cad.CategoryId == item && !Product.Any(p => p.ProductId == product.ProductId))
                            {
                                Product.Add(product);
                            }
                        }
                    }
                }
            }
            else
            {
                Product = productsTmp;
            }



            return Page();
        }
    }
}
