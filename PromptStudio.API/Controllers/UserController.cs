using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Application.Services.Users;

namespace PromptStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User cannot found");
            }
            var result = await _userService.DeleteUserAsync(userId);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // UPDATE api/user/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User cannot found");
            }
            var result = await _userService.UpdateUserAsync(userId, updateUserDTO);
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
