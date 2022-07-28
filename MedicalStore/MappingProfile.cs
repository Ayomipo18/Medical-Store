using AutoMapper;
using Entities.Models.Identities;
using Shared.DataTransferObjects;

namespace MedicalStore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateDto, User>();

            CreateMap<User, UserDto>();
        }
    }
}
