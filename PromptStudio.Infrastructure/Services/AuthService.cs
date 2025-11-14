using System;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Application.Services.Users;
using PromptStudio.Infrastructure.Data;

namespace PromptStudio.Infrastructure.Services;

public class AuthService : IAuthService
{

    private readonly PromptStudioDbContext _context;
    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;
    
    public AuthService(PromptStudioDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<string?> LoginAsync(LoginDTO loginDTO)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);
        if (user == null)
        {
            return null;
        }
        bool passwordIsValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash);
        if (!passwordIsValid)
        {
            return null;
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

        
        
        

    }

    public Task<string?> RefreshTokenAsync(string refreshToken)
    {
        
    }

    public Task<UserResponseDTO?> RegisterAsync(CreateUserDTO createUserDTO)
    {
        
    }
}
