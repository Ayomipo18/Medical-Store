using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public record CategoryCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public record CategoryUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
