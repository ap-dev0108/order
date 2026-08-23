using System.Linq.Expressions;

namespace OrderManagement.Application.Interface;

public interface ISearchable
{
    Task<List<T>> SearchByNameAsync<T>(Expression<Func<T, bool>> predicate) where T :class;
}