using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject;
using Repository;
using Microsoft.AspNetCore.Components;

namespace RazorPage.Pages.Admin.Orders
{
    public class DetailsModel : PageModel
    {
        IOrderRepository orderRepository = new OrderRepository();
        IOrderComboDetailRepository orderComboDetailRepository = new OrderComboDetailRepository();
        IOrderProductDetailRepository orderProductDetailRepository = new OrderProductDetailRepository();



        public Order Order { get; set; }
        public List<OrderComboDetail> orderComboDetails { get; set; }
        public List<OrderProductDetail> orderProductDetails { get; set; }

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

            Order = orderRepository.GetById((int)id);
            orderComboDetails = orderComboDetailRepository.GetAll().Where(ocd => ocd.OrderId == (int) id).ToList();
            orderProductDetails = orderProductDetailRepository.GetAll().Where(opd => opd.OrderId == (int)id).ToList();

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
