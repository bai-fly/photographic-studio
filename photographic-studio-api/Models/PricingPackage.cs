using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Models
{
    public class PricingPackage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cover { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public int Order { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
