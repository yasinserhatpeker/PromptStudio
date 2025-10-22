using System;

namespace PromptStudio.Application.DTOs.Prompt;

public class CreatePromptDTO
{

    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? Tags { get; set; }
    public Guid UserId { get; set; }




}
