using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;

namespace WEB_153505_PIKHTOVNIKAVA.Domain.Models
{
    public class Cart
    {
        /// <summary>
        /// Список объектов в корзине
        /// key - идентификатор объекта
        /// </summary>
        public Dictionary<int, CartItem> CartItems { get; set; } = new();

        /// <summary>
        /// Добавить объект в корзину
        /// </summary>
        /// <param name="dish">Добавляемый объект</param>
        public virtual void AddToCart(Sneaker sneaker)
        {
            // добавляем элемент в корзину. Если элемент уже есть - просто увеличиваем количество
            if (!CartItems.TryAdd(sneaker.Id, new CartItem { sneaker = sneaker, Count = 1 }))
                CartItems[sneaker.Id].Count++;
        }

        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id"> id удаляемого объекта</param>
        public virtual void RemoveItems(int id)
        {
            CartItems.Remove(id);
        }

        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            CartItems.Clear();
        }

        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count { get => CartItems.Sum(item => item.Value.Count); }

        public int Price { get => CartItems.Sum(item => item.Value.sneaker.Price * item.Value.Count); }
    }
}
    

