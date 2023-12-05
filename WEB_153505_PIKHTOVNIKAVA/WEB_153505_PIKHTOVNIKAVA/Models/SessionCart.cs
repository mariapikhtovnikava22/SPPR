using System.Text.Json.Serialization;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;
using WEB_153505_PIKHTOVNIKAVA.Domain.Models;
using WEB_153505_PIKHTOVNIKAVA.Extensions;

namespace WEB_153505_PIKHTOVNIKAVA.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
            .HttpContext?.Session;
            SessionCart cart = session?.Get<SessionCart>("Cart") ?? new SessionCart();
            cart.session = session;
            return cart;
        }

        public override void AddToCart(Sneaker sneaker)
        {
            base.AddToCart(sneaker);
            session?.Set<Cart>("Cart", this);
        }

        public override void RemoveItems(int id)
        {
            base.RemoveItems(id);
            session?.Set<Cart>("Cart", this);
        }

        public override void ClearAll()
        {
            base.ClearAll();
            session?.Set<Cart>("Cart", this);
        }

        [JsonIgnore]
        ISession? session;
    }
}
