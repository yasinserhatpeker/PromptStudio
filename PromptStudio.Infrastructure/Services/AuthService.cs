using System;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Application.Services.Users;
using PromptStudio.Infrastructure.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PromptStudio.Domain.Entites;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata.Ecma335;

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

    public async Task<AuthResultDTO?> LoginAsync(LoginDTO loginDTO)
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

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var tokenDescriptor= new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires=DateTime.UtcNow.AddHours(1),
            Issuer=_configuration["Jwt:Issuer"],
            Audience=_configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        var refreshTokenValue = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            userId=user.Id,
            Token=refreshTokenValue,
            ExpiresAt=DateTime.UtcNow.AddDays(7),
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        return new AuthResultDTO
        {
            AccessToken=jwt,
            RefreshToken=refreshTokenValue,
        };

        
 }

   

    public async Task<AuthResultDTO?> RefreshTokenAsync(string refreshToken)
    {
        var existingToken = await _context.RefreshTokens.Include(x=>x.User).FirstOrDefaultAsync(x=>x.Token==refreshToken);

        if(existingToken == null)
        {
            return null;
        }
        if(existingToken.ExpiresAt < DateTime.UtcNow)
        {
            return null;
        }
        if(existingToken.RevokedAt != null)
        {
            return null;
        }
        var user =existingToken.User;
        if(user == null)
        {
            return null;
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
        
        var claims = new []
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"], 
            SigningCredentials = new SigningCredentials(new
            SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var newAccessToken = tokenHandler.WriteToken(token);

        return new AuthResultDTO
        {
            AccessToken = newAccessToken,
            RefreshToken=refreshToken
        };


    }

    public async Task<UserResponseDTO?> RegisterAsync(CreateUserDTO createUserDTO)
    {
        var exists = await _context.Users.AnyAsync(u => u.Email == createUserDTO.Email);
        if(exists)
        {
            return null;
        }
        var user = _mapper.Map<User>(createUserDTO);

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password);
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserResponseDTO>(user);

}

    public async Task<bool> LogoutAsync(string refreshToken)
    {
      var existingToken = await _context.RefreshTokens.FirstOrDefaultAsync(u => u.Token==refreshToken);

      if(existingToken==null)
        {
            return false;
        }
        if(existingToken.RevokedAt != null)
        {
            return true;
        }
        existingToken.RevokedAt = DateTime.UtcNow;
        return true;
    }

    

}
