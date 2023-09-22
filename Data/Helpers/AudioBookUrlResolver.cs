using ABP_Backend.Data.Dtos;
using ABP_Backend.Data.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace ABP_Backend.Data.Helpers
{
    public class AudioBookUrlResolver : IValueResolver<AudioBook, AudioBookInLibraryDto, string>
    {
        private readonly IConfiguration _config;

        public AudioBookUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(AudioBook source, AudioBookInLibraryDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
