﻿using RewardsCalculator.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Data
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
    }
}
