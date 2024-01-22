namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IDashboardService
    {
        Task<int[]> GetUserCountByMothAsync();
    }
}
