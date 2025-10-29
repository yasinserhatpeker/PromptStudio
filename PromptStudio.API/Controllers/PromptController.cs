using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromptStudio.Application.DTOs.Prompt;
using PromptStudio.Infrastructure.Services;

namespace PromptStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptController : ControllerBase
    {
        private readonly PromptService _promptService;

        public PromptController(PromptService promptService)
        {
            _promptService = promptService;
        }

        // POST api/prompt
        [HttpPost]
        public async Task<IActionResult> CreatePrompt([FromBody] CreatePromptDTO createPromptDTO, [FromQuery] Guid userId)
        {
            if (createPromptDTO == null)
            {
                return BadRequest("Prompt data is required.");
            }
            var result = await _promptService.CreatePromptAsync(userId, createPromptDTO);
            if (result == null)
            {
                return BadRequest("Failed to create prompt");
            }

            return CreatedAtAction(nameof(GetPromptById), new { id = result.UserId }, result);

        }

        // GET api/prompt/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromptById(Guid id)
        {
            var prompt = await _promptService.GetPromptByIdAsync(id);
            if (prompt == null)
            {
                return BadRequest("prompt cannot found.");
            }
            return Ok(prompt);
        }


        // DELETE api/prompt/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromptById(Guid userId, Guid id)
        {
            var prompt = await _promptService.GetPromptByIdAsync(id);
            if (prompt == null)
            {
                return NotFound("prompt cannot found");
            }
            if (prompt.UserId != userId)
            {
                return Forbid();
            }
            var result = await _promptService.DeletePromptAsync(userId, id);
            if (!result)
            {
                return StatusCode(500, "An error occured during the prompt deletion");
            }
            return NoContent();


        }
        
        
    
        

        
    }
}
