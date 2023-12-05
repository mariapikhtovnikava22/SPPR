using Microsoft.AspNetCore.Mvc;
using WEB_153505_PIKHTOVNIKAVA.Domain.Models;
using WEB_153505_PIKHTOVNIKAVA.Extensions;

namespace WEB_153505_PIKHTOVNIKAVA.ViewComponents
{
    public class OrderCart : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(GetItems());
        }

        // для иммитации получения данных из какого то места
        private Tuple<int, int> GetItems()
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new();

            int price = cart.Price;
            int count = cart.Count;
            return new Tuple<int, int>(price, count);
        }
    }
}
