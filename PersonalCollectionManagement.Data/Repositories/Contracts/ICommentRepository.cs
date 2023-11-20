using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Repositories.Contracts
{
    public interface ICommentRepository : IBaseRepository<CommentEntity>
    {
        Task<IEnumerable<CommentEntity>> GetAllCommentsForItemAsync(int id);
    }
}
