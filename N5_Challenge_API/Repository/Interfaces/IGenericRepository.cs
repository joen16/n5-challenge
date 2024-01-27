using System.Linq.Expressions;

namespace N5_Challenge_API.Repository.Interfaces
{
    public interface IGenericRepository
    {
        Task<T?> GetById<T>(long id) where T : class;
        IQueryable<T> FindQueryable<T>(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null) where T : class;
        Task<List<T>> FindListAsync<T>(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default) where T : class;
        Task<List<T>> FindListAsync<T>(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", CancellationToken cancellationToken = default) where T : class;
        Task<List<T>> FindAllAsync<T>(CancellationToken cancellationToken) where T : class;
        Task<T?> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, string includeProperties) where T : class;
        T Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void UpdateRange<T>(IEnumerable<T> entities) where T : class;
        void Delete<T>(T entity) where T : class;
    }
}
