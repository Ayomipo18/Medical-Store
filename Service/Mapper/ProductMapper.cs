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
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductCreateDto, Product>();

            CreateMap<Product, ProductDto>();

            CreateMap<Product, ProductOrderDto>();
        }
    }
}
