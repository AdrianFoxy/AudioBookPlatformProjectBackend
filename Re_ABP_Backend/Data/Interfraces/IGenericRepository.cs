using Re_ABP_Backend.Data.Entities.Base;
using Re_ABP_Backend.Data.Specification;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetListWithSpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);

    }
}
