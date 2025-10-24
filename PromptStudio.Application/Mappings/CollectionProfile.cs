using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.VisualBasic;
using PromptStudio.Application.DTOs.Collection;
using PromptStudio.Application.DTOs.Prompt;

namespace PromptStudio.Application.Mappings;

public class CollectionProfile : Profile
{
   public CollectionProfile()
    {
        CreateMap<Collection, ResponseCollectionDTO>().ReverseMap();
        CreateMap<CreateCollectionDTO, Collection>().ReverseMap();
        CreateMap<UpdateCollectionDTO, Collection>().ReverseMap();
    }
   
}
