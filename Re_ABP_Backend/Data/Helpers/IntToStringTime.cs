using AutoMapper;
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Helpers
{
    public class IntToStringTime<TDestination> : IValueResolver<AudioBook, TDestination, string>
    {
        private readonly IConfiguration _config;

        public IntToStringTime(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(AudioBook source, TDestination destination, string destMember, ResolutionContext context)
        {
            int hours = source.BookDuration / 3600;
            int minutes = (source.BookDuration % 3600) / 60;
            int seconds = source.BookDuration % 60;

            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

            /*Console.WriteLine(formattedTime);*/

            return formattedTime;
        }
    }
}
