using KeralaMiniMart.Entities.Database;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IVersionControlRepository : IRepository<VersionControl>
    {
        VersionControl CurrentVersion(string AppId);
    }
}
