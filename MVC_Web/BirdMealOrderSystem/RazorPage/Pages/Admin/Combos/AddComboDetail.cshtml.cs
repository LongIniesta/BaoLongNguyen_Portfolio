using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;

namespace RazorPage.Pages.Admin.Combos
{
    public class AddComboDetailModel : PageModel
    {
        IProductRepository productRepository = new ProductRepository();
        public List<Product> Products;
        public IActionResult OnGet()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "admin"))
            {
                return Redirect("~/ErrorRole");
            }
            Products = productRepository.GetAll();
            return Page();
        }
    }
}
