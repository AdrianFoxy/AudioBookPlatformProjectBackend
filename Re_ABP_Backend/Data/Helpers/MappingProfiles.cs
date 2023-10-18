﻿using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using AutoMapper;
using Re_ABP_Backend.Data.Dtos.FilteringDtos;
using Re_ABP_Backend.Data.Dtos.AuthDtos;
using Re_ABP_Backend.Data.Entities.Identity;

namespace Re_ABP_Backend.Data.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<AudioBook, AudioBookInLibraryDto>()
                .ForMember(d => d.Author, o => o.MapFrom(s => s.Author))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<AudioBookUrlResolver<AudioBookInLibraryDto>>())
                .ForMember(d => d.BookDuration, o => o.MapFrom<IntToStringTime<AudioBookInLibraryDto>>());

            CreateMap<AudioBook, SingleAudioBookDto>()
                .ForMember(d => d.Author, o => o.MapFrom(s => s.Author))
                .ForMember(d => d.Genre, o => o.MapFrom(s => s.Genre))
                .ForMember(d => d.BookLanguage, o => o.MapFrom(s => s.BookLanguage))
                .ForMember(d => d.Narrator, o => o.MapFrom(s => s.Narrator))
                .ForMember(d => d.BookSeries, o => o.MapFrom(s => s.BookSeries))
                .ForMember(d => d.BookAudioFile, o => o.MapFrom(s => s.BookAudioFile))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<AudioBookUrlResolver<SingleAudioBookDto>>())
                .ForMember(d => d.BookDuration, o => o.MapFrom<IntToStringTime<SingleAudioBookDto>>());

            CreateMap<Author, AuthorFilteringDto>();
            CreateMap<Genre, GenreFilteringDto>();
            CreateMap<BookLanguage, BookLanguageFilteringDto>();
            CreateMap<Narrator, NarratorFilteringDto>();
            CreateMap<BookSeries, BookSeriesFilteringDto>();
            CreateMap<BookAudioFile, BookAudioFileDto>();

            CreateMap<User, UserDto>()
               .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));



        }
    }
}
