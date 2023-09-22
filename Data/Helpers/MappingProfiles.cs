﻿using ABP_Backend.Data.Dtos;
using ABP_Backend.Data.Entities;
using AutoMapper;

namespace ABP_Backend.Data.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<AudioBook, AudioBookInLibraryDto>()
                .ForMember(d => d.Author, o => o.MapFrom(s => s.Author))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<AudioBookUrlResolver>());

            CreateMap<Author, AuthorInLibraryAudioBook>();
        }
    }
}
