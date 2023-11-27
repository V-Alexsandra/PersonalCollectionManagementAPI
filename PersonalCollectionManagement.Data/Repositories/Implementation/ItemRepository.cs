using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class ItemRepository : BaseRepository<ItemEntity>, IItemRepository
    {
        protected IApplicationDbContext appContext;
        protected DbSet<ItemEntity> DbSet;

        public ItemRepository(IApplicationDbContext appContext) : base(appContext) 
        {
            DbSet = appContext.Set<ItemEntity>();
        }

        public async Task<IEnumerable<ItemEntity>> GetAllCollectionItemsAsync(int id)
        {
            var collectionsItems = await DbSet
                .AsNoTracking()
                .Where(i => i.CollectionId == id)
                .ToListAsync();

            return collectionsItems;
        }

        public async Task<IEnumerable<ItemEntity>> GetLastAddedItemsAsync()
        {
            var collectionsItems = await DbSet
                .AsNoTracking()
                .OrderByDescending(c => c.CreationDate)
                .Take(20)
                .ToListAsync();

            return collectionsItems;
        }
    }
}
