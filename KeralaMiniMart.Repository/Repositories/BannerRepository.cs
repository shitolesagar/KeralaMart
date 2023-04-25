using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Repository.Repositories
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        public BannerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Banner>> GetBannerList(int skip, int take, string AppId)
        {
            return Set.Where(x => (x.StartDate == null && x.ExpiryDate == null) || (x.StartDate != null && x.ExpiryDate != null && x.StartDate.Value.Date <= DateTime.Now.Date && x.ExpiryDate.Value.Date > DateTime.Now.Date) || (x.ExpiryDate != null && x.StartDate == null && x.ExpiryDate.Value.Date > DateTime.Now.Date) || (x.StartDate != null && x.ExpiryDate == null && x.StartDate.Value.Date <= DateTime.Now.Date)).Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<Banner>> GetIndexViewRecordsAsync(BannerFilter filter, int skip, int pageSize)
        {
            var query = Set.AsQueryable();
            if (filter.showExpired == true)
                query = query.Where(x => x.ExpiryDate != null && x.ExpiryDate.Value.Date < DateTime.Now.Date);
            else if (filter.showInActive == true)
                query = query.Where(x => x.StartDate != null && x.StartDate.Value.Date > DateTime.Now.Date);
            else
                query = query.Where(x => (x.StartDate == null && x.ExpiryDate == null) || (x.StartDate != null && x.ExpiryDate != null && x.StartDate.Value.Date <= DateTime.Now.Date && x.ExpiryDate.Value.Date > DateTime.Now.Date) || (x.ExpiryDate != null && x.StartDate == null && x.ExpiryDate.Value.Date > DateTime.Now.Date) || (x.StartDate != null && x.ExpiryDate == null && x.StartDate.Value.Date <= DateTime.Now.Date));
            return query.OrderByDescending(x => x.CreatedDateTime).Skip(skip).Take(pageSize).ToListAsync();
        }

        public int GetIndexViewTotalCount(BannerFilter filter)
        {
            var query = Set.AsQueryable();
            if (filter.showExpired == true)
                query = query.Where(x => x.ExpiryDate != null && x.ExpiryDate.Value.Date < DateTime.Now.Date);
            else if (filter.showInActive == true)
                query = query.Where(x => x.StartDate != null && x.StartDate.Value.Date > DateTime.Now.Date);
            else
                query = query.Where(x => (x.StartDate == null && x.ExpiryDate == null) || (x.StartDate != null && x.ExpiryDate != null && x.StartDate.Value.Date <= DateTime.Now.Date && x.ExpiryDate.Value.Date > DateTime.Now.Date) || (x.ExpiryDate != null && x.StartDate == null && x.ExpiryDate.Value.Date > DateTime.Now.Date) || (x.StartDate != null && x.ExpiryDate == null && x.StartDate.Value.Date <= DateTime.Now.Date));
            return query.Count();
        }
    }
}
