using System;

namespace PromptStudio.Application.DTOs.Collection;

public class ResponseCollectionDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid UserId { get; set; }
    
}
