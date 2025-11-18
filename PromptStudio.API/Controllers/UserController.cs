using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Application.Services.Users;

namespace PromptStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _userService.CreateUserAsync(createUserDTO);
            if (result == null)
            {
                return BadRequest("User cannot be created");
            }
            return CreatedAtAction(nameof(GetUserById), new { userId = result.Id }, result);
        }

        // GET api/user/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            var result = await _userService.GetUserByIdAsync(userId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
            
            }

        // DELETE api/user/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser()
        {
            var userId = GetUserId();
            if (userId is null)
            {
                return Unauthorized();
            }
            var result = await _userService.DeleteUserAsync(userId.Value);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // UPDATE api/user/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = GetUserId();
            if(userId is null)
            {
                return Unauthorized();
            }
            var user = await _userService.GetUserByIdAsync(userId.Value);
            if (user == null)
            {
                return NotFound("User cannot found");
            }
            var result = await _userService.UpdateUserAsync(userId.Value, updateUserDTO);
            if (result == null)
            {
                return BadRequest("User cannot updated");
            }
            return Ok(result);
        }
    // GET api/users
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if(!users.Any() || users == null)
            {
                return NoContent();
            }
            return Ok(users);

        }

    }
    
    
}
