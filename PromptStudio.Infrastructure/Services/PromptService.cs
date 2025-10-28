using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

    public async Task<PromptResponseDTO> CreatePromptAsync(Guid userId,CreatePromptDTO createPromptDTO)
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
    

    public async Task<bool> DeletePromptAsync(Guid userId, Guid id)
    {
        var prompt = await _context.Prompts.FirstOrDefaultAsync(p => p.Id == id);
        if (prompt == null)
        {
            return false;

        }
        if (prompt.UserId != userId)
        {
            return false;
        }
        
        _context.Prompts.Remove(prompt);
        await _context.SaveChangesAsync();
        return true;
        
    }

    public async Task<PromptResponseDTO> GetPromptByIdAsync(Guid id)
    {
        var prompt = await _context.Prompts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
       
        return _mapper.Map<PromptResponseDTO>(prompt);
    }

    public async Task<List<PromptResponseDTO>> GetPromptsByUserAsync(Guid UserId)
    {
        var prompts = await _context.Prompts.Where(p => p.UserId == UserId).OrderByDescending(p=>p.CreatedAt).AsNoTracking().ToListAsync();

       return _mapper.Map<List<PromptResponseDTO>>(prompts);
         
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
         // mapping over existing prompt entity
         _mapper.Map(prompt, updatePromptDTO);
          

        prompt.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return _mapper.Map<PromptResponseDTO>(prompt);

        

        
    }
}
