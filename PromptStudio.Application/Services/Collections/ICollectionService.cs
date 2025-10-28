using System;
using PromptStudio.Application.DTOs.Collection;

namespace PromptStudio.Application.Services.Collections;

public interface ICollectionService
{
    Task<ResponseCollectionDTO> CreatePromptCollectionAsync(CreateCollectionDTO createCollectionDTO,Guid userId);
    Task<ResponseCollectionDTO> UpdatePromptCollectionAsync(Guid Id, Guid userId, UpdateCollectionDTO updateCollectionDTO);
    Task<bool> DeletePromptCollectionAsync(Guid Id,Guid userId);
    Task<List<ResponseCollectionDTO>>  GetAllPromptCollectionAsync(Guid UserId);
    Task <List<ResponseCollectionDTO>> GetPromptCollectionsByUserAsync(Guid UserId);

}
