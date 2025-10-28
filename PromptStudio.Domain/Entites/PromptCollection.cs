using System;

namespace PromptStudio.Domain.Entites;

public class PromptCollection
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public ICollection<Prompt> Prompts { get; set; } = new List<Prompt>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
 
}
