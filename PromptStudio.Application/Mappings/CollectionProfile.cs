using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.VisualBasic;
using PromptStudio.Application.DTOs.Collection;
using PromptStudio.Application.DTOs.Prompt;
using PromptStudio.Domain.Entites;

namespace PromptStudio.Application.Mappings;

public class CollectionProfile : Profile
{
   public CollectionProfile()
    {
        CreateMap<PromptCollection, ResponseCollectionDTO>().ReverseMap();
        CreateMap<CreateCollectionDTO, PromptCollection>().ReverseMap();
        CreateMap<UpdateCollectionDTO, PromptCollection>().ReverseMap();
    }
   
}
