using System;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Domain.Entites;

namespace PromptStudio.Application.Services.Users;

public interface IAuthService
{
    Task<AuthResultDTO?> LoginAsync(LoginDTO loginDTO);
    Task<UserResponseDTO?> RegisterAsync(CreateUserDTO createUserDTO);
    Task<AuthResultDTO?> RefreshTokenAsync(string refreshToken);

 }
