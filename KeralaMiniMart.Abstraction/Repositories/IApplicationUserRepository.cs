using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser FindByEmail(string Email);
        int GetCustomerIndexViewTotalCount(FilterBase filter);
        Task<List<ApplicationUser>> GetCusotmerIndexViewRecordsAsync(FilterBase filter, int skip, int pageSize);
        Task<ApplicationUser> GetCustomerDetails(int id);
        ApplicationUser FindByPhoneNumber(string phoneNumber);
    }
}
