using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Product Product { get; set; }
    }
}
