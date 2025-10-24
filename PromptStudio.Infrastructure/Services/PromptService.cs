using System;
using AutoMapper;
using PromptStudio.Application.DTOs.Prompt;
using PromptStudio.Application.Services.Prompts;
using PromptStudio.Domain.Entites;
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
    
    public async Task<PromptResponseDTO> CreatePromptAsync(CreatePromptDTO createPromptDTO,Guid userId)
    {
        // DTO -> Entity
        var prompt = _mapper.Map<Prompt>(createPromptDTO);

        prompt.UserId = userId;
        prompt.CreatedAt = DateTime.UtcNow;
        prompt.Id = Guid.NewGuid();

        _context.Prompts.Add(prompt);
        await _context.SaveChangesAsync();


        //  Entity -> ResponseDTO
        return _mapper.Map<PromptResponseDTO>(prompt);
        
    }

    public Task<PromptResponseDTO> CreatePromptAsync(Guid userId, CreatePromptDTO createPromptDTO)
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

    public async Task<PromptResponseDTO> UpdatePromptAsync(Guid Id, Guid userId, UpdatePromptDTO updatePromptDTO)
    {
        var prompt = _context.Prompts.FirstOrDefault(p => p.Id == Id);
        if (prompt == null)
        {
            return null;
        }
        if (prompt.UserId != userId)
        {
            return null;
        }

        _mapper.Map(prompt, updatePromptDTO);

        prompt.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return _mapper.Map<PromptResponseDTO>(prompt);

        

        
    }
}
