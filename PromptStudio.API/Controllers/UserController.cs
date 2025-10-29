using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        
    }
}
