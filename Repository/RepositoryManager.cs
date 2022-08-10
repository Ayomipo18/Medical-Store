using Contracts;
using Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _appDbContext;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;
        private readonly Lazy<IProductRepository> _productRepository;

        public RepositoryManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(appDbContext));
            _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(appDbContext));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(appDbContext));
        }

        public ICategoryRepository Category => _categoryRepository.Value;
        public IOrderRepository Order => _orderRepository.Value;
        public IProductRepository Product => _productRepository.Value;
        public async Task SaveAsync() => await _appDbContext.SaveChangesAsync();
        public async Task BeginTransaction(Func<Task> action)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                await action();

                await SaveAsync();
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
