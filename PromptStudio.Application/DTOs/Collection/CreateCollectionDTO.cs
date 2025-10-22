using System;

namespace PromptStudio.Application.DTOs.Collection;

public class CreateCollectionDTO
{
   
    public string Name { get; set; } = default!;
    public Guid UserId { get; set; }
    
}
