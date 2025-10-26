using System;
using System.Collections.ObjectModel;
using AutoMapper;
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

    public Task<ResponseCollectionDTO> CreatePromptCollectionAsync(CreateCollectionDTO createCollectionDTO)
    {
         throw new NotImplementedException();


    }

    public Task<bool> DeletePromptCollectionAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ResponseCollectionDTO>> GetAllPromptCollectionAsync(Guid UserId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseCollectionDTO> GetPromptByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseCollectionDTO> UpdatePromptCollectionAsync(Guid Id, Guid userId, UpdateCollectionDTO updateCollectionDTO)
    {
        throw new NotImplementedException();
    }
}
