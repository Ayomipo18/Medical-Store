using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float CostPrice { get; set; }
        public float ProfitMargin { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Category Category { get; set; }
    }
}
