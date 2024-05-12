using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject;
using DTO;
using Repository;

namespace RazorPage.Pages.Staff.Combos
{
    public class CreateModel : PageModel
    {

        IComboRepository comboRepository = new ComboRepository();
        IComboDetailRepository comboDetailRepository = new ComboDetailRepository();
        IProductRepository productRepository = new ProductRepository();
        public List<ComboDetailDTO> cart { get; set; }
        public decimal Total { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            cart = SessionHelper.GetObjectFromJson<List<ComboDetailDTO>>(HttpContext.Session, "cart");
            if (cart != null && cart.Count > 0)
                Total = (decimal)cart.Sum(i => ((double)i.UnitPrice) * i.Quantity - ((double)i.UnitPrice) * i.Quantity * (i.ProductDiscount / 100));
            return Page();
        }

        [BindProperty]
        public Combo Combo { get; set; }


        public IActionResult OnGetBuyNow(int id)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            cart = SessionHelper.GetObjectFromJson<List<ComboDetailDTO>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<ComboDetailDTO>();
                Product product = productRepository.GetById((int)id);
                cart.Add(new ComboDetailDTO
                {
                    ImageLink = product.ImageLink,
                    ProductId = id,
                    Name = product.ProductName,
                    ProductDiscount = product.ProductDiscount,
                    UnitPrice = product.UnitPrice,
                    Quantity = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = Exists(cart, id);
                if (index == -1)
                {
                    Product product = productRepository.GetById((int)id);
                    cart.Add(new ComboDetailDTO
                    {
                        ImageLink = product.ImageLink,
                        ProductId = id,
                        Name = product.ProductName,
                        ProductDiscount = product.ProductDiscount,
                        UnitPrice = product.UnitPrice,
                        Quantity = 1
                    });
                }
                else
                {
                    cart[index].Quantity++;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToPage("Create");
        }
        public IActionResult OnGetDelete(int id)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            cart = SessionHelper.GetObjectFromJson<List<ComboDetailDTO>>(HttpContext.Session, "cart");
            int index = Exists(cart, id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToPage("Create");
        }

        public IActionResult OnPostUpdate(int[] quantities)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            cart = SessionHelper.GetObjectFromJson<List<ComboDetailDTO>>(HttpContext.Session, "cart");
            for (var i = 0; i < cart.Count; i++)
            {
                cart[i].Quantity = quantities[i];
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToPage("Create");
        }
        public IActionResult OnPostSubmit()
        {
            cart = SessionHelper.GetObjectFromJson<List<ComboDetailDTO>>(HttpContext.Session, "cart");
            if (cart != null && cart.Count > 0)
                Total = (decimal)cart.Sum(i => ((double)i.UnitPrice) * i.Quantity - ((double)i.UnitPrice) * i.Quantity * (i.ProductDiscount / 100));
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            cart = SessionHelper.GetObjectFromJson<List<ComboDetailDTO>>(HttpContext.Session, "cart");
            if (!ModelState.IsValid)
            {
                cart = SessionHelper.GetObjectFromJson<List<ComboDetailDTO>>(HttpContext.Session, "cart");
                if (cart != null && cart.Count > 0)
                    Total = (decimal)cart.Sum(i => ((double)i.UnitPrice) * i.Quantity - ((double)i.UnitPrice) * i.Quantity * (i.ProductDiscount / 100));
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return Page();
            }

            /*for (var i = 0; i < cart.Count; i++)
            {
                Product product = productRepository.GetAll().SingleOrDefault(f => f.ProductId == cart[i].ProductId);
                if (product.UnitInStock < cart[i].Quantity)
                {
                    Message = product.ProductName + " is not enough quantity to buy!";
                    cart = SessionHelper.GetObjectFromJson<List<ComboDetailDTO>>(HttpContext.Session, "cart");
                    if (cart != null && cart.Count > 0)
                        Total = (decimal)cart.Sum(i => ((double)i.UnitPrice) * i.Quantity - ((double)i.UnitPrice) * i.Quantity * (i.ProductDiscount / 100));
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                    return Page();
                }
            }*/

            if (Total < Combo.Price)
            {
                double a = (double)Total;
                double b = (double)Combo.Price;
                Message = "Combo price must be less than total of component!";
                cart = SessionHelper.GetObjectFromJson<List<ComboDetailDTO>>(HttpContext.Session, "cart");
                if (cart != null && cart.Count > 0)
                    Total = (decimal)cart.Sum(i => ((double)i.UnitPrice) * i.Quantity - ((double)i.UnitPrice) * i.Quantity * (i.ProductDiscount / 100));
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return Page();
            }

            comboRepository.Create(Combo);
            int comboID = comboRepository.GetAll(true).Max(cb => cb.ComboId);
            for (var i = 0; i < cart.Count; i++)
            {
                ComboDetail cbd = new ComboDetail();
                cbd.ProductId = cart[i].ProductId;
                cbd.Quantity = cart[i].Quantity;
                cbd.ComboId = comboID;
                /*Product product = productRepository.GetById(cart[i].ProductId);
                product.UnitInStock = product.UnitInStock - cbd.Quantity;
                productRepository.Update(product);*/
                comboDetailRepository.Create(cbd);
            }

            cart = null;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToPage("./Index");
        }
        private int Exists(List<ComboDetailDTO> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
