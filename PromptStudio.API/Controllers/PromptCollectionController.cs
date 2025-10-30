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
        

    }
}
