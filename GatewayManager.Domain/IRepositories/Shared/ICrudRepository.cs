using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using GatewayManager.Domain.Pagination;

namespace GatewayManager.Domain.IRepositories.Shared
{
    public interface ICrudRepository<T>
    {
        Task<T> Create(T entity);
        Task Delete(long idEntity);
        Task DeleteAll(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAll();
        Task<T> Find(long idEntity);
        Task UpdateRange(IEnumerable<T> entity);
        Task Update(T entity);
        Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where);
        Task<T> Get(Expression<Func<T, bool>> where);
        Task<int> Count(Expression<Func<T, bool>>? where = null);
        void TrackerClear();
        Task<(IEnumerable<T>, int)> GetPage<TOrder>(PageInfo filter, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order, SortOrder sortOrder);
    }
}
