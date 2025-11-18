using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromptStudio.Application.DTOs.Collection;
using PromptStudio.Application.Services.Collections;
using PromptStudio.Application.Services.Prompts;

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

        // POST api/collection/
        [HttpPost]
        public async Task<IActionResult> CreatePromptCollection([FromBody] CreateCollectionDTO createCollectionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var promptCollection = await _collectionService.CreatePromptCollectionAsync(createCollectionDTO, userId.Value);

            if (promptCollection == null)
            {
                return BadRequest("creating collection is failed");
            }

            return CreatedAtAction(nameof(GetPromptCollectionsByUser), new { userId = userId }, promptCollection);

        }

        // GET api/collection/{userId}
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetPromptCollectionsByUser()
        {
            var userId = GetUserId();
            if(userId == null)
            {
                return Unauthorized();
            }
            var promptCollection = await _collectionService.GetPromptCollectionsByUserAsync(userId.Value);
            if (!promptCollection.Any() || promptCollection == null)
            {
                return Ok(Enumerable.Empty<object>());
            }
            return Ok(promptCollection);
        }

        // DELETE api/collection/{userId}
        [HttpDelete("{guid}/{Id}")]
        public async Task<IActionResult> DeletePromptCollection([FromRoute] Guid Id)
        {    
            var userId = GetUserId();
            if(userId == null)
            {
                return Unauthorized();
            }
            var promptCollection = await _collectionService.GetPromptCollectionAsync(userId.Value, Id);
            if (promptCollection == null)
            {
                return NotFound("Collection not found.");
            }
            var result = await _collectionService.DeletePromptCollectionAsync(Id, userId.Value);
            if (!result)
            {
                return BadRequest("the collection could not be deleted");
            }
            return NoContent();

        }

        // PUT api/collection/{userId}/{Id}
        [HttpPut("{guid}/{Id}")]
        public async Task<IActionResult> UpdatePromptCollection([FromBody]UpdateCollectionDTO updateCollectionDTO, [FromRoute] Guid Id )
        {
            var userId = GetUserId();
            if( userId == null)
            {
                return Unauthorized();
            }
            var promptCollection = await _collectionService.GetPromptCollectionAsync(userId.Value, Id);
            if (promptCollection == null)
            {
                return NotFound("Collection not found.");
            }
            var result = await _collectionService.UpdatePromptCollectionAsync(Id, userId.Value, updateCollectionDTO);
            if (result == null)
            {
                return BadRequest("Collection could not be updated");
            }
            return Ok(result);

             
            
        }

        
        


    }
}
