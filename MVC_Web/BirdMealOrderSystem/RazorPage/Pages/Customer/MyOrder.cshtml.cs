using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace RazorPage.Pages.Customer
{
    public class MyOrderModel : PageModel
    {
        IOrderRepository orderRepository = new OrderRepository();
        public int numberOfOrder { get; set; }
        public decimal Total { get; set; }
        public string Message { get; set; }

        [Required]
        [BindProperty]
        public DateTime orderStart { get; set; }

        [Required]
        [BindProperty]
        public DateTime orderEnd { get; set; }



        public IList<Order> Order { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "customer"))
            {
                return Redirect("~/ErrorRole");
            }
            Order = orderRepository.GetAll().Where(o => o.UserId == SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cusId")).ToList();
            numberOfOrder = Order.Count();
            Total = (decimal)Order.Sum(od => od.Total);
            orderStart = DateTime.Now.AddDays(-7);
            orderEnd = DateTime.Now;
            return Page();
        }

        public IActionResult OnPostGenerate()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "customer"))
            {
                return Redirect("~/ErrorRole");
            }
            if (orderEnd <= orderStart)
            {
                Message = "Start date must less than end date!";
                return Page();
            }

            Order = (from o in orderRepository.GetAll().Where(o => o.UserId == SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cusId")).ToList()
                     where o.OrdeDate <= orderEnd && o.OrdeDate >= orderStart
                     orderby o.Total descending
                     select o
                     ).ToList();
            numberOfOrder = Order.Count();
            Total = (decimal)Order.Sum(od => od.Total);
            return Page();
        }
    }
}
