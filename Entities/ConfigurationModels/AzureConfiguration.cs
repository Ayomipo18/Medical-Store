using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ConfigurationModels
{
    public class AzureConfiguration
    {
        public string Section { get; set; } = "Azure";
        public string BlobConnectionString { get; set; }
        public string BlobContainerName { get; set; }
    }
}
