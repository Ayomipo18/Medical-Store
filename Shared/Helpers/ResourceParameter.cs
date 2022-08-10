using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public class ResourceParameter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; }
        public string Sort { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class ProductParameters : ResourceParameter
    {
        public float MinSellingPrice { get; set; } = 0.0f;
        public float MaxSellingPrice { get; set; } = Single.MaxValue;
        public int Quantity { get; set; } = 0;
    }
}
