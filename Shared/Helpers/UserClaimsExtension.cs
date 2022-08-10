using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class UserClaimsExtension
    {
        public static UserClaims UserClaims(this ClaimsPrincipal user)
        {
            var userId = user.Claims.Where(x => x.Type == ClaimTypeHelper.UserId)?.FirstOrDefault()?.Value;
            var email = user.Claims.Where(x => x.Type == ClaimTypeHelper.Email)?.FirstOrDefault()?.Value;
            var role = user.Claims.Where(x => x.Type == ClaimTypeHelper.Role)?.FirstOrDefault()?.Value;
            var firstName = user.Claims.Where(x => x.Type == ClaimTypeHelper.FirstName)?.FirstOrDefault()?.Value;
            var lastName = user.Claims.Where(x => x.Type == ClaimTypeHelper.LastName)?.FirstOrDefault()?.Value;
            var phoneNumber = user.Claims.Where(x => x.Type == ClaimTypeHelper.PhoneNumber)?.FirstOrDefault()?.Value;

            var getUserIdGuid = Guid.TryParse(userId, out Guid userIdGuid);

            return new UserClaims
            {
                UserId = getUserIdGuid ? userIdGuid : Guid.Empty,
                Email = email,
                Role = role,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
            };
        }
    }

    public class UserClaims
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
