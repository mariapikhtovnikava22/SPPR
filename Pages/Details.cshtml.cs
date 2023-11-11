using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;

namespace WEB_153505_PIKHTOVNIKAVA.Areas.Admin.Pages;

public class DetailsModel : PageModel
{
    private readonly Services.ProductService.IProductService _context;

    public DetailsModel(Services.ProductService.IProductService context)
    {
        _context = context;
    }

    public Sneaker Sneaker { get; set; } = default!;

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
        else
        {
            Sneaker = sneaker.Data;
        }
        return Page();
    }
}

