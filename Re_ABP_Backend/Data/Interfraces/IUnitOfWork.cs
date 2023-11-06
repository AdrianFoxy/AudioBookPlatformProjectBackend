using Re_ABP_Backend.Data.Entities.Base;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        // Complete. Idea is return number of changes in database
        Task<int> Complete();
    }
}
