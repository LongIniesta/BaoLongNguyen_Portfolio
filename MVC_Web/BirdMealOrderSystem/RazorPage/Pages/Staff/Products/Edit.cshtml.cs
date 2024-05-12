using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Repository;
using System.IO;

namespace RazorPage.Pages.Staff.Products
{
    public class EditModel : PageModel
    {
        IProductRepository productRepository = new ProductRepository();
        ISupplierRepository supplierRepository = new SupplierRepository();
        ICategoryDetail categoryDetailRepository = new CategoryDetailRepository();
        ICategoryRepository categoryRepository = new CategoryRepository();
        private readonly IWebHostEnvironment _environment;
        public string Message { get; set; }
        public List<CategoryDetail> listCategory { get; set; }

        public EditModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; }

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

            Product = productRepository.GetById((int)id);

            if (Product == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
            listCategory = categoryDetailRepository.GetAll();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile file, List<int> selectedItems)
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            if (!ModelState.IsValid)
            {
                ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
                listCategory = categoryDetailRepository.GetAll();
                Message = "error";
                Product = productRepository.GetById(Product.ProductId);
                return Page();
            }

            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                var uniqueFileName = Path.GetRandomFileName() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


                Product.ImageLink = uniqueFileName;
            }

            try
            {
                productRepository.Update(Product);

                foreach (var item in categoryDetailRepository.GetAll())
                {
                    Category cat = new Category();
                    cat.ProductId = Product.ProductId;
                    cat.CategoryId = item.CategoryId;

                    Message += cat.CategoryId + "-" + cat.ProductId + "\n";

                    if (categoryRepository.checkExits(cat) && !selectedItems.Any(i => i == cat.CategoryId))
                    {
                        categoryRepository.removeCategory(cat);
                    }
                    if (selectedItems.Any(i => i == cat.CategoryId) && !categoryRepository.checkExits(cat))
                    {
                        categoryRepository.addCategory(cat);
                    }
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
                listCategory = categoryDetailRepository.GetAll();
                Product = productRepository.GetById(Product.ProductId);
                return Page();
            }
        }
    }
}
