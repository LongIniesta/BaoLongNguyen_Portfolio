using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Repository;
using System.IO;

namespace RazorPage.Pages.Staff.Products
{
    public class CreateModel : PageModel
    {
        IProductRepository productRepository = new ProductRepository();
        ISupplierRepository supplierRepository = new SupplierRepository();
        ICategoryDetail categoryDetailRepository = new CategoryDetailRepository();
        ICategoryRepository categoryRepository = new CategoryRepository();
        private readonly IWebHostEnvironment _environment;
        public string Message { get; set; }
        public List<CategoryDetail> listCategory { get; set; }

        public CreateModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }



        public IActionResult OnGet()
        {
            if (!SessionHelper.checkPermission(HttpContext.Session, "staff"))
            {
                return Redirect("~/ErrorRole");
            }
            ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
            listCategory = categoryDetailRepository.GetAll();
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
                try
                {
                    productRepository.Create(Product);
                    int productId = productRepository.GetAll().Max(p => p.ProductId);
                    foreach (int catId in selectedItems)
                    {
                        Category cat = new Category();
                        cat.ProductId = productId;
                        cat.CategoryId = catId;
                        categoryRepository.addCategory(cat);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return RedirectToPage("./Index");
            }
            Message = "Image is required!";
            ViewData["SupplierId"] = new SelectList(supplierRepository.GetAll(), "SupplierId", "SupplierName");
            listCategory = categoryDetailRepository.GetAll();
            return Page();
        }
    }
}
