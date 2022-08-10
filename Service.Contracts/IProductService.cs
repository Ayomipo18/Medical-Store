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
    public interface IProductService
    {
        Task<SuccessResponse<ProductDto>> CreateProduct(ProductCreateDto productCreateDto);
        Task<SuccessResponse<ProductDto>> UpdateProduct(Guid id, ProductUpdateDto productUpdateDto);
        Task<SuccessResponse<bool>> DeleteProduct(Guid id);
        Task<PagedResponse<IEnumerable<ProductDto>>> GetAllProducts(ProductParameters parameter, string actionName, IUrlHelper urlHelper);
        Task<SuccessResponse<ProductDto>> GetProduct(Guid id);
    }
}
