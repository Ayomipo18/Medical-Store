using AutoMapper;
using Contracts;
using Entities.Models.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
        }

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
