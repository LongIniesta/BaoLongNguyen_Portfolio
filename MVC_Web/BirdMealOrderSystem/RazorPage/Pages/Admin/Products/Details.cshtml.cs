using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject;
using Repository;

namespace RazorPage.Pages.Admin.Products
{
    public class DetailsModel : PageModel
    {
        IProductRepository productRepository = new ProductRepository();
        IOrderProductDetailRepository productDetailRepository = new OrderProductDetailRepository();
        public Product Product { get; set; }
        public List<OrderProductDetail> listOrderProductDetai { get; set; }

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

            listOrderProductDetai = productDetailRepository.GetAll().Where(i => i.ProductId == (int)id && i.Feedback != null && !i.Feedback.Equals("notfeedback")).ToList();
            Product = productRepository.GetById((int) id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
