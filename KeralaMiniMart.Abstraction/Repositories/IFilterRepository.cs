using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<List<Subcategory>> GetSubcategoryAsync(int CategoryId, string AppId);
        int GetIndexViewTotalCount(SubcategoryFilter filter);
        Task<Subcategory> FindByIdAsync(int id, bool include);
        Task<List<Subcategory>> GetIndexViewRecordsAsync(SubcategoryFilter filter, int skip, int pageSize);
        List<Subcategory> GetByIds(List<int> Ids);
    }
}
