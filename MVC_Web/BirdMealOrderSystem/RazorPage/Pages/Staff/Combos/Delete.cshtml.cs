using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject;
using Repository;

namespace RazorPage.Pages.Staff.Combos
{
    public class DeleteModel : PageModel
    {

        IComboRepository comboRepository = new ComboRepository();
        IOrderComboDetailRepository orderComboDetailRepository = new OrderComboDetailRepository();
        IComboDetailRepository comboDetailRepository = new ComboDetailRepository();
        [BindProperty]
        public Combo Combo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Combo = comboRepository.GetById((int)id);

            if (Combo == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
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
            if (Combo != null)
            {
                if (orderComboDetailRepository.GetAll().Any(od => od.ComboId == (int)id))
                {
                    Combo.ComboStatus = false;
                    comboRepository.Update(Combo);
                }
                else
                {
                    foreach (ComboDetail cbd in comboDetailRepository.GetAll().Where(cbdt => cbdt.ComboId == (int)id))
                    {
                        comboDetailRepository.DeleteById(cbd.ComboId, cbd.ProductId);

                    }
                    comboRepository.DeleteById((int)id);
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
