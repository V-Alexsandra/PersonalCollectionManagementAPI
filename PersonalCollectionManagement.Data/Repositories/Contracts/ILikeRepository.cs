using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Data.Repositories.Contracts
{
    public interface ILikeRepository : IBaseRepository<LikeEntity>
    {
        Task<int> GetLikesCountAsync(int itemId);
        Task<LikeEntity> GetUserLike(string userId, int itemId);
    }
}
