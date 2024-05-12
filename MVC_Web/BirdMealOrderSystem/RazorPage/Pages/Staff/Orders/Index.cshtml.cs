using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject;
using Repository;
using System.ComponentModel.DataAnnotations;

namespace RazorPage.Pages.Staff.Orders
{
    public class IndexModel : PageModel
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
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            Order = orderRepository.GetAll();
            numberOfOrder = Order.Count();
            Total = (decimal)Order.Sum(od => od.Total);
            orderStart = DateTime.Now.AddDays(-7);
            orderEnd = DateTime.Now;
            return Page();
        }

        public IActionResult OnPostGenerate()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            if (orderEnd <= orderStart)
            {
                Message = "Start date must less than end date!";
                return Page();
            }

            Order = (from o in orderRepository.GetAll().ToList()
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
