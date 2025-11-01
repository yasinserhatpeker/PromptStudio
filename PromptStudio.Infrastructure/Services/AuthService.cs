using System;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Application.Services.Users;

namespace PromptStudio.Infrastructure.Services;

public class AuthService : IAuthService
{
    public Task<string?> LoginAsync(LoginDTO loginDTO)
    {
        throw new NotImplementedException();
    }

    public Task<string?> RefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponseDTO?> RegisterAsync(CreateUserDTO createUserDTO)
    {
        throw new NotImplementedException();
    }
}
