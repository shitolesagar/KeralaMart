using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;

namespace KeralaMiniMart.Repository.Repositories
{
    public class ActivityLogRepository : Repository<ActivityLog>, IActivityLogRepository
    {
        public ActivityLogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
