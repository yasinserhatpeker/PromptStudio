using System;
using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Application.DTOs.User;

public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; } 
}
