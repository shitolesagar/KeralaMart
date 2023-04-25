using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using Microsoft.EntityFrameworkCore;

namespace KeralaMiniMart.Repository.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Order>> GetIndexViewRecordsAsync(OrderFilter filter, int skip, int pageSize)
        {
            var query = Set.Include(x => x.UserAddress).ThenInclude(x => x.DeliveryLocation).Include(x => x.DeliveryStatus).Include(x => x.ApplicationUser).AsQueryable();
            if (!string.IsNullOrEmpty(filter.search))
            {
                filter.search = filter.search.ToLower();
                query = query.Where(x => x.OrderNumber.ToLower().Contains(filter.search) || x.ApplicationUser.Email.ToLower().Contains(filter.search));
            }
            if (!string.IsNullOrEmpty(filter.DeliveryDay))
            {
                query = query.Where(x => x.EstimatedDeliveryDate.DayOfWeek.ToString().ToLower()==filter.DeliveryDay.ToLower());
            }
            if (filter.StatusId != 0)
            {
                query = query.Where(x => x.DeliveryStatusId == filter.StatusId);
            }
            return query.OrderByDescending(x => x.CreatedDate).Skip(skip).Take(pageSize).ToListAsync();
        }

        public int GetIndexViewTotalCount(OrderFilter filter)
        {
            var query = Set.Include(x => x.DeliveryStatus).AsQueryable();
            if (!string.IsNullOrEmpty(filter.search))
            {
                filter.search = filter.search.ToLower();
                query = query.Where(x => x.OrderNumber.ToLower().Contains(filter.search) || x.ApplicationUser.Email.ToLower().Contains(filter.search));
            }
            if (!string.IsNullOrEmpty(filter.DeliveryDay))
            {
                query = query.Where(x => x.EstimatedDeliveryDate.DayOfWeek.ToString().ToLower() == filter.DeliveryDay.ToLower());
            }
            if (filter.StatusId != 0)
            {
                query = query.Where(x => x.DeliveryStatusId == filter.StatusId).OrderByDescending(x => x.Id);
            }
            return query.Count();
        }
        public Task<Order> GetOrderDetails(int id)
        {
            return Set.Include(x => x.ApplicationUser).ThenInclude(y => y.UserAddresses).Include(x => x.DeliveryStatus).Include(x => x.PaymentStatus).Include(x => x.OrderDetails).ThenInclude(y => y.Product).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Order>> GetAllOrders(int ApplicationUserId)
        {
            return Set.Where(x => x.ApplicationUserId == ApplicationUserId).Include(x => x.OrderDetails).ThenInclude(y => y.Product).ToListAsync();
        }
    }
}
