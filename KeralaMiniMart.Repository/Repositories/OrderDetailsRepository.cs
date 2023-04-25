using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Repository.Repositories
{
    public class OrderDetailsRepository : Repository<OrderDetail>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }
        public List<OrderDetail> GetAllItemsForOrder(int id)
        {
            return Set.Where(x => x.OrderId == id).Include(x => x.Product).Include(x => x.Product).ThenInclude(x => x.Unit).ToList();
        }

        public OrderDetail GetProductForOrder(int OrderId, int ProductId)
        {
            return Set.Where(x => x.OrderId == OrderId && x.ProductId == ProductId).Include(x => x.Product).Include(x => x.Colors).Include(x => x.Size).FirstOrDefault();
        }
    }
}
