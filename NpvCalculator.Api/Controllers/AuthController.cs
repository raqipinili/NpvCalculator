using Microsoft.AspNetCore.Mvc;
using NpvCalculator.Core;
using NpvCalculator.Core.Classes;
using NpvCalculator.Security.Classes;
using NpvCalculator.Security.Services;
using System;
using System.Threading.Tasks;

namespace NpvCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost("login")]
        public async Task<IActionResult> CalculatePresentValue(Login login)
        {
            var result = await _authService.Login(login);
            return Ok(new { Token = result });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register register)
        {
            var result = await _authService.Register(register);
            return Ok(result);
        }
    }
}
