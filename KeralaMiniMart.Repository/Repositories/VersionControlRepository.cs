using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using System.Linq;

namespace KeralaMiniMart.Repository.Repositories
{
    public class VersionControlRepository : Repository<VersionControl>, IVersionControlRepository
    {
        public VersionControlRepository(ApplicationDbContext context) : base(context)
        {
        }

        public VersionControl CurrentVersion(string AppId)
        {
            return Set.Where(x => x.CurrentLiveVersion == true ).Single();
        }
    }
}
