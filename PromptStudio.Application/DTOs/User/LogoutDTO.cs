using System;

namespace PromptStudio.Application.DTOs.User;

public class LogoutDTO
{
   public string RefreshToken { get; set; } = default!;
}
