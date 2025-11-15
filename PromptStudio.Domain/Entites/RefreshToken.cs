using System;

namespace PromptStudio.Domain.Entites;

public class RefreshToken
{
    public Guid Id { get; set; }

    public Guid userId { get; set; }

    public User User { get; set; } = default!;

    public string Token { get; set; } = default!;

    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? RevokedAt { get; set; }
}
