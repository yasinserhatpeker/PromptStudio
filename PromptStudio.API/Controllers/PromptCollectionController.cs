using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromptStudio.Application.DTOs.Collection;
using PromptStudio.Application.Services.Collections;
using PromptStudio.Application.Services.Prompts;

namespace PromptStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptCollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public PromptCollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        // POST api/collection/
        [HttpPost]
        public async Task<IActionResult> CreatePromptCollection([FromBody] CreateCollectionDTO createCollectionDTO, [FromRoute] Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var promptCollection = await _collectionService.CreatePromptCollectionAsync(createCollectionDTO, userId);

            if (promptCollection == null)
            {
                return BadRequest("creating collection is failed");
            }

            return CreatedAtAction(nameof(GetPromptCollectionsByUser), new { userId = userId }, promptCollection);

        }

        // GET api/collection/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPromptCollectionsByUser([FromRoute] Guid userId)
        {
            var promptCollection = await _collectionService.GetPromptCollectionsByUserAsync(userId);
            if (!promptCollection.Any() || promptCollection == null)
            {
                return Ok(Enumerable.Empty<object>());
            }
            return Ok(promptCollection);
        }

        // DELETE api/collection/{userId}
        [HttpDelete("{userId}/{Id}")]
        public async Task<IActionResult> DeletePromptCollection([FromRoute] Guid userId, [FromRoute] Guid Id)
        {
            var promptCollection = await _collectionService.GetPromptCollectionAsync(userId,Id);
            if (promptCollection == null)
            {
                return NotFound();
            }
            var result = await _collectionService.DeletePromptCollectionAsync(Id, userId);
            if (!result)
            {
                return BadRequest("the collection cannot be deleted");
            }
            return NoContent();

        }

        // GET api/collection/{userId}/{Id}
        [HttpGet("{userId}/{Id}")]
        


    }
}
