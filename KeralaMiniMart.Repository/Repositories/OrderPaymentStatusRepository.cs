using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Repository.Repositories
{
    public class OrderPaymentStatusRepository : Repository<OrderPaymentStatus>, IOrderPaymentStatusRepository
    {
        public OrderPaymentStatusRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
