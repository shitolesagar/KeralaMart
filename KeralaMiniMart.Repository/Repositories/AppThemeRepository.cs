using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;

namespace KeralaMiniMart.Repository.Repositories
{
    public class AppThemeRepository : Repository<AppTheme>, IAppThemeRepository
    {
        public AppThemeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
