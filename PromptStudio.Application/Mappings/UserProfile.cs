using System;
using AutoMapper;
using PromptStudio.Application.DTOs.Collection;
using PromptStudio.Application.DTOs.User;
using PromptStudio.Domain.Entites;

namespace PromptStudio.Application.Mappings;

public class UserProfile : Profile
{ 
    public UserProfile()
    {
        CreateMap<User, UserResponseDTO>().ReverseMap();
        CreateMap<CreateUserDTO, User>().ReverseMap();
        CreateMap<UpdateUserDTO,User>().ReverseMap(); 
        
    }
}
