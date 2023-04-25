using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Repository.Repositories
{
    public class SubcategoryRepository : Repository<Subcategory>, ISubcategoryRepository
    {
        public SubcategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Subcategory>> GetSubcategoryAsync(int CategoryId, string AppId)
        {
            return Set.Where(x => x.CategoryId == CategoryId).Where(x => x.Products.Count() > 0).ToListAsync();
        }

        public Task<List<Subcategory>> GetIndexViewRecordsAsync(SubcategoryFilter filter, int skip, int pageSize)
        {
            var query = Set.AsQueryable();
            if (filter.CategoryId != 0)
                query = query.Where(x => x.CategoryId == filter.CategoryId);
            return query.Include(x => x.Category).OrderByDescending(x => x.Id).Skip(skip).Take(pageSize).ToListAsync();
        }

        public int GetIndexViewTotalCount(SubcategoryFilter filter)
        {
            var query = Set.AsQueryable();
            if (filter.CategoryId != 0)
                query = query.Where(x => x.CategoryId == filter.CategoryId);
            return query.Count();
        }

        public Task<Subcategory> FindByIdAsync(int id, bool include)
        {
            if (include)
            {
                return Set.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            }
            return FindByIdAsync(id);
        }

        public List<Subcategory> GetByIds(List<int> Ids)
        {
            return Set.Where(x => (Ids.Any(subCatId => x.Id == subCatId))).ToList();
        }
    }
}
