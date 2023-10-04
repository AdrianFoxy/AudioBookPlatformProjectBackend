using Re_ABP_Backend.Data.Helpers;

namespace Re_ABP_Backend.Data.Specification
{
    public class ABSpecParams
    {
        private const int MaxPageSize = 40;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Sort { get; set; }
        public List<int>? AuthorIds { get; set; }
        public List<int>? GenreIds { get; set; }
        public List<int>? BookSeriesIds { get; set; }
        public List<int>? BookLanguageIds { get; set; }
        public List<int>? NarratorIds { get; set; }
        public List<int>? ExceptAuthorIds { get; set; }
        public List<int>? ExceptGenreIds { get; set; }
        public List<int>? ExceptBookSeriesIds { get; set; }
        public List<int>? ExceptBookLanguageIds { get; set; }
        public List<int>? ExceptNarratorIds { get; set; }
        public string? LowerDuration { get; set; }
        public string? HighDuration { get; set; }
        public int LowerRating  { get; set; }
        public int HighRating { get; set; }

        private string _search;
        public string? Search 
        { 
            get => _search;
            set => _search = value.ToLower();
        }

    }
}
