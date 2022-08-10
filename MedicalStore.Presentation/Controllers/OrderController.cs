using MedicalStore.Presentation.Controllers.CommonController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStore.Presentation.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IServiceManager _service;
        public OrderController(IServiceManager service) => _service = service;

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            var response = await _service.OrderService.CreateOrder(orderCreateDto, loggedInUser);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders([FromQuery] ResourceParameter parameter)
        {
            var response = await _service.OrderService.GetAllOrders(parameter, nameof(GetAllOrders), Url);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var response = await _service.OrderService.GetOrder(id);
            return Ok(response);
        }

        //[HttpGet("{id:guid}")]
        //[Authorize(Roles = "Customer")]
        //public async Task<IActionResult> GetOrdersForUser(Guid id)
        //{
        //    var response = await _service.OrderService.GetOrder(id);
        //    return Ok(response);
        //}
    }
}
