using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;

namespace WEB_153505_PIKHTOVNIKAVA.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly Services.ProductService.IProductService _context;
        private readonly Services.SeasonCategoryService.ISeasonCategoryService _service;


        [BindProperty]
        public IFormFile? Image { get; set; }

        public EditModel(Services.ProductService.IProductService context,
                         Services.SeasonCategoryService.ISeasonCategoryService service)
        {
            _context = context;
            _service = service;
        }

        [BindProperty]
        public Sneaker Sneaker { get; set; } = default!;

        [BindProperty]
        public int CategoryId { get; set; }

        public SelectList selectList { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sneaker = await _context.GetProductByIdAsync(id ?? default(int));
            if (sneaker.Success == false)
            {
                return NotFound();
            }

            Sneaker = sneaker.Data;
            selectList = new SelectList((await _service.GetCategoryListAsync()).Data,
                            nameof(SeasonCategory.Id), nameof(SeasonCategory.Name), Sneaker.SeasonCategory!.Name);

            //if (Sneaker.PhotoPath is not null)
            //{
            //    var imageResponse = await _context.GetImageAsync((int)id);

            //    if (imageResponse)
            //    {
            //        Image = imageResponse.Data;
            //    }
            //}


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Sneaker.SeasonCategory = (await _service.GetCategoryListAsync()).Data.Where(c => c.Id == CategoryId).FirstOrDefault();

            ModelState.ClearValidationState(nameof(Sneaker));
            if (!TryValidateModel(Sneaker, nameof(Sneaker)))
            {
                selectList = new SelectList((await _service.GetCategoryListAsync()).Data,
                                        nameof(SeasonCategory.Id), nameof(SeasonCategory.Name));
                return Page();
            }

            await _context.UpdateProductAsync(Sneaker.Id, Sneaker, Image);

            return RedirectToPage("./Index");
        }
    }
}

