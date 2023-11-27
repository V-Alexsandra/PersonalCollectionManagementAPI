using Microsoft.EntityFrameworkCore;
using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
    {
        protected IApplicationDbContext appContext;
        protected DbSet<CommentEntity> DbSet;
        public CommentRepository(IApplicationDbContext appContext) : base(appContext)
        {
            DbSet = appContext.Set<CommentEntity>();
        }

        public async Task<IEnumerable<CommentEntity>> GetAllCommentsForItemAsync(int id)
        {
            var comments = await DbSet
                .AsNoTracking()
                .Where(c => c.ItemId == id)
                .ToListAsync();

            return comments;
        }
    }
}
