using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromptStudio.Application.DTOs.Prompt;
using PromptStudio.Application.Services.Prompts;

namespace PromptStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class PromptController : BaseApiController
    {
        private readonly IPromptService _promptService;

        public PromptController(IPromptService promptService)
        {
            _promptService = promptService;
        }

        // POST api/prompt
        [HttpPost]
        public async Task<IActionResult> CreatePrompt([FromBody] CreatePromptDTO createPromptDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserId();
            if (userId is null)
            {
                return Unauthorized();
            }

            var result = await _promptService.CreatePromptAsync(userId.Value, createPromptDTO);
            if (result == null)
            {
                return BadRequest("Failed to create prompt");
            }

            return CreatedAtAction(nameof(GetPromptById), new { id = result.Id }, result);
        }

        // GET api/prompt/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPromptById(Guid id)
        {
            var userId = GetUserId();
            if (userId is null)
            {
                return Unauthorized();
            }

            var prompt = await _promptService.GetPromptByIdAsync(id);
            if (prompt == null)
            {
                return NotFound("Prompt cannot found.");
            }

            if (prompt.UserId != userId.Value)
            {
                return Forbid();
            }

            return Ok(prompt);
        }

        // DELETE api/prompt/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePromptById(Guid id)
        {
            var userId = GetUserId();
            if (userId is null)
            {
                return Unauthorized();
            }

            var prompt = await _promptService.GetPromptByIdAsync(id);
            if (prompt == null)
            {
                return NotFound("Prompt cannot found");
            }

            if (prompt.UserId != userId.Value)
            {
                return Forbid();
            }

            var result = await _promptService.DeletePromptAsync(userId.Value, id);
            if (!result)
            {
                return StatusCode(500, "An error occured during the prompt deletion");
            }

            return NoContent();
        }

        // UPDATE api/prompt/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePrompt(Guid id, [FromBody] UpdatePromptDTO updatePromptDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserId();
            if (userId is null)
            {
                return Unauthorized();
            }

            var prompt = await _promptService.GetPromptByIdAsync(id);
            if (prompt == null)
            {
                return NotFound("Prompt cannot found");
            }

            if (prompt.UserId != userId.Value)
            {
                return Forbid();
            }

            var result = await _promptService.UpdatePromptAsync(userId.Value, id, updatePromptDTO);
            if (result == null)
            {
                return StatusCode(500, "An error occured during the prompt updating");
            }

            return Ok(result);
        }

        // GET api/prompt/me
        [HttpGet("me")]
        public async Task<IActionResult> GetMyPrompts()
        {
            var userId = GetUserId();
            if (userId is null)
            {
                return Unauthorized();
            }

            var prompts = await _promptService.GetPromptsByUserAsync(userId.Value);
            return Ok(prompts);
        }
    }
}
