﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICategoryRepository Category { get; }
        IInventoryRepository Inventory { get; }
        IOrderRepository Order { get; }
        IProductRepository Product { get; }
        Task SaveAsync();
    }
}
