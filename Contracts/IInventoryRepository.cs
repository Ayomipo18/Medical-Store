using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IInventoryRepository
    {
        void CreateInventory(Inventory inventory);
        Task<IEnumerable<Inventory>> GetAllInventoriesAsync(bool trackchanges);
        //Task<Inventory> GetInventoryAsync(Guid id, bool trackChanges);
        //void DeleteInventory(Inventory inventory);
    }
}
