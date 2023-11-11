using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_153505_PIKHTOVNIKAVA.Domain.Entities
{
    public class Sneaker
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SeasonCategory? SeasonCategory { get; set; }

        public int ? SeasonCategoryId { get; set; }

        public int Price { get; set; }

        public string? PhotoPath { get; set; }
    }
}
