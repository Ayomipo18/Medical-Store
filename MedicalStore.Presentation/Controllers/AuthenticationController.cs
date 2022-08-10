using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStore.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreate)
        {
            var response = await _service.AuthenticationService.CreateUser(userCreate);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto userLogin)
        {
            var response = await _service.AuthenticationService.Login(userLogin);
            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto tokenDto)
        {
            var response =  await _service.AuthenticationService.RefreshToken(tokenDto);
            return Ok(response);
        }
    }
}
