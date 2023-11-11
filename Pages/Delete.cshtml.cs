using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;

namespace WEB_153505_PIKHTOVNIKAVA.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Services.ProductService.IProductService _context;
        public DeleteModel(Services.ProductService.IProductService context)
        {
            _context = context;
        }

        [BindProperty]
      public Sneaker Sneaker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sneaker = await _context.GetProductByIdAsync(id ?? default(int));

            if (sneaker == null)
            {
                return NotFound();
            }
            else
            {
                Sneaker = sneaker.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _context.DeleteProductAsync(id ?? default(int));

            return RedirectToPage("./Index");
        }
    }
}

