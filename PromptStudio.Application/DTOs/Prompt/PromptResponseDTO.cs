using System;

namespace PromptStudio.Application.DTOs.Prompt;

public class PromptResponseDTO
{
     public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? Tags { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
