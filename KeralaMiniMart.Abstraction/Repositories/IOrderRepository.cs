using System.Collections.Generic;
using System.Threading.Tasks;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        int GetIndexViewTotalCount(OrderFilter filter);
        Task<List<Order>> GetIndexViewRecordsAsync(OrderFilter filter, int skip, int pageSize);
        Task<Order> GetOrderDetails(int id);
        Task<List<Order>> GetAllOrders(int ApplicationUserId);
    }
}
