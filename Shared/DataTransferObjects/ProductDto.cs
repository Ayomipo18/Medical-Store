using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ProductDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float SellingPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public record ProductOrderDto
    {
        public string Name { get; set; }
    }

    public record ProductCreateDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float CostPrice { get; set; }
        public float ProfitMargin { get; set; }
        public int Quantity { get; set; }
    }

    public record ProductUpdateDto : ProductCreateDto
    {

    }
}
