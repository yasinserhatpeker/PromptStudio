using System;
using PromptStudio.Application.DTOs.Collection;

namespace PromptStudio.Application.Services.Collections;

public interface ICollectionService
{
    Task<ResponseCollectionDTO> CreateCollectionAsync(CreateCollectionDTO createCollectionDTO);
    Task<ResponseCollectionDTO> UpdateCollectionAsync(Guid Id, Guid userId, UpdateCollectionDTO updateCollectionDTO);
    Task<bool> DeleteCollectionAsync(Guid Id);
    Task<List<ResponseCollectionDTO>>  GetAllCollectionAsync(Guid UserId);
    Task <ResponseCollectionDTO> GetPromptByIdAsync(Guid Id);

}
