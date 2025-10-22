using System;

namespace PromptStudio.Application.DTOs.Prompt;

public class UpdatePromptDTO
{
    
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? Tags { get; set; }
   
}
