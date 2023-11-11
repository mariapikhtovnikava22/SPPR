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
    public class IndexModel : PageModel
    {
        private readonly Services.ProductService.IProductService _context;

        public IndexModel(Services.ProductService.IProductService context)
        {
            _context = context;
        }

        public IList<Sneaker> Sneaker { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var answer = (await _context.GetProductListAsync(null));
            if (answer.Success)
            {
                Sneaker = answer.Data.Items;
            }
        }
        
    }
}
