﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public Guid InventoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double SellingPrice { get; set; }
        public double CostPrice { get; set; }
        public float ProfitMargin { get; set; }
        public string Manufacturer { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Inventory Inventory { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
