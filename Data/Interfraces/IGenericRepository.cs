using ABP_Backend.Data.Entities;

namespace ABP_Backend.Data.Interfraces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetListAllAsync();

    }
}
