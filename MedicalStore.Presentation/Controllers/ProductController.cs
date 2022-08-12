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
    [Route("api/products")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IServiceManager _service;

        public ProductController(IServiceManager service) => _service = service;

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto productCreateDto)
        {
            var response = await _service.ProductService.CreateProduct(productCreateDto);
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] ProductUpdateDto productUpdateDto)
        {
            var response = await _service.ProductService.UpdateProduct(id, productUpdateDto);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var response = await _service.ProductService.DeleteProduct(id);
            return Ok(response);
        }

        [HttpGet(Name = nameof(GetAllProducts))]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductParameters parameter)
        {
            var response = await _service.ProductService.GetAllProducts(parameter, nameof(GetAllProducts), Url);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var response = await _service.ProductService.GetProduct(id);
            return Ok(response);
        }
    }
}
