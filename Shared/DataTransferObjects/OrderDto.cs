using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record OrderDto
    {
        public Guid Id { get; set; }
        public UserOrderDto User { get; set; }
        public ProductOrderDto Product { get; set; }
        public int Quantity { get; set; }
        public float TotalAmount { get; set; }
    }

    public record OrderCreateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
