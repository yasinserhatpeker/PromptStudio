using System;
using PromptStudio.Application.DTOs.Prompt;
using PromptStudio.Application.Services.Prompts;

namespace PromptStudio.Infrastructure.Services;

public class PromptService : IPromptService
{
    public Task<PromptResponseDTO> CreatePromptAsync(CreatePromptDTO createPromptDTO)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletePromptAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PromptResponseDTO> GetPromptByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PromptResponseDTO>> GetPromptsByUserAsync(Guid UserId)
    {
        throw new NotImplementedException();
    }

    public Task<PromptResponseDTO> UpdatePromptAsync(Guid Id, Guid userId, UpdatePromptDTO updatePromptDTO)
    {
        throw new NotImplementedException();
    }
}
