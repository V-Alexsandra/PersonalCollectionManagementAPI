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
            DbSet = appContext.Set<CollectionEntity>();
        }

        public async Task<IEnumerable<CollectionEntity>> GetFiveLargestAsync()
        {
            var largestCollections = await DbSet
                .AsNoTracking()
                .Include(c => c.Items)
                .OrderByDescending(c => c.Items.Count())
                .Take(5)
                .ToListAsync();

            return largestCollections;
        }

        public async Task<IEnumerable<CollectionEntity>> GetAllUsersCollectionsAsync(string userId)
        {
            var usersCollections = await DbSet
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return usersCollections;
        }
    }
}
