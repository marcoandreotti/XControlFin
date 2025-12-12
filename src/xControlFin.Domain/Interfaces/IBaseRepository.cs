using xControlFin.Domain.Entities.Base;

namespace xControlFin.Domain.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(long id, CancellationToken cancellationToken);
}
