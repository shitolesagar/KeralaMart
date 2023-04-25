using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Repository.Repositories
{
    public class OrderDeliveryStatusRepository : Repository<OrderDeliveryStatus>, IOrderDeliveryStatusRepository
    {
        public OrderDeliveryStatusRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
