using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeralaMiniMart.Repository.Repositories
{
    public class Delivery_chargeRepository : Repository<DeliveryCharge>, IDelivery_chargeRepository
    {
        public Delivery_chargeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DeliveryCharge findByMin(int min, string AppId)
        {
            return Set.Where(x => x.Min == min).FirstOrDefault();
        }
    }
}
