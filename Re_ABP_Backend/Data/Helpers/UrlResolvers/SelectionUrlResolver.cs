using AutoMapper;
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Helpers.UrlResolvers
{
    public class SelectionUrlResolver<TDestination> : IValueResolver<BookSelection, TDestination, string>
    {
        private readonly IConfiguration _config;

        public SelectionUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(BookSelection source, TDestination destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return _config["ApiUrl"] + source.ImageUrl;
            }

            return null;
        }
    }
}
