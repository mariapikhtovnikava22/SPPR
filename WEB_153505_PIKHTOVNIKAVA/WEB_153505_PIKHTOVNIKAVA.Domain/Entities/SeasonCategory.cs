using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_153505_PIKHTOVNIKAVA.Domain.Entities
{
    public class SeasonCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Sneaker>? Sneakers { get; set; }

        public string NormalizedName { get; set; }

    }
}
