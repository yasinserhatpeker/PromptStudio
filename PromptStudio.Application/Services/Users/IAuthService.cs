using System;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Domain.Entites;

namespace PromptStudio.Application.Services.Users;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginDTO loginDTO);
    Task<UserResponseDTO?> RegisterAsync(CreateUserDTO createUserDTO);
    Task<string?> RefreshTokenAsync(string refreshToken);   

 }
