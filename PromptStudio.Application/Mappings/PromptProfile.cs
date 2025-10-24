using System;
using AutoMapper;
using PromptStudio.Application.DTOs.Prompt;
using PromptStudio.Domain.Entites;

namespace PromptStudio.Application.Mappings;

public class PromptProfile : Profile
{
    public PromptProfile()
    {
        CreateMap<Prompt, PromptResponseDTO>().ReverseMap();
        CreateMap<CreatePromptDTO, Prompt>().ReverseMap();
        CreateMap<UpdatePromptDTO, Prompt>().ReverseMap();

    }
}
