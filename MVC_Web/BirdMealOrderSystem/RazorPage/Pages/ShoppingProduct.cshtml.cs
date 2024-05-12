using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPage.Pages
{
    public class ShoppingProductModel : PageModel
    {
        IProductRepository productRepository = new ProductRepository();
        ICategoryDetail categoryDetailRepository = new CategoryDetailRepository();
        public IList<Product> Products { get; set; }
        public List<CategoryDetail> listCategory { get; set; }
        public IActionResult OnGet()
        {
            listCategory = categoryDetailRepository.GetAll();
            Products = productRepository.GetAll().Where(p => p.ProductSattus ==true).ToList();
            return Page();
        }
        public IActionResult OnPost(string? strsearch, List<int> selectedItems)
        {
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

            Products = new List<Product>();
            if (selectedItems != null && selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    foreach (Product product in productsTmp)
                    {
                        foreach (Category cad in product.Categories)
                        {
                            if (cad.CategoryId == item && !Products.Any(p => p.ProductId == product.ProductId))
                            {
                                Products.Add(product);
                            }
                        }
                    }
                }
            }
            else
            {
                Products = productsTmp;
            }



            return Page();
        }
    }
}
