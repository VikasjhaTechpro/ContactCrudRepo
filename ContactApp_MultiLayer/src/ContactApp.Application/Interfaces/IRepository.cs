using System.Linq.Expressions;

namespace ContactApp.Application.Interfaces
{
    /// <summary>
    /// Generic repository contract used in the application layer.
    /// Application layer depends on abstractions only (inverted).
    /// </summary>
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
