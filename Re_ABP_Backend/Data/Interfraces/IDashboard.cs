namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IDashboard
    {
        Task<int[]> GetUserCountByMothAsync();
    }
}
