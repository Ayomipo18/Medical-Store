using Shared.DataTransferObjects;
using Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IOrderService
    {
        Task<SuccessResponse<OrderDto>> CreateOrder(OrderCreateDto createOrderDto, LoggedInUserDto loggedInUser);
        Task<PagedResponse<IEnumerable<OrderDto>>> GetAllOrders(ResourceParameter parameter, string actionName, IUrlHelper Url, LoggedInUserDto loggedInUser);
        Task<SuccessResponse<OrderDto>> GetOrder(Guid id, LoggedInUserDto loggedInUser);
    }
}
