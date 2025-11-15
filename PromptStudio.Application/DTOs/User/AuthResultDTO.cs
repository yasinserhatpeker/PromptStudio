using System;

namespace PromptStudio.Application.DTOs.User;

public class AuthResultDTO
{
    public string AccessToken { get; set; } =default!;
    public string RefreshToken { get; set; } =default!;
}
