using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class CollectionFieldRepository : BaseRepository<CollectionFieldEntity>, ICollectionFieldRepository
    {
        public CollectionFieldRepository(IApplicationDbContext appContext) : base(appContext) 
        {
            DbSet = appContext.Set<CollectionFieldEntity>();
        }

        public async Task<IEnumerable<CollectionFieldEntity>> GetCollectionFieldsByCollectionIdAsync(int collectionId)
        {
            return await DbSet
                .AsNoTracking()
                .Where(f => f.CollectionId == collectionId)
                .ToListAsync();
        }

    }
}
