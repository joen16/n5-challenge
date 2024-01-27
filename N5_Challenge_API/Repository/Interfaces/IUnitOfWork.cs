
using Microsoft.EntityFrameworkCore;

namespace N5_Challenge_API.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository Repository();
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
