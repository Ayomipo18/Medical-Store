using AutoMapper;
using Entities.Models;
using Entities.Models.Identities;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserCreateDto, User>();

            CreateMap<User, UserDto>();

            CreateMap<LoggedInUserDto, UserOrderDto>();

            CreateMap<User, UserOrderDto>();

        }
    }
}
