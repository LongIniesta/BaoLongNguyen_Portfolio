using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;

namespace RazorPage.Pages.Staff.Combos
{
    public class AddComboDetailModel : PageModel
    {

        IProductRepository productRepository = new ProductRepository();
        public List<Product> Products;
        public IActionResult OnGet()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            Products = productRepository.GetAll();
            return Page();
        }
    }
}
