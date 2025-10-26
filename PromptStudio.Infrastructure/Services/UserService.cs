using System;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Application.Services.Users;
using PromptStudio.Domain.Entites;
using PromptStudio.Infrastructure.Data;

namespace PromptStudio.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly PromptStudioDbContext _context;

    public UserService(IMapper mapper, PromptStudioDbContext context)
    {
        _mapper = mapper;
        _context = context;
    } 
    public async Task<UserResponseDTO> CreateUserAsync(CreateUserDTO createUserDTO)
    {
        // DTO -> Entity
        var user = _mapper.Map<User>(createUserDTO);
        if (user == null)
        {
            return null;
        }
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        
        // hashing the password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        // cleaning the plain text password
        createUserDTO.Password=string.Empty;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        // entity -> DTO
        return _mapper.Map<UserResponseDTO>(user);
        
}

    public Task<bool> DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserResponseDTO>> GetAllUsers()
    {
          throw new NotImplementedException();
    }

    public async Task<UserResponseDTO> GetUserByIdAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return null;
        }
        return _mapper.Map<UserResponseDTO>(user);
    }

    public async Task<UserResponseDTO> UpdateUserAsync(Guid userId, UpdateUserDTO updateUserDTO)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return null;
        }
        // mapping over existing user entity
        _mapper.Map(updateUserDTO, user);

        user.UpdatedAt = DateTime.UtcNow;

        
        await _context.SaveChangesAsync();

         return _mapper.Map<UserResponseDTO>(user);

    }
}
