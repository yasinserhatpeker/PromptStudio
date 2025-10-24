using System;
using System.Data.Common;
using PromptStudio.Application.DTOs.Prompt;
using PromptStudio.Domain.Entites;

namespace PromptStudio.Application.Services.Prompts;

public interface IPromptService
{
    Task <PromptResponseDTO> CreatePromptAsync(Guid userId, CreatePromptDTO createPromptDTO);
    Task <PromptResponseDTO> UpdatePromptAsync(Guid Id, Guid userId, UpdatePromptDTO updatePromptDTO);
    Task <bool> DeletePromptAsync(Guid id);
    Task <List<PromptResponseDTO>> GetPromptsByUserAsync(Guid UserId);
    Task <PromptResponseDTO> GetPromptByIdAsync(Guid Id);



}
