using System.Linq.Expressions;

namespace ABP_Backend.Data.Specification
{
    public interface ISpecification<T>
    {
        // Where Criteria
        Expression<Func<T, bool>> Criteria {get;}
        List<Expression<Func<T, object>>> Includes { get;}
    }
}
