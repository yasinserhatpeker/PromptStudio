using System;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Application.Services.Users;

namespace PromptStudio.Infrastructure.Services;

public class UserService : IUserService
{
    public Task<UserResponseDTO> CreateUserAsync(CreateUserDTO createUserDTO)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserResponseDTO>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<UserResponseDTO> GetUserByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponseDTO> UpdateUserAsync(Guid userId, UpdateUserDTO updateUserDTO)
    {
        throw new NotImplementedException();
    }
}
