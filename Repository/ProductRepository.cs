using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public void CreateProduct(Product product) => Create(product);
        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .ToListAsync();

        //public async Task<Category> GetCategoryAsync(Guid id, bool trackChanges) =>
        //    await FindByCondition()
    }
}
