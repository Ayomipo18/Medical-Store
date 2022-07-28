using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync(bool trackchanges);
        //Task<Product> GetProductAsync(Guid id, bool trackChanges);
        //void DeleteProduct(Product product);
    }
}
