using Re_ABP_Backend.Data.Entities;
using AutoMapper;

namespace Re_ABP_Backend.Data.Helpers
{
    public class AudioBookUrlResolver<TDestination> : IValueResolver<AudioBook, TDestination, string>
    {
        private readonly IConfiguration _config;

        public AudioBookUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(AudioBook source, TDestination destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
