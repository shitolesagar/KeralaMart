using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;

namespace KeralaMiniMart.Repository.Repositories
{
    public class SmtpMailRepository : Repository<SmtpMail>, ISmtpMailRepository
    {
        public SmtpMailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
