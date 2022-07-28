using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public record AuthDto
    {
        public string AccessToken { get; set; }
        public DateTime? ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }

    public record UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public record UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public record RefreshTokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }


}
