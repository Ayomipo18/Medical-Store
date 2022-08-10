using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using Entities.Models;
using Entities.Models.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public OrderService(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repositoryManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<SuccessResponse<OrderDto>> CreateOrder(OrderCreateDto orderCreateDto, LoggedInUserDto loggedInUser)
        {
            var product = await ProductExists(orderCreateDto.ProductId);

            if (product.Quantity < orderCreateDto.Quantity)
                throw new RestException(HttpStatusCode.BadRequest, "The avalaible product quantity is less than the selected quantity");

            product.Quantity -= orderCreateDto.Quantity;

            var order = _mapper.Map<Order>(orderCreateDto);

            await _repository.Order.AddAsync(order);

            order.UserId = loggedInUser.UserId;
            order.TotalAmount = order.Quantity * product.SellingPrice;

            await _repository.SaveAsync();

            var orderDto = _mapper.Map<OrderDto>(order);

            var customerDetails = _mapper.Map<UserOrderDto>(loggedInUser);
            var productDetails = _mapper.Map<ProductOrderDto>(product);

            orderDto.User = customerDetails;
            orderDto.Product = productDetails;

            return new SuccessResponse<OrderDto>
            {
                Data = orderDto,
                Message = "Order Created Successfully"
            };
        }

        public async Task<PagedResponse<IEnumerable<OrderDto>>> GetAllOrders(ResourceParameter parameter, string actionName, IUrlHelper urlHelper)
        {
            var ordersQuery = _repository.Order.QueryAll()
                .Include(x => x.Product)
                .Include(x => x.User) as IQueryable<Order>;

            var ordersDto = ordersQuery.ProjectTo<OrderDto>(_mapper.ConfigurationProvider);
            var orders = await PagedList<OrderDto>.CreateAsync(ordersDto, parameter.PageNumber, parameter.PageSize, parameter.Sort);
            var dynamicParameters = PageUtility<OrderDto>.GenerateResourceParameters(parameter, orders);
            var page = PageUtility<OrderDto>.CreateResourcePageUrl(dynamicParameters, actionName, orders, urlHelper);

            return new PagedResponse<IEnumerable<OrderDto>>
            {
                Message = "Orders Gotten Successfully",
                Data = orders,
                Meta = new Meta
                {
                    Pagination = page
                }
            };
        }


        public async Task<SuccessResponse<OrderDto>> GetOrder(Guid id)
        {
            var order = await _repository.Order.Get(x => x.Id == id)
                .Include(x => x.Product)
                .Include(x => x.User)
                .FirstOrDefaultAsync();

            var orderDto = _mapper.Map<OrderDto>(order);

            return new SuccessResponse<OrderDto>
            {
                Data = orderDto,
                Message = "Order Gotten Successfully"
            };
        }

        private async Task<Product> ProductExists(Guid id)
        {
            var product = await _repository.Product.GetByIdAsync(id);
            if (product == null)
                throw new RestException(HttpStatusCode.BadRequest, "Product doesn't Exist");

            return product;
        }

        private async Task<Order> OrderExists(Guid id)
        {
            var order = await _repository.Order.GetByIdAsync(id);
            if (order == null)
                throw new RestException(HttpStatusCode.BadRequest, "Order doesn't Exist");

            return order;
        }
    }
}
