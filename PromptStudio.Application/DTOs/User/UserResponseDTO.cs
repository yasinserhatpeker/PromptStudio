using System;

namespace PromptStudio.Application.DTOs.User;

public class UserResponseDTO
{
     public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string? Username { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
