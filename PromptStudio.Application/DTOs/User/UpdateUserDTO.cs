using System;

namespace PromptStudio.Application.DTOs.User;

public class UpdateUserDTO
{
     public string? Username { get; set; }

     public string PasswordHash { get; set; } = default!;
}
