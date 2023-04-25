using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Repository.Repositories
{
    public class CartDetailsRepository : Repository<CartDetails>, ICartDetailsRepository
    {
        public CartDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<CartDetails> FindByApplicationUserId(int ApplicationUserId)
        {

            return Set.Where(x => x.ApplicationUserId == ApplicationUserId).ToList();
        }

        public CartDetails FindByProductDetailId(int ProductId, string AppId, int ApplicationUserId)
        {
            return Set.Where(x => x.ProductId == ProductId && x.ApplicationUserId == ApplicationUserId).FirstOrDefault();
        }

        public Task<List<CartDetails>> GetAllItemsForUser(string AppId, int ApplicationUserId)
        {
            return Set.Where(x => x.ApplicationUserId == ApplicationUserId).ToListAsync();
        }

        public CartDetails FindByProductsDetailId(int ProductId, string AppId, int ApplicationUserId)
        {
            return Set.Where(x => x.ProductId == ProductId && x.ApplicationUserId == ApplicationUserId).FirstOrDefault();
        }

        public CartDetails FindByCartDetailsId(int Id)
        {
            return Set.Where(x => x.Id == Id).Include(x => x.Colors).Include(x => x.SIze).FirstOrDefault();
        }
    }
}
