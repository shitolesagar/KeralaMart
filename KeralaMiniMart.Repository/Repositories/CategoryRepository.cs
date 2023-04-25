using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Repository.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {

        }

        public Category FindCategoryWithoutProducts(int id)
        {
            return Set.Include(x=>x.Products).FirstOrDefault(x => x.Id == id);
        }

        public Task<List<Category>> GetCategoryList(int skip, int take, string AppId)
        {
            return Set.Where(x => x.Products.Where(y=>y.IsPublish == true && y.IsDeleted == false).Count() > 0).Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<Category>> GetIndexViewRecordsAsync(FilterBase filter, int skip, int pageSize)
        {
            return Set.OrderByDescending(x => x.Id).Skip(skip).Take(pageSize).ToListAsync();
        }

        public int GetIndexViewTotalCount(FilterBase filter)
        {
            return Set.Count();
        }
    }
}
