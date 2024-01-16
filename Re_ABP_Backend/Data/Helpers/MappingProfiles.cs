using Re_ABP_Backend.Data.Dtos;
using Re_ABP_Backend.Data.Entities;
using AutoMapper;
using Re_ABP_Backend.Data.Dtos.FilteringDtos;
using Re_ABP_Backend.Data.Entities.Identity;
using Re_ABP_Backend.Data.Helpers.UrlResolvers;
using Re_ABP_Backend.Data.Dtos.UserDtos;
using Re_ABP_Backend.Data.Dtos.ReviewDtos;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.GenreDtos;
using Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.NarratorDtos;

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

            CreateMap<Author, SingleAuthorDto>()
                .ForMember(d => d.ImageUrl, o => o.MapFrom<AuthorUrlResolver<SingleAuthorDto>>());
            CreateMap<BookSelection, SingleSelectionDto>()
                .ForMember(d => d.ImageUrl, o => o.MapFrom<SelectionUrlResolver<SingleSelectionDto>>());

            CreateMap<User, UserDto>()
               .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<ReviewCreateDto, Review>();
            CreateMap<Review, ReviewDto>()
                .ForMember(d => d.Username, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<Genre, GenreDto>();
            CreateMap<AddGenreDto, Genre>();

            CreateMap<Narrator, NarratorDto>();
            CreateMap<AddNarratorDto, Narrator>();


        }
    }
}
