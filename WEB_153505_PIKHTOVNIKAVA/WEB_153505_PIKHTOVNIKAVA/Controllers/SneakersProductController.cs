using Microsoft.AspNetCore.Mvc;
using WEB_153505_PIKHTOVNIKAVA.Services.ProductService;
using WEB_153505_PIKHTOVNIKAVA.Domain.Models;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;
using WEB_153505_PIKHTOVNIKAVA.Extensions;
using WEB_153505_PIKHTOVNIKAVA.Services.SeasonCategoryService;

namespace WEB_153505_PIKHTOVNIKAVA.Controllers
{
    public class SneakersProductController : Controller
    {
        readonly IProductService _productService;
        readonly ISeasonCategoryService _categoryService;


        public SneakersProductController(IProductService productService, ISeasonCategoryService categoryService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
        }


        [Route("Catalog/{category?}", Name = "catalog")]
        public async Task<IActionResult> Index(string? category, int pageno)
        {
            // получаем список кроссовок
            var productResponse =
           await _productService.GetProductListAsync(category, pageno);

            // обработка неуспешного обращения
            if (!productResponse.Success)
                return NotFound(productResponse.ErrorMessage);

            // получаем все категории
            var categories = (await _categoryService.GetCategoryListAsync()).Data;

            // currentCategory вынесено в отдельную переменную, чтобы проверить на null и, в случае null, передать 
            // в представление строку "Все"
            var currentCategory = categories.Find(c => c.NormalizedName == category);


            // чтобы отображать категорию "все", на странице с типами
            ViewData["currentCategory"] = currentCategory == null ? "Все" : currentCategory.Name;
            ViewBag.categories = categories;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_FurnituriesPartial", productResponse.Data);
            }

            return View(productResponse.Data);
        }
    }
}
