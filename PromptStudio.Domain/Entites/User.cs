using System;

namespace PromptStudio.Domain.Entites;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string? Username { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


}
