using System;

namespace PromptStudio.Domain.Entites;

public class Prompt
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? Tags { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}
