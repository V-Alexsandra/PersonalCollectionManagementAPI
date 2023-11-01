using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class LikeRepository : BaseRepository<LikeEntity>, ILikeRepository
    {
        public LikeRepository(IApplicationDbContext appContext) : base(appContext) {}
    }
}
