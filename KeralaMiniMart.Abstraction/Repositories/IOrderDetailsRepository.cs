using KeralaMiniMart.Entities.Database;
using System.Collections.Generic;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IOrderDetailsRepository : IRepository<OrderDetail>
    {
        List<OrderDetail> GetAllItemsForOrder(int id);

        OrderDetail GetProductForOrder(int OrderId, int ProductId);
    }
}
