using System;
using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Application.DTOs.User;

public class UpdateUserDTO
{
     public string? Username { get; set; }

     [Required]
     public string Password { get; set; } = default!;
}
