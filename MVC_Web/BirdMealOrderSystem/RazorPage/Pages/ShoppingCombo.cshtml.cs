using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPage.Pages
{
    public class ShoppingComboModel : PageModel
    {
        IComboRepository comboRepository = new ComboRepository();
        public List<Combo> Combo { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Combo = comboRepository.GetAll(true);
            //Combo.AddRange(comboRepository.GetAll(false));
            return Page();
        }
        public IActionResult OnPost(string? strsearch)
        {
            if (strsearch == null)
            {
                strsearch = "";
            }
            else
            {
                strsearch = strsearch.ToLower();
            }
            Combo = comboRepository.GetAll(true).Where(o => o.ComboName.ToLower().Contains(strsearch)).ToList();
            return Page();
        }
    }
}
