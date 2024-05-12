using BussinessObject;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Protocols;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace RazorPage.Pages.Customer
{
    public class CustomerCartModel : PageModel
    {
        IComboRepository comboRepository = new ComboRepository();
        IComboDetailRepository comboDetailRepository = new ComboDetailRepository();
        IProductRepository productRepository = new ProductRepository();
        IOrderRepository orderRepository = new OrderRepository();
        IOrderComboDetailRepository orderComboDetailRepository = new OrderComboDetailRepository();
        IOrderProductDetailRepository orderProductDetailRepository = new OrderProductDetailRepository();
        IUserRepository userRepository = new UserRepository();
        public List<ShoppingItem> cart { get; set; }
        public decimal Total { get; set; }
        [Required(ErrorMessage = "ship address is required!")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The length of ship address is from 1 to 200 charater")]
        public string Shippaddress { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "customer"))
            {
                return Redirect("~/Index");
            }
            List<string> listStatus = new List<string>();
            listStatus.Add("VNPay");
            listStatus.Add("Payment on delivery");
            ViewData["payment"] = new SelectList(listStatus);
            cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
            if (cart != null && cart.Count > 0)
                Total = (decimal)cart.Sum(i => ((double)i.unitPirce) * i.quantity - ((double)i.unitPirce) * i.quantity * (i.discount / 100));
            return Page();
        }

        public IActionResult OnGetBuyNow(int id, string type)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "customer"))
            {
                return Redirect("~/Index");
            }
            List<string> listStatus = new List<string>();
            listStatus.Add("VNPay");
            listStatus.Add("Payment on delivery");
            ViewData["payment"] = new SelectList(listStatus);
            cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<ShoppingItem>();
                if (type.Equals("combo"))
                {
                    Combo combo = comboRepository.GetById(id);
                    cart.Add(new ShoppingItem
                    {
                        type = "combo",
                        ItemId = id,
                        discount = combo.Discount,
                        quantity = 1,
                        unitPirce = combo.Price,
                        imgLink = "",
                        name ="Combo " + combo.ComboName
                    });
                }

                if (type.Equals("product"))
                {
                    Product product = productRepository.GetById(id);
                    cart.Add(new ShoppingItem
                    {
                        type = "product",
                        ItemId = id,
                        discount = product.ProductDiscount,
                        quantity = 1,
                        unitPirce = product.UnitPrice,
                        imgLink = product.ImageLink,
                        name = product.ProductName
                    });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = Exists(cart, id, type);
                if (index == -1)
                {

                    if (type.Equals("product"))
                    {
                        Product product = productRepository.GetById(id);
                        cart.Add(new ShoppingItem
                        {
                            type = "product",
                            ItemId = id,
                            discount = product.ProductDiscount,
                            quantity = 1,
                            unitPirce = product.UnitPrice,
                            imgLink = product.ImageLink,
                            name = product.ProductName
                        });
                    }
                    if (type.Equals("combo"))
                    {
                        Combo combo = comboRepository.GetById(id);
                        cart.Add(new ShoppingItem
                        {
                            type = "combo",
                            ItemId = id,
                            discount = combo.Discount,
                            quantity = 1,
                            unitPirce = combo.Price,
                            imgLink = "",
                            name ="Combo " + combo.ComboName
                        });
                    }

                }
                else
                {
                    cart[index].quantity++;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToPage("CustomerCart");
        }
        public IActionResult OnGetDelete(int id, string type)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "customer"))
            {
                return Redirect("~/Index");
            }
            List<string> listStatus = new List<string>();
            listStatus.Add("VNPay");
            listStatus.Add("Payment on delivery");
            ViewData["payment"] = new SelectList(listStatus);
            cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
            int index = Exists(cart, id, type);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToPage("CustomerCart");
        }

        public IActionResult OnPostUpdate(int[] quantities)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "customer"))
            {
                return Redirect("~/Index");
            }
            List<string> listStatus = new List<string>();
            listStatus.Add("VNPay");
            listStatus.Add("Payment on delivery");
            ViewData["payment"] = new SelectList(listStatus);
            cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
            for (var i = 0; i < cart.Count; i++)
            {
                cart[i].quantity = quantities[i];
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToPage("CustomerCart");
        }
        public IActionResult OnPostSubmit(string shippaddress, string paymentMethod)
        {
            Shippaddress = shippaddress;
            string s = paymentMethod;
            cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
            if (cart != null && cart.Count > 0)
                Total = (decimal)cart.Sum(i => ((double)i.unitPirce) * i.quantity - ((double)i.unitPirce) * i.quantity * (i.discount / 100));
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            if (!SessionHelper.checkPermission(HttpContext.Session, "customer"))
            {
                return Redirect("~/Index");
            }
            List<string> listStatus = new List<string>();
            listStatus.Add("VNPay");
            listStatus.Add("Payment on delivery");
            ViewData["payment"] = new SelectList(listStatus);
            cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
            if (!ModelState.IsValid)
            {
                cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
                if (cart != null && cart.Count > 0)
                    Total = (decimal)cart.Sum(i => ((double)i.unitPirce) * i.quantity - ((double)i.unitPirce) * i.quantity * (i.discount / 100));
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return Page();
            }

            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].type.Equals("combo"))
                {
                    Combo combo = comboRepository.GetById((int)cart[i].ItemId);
                    foreach (ComboDetail cbd in combo.ComboDetails)
                    {
                        Product product = productRepository.GetById((int)cbd.ProductId);
                        ShoppingItem tmp = cart.Where(item => item.type.Equals("product") && item.ItemId == cbd.ProductId).ToList().SingleOrDefault();
                        int quantityTmp = 0;
                        if (tmp != null)
                        {
                            quantityTmp = tmp.quantity;
                        }
                        if (product.UnitInStock < (cart[i].quantity*cbd.Quantity + quantityTmp))
                        {
                            Message = combo.ComboName + " is not enough quantity to buy!\n";
                            cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
                            if (cart != null && cart.Count > 0)
                                Total = (decimal)cart.Sum(i => ((double)i.unitPirce) * i.quantity - ((double)i.unitPirce) * i.quantity * (i.discount / 100));
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                            return Page();
                        }
                    }
                }
                if (cart[i].type.Equals("product"))
                {
                    Product product = productRepository.GetById((int)cart[i].ItemId);
                    if (product.UnitInStock < cart[i].quantity)
                    {
                        Message = product.ProductName + " is not enough quantity to buy!\n";
                        cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
                        if (cart != null && cart.Count > 0)
                            Total = (decimal)cart.Sum(i => ((double)i.unitPirce) * i.quantity - ((double)i.unitPirce) * i.quantity * (i.discount / 100));
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        return Page();
                    }
                }
                
            }

            if (paymentMethod.Equals("VNPay")) {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "shipaddress", Shippaddress);
                return payment();
            } else
            {
                Order order = new Order();
                order.Paymentmethod = "On delivery";
                order.PaymentStatus = "Not yet";
                order.OrderStatus = "ready";
                order.UserId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cusId");
                order.OrdeDate = DateTime.Now;
                order.Total = Total;
                order.ShipAddress = Shippaddress;
                orderRepository.Create(order);
                int orderId = orderRepository.GetAll().Max(x => x.OrderId);
                for (var i = 0; i < cart.Count; i++)
                {
                    if (cart[i].type.Equals("combo"))
                    {
                        Combo combo = comboRepository.GetById((int)cart[i].ItemId);
                        foreach (ComboDetail cbd in combo.ComboDetails)
                        {
                            Product product = productRepository.GetById((int)cbd.ProductId);

                            product.UnitInStock -= cbd.Quantity * cart[i].quantity;
                            productRepository.Update(product);
                        }
                        OrderComboDetail orderComboDetail = new OrderComboDetail
                        {
                            ComboId = combo.ComboId,
                            Discount = 0,
                            Feedback = "notfeedback",
                            Quantity = cart[i].quantity,
                            UnitPrice = combo.Price - combo.Price * (decimal)combo.Discount / 100,
                            OrderId = orderId,
                        };
                        orderComboDetailRepository.Create(orderComboDetail);
                    }
                    if (cart[i].type.Equals("product"))
                    {
                        Product product = productRepository.GetById((int)cart[i].ItemId);

                        product.UnitInStock -= cart[i].quantity;
                        productRepository.Update(product);
                        OrderProductDetail orderProductDetail = new OrderProductDetail
                        {
                            ProductId = product.ProductId,
                            Discount = 0,
                            Feedback = "notfeedback",
                            Quantity = cart[i].quantity,
                            UnitPrice = product.UnitPrice - product.UnitPrice * (decimal)product.ProductDiscount / 100,
                            OrderId = orderId,
                        };
                        orderProductDetailRepository.Create(orderProductDetail);
                    }

                }
                cart = null;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "shipaddress", null);
                return RedirectToPage("MyOrder");
;            }
            
            
        }
        private int Exists(List<ShoppingItem> cart, int id, string type)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].ItemId == id && cart[i].type.Equals(type))
                {
                    return i;
                }
            }
            return -1;
        }
        private IActionResult payment()
        {
            string vnp_Returnurl = "https://localhost:44338/OrderSuccess"; //URL nhan ket qua tra ve 
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = "4LOOJLGC"; //Ma website
            string vnp_HashSecret = "NEHWQAVTLXMXEHMFADKYMMAKTFQDGETR"; //Chuoi bi mat
            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                Message += "Error";
                return Page();
            }
            int uId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cusId");
            User us = userRepository.GetById(uId);
            string content = us.UserName + "Thanh toan don hang, sdt " + us.PhoneNumber;
            VNPayLibrary vnpay = new VNPayLibrary();
            vnpay.AddRequestData("vnp_Version", VNPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((int)Total*100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "khongco");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", content);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            Random random = new Random();
            int randomNum = random.Next(1, 100000);
            vnpay.AddRequestData("vnp_TxnRef", ""+randomNum); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            vnpay.AddRequestData("vnp_Bill_Email", "longnbse161068@fpt.edu.vn");


            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return Redirect(paymentUrl);
        }
    }
}
