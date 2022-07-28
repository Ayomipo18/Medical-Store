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
    internal sealed class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public void CreateOrder(Order order) => Create(order);
        public async Task<IEnumerable<Order>> GetAllOrdersAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .ToListAsync();

        //public async Task<Category> GetCategoryAsync(Guid id, bool trackChanges) =>
        //    await FindByCondition()
    }
}
