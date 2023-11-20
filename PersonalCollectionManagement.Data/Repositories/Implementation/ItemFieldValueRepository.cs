using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class ItemFieldValueRepository : BaseRepository<ItemFieldValueEntity>, IItemFieldValueRepository
    {
        public ItemFieldValueRepository(IApplicationDbContext appContext) : base(appContext)
        {
            DbSet = appContext.Set<ItemFieldValueEntity>();
        }

        public async Task<IEnumerable<ItemFieldValueEntity>> GetValueByFieldIdAsync(int id)
        {
            return await DbSet
               .AsNoTracking()
               .Where(f => f.CollectionFieldId == id)
               .ToListAsync();
        }
    }
}
