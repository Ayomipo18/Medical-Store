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
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly IServiceManager _service;

        public CategoryController(IServiceManager service) => _service = service;

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
        {
            var response = await _service.CategoryService.CreateCategory(categoryCreateDto);
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            var response = await _service.CategoryService.UpdateCategory(id, categoryUpdateDto);
            return Ok(response);
        }

        [HttpGet(Name = nameof(GetAllCategories))]
        public async Task<IActionResult> GetAllCategories([FromQuery] ResourceParameter parameter)
        {
            var response = await _service.CategoryService.GetAllCategories(parameter, nameof(GetAllCategories), Url);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var response = await _service.CategoryService.GetCategory(id);
            return Ok(response);
        }
    }
}
