﻿using System;

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
