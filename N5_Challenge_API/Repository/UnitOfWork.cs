using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using N5_Challenge_API.Entitys;
using N5_Challenge_API.Repository.Interfaces;
using System.Data.Entity.Validation;

namespace N5_Challenge_API.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly n5Context _databaseContext;
        private bool _disposed;

        public UnitOfWork(n5Context databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository Repository()
        {
            return new GenericRepository(_databaseContext);
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return _databaseContext.SaveChangesAsync(cancellationToken);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _databaseContext.Dispose();
            _disposed = true;
        }
    }
}
