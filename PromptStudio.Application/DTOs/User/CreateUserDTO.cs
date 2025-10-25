using System;

namespace PromptStudio.Application.DTOs.User;

public class CreateUserDTO
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? Username { get; set; }
   
    
}
