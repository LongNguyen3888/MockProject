using System.Linq.Expressions;


namespace WebBlog.Business
{
    public interface IBaseService<T> where T : class
    {
        Task<object> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);

        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<PaginatedResult<T>> GetAsync(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "", int pageIndex = 1, int pageSize = 10);
    }
}
