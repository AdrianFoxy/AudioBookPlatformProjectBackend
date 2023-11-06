using Re_ABP_Backend.Data.DB;
using Re_ABP_Backend.Data.Entities.Base;
using Re_ABP_Backend.Data.Interfraces;
using System.Collections;

namespace Re_ABP_Backend.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        private Hashtable _repositories;
        public UnitOfWork(AppDBContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
           _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if( _repositories == null ) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstanse = Activator.CreateInstance(repositoryType.MakeGenericType
                    (typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstanse);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}
