﻿using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Products
{
    public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public Task<List<Product>> GetTopPriceProductsAsync(int count)
        {
            return Context.Products
                 .OrderByDescending(p => p.Price)
                 .Take(count)
                 .ToListAsync();
        }
    }
}
