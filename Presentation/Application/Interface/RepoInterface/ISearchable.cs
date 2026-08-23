using System.Linq.Expressions;

namespace OrderManagement.Application.Interface;

public interface ISerachable<T>
{
    Task<List<T>> SearchTerm<T>(Expression<Func<T, bool>> predicate);
}