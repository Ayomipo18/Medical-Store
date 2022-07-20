using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderRequestId { get; set; }
        public int Quantity { get; set; }
        public double TotalMoney { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public OrderRequest OrderRequest { get; set; }
    }
}
