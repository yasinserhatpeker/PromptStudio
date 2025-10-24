using System;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Domain.Entites;

namespace PromptStudio.Application.Services.Users;

public interface IUserService
{
    Task<UserResponseDTO> CreateUserAsync(CreateUserDTO createUserDTO);
    Task<UserResponseDTO> UpdateUserAsync(Guid userId, UpdateUserDTO updateUserDTO);
    Task<bool> DeleteUserAsync(Guid userId);
    Task<List<UserResponseDTO>> GetAllUsers();
    Task<UserResponseDTO> GetUserByIdAsync(Guid userId);
}
