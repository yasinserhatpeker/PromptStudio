using System;
using AutoMapper;
using PromptStudio.Application.DTOs.Prompt;
using PromptStudio.Application.Services.Prompts;
using PromptStudio.Infrastructure.Data;

namespace PromptStudio.Infrastructure.Services;

public class PromptService : IPromptService
{
    private readonly PromptStudioDbContext _context;
    private readonly IMapper _mapper;
     public PromptService(PromptStudioDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public Task<PromptResponseDTO> CreatePromptAsync(CreatePromptDTO createPromptDTO)
    {
        // Mapping domainModel to DTO
        
        
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
