using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromptStudio.Application.DTOs.Collection;
using PromptStudio.Application.Services.Collections;

namespace PromptStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PromptCollectionController : BaseApiController
    {
        private readonly ICollectionService _collectionService;

        public PromptCollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        // POST api/promptcollection
        [HttpPost]
        public async Task<IActionResult> CreatePromptCollection([FromBody] CreateCollectionDTO createCollectionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var promptCollection = await _collectionService.CreatePromptCollectionAsync(createCollectionDTO, userId.Value);
            if (promptCollection == null)
                return BadRequest("Creating collection failed.");

            return CreatedAtAction(
                nameof(GetCollectionById),
                new { id = promptCollection.Id },
                promptCollection
            );
        }

        // GET api/promptcollection/me
        [HttpGet("me")]
        public async Task<IActionResult> GetPromptCollectionsByUser()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var promptCollections = await _collectionService.GetPromptCollectionsByUserAsync(userId.Value);
            if (promptCollections == null || !promptCollections.Any())
                return Ok(Enumerable.Empty<object>());

            return Ok(promptCollections);
        }

        // GET api/promptcollection/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCollectionById([FromRoute] Guid id)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var collection = await _collectionService.GetPromptCollectionAsync(userId.Value, id);
            if (collection == null)
                return NotFound("Collection not found.");

            return Ok(collection);
        }

        // DELETE api/promptcollection/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePromptCollection([FromRoute] Guid id)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var promptCollection = await _collectionService.GetPromptCollectionAsync(userId.Value, id);
            if (promptCollection == null)
                return NotFound("Collection not found.");

            var result = await _collectionService.DeletePromptCollectionAsync(id, userId.Value);
            if (!result)
                return BadRequest("The collection could not be deleted.");

            return NoContent();
        }

        // PUT api/promptcollection/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePromptCollection([FromRoute] Guid id, [FromBody] UpdateCollectionDTO updateCollectionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var promptCollection = await _collectionService.GetPromptCollectionAsync(userId.Value, id);
            if (promptCollection == null)
                return NotFound("Collection not found.");

            var result = await _collectionService.UpdatePromptCollectionAsync(id, userId.Value, updateCollectionDTO);
            if (result == null)
                return BadRequest("Collection could not be updated.");

            return Ok(result);
        }
    }
}
