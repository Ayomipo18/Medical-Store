using AutoMapper;
using Contracts;
using Entities.Models.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Service.Utils.Azure;
using Service.Utils.Email;
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
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IOrderService> _orderService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IAzureBlobStorage azure, IEmailManager emailManager)
        {
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration, emailManager));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, mapper));
            _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, mapper, azure));
            _orderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager, mapper, userManager));
        }

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public ICategoryService CategoryService => _categoryService.Value;
        public IProductService ProductService => _productService.Value;
        public IOrderService OrderService => _orderService.Value;
    }
}
