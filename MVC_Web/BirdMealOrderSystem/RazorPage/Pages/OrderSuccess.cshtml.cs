using BussinessObject;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient.Server;
using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RazorPage.Pages
{
    public class OrderSuccessModel : PageModel
    {
        public class ResultPay {
            public ResultPay()
            {
                
            }

            public string vnp_Amount { get; set; }
            public string vnp_OrderInfo { get; set; }
            public string vnp_PayDate { get; set; }
            public string vnp_ResponseCode { get; set; }
            public string vnp_TransactionStatus { get; set; }
        }
        public ResultPay resulPay { get; set; }
        IComboRepository comboRepository = new ComboRepository();
        IComboDetailRepository comboDetailRepository = new ComboDetailRepository();
        IProductRepository productRepository = new ProductRepository();
        IOrderRepository orderRepository = new OrderRepository();
        IOrderComboDetailRepository orderComboDetailRepository = new OrderComboDetailRepository();
        IOrderProductDetailRepository orderProductDetailRepository = new OrderProductDetailRepository();

        public List<ShoppingItem> cart { get; set; }
        public void OnGet()
        {
            var Request = HttpContext.Request; 
            string vnp_Amount = Request.Query["vnp_Amount"];
            string vnp_OrderInfo = Request.Query["vnp_OrderInfo"];
            string vnp_PayDate = Request.Query["vnp_PayDate"];
            string vnp_ResponseCode = Request.Query["vnp_ResponseCode"];
            string vnp_TransactionStatus = Request.Query["vnp_TransactionStatus"];

            resulPay = new ResultPay();
            resulPay.vnp_Amount = decimal.Parse(vnp_Amount) / 100 +"";
            resulPay.vnp_TransactionStatus= vnp_TransactionStatus;
            resulPay.vnp_OrderInfo= vnp_OrderInfo;
            resulPay.vnp_PayDate= vnp_PayDate;
            resulPay.vnp_ResponseCode= vnp_ResponseCode; 

            if (resulPay.vnp_ResponseCode == "00" && resulPay.vnp_TransactionStatus == "00")
            {
                cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
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
                            if (product.UnitInStock < (cart[i].quantity * cbd.Quantity + quantityTmp))
                            {
                               // Message = combo.ComboName + " is not enough quantity to buy!\n";
                                cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
                                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                                return;
                            }
                        }
                    }
                    if (cart[i].type.Equals("product"))
                    {
                        Product product = productRepository.GetById((int)cart[i].ItemId);
                        if (product.UnitInStock < cart[i].quantity)
                        {
                          //  Message = product.ProductName + " is not enough quantity to buy!\n";
                            cart = SessionHelper.GetObjectFromJson<List<ShoppingItem>>(HttpContext.Session, "cart");
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                            return;
                        }
                    }

                }

                Order order = new Order();
                order.Paymentmethod = "VNPay";
                order.PaymentStatus = "Done";
                order.OrderStatus = "ready";
                order.UserId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cusId");
                string format = "yyyyMMddHHmmss";
                order.OrdeDate = DateTime.ParseExact(vnp_PayDate, format, CultureInfo.InvariantCulture);
                order.Total = decimal.Parse(vnp_Amount)/100;
                order.ShipAddress = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "shipaddress");
                int it = 0;
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
                            UnitPrice = product.UnitPrice - product.UnitPrice*(decimal)product.ProductDiscount / 100,
                            OrderId = orderId,
                        };

                        orderProductDetailRepository.Create(orderProductDetail);
                    }

                }
            }
        }
        
    }
}
