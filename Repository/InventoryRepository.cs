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
    internal sealed class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
    {
        public InventoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public void CreateInventory(Inventory inventory) => Create(inventory);
        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .ToListAsync();

        //public async Task<Category> GetCategoryAsync(Guid id, bool trackChanges) =>
        //    await FindByCondition()
    }
}
