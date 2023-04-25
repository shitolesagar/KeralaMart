using KeralaMiniMart.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeralaMiniMart.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        #endregion

        #region IUnitOfWork Members

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
