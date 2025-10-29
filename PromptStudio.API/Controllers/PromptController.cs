using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromptStudio.Application.DTOs.Prompt;
using PromptStudio.Application.Services.Prompts;
using PromptStudio.Infrastructure.Migrations;
using PromptStudio.Infrastructure.Services;

namespace PromptStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptController : ControllerBase
    {
        private readonly IPromptService _promptService;

        public PromptController(IPromptService promptService)
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
        public async Task<IActionResult> DeletePromptById([FromQuery] Guid userId, Guid id)
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
        // UPDATE api/prompt/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrompt([FromQuery] Guid userId, Guid id, [FromBody] UpdatePromptDTO updatePromptDTO)
        {
            var prompt = await _promptService.GetPromptByIdAsync(id);
            if (prompt == null)
            {
                return NotFound("promt cannot found");
            }
            if (prompt.UserId != userId)
            {
                return Forbid();
            }
            var result = await _promptService.UpdatePromptAsync(userId, id, updatePromptDTO);
            if (result == null)
            {
                return StatusCode(500, "An error occured during the prompt updating");
            }

            return Ok(result);

        }
        // GET api/prompt/user
        [HttpGet]
        public async Task<IActionResult> GetPromptsByUser([FromQuery]Guid userId)
        {
            var prompts = await _promptService.GetPromptsByUserAsync(userId);
            if (prompts.Count == 0 || prompts== null)
            {
                return NotFound("prompts cannot found");
            }
           return Ok(prompts);
        }
        




        
        
    
        

        
    }
}
