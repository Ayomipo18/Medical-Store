using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryCreateDto, Category>();

            CreateMap<Category, CategoryDto>();

            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
