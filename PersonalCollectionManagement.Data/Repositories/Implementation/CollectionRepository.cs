using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class CollectionRepository : BaseRepository<CollectionEntity>, ICollectionRepository
    {
        protected IApplicationDbContext appContext;
        protected DbSet<CollectionEntity> DbSet;
        public CollectionRepository(IApplicationDbContext appContext) : base(appContext)
        {
            this.appContext = appContext;
            DbSet = appContext.Set<CollectionEntity>();
        }

        public async Task<IEnumerable<CollectionEntity>> GetAllUsersCollectionsAsync(string userId)
        {
            var usersCollections = await DbSet
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return usersCollections;
        }

        public async Task<IEnumerable<CollectionEntity>> GetFiveLargestCollectionsAsync()
        {
                var collectionsWithItemCount = await DbSet
                    .Select(c => new
                    {
                        Collection = c,
                        ItemCount = appContext.Items.Count(i => i.CollectionId == c.Id)
                    })
                    .OrderByDescending(x => x.ItemCount)
                    .Take(5)
                    .ToListAsync();

                return collectionsWithItemCount.Select(x => x.Collection);
        }
    }
}
