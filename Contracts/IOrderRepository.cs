using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync(bool trackchanges);
        //Task<Order> GetOrderAsync(Guid id, bool trackChanges);
        //void DeleteOrder(Order order);
    }
}
