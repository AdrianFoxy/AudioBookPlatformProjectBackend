namespace Re_ABP_Backend.Data.Specification.SpecClasses
{
    public class ABOfSomethingParams
    {
        private const int MaxPageSize = 40;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public int Id { get; set; }
    }
}
