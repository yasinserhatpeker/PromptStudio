using System;
using PromptStudio.Application.DTOs.Collection;
using PromptStudio.Application.Services.Collections;

namespace PromptStudio.Infrastructure.Services;

public class CollectionService : ICollectionService
{
    public Task<ResponseCollectionDTO> CreateCollectionAsync(CreateCollectionDTO createCollectionDTO)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCollectionAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ResponseCollectionDTO>> GetAllCollectionAsync(Guid UserId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseCollectionDTO> GetPromptByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseCollectionDTO> UpdateCollectionAsync(Guid Id, Guid userId, UpdateCollectionDTO updateCollectionDTO)
    {
        throw new NotImplementedException();
    }
}
