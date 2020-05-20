using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Models
{
    public class Transaction
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Amount { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
