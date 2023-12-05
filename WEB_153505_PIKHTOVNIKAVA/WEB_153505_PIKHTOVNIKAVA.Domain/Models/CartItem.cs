using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB_153505_PIKHTOVNIKAVA.Domain.Entities;

namespace WEB_153505_PIKHTOVNIKAVA.Domain.Models
{
    public class CartItem
    {
        public int Count { get; set; }

        public Sneaker sneaker { get; set; }
    }
}
