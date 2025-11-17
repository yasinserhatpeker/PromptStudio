using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Application.Services.Users;
using PromptStudio.Domain.Entites;

namespace PromptStudio.API.Controllers
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

        [HttpPost("register")]
        [AllowAnonymous]
        // POST api/auth/register
        public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDTO )
        {
          if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _authService.RegisterAsync(createUserDTO);
            if(user == null)
            {
                return BadRequest("The registration could not be completed.");
            }
            
           return CreatedAtAction(nameof(UserController.GetUserById),new {id =user.Id},user);

        }

        [HttpPost("login")]
        [AllowAnonymous]
        [EnableRateLimiting("LoginPolicy")]
        // POST api/auth/login
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var login = await _authService.LoginAsync(loginDTO);
            if(login == null)
            {
                return Unauthorized("Email or password are incorrect.");
            }

            return Ok(login);
            
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        // POST api/auth/logout
        public async Task<IActionResult> Logout([FromBody] LogoutDTO logoutDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var logout = await _authService.LogoutAsync(logoutDTO.RefreshToken);
            if(!logout)
            {
                return NotFound("Refresh token is not found");
            }
            return NoContent();
         }
         
    }
}
