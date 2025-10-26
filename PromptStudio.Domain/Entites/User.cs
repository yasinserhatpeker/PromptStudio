using System;

namespace PromptStudio.Domain.Entites;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string? Username { get; set; }
    public ICollection<Prompt> Prompts { get; set; } = new List<Prompt>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;


}
