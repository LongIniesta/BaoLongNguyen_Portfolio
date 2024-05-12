using BussinessObject;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPage.Pages.Customer
{
    public class MyOrderDetailModel : PageModel
    {
        IOrderRepository orderRepository = new OrderRepository();
        IOrderComboDetailRepository orderComboDetailRepository = new OrderComboDetailRepository();
        IOrderProductDetailRepository orderProductDetailRepository = new OrderProductDetailRepository();
        IProductRepository productRepository = new ProductRepository();
        IComboRepository comboRepository = new ComboRepository();


        public Order Order { get; set; }
        public string Message { get; set; }
        public List<OrderComboDetail> orderComboDetails { get; set; }
        public List<OrderProductDetail> orderProductDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "customer"))
            {
                return Redirect("~/ErrorRole");
            }
            if (id == null)
            {
                return NotFound();
            }

            Order = orderRepository.GetById((int)id);
            orderComboDetails = orderComboDetailRepository.GetAll().Where(ocd => ocd.OrderId == (int)id).ToList();
            orderProductDetails = orderProductDetailRepository.GetAll().Where(opd => opd.OrderId == (int)id).ToList();

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPostCommentProduct(int Orderid, int id, string commment, int rating)
        {
            Order = orderRepository.GetById((int)Orderid);
            if (commment != null && !commment.Equals("") && rating >= 0 && rating <= 100)
            {
                OrderProductDetail orderProductDetail = orderProductDetailRepository.GetAll().Where(op => op.ProductId == id && op.OrderId == Orderid).ToList().SingleOrDefault();
                orderProductDetail.Rating = rating;
                orderProductDetail.Feedback = commment;
                orderProductDetailRepository.Update(orderProductDetail);

                List<OrderProductDetail> listOrderProductDetai = orderProductDetailRepository.GetAll().Where(i => i.ProductId == (int)id && (i.Feedback != null && !i.Feedback.Equals("notfeedback"))).ToList();
                Product product = productRepository.GetById(id);
                if (product.Ratingavg == null)
                {
                    product.Ratingavg = rating;
                }
                else
                {
                    product.Ratingavg = (int)(product.Ratingavg * (listOrderProductDetai.Count - 1) + rating) / (listOrderProductDetai.Count);
                }
                productRepository.Update(product);
            }
            orderComboDetails = orderComboDetailRepository.GetAll().Where(ocd => ocd.OrderId == (int)Orderid).ToList();
            orderProductDetails = orderProductDetailRepository.GetAll().Where(opd => opd.OrderId == (int)Orderid).ToList();
            return Page();
        }
        public IActionResult OnPostCommentCombo(int Orderid, int id, string commment, int rating)
        {

            

            Order = orderRepository.GetById((int)Orderid);
            if (commment != null && !commment.Equals("") && rating >=0 && rating <=100)
            {
                OrderComboDetail orderComboDetail = orderComboDetailRepository.GetAll().Where(op => op.ComboId == id && op.OrderId == Orderid).ToList().SingleOrDefault();
                orderComboDetail.Rating = rating;
                orderComboDetail.Feedback = commment;
                orderComboDetailRepository.Update(orderComboDetail);

                List<OrderComboDetail> listOrderComboDetai = orderComboDetailRepository.GetAll().Where(i => i.ComboId == (int)id && (i.Feedback != null && !i.Feedback.Equals("notfeedback"))).ToList();
                Combo combo = comboRepository.GetById(id);
                if (combo.Ratingavg == 0)
                {
                    combo.Ratingavg = rating;
                }
                else
                {
                    combo.Ratingavg = (int)(combo.Ratingavg * (listOrderComboDetai.Count - 1) + rating) / (listOrderComboDetai.Count);
                }
                comboRepository.Update(combo);
            }
            orderComboDetails = orderComboDetailRepository.GetAll().Where(ocd => ocd.OrderId == (int)Orderid).ToList();
            orderProductDetails = orderProductDetailRepository.GetAll().Where(opd => opd.OrderId == (int)Orderid).ToList();
            return Page();
        }
        public IActionResult OnGetDelete(int id)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "customer"))
            {
                return Redirect("~/Index");
            }
            Order = orderRepository.GetById((int)id);
            orderComboDetails = orderComboDetailRepository.GetAll().Where(ocd => ocd.OrderId == (int)Order.OrderId).ToList();
            orderProductDetails = orderProductDetailRepository.GetAll().Where(opd => opd.OrderId == (int)Order.OrderId).ToList();
            if (!Order.OrderStatus.Equals("ready"))
            {
                 Message = "Can't cancle because order is " + Order.OrderStatus;
                return Page();
            }
            else
            {
                Order.OrderStatus = "cancled";
                orderRepository.Update(Order);
                List<ShoppingItem> cart = new List<ShoppingItem>();
                if (orderComboDetails != null && orderComboDetails.Count > 0)
                {
                    foreach (OrderComboDetail item in orderComboDetails)
                    {
                        cart.Add(new ShoppingItem
                        {
                            quantity = item.Quantity,
                            type = "combo",
                            ItemId = item.ComboId
                        }) ;
                    }
                }
                if (orderProductDetails != null && orderProductDetails.Count > 0)
                {
                    foreach (OrderProductDetail item in orderProductDetails)
                    {
                        cart.Add(new ShoppingItem
                        {
                            quantity = item.Quantity,
                            type = "product",
                            ItemId = item.ProductId
                        });
                    }
                }
                for (var i = 0; i < cart.Count; i++)
                {
                    if (cart[i].type.Equals("combo"))
                    {
                        Combo combo = comboRepository.GetById((int)cart[i].ItemId);
                        foreach (ComboDetail cbd in combo.ComboDetails)
                        {
                            Product product = productRepository.GetById((int)cbd.ProductId);
                            product.UnitInStock += cbd.Quantity * cart[i].quantity;
                            productRepository.Update(product);
                        }
                    }
                    if (cart[i].type.Equals("product"))
                    {
                        Product product = productRepository.GetById((int)cart[i].ItemId);

                        product.UnitInStock += cart[i].quantity;
                        productRepository.Update(product);
                    }

                }
            }
            return RedirectToPage("MyOrder");
        }
    }
}
