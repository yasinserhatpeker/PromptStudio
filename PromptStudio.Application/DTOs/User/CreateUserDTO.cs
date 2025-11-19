using System;
using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Application.DTOs.User;

public class CreateUserDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string Username { get; set; } = default!;
}
