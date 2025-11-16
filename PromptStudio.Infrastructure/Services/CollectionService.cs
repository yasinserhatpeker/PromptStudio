using System;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PromptStudio.Application.DTOs.Collection;
using PromptStudio.Application.Services.Collections;
using PromptStudio.Domain.Entites;
using PromptStudio.Infrastructure.Data;

namespace PromptStudio.Infrastructure.Services;

public class CollectionService : ICollectionService
{
    private readonly PromptStudioDbContext _context;
    private readonly IMapper _mapper;

    public CollectionService(PromptStudioDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseCollectionDTO> CreatePromptCollectionAsync(CreateCollectionDTO createCollectionDTO,Guid userId)
    {   //  DTO -> Entity 
        var collection = _mapper.Map<PromptCollection>(createCollectionDTO);
        if (collection == null)
        {
            return null;
        }
       
        collection.Id = Guid.NewGuid();
        collection.UserId = userId;
        collection.CreatedAt = DateTime.UtcNow;

        _context.Collections.Add(collection);
        await _context.SaveChangesAsync();
        // entity -> dto
        return _mapper.Map<ResponseCollectionDTO>(collection);
        
    }

    public async Task<bool> DeletePromptCollectionAsync(Guid Id,Guid userId)
    {
        var collection = await _context.Collections.FindAsync(Id);
        if (collection == null)
        {
            return false;
        }
        if(collection.UserId!=userId)
        {
            return false;
        }
        _context.Collections.Remove(collection);
        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<ResponseCollectionDTO> GetPromptCollectionAsync(Guid UserId,Guid Id)
    {
        var collection = await _context.Collections.Where(c => c.UserId == UserId && c.Id == Id).AsNoTracking().OrderByDescending(c => c.CreatedAt).FirstOrDefaultAsync();
        if (collection == null)
         return null!;
         
        // entity -> dto
        return _mapper.Map<ResponseCollectionDTO>(collection);
    }

    public async Task<List<ResponseCollectionDTO>> GetPromptCollectionsByUserAsync(Guid UserId)
    {
        var collections = await _context.Collections.Where(p => p.UserId == UserId).OrderByDescending(p => p.CreatedAt).AsNoTracking().ToListAsync();
        
        // entity -> dto
        return _mapper.Map<List<ResponseCollectionDTO>>(collections);

    }

    public async Task<ResponseCollectionDTO> UpdatePromptCollectionAsync(Guid Id, Guid userId, UpdateCollectionDTO updateCollectionDTO)
    {
        var collection = await _context.Collections.FirstOrDefaultAsync(c => c.Id == Id);
        if (collection == null)
        {
            return null!;
        }
        if (collection.UserId != userId)
        {
            return null!;
        }
        // mapping-existing
        _mapper.Map(updateCollectionDTO, collection);
        collection.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
         // entity-> dto
        return _mapper.Map<ResponseCollectionDTO>(collection);
       
    }
}
