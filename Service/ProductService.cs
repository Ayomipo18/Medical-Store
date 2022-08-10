using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using Entities.Models;
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
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repository = repositoryManager;
            _mapper = mapper;
        }

        public async Task<SuccessResponse<ProductDto>> CreateProduct(ProductCreateDto productCreateDto)
        {
            var productExists = await _repository.Product.Get(x => x.Name == productCreateDto.Name).FirstOrDefaultAsync();
            if (productExists != null)
                throw new RestException(HttpStatusCode.BadRequest, "Product Already Exists");

            var product = _mapper.Map<Product>(productCreateDto);
            product.SellingPrice = CalculateSellingPrice(product.CostPrice, product.ProfitMargin);
            await _repository.Product.AddAsync(product);

            await _repository.SaveAsync();

            var productDto = _mapper.Map<ProductDto>(product);

            return new SuccessResponse<ProductDto>
            {
                Data = productDto,
                Message = "Product Created Successfully"
            };
            
        }

        public async Task<SuccessResponse<ProductDto>> UpdateProduct(Guid id, ProductUpdateDto productUpdateDto)
        {
            var productExists = await ProductExists(id);

            var product = _mapper.Map(productUpdateDto, productExists);
            product.SellingPrice = CalculateSellingPrice(product.CostPrice, product.ProfitMargin);
            await _repository.SaveAsync();

            var productDto = _mapper.Map<ProductDto>(product);

            return new SuccessResponse<ProductDto>
            {
                Data = productDto,
                Message = "Product Updated Successfully"
            };
        }

        public async Task<SuccessResponse<bool>> DeleteProduct(Guid id)
        {
            var product = await ProductExists(id);

            _repository.Product.Remove(product);
            await _repository.SaveAsync();

            return new SuccessResponse<bool>
            {
                Data = true,
                Message = "Product Deleted Successfully"
            };
        }

        public async Task<PagedResponse<IEnumerable<ProductDto>>> GetAllProducts(ProductParameters parameter, string actionName, IUrlHelper urlHelper)
        {
            var productsQuery = _repository.Product.QueryAll();

            if (!string.IsNullOrWhiteSpace(parameter.Search))
            {
                var search = parameter.Search.Trim().ToLower();
                productsQuery = productsQuery.Where(
                    x => x.Name.ToLower().Contains(search));
            };

            if (parameter.MinSellingPrice > 0.0 || parameter.MaxSellingPrice < Single.MaxValue)
            {
                productsQuery = productsQuery.Where(x => (x.SellingPrice >= parameter.MinSellingPrice 
                && x.SellingPrice <= parameter.MaxSellingPrice));
            };

            if (parameter.Quantity > 0)
            {
                productsQuery = productsQuery.Where(x => x.Quantity >= parameter.Quantity);
            }

            var productsDto = productsQuery.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
            var products = await PagedList<ProductDto>.CreateAsync(productsDto, parameter.PageNumber, parameter.PageSize, parameter.Sort);
            var dynamicParameters = PageUtility<ProductDto>.GenerateResourceParameters(parameter, products);
            var page = PageUtility<ProductDto>.CreateResourcePageUrl(dynamicParameters, actionName, products, urlHelper);

            return new PagedResponse<IEnumerable<ProductDto>>
            {
                Message = "Products Gotten Successfully",
                Data = products,
                Meta = new Meta
                {
                    Pagination = page
                }
            };
        }

        public async Task<SuccessResponse<ProductDto>> GetProduct(Guid id)
        {
            var product = await ProductExists(id);

            var productDto = _mapper.Map<ProductDto>(product);

            return new SuccessResponse<ProductDto>
            {
                Data = productDto,
                Message = "Product Gotten Successfully"
            };
        }

        private static float CalculateSellingPrice(float costPrice, float profitMargin)
        {
            float profit = profitMargin * 0.01f;
            float sellingPrice = costPrice + (profit * costPrice);
            return sellingPrice;
        }

        private async Task<Product> ProductExists(Guid id)
        {
            var product = await _repository.Product.GetByIdAsync(id);
            if (product == null)
                throw new RestException(HttpStatusCode.BadRequest, "Product doesn't Exist");

            return product;
        }
    }
}
