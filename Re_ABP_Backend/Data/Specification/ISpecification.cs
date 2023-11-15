using System.Linq.Expressions;

namespace Re_ABP_Backend.Data.Specification
{
    public interface ISpecification<T>
    {
        // Where Criteria
        Expression<Func<T, bool>> Criteria {get;}
        List<Expression<Func<T, object>>> Includes { get;}
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get;}
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }

    }
}
