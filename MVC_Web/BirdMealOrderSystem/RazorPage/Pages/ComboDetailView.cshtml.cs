using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPage.Pages
{
    public class ComboDetailViewModel : PageModel
    {
        IComboRepository comboRepository = new ComboRepository();
        IComboDetailRepository comboDetailRepository = new ComboDetailRepository();
        IOrderComboDetailRepository orderComboDetailRepository = new OrderComboDetailRepository();

        public Combo Combo { get; set; }
        public List<ComboDetail> ComboDetails { get; set; }
        public List<OrderComboDetail> orderComboDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Combo = comboRepository.GetById((int)id);
            orderComboDetails = orderComboDetailRepository.GetAll().Where(i => i.ComboId == (int)id && i.Feedback != null && !i.Feedback.Equals("notfeedback")).ToList();
            ComboDetails = new List<ComboDetail>();
            foreach (ComboDetail cbd in Combo.ComboDetails.ToList())
            {
                ComboDetail cbd1 = comboDetailRepository.GetAll().Where(c => c.ProductId == cbd.ProductId && c.ComboId == Combo.ComboId).SingleOrDefault();
                if (cbd1 != null)
                {
                    ComboDetails.Add(cbd1);
                }
            }

            if (Combo == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
