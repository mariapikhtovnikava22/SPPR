using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;

namespace WEB_153505_PIKHTOVNIKAVA.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Services.ProductService.IProductService _context;
        private readonly Services.SeasonCategoryService.ISeasonCategoryService _service;

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        public SelectList selectList { get; set; }

        [BindProperty]
        public Sneaker Sneaker { get; set; } = default!;

        public CreateModel(Services.ProductService.IProductService context,
                            Services.SeasonCategoryService.ISeasonCategoryService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<IActionResult> OnGet()
        {
            selectList = new SelectList((await _service.GetCategoryListAsync()).Data,
                                            nameof(SeasonCategory.Id), nameof(SeasonCategory.Name));
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Sneaker.SeasonCategory = (await _service.GetCategoryListAsync()).Data.Where(c => c.Id == CategoryId).FirstOrDefault();

            ModelState.ClearValidationState(nameof(Sneaker));
            if (!TryValidateModel(Sneaker, nameof(Sneaker)) || Sneaker == null || Image == null)
            {
                selectList = new SelectList((await _service.GetCategoryListAsync()).Data,
                                        nameof(SeasonCategory.Id), nameof(SeasonCategory.Name));
                return Page();
            }

            var response = await _context.CreateProductAsync(Sneaker, Image);
            if (!response.Success)
                throw new Exception(response.ErrorMessage);

            return RedirectToPage("./Index");
        }
    }
}
