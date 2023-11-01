using PersonalCollectionManagement.Data.Contexts;
using PersonalCollectionManagement.Data.Entities;
using PersonalCollectionManagement.Data.Repositories.Contracts;

namespace PersonalCollectionManagement.Data.Repositories.Implementation
{
    public class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(IApplicationDbContext appContext) : base(appContext){}
    }
}
