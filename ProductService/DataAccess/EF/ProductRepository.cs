﻿using Microsoft.EntityFrameworkCore;
using ProductService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.DataAccess.EF
{
    public class ProductRepository : IProductRepository
    {
        private ProductDbContext productDbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            this.productDbContext = productDbContext ?? throw new ArgumentNullException(nameof(productDbContext));
        }

        public Task<Product> Add(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> FindAllActive()
        {
           return await productDbContext
               .Products
               .Include(c => c.Covers)
               .Include("Questions.Choices")
               .Where(p => p.Status == ProductStatus.Active)
               .ToListAsync();
        }

        public async Task<Product> FindOne(string productCode)
        {
            return await productDbContext
                .Products
                .Include(c => c.Covers)
                .Include("Questions.Choices")
                .FirstOrDefaultAsync(p => p.Code.Equals(productCode, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<Product> FindById(Guid id)
        {
            return await productDbContext.Products.Include(c => c.Covers).Include("Questions.Choices")
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
