using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
        public int UnitPrice { get; set; }
    }
}
