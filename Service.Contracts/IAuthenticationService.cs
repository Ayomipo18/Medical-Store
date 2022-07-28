using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<SuccessResponse<UserDto>> CreateUser(UserCreateDto userCreate);
        Task<SuccessResponse<AuthDto>> Login(UserLoginDto userLogin);
        Task<SuccessResponse<AuthDto>> RefreshToken(RefreshTokenDto tokenDto);
    }
}
