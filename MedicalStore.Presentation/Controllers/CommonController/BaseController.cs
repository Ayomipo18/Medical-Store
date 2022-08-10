using MedicalStore.Presentation.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStore.Presentation.Controllers.CommonController
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public LoggedInUserDto loggedInUser => new()
        {
            UserId = WebHelper.UserId,
            FirstName = WebHelper.FirstName,
            LastName = WebHelper.LastName,
            Email = WebHelper.Email,
            PhoneNumber = WebHelper.PhoneNumber,
            Role = WebHelper.Role
        };
    }
}
