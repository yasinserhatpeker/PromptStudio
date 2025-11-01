using System;
using AutoMapper;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Domain.Entites;

namespace PromptStudio.Application.Mappings;

public class AuthProfile : Profile
{
   public AuthProfile()
    {
        CreateMap<UserResponseDTO,User>();
        CreateMap<User,UserResponseDTO>();
        
    }
}
