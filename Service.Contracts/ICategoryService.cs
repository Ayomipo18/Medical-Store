using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        Task<SuccessResponse<CategoryDto>> CreateCategory(CategoryCreateDto categoryCreateDto);
        Task<SuccessResponse<CategoryDto>> UpdateCategory(Guid id, CategoryUpdateDto categoryUpdateDto);
        Task<PagedResponse<IEnumerable<CategoryDto>>> GetAllCategories(ResourceParameter parameter, string actionName, IUrlHelper urlHelper);
        Task<SuccessResponse<CategoryDto>> GetCategory(Guid id);
    }
}
