using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Repository.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationUser FindByEmail(string Email)
        {

            return Set.Where(x => x.Email == Email).FirstOrDefault();
        }

        public ApplicationUser FindByPhoneNumber(string phoneNumber)
        {
            return Set.Where(x => x.MobileNumber == phoneNumber).FirstOrDefault();
        }

        public Task<List<ApplicationUser>> GetCusotmerIndexViewRecordsAsync(FilterBase filter, int skip, int pageSize)
        {
            var query = Set.Where(x=> x.Role.Name == "Customer").AsQueryable();
            if(!string.IsNullOrEmpty(filter.search))
            {
                query = query.Where(x => x.Email.ToLower().Contains(filter.search.ToLower()) || x.Name.ToLower().Contains(filter.search.ToLower()));
            }
            return query.OrderByDescending(x => x.CreatedDateTime).Skip(skip).Take(pageSize).ToListAsync();
        }

        public Task<ApplicationUser> GetCustomerDetails(int id)
        {
            return Set.Include(x => x.Orders).ThenInclude(x => x.DeliveryStatus).Include(x => x.UserAddresses).FirstOrDefaultAsync(x => x.Id == id);
        }

        public int GetCustomerIndexViewTotalCount(FilterBase filter)
        {
            var query = Set.Where(x => x.Role.Name == "Customer").AsQueryable();
            if (!string.IsNullOrEmpty(filter.search))
            {
                query = query.Where(x => x.Email.ToLower().Contains(filter.search.ToLower()) || x.Name.ToLower().Contains(filter.search.ToLower()));
            }
            return query.Count();
        }
    }
}
