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
    internal sealed class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public void CreateCategory(Category category) => Create(category);
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .ToListAsync();

        //public async Task<Category> GetCategoryAsync(Guid id, bool trackChanges) =>
        //    await FindByCondition()
    }
}
