using Microsoft.AspNetCore.Mvc;
using Security.Core.Classes;
using Security.Core.Services;
using System.Threading.Tasks;

namespace NpvCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login)
        {
            var result = await _authService.Login(login);
            return Ok(new { Token = result });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register register)
        {
            var result = await _userService.Register(register);
            return Ok(result);
        }
    }
}
