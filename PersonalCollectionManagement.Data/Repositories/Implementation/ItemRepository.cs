using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class ItemRepository : BaseRepository<ItemEntity>, IItemRepository
    {
        public ItemRepository(IApplicationDbContext appContext) : base(appContext) {}
    }
}
