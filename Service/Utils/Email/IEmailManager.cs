using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utils.Email
{
    public interface IEmailManager
    {
        Task SendSingleEmail(string receiverAddress, string message, string subject);
    }
}
