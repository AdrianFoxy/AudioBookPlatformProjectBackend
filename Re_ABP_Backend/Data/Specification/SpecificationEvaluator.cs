using Microsoft.EntityFrameworkCore;
using Re_ABP_Backend.Data.Entities.Base;

namespace Re_ABP_Backend.Data.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        // Friendly remind for stupid me in future
        // How does it work? 0_0
        // Here we will get inputQuery, just iqueriable of products, audiobooks, etc
        // Then check spec.Criteria, for example it can be
        // p => p.Duration > 00:12:32
        // then we will add includes, if they exist
        // it looks like query, (current, include) where it can be .Include(p => p.Genre).Include(p => p.Authors) etc
        // and then we return query, Hallelujah \o/
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> spec) 
        {
            var query = inputQuery;
            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // p => p.ProductTypeId == id
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            query = spec.IncludeStrings.Aggregate(query,
                (current, include) =>
                current.Include(include));

            return query;
        }
    }
}
