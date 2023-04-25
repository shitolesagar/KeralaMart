using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;

namespace KeralaMiniMart.Repository.Repositories
{
    public class ConfigurationDataRepository : Repository<ConfigurationData>, IConfigurationDataRepository
    {
        public ConfigurationDataRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
