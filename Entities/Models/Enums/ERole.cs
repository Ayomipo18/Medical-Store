using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Enums
{
    public enum ERole
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Customer")]
        Customer = 2
    }
}
