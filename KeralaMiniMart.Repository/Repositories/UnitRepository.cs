using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;

namespace KeralaMiniMart.Repository.Repositories
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        public UnitRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
