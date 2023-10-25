using AutoMapper;
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Helpers
{
    public class AuthorUrlResolver<TDestination> : IValueResolver<Author, TDestination, string>
    {
        private readonly IConfiguration _config;

        public AuthorUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Author source, TDestination destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return _config["ApiUrl"] + source.ImageUrl;
            }

            return null;
        }

    }
}
