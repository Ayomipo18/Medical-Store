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
    public class CategoryService : ICategoryService
    {
        IRepositoryManager _repository;
        IMapper _mapper;
        public CategoryService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repository = repositoryManager;
            _mapper = mapper;
        }
        public async Task<SuccessResponse<CategoryDto>> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var categoryExists = await _repository.Category.Get(x => x.Name == categoryCreateDto.Name).FirstOrDefaultAsync();
            if (categoryExists != null)
                throw new RestException(HttpStatusCode.BadRequest, "Category Already Exists");

            var category = _mapper.Map<Category>(categoryCreateDto);
           await _repository.Category.AddAsync(category);
            await _repository.SaveAsync();

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return new SuccessResponse<CategoryDto>
            {
                Data = categoryDto,
                Message = "Category created successfully"
            };
        }

        public async Task<SuccessResponse<CategoryDto>> UpdateCategory(Guid id, CategoryUpdateDto categoryUpdateDto)
        {
            var categoryExists = await _repository.Category.GetByIdAsync(id);
            if (categoryExists == null)
                throw new RestException(HttpStatusCode.NotFound, "Category Doesn't Exist");

            var category = _mapper.Map(categoryUpdateDto, categoryExists);
            await _repository.SaveAsync();

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return new SuccessResponse<CategoryDto>
            {
                Data = categoryDto,
                Message = "Category Updated Sucessfully"
            };
        }

        public async Task<PagedResponse<IEnumerable<CategoryDto>>> GetAllCategories(ResourceParameter parameter, string actionName, IUrlHelper urlHelper)
        {
            var categoriesQuery = _repository.Category.QueryAll();

            if (!string.IsNullOrWhiteSpace(parameter.Search))
            {
                var search = parameter.Search.Trim().ToLower();
                categoriesQuery = categoriesQuery.Where(
                    x => x.Name.ToLower().Contains(search));
            };

            var categoriesDto = categoriesQuery.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider);
            var categories = await PagedList<CategoryDto>.CreateAsync(categoriesDto, parameter.PageNumber, parameter.PageSize, parameter.Sort);
            var dynamicParameters = PageUtility<CategoryDto>.GenerateResourceParameters(parameter, categories);
            var page = PageUtility<CategoryDto>.CreateResourcePageUrl(dynamicParameters, actionName, categories, urlHelper);

            return new PagedResponse<IEnumerable<CategoryDto>>
            {
                Message = "Categories Fetched Sucessfully",
                Data = categories,
                Meta = new Meta
                {
                    Pagination = page
                }
            };
        }

        public async Task<SuccessResponse<CategoryDto>> GetCategory(Guid id)
        {
            var category = await _repository.Category.GetByIdAsync(id);
            if (category == null)
                throw new RestException(HttpStatusCode.NotFound, "Category Doesn't Exist");

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return new SuccessResponse<CategoryDto>
            {
                Data = categoryDto,
                Message = "Category Fetched Sucessfully"
            };
        }
    }
}
