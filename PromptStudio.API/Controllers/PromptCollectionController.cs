using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        
    }
}
