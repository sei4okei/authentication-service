using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Middleware;
using DataTransferObject;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginRequest)
        {
            var loginResponse = await _accountService.Login(loginRequest);

            if (loginResponse.Error != null) return BadRequest(loginResponse);

            return Ok(loginResponse);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerRequest)
        {
            var registerResponse = await _accountService.Register(registerRequest);

            if (registerResponse.Error != null) return BadRequest(registerResponse);

            return Ok(registerResponse);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromHeader(Name = "Refresh")] string refreshRequest)
        {
            var refreshResponse = await _accountService.Refresh(refreshRequest);

            if (refreshResponse.Error != null) return BadRequest(refreshResponse);

            return Ok(refreshResponse);
        }

        [HttpGet]
        [Route("status")]
        [Authorize]
        public IActionResult Status([FromHeader(Name = "Authorization")] string authorizationRequest)
        {
            var authorizationResponse = _accountService.Status(authorizationRequest);

            if (authorizationResponse.Error != null) return BadRequest(authorizationResponse);

            return Ok(authorizationResponse);
        }
    }
}
