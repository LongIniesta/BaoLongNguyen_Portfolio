using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject;
using Repository;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace RazorPage.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        IProductRepository productRepository = new ProductRepository();
        IOrderProductDetailRepository orderProductDetailRepository = new OrderProductDetailRepository();
        IComboDetailRepository comboDetailRepository = new ComboDetailRepository();
        ICategoryRepository categoryRepository = new CategoryRepository();
        ICategoryDetail categoryDetailRepository = new CategoryDetailRepository();

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "admin"))
            {
                return Redirect("~/ErrorRole");
            }
            if (id == null)
            {
                return NotFound();
            }

            Product = productRepository.GetById((int)id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "admin"))
            {
                return Redirect("~/ErrorRole");
            }
            if (id == null)
            {
                return NotFound();
            }

            Product = productRepository.GetById((int)id);

            if (Product != null)
            {

                if (comboDetailRepository.GetAll().Any(c => c.ProductId == (int)id)
                    || orderProductDetailRepository.GetByProductId((int)id).Count > 0)
                {
                    Product.ProductSattus = false;
                    productRepository.Update(Product);
                }
                else {
                    foreach (var item in categoryDetailRepository.GetAll())
                    {
                        Category cat = new Category();
                        cat.ProductId = Product.ProductId;
                        cat.CategoryId = item.CategoryId;
                        if (categoryRepository.checkExits(cat)) categoryRepository.removeCategory(cat);
                    }
                    productRepository.DeleteById((int)id);
                } 
            }
            return RedirectToPage("./Index");
        }
    }
}
