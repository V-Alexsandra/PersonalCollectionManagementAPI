using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class LikeRepository : BaseRepository<LikeEntity>, ILikeRepository
    {
        protected IApplicationDbContext appContext;
        protected DbSet<LikeEntity> DbSet;

        public LikeRepository(IApplicationDbContext appContext) : base(appContext) 
        {
            DbSet = appContext.Set<LikeEntity>();
        }

        public async Task<int> GetLikesCountAsync(int itemId)
        {
            var likesCount = await DbSet
                .AsNoTracking()
                .Where(l => l.ItemId == itemId)
                .CountAsync();

            return likesCount;
        }

        public async Task<LikeEntity> GetUserLike(string userId, int itemId)
        {
            var like = await DbSet
                  .AsNoTracking()
                  .Where(l => l.ItemId == itemId)
                  .Where(l => l.UserId == userId)
                  .FirstOrDefaultAsync();

            return like;
        }
    }
}
