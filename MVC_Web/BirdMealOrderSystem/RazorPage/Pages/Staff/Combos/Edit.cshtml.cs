using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject;
using Repository;

namespace RazorPage.Pages.Staff.Combos
{
    public class EditModel : PageModel
    {
        IComboRepository comboRepository = new ComboRepository();
        IComboDetailRepository comboDetailRepository = new ComboDetailRepository();
        [BindProperty]
        public Combo Combo { get; set; }
        public double Total { get; set; }
        public string Message { get; set; }
        public List<ComboDetail> ComboDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            if (id == null)
            {
                return NotFound();
            }

            Combo = comboRepository.GetById((int)id);
            Total = 0;
            ComboDetails = new List<ComboDetail>();
            foreach (ComboDetail cbd in Combo.ComboDetails.ToList())
            {
                ComboDetail cbd1 = comboDetailRepository.GetAll().Where(c => c.ProductId == cbd.ProductId && c.ComboId == Combo.ComboId).SingleOrDefault();
                if (cbd1 != null)
                {
                    ComboDetails.Add(cbd1);
                    Total += cbd1.Quantity * ((double)cbd1.Product.UnitPrice) - cbd1.Quantity * ((double)cbd1.Product.UnitPrice) * (cbd1.Product.ProductDiscount / 100);
                }
            }

            if (Combo == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            Combo.ComboDetails = comboRepository.GetById((int)Combo.ComboId).ComboDetails;
            ComboDetails = new List<ComboDetail>();
            Total = 0;
            foreach (ComboDetail cbd in Combo.ComboDetails.ToList())
            {
                ComboDetail cbd1 = comboDetailRepository.GetAll().Where(c => c.ProductId == cbd.ProductId && c.ComboId == Combo.ComboId).SingleOrDefault();
                if (cbd1 != null)
                {
                    Total += cbd1.Quantity * ((double)cbd1.Product.UnitPrice) - cbd1.Quantity * ((double)cbd1.Product.UnitPrice) * (cbd1.Product.ProductDiscount / 100);
                    ComboDetails.Add(cbd1);
                }
            }
            if (!ModelState.IsValid)
            {
                ComboDetails = new List<ComboDetail>();
                Total = 0;
                foreach (ComboDetail cbd in Combo.ComboDetails.ToList())
                {
                    ComboDetail cbd1 = comboDetailRepository.GetAll().Where(c => c.ProductId == cbd.ProductId && c.ComboId == Combo.ComboId).SingleOrDefault();
                    if (cbd1 != null)
                    {
                        Total += cbd1.Quantity * ((double)cbd1.Product.UnitPrice) - cbd1.Quantity * ((double)cbd1.Product.UnitPrice) * (cbd1.Product.ProductDiscount / 100);
                        ComboDetails.Add(cbd1);
                    }
                }
                return Page();
            }
            if (Total < (double)Combo.Price)
            {
                double a = Total;
                double b = (double)Combo.Price;
                ComboDetails = new List<ComboDetail>();
                Total = 0;
                foreach (ComboDetail cbd in Combo.ComboDetails.ToList())
                {
                    ComboDetail cbd1 = comboDetailRepository.GetAll().Where(c => c.ProductId == cbd.ProductId && c.ComboId == Combo.ComboId).SingleOrDefault();
                    if (cbd1 != null)
                    {
                        Total += cbd1.Quantity * ((double)cbd1.Product.UnitPrice) - cbd1.Quantity * ((double)cbd1.Product.UnitPrice) * (cbd1.Product.ProductDiscount / 100);
                        ComboDetails.Add(cbd1);
                    }
                }
                Message = "Combo price must be less than total component price!";
                return Page();
            }


            try
            {
                comboRepository.Update(Combo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComboExists(Combo.ComboId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ComboExists(int id)
        {
            return comboRepository.GetById((int)id) != null;
        }
    }
}
