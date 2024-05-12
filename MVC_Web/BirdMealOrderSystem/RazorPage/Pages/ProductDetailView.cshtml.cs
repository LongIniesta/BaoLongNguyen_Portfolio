using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPage.Pages
{
    public class ProductDetailViewModel : PageModel
    {
        IProductRepository productRepository = new ProductRepository();
        IOrderProductDetailRepository productDetailRepository = new OrderProductDetailRepository();
        public Product Product { get; set; }
        public List<OrderProductDetail> listOrderProductDetai { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            listOrderProductDetai = productDetailRepository.GetAll().Where(i => i.ProductId == (int)id && i.Feedback != null && !i.Feedback.Equals("notfeedback")).ToList();
            Product = productRepository.GetById((int)id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
