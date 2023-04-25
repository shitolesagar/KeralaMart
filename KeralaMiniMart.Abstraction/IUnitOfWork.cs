using System;
using System.Threading;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        #region Methods
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion
    }
}
