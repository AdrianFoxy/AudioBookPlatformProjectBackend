using ABP_Backend.Data.Entities;
using ABP_Backend.Data.Specification;

namespace ABP_Backend.Data.Interfraces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetListWithSpecAsync(ISpecification<T> spec);
    }
}
